using kix;
using OscalertSvc.Models;
using OscalertSvc.Scrape.Interface;
using System;
using System.Collections.Specialized;
using System.Net;
using System.Threading;
using static OscalertSvc.Scrape.Interface.IClass_ss;

namespace Class_biz_cad_activity_notification_agent
  {

  public class TClass_biz_cad_activity_notification_agent : ObjectBiz
    {

    readonly private IClass_ss ss_cad_provider = null;
    readonly private NameValueCollection appSettings = null;

    public TClass_biz_cad_activity_notification_agent // CONSTRUCTOR
      (
      IClass_ss ss_cad_provider_imp,
      NameValueCollection appSettings_imp
      )
      {
      ss_cad_provider = ss_cad_provider_imp.WithReport(Report);
      appSettings = appSettings_imp;
      }

    internal void Work(Biz biz)
      {
      var address = k.EMPTY;
      var be_augmenting_enabled = bool.Parse(appSettings["be_augmenting_enabled"]);
      var current_incident_num = k.EMPTY;
      var incident_date_time_initialized = k.EMPTY;
      var nature = k.EMPTY;
      //var saved_incident_num = k.EMPTY; // for use managing nature
      var saved_meta_surge_alert_timestamp_ems = DateTime.MinValue;
      var saved_meta_surge_alert_timestamp_als = DateTime.MinValue;
      var saved_meta_surge_alert_timestamp_fire = DateTime.MinValue;
      var vbemsbridge_refresh_rate_in_milliseconds = int.Parse(appSettings["vbemsbridge_refresh_rate_in_seconds"])*1000;
      //
      var cookie_container = new CookieContainer();
      //
      EmsCadList current_ems_cad_list;
      //
      var datetime_of_last_login = DateTime.MinValue;
      var datetime_of_last_nudge = DateTime.Now;
      var request_identifier = Guid.NewGuid().ToString();
      while (!BeQuitCommanded)
        {
        try
          {
          if (DateTime.Now > datetime_of_last_login.AddMinutes(double.Parse(appSettings["login_interval_minutes"])))
            {
            ReportProgress(new("Logging into CAD provider..."));
            ss_cad_provider.Login
              (
              username:appSettings["vbemsbridge_username"],
              password:appSettings["vbemsbridge_password"],
              cookie_container:cookie_container
              );
            datetime_of_last_login = DateTime.Now;
            datetime_of_last_nudge = DateTime.Now;
            ReportProgress(new("Login complete."));
            }
          else if (DateTime.Now > datetime_of_last_nudge.AddMinutes(double.Parse(appSettings["nudge_interval_minutes"])))
            {
            Report.Debug("Nudging CAD provider...");
            ss_cad_provider.Nudge(cookie_container);
            datetime_of_last_nudge = DateTime.Now;
            Report.Debug("Nudge complete.");
            }
          Report.Debug("Retrieving provider's current EMS CAD list...");
          current_ems_cad_list = ss_cad_provider.CurrentEmsCadList(cookie_container,request_identifier);
          Report.Debug("Retrieval complete.");
          if (current_ems_cad_list == null)
            {
            Report.Error("ss_cad_provider.CurrentEmsCadList returned null.");
            }
          else
            {
            request_identifier = current_ems_cad_list.RequestIdentifier;
            var rows = current_ems_cad_list.Records;
            Report.Debug("Parsing provider's current CAD records and establishing them in the BizModel...");
            for (var i = new k.subtype<int>(0,rows.Count); i.val < i.LAST; i.val++)
              {
              var cells = rows[i.val].Columns;
              //
              // Remove the IncidentNumberNotValue cell if it exists in this record.  It only appears in some records.
              //
              if (cells.Count == 19)
                {
                cells.RemoveAt(2);
                }
              //
              address = cells[4].Value;
              current_incident_num = cells[1].Value;
              incident_date_time_initialized = cells[9].Value;
              if(
                  (incident_date_time_initialized.Length > 1) // there is an incident_date/time_initialized
                &&
                  (address.Length > 1) // there is an address
                &&
                  (
                    (current_incident_num[0] == 'E') // this is an EMS incident whose number starts with EMS
                  ||
                    (current_incident_num[0] == 'F') // this if a fire incident whose number starts with FD
                  )
                )
                {
                //if (current_incident_num != saved_incident_num)
                //  {
                //  //
                //  // Determine nature, if supported.
                //  //
                //  }
                biz.cad_records.Set
                  (
                  id:k.EMPTY,
                  incident_date:(incident_date_time_initialized.Split())[0],
                  incident_num:current_incident_num,
                  incident_address:k.Safe(address,k.safe_hint_type.PUNCTUATED),
                  call_sign:cells[6].Value,
                  time_initialized:(incident_date_time_initialized.Split())[1],
                  time_of_alarm:(cells[0].Value.Contains(k.SPACE) ? (cells[0].Value.Split())[1] : k.EMPTY),
                  time_enroute:(cells[10].Value.Contains(k.SPACE) ? (cells[10].Value.Split())[1] : k.EMPTY),
                  time_on_scene:(cells[16].Value.Contains(k.SPACE) ? (cells[16].Value.Split())[1] : k.EMPTY),
                  time_transporting:(cells[12].Value.Contains(k.SPACE) ? (cells[12].Value.Split())[1] : k.EMPTY),
                  time_at_hospital:(cells[13].Value.Contains(k.SPACE) ? (cells[13].Value.Split())[1] : k.EMPTY),
                  time_available:(cells[14].Value.Contains(k.SPACE) ? (cells[14].Value.Split())[1] : k.EMPTY),
                  time_downloaded:k.EMPTY,
                  nature:nature
                  );
                //saved_incident_num = current_incident_num; // for use managing nature
                }
              cells.Clear();
              }
            rows.Clear();
            Report.Debug("Parsing and establishment complete.");
            }
          //
          if (be_augmenting_enabled)
            {
            Report.Debug("Augmenting BizModel CAD records...");
            biz.cad_records.Augment();
            Report.Debug("Augmentation complete.");
            }
          //
          // Validate and trim the cad_records.
          //
          Report.Debug("Validating and trimming BizModel CAD records...");
          biz.cad_records.ValidateAndTrim();
          Report.Debug("Validation and trimming complete.");
          //
          // Notify members as appropriate.
          //
          Report.Debug("Detecting field situations and making appropriate notifications...");
          biz.field_situations.DetectAndNotify
            (
            saved_multambholds_alert_timestamp:ref saved_meta_surge_alert_timestamp_ems,
            saved_multalsholds_alert_timestamp:ref saved_meta_surge_alert_timestamp_als,
            saved_firesurge_alert_timestamp:ref saved_meta_surge_alert_timestamp_fire
            );
          Report.Debug("Field situations detection and notifications complete.");
          //
          Thread.Sleep(millisecondsTimeout:vbemsbridge_refresh_rate_in_milliseconds);
          }
        catch (Exception e)
          {
          Report.Error($"{e}");
          Report.Warning("Pausing to recover...");
          Thread.Sleep(millisecondsTimeout:int.Parse(appSettings["recovery_interval_minutes"]));
          datetime_of_last_login = DateTime.MinValue;
          }
        }
      }

    }

  }
