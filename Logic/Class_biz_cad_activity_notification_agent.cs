using kix;
using Oscalerter.Logic;
using Oscalerter.Scrape.Interface;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading;

namespace Class_biz_cad_activity_notification_agent
  {

  public class TClass_biz_cad_activity_notification_agent : ObjectBiz
    {

    public TClass_biz_cad_activity_notification_agent // CONSTRUCTOR
      (
      IClass_ss ss_cad_provider_imp,
      NameValueCollection appSettings_imp,
      ConnectionStringSettingsCollection connectionStrings_imp
      )
      {
      ss_cad_provider = ss_cad_provider_imp.WithReport(Report);
      appSettings = appSettings_imp;
      connectionStrings = connectionStrings_imp;
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
      IClass_ss.EmsCadList current_ems_cad_list;
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
              username:connectionStrings["vbemsbridge_username"].ConnectionString,
              password:connectionStrings["vbemsbridge_password"].ConnectionString,
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
              address = cells.Where(cell => cell.Name == "StreetAddress").Single().Value;
              current_incident_num = cells.Where(cell => cell.Name == "IncidentNumber").Single().Value;
              incident_date_time_initialized = cells.Where(cell => cell.Name == "PSAP").Single().Value;
              if(
                  (incident_date_time_initialized.Length > 1) // there is an incident_date/time_initialized
                &&
                  (address.Length > 1) // there is an address
                &&
                  (
                    current_incident_num.StartsWith("E") // this is an EMS incident whose number starts with EMS
                  ||
                    current_incident_num.StartsWith("F") // this if a fire incident whose number starts with FD
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
                  call_sign:cells.Where(cell => cell.Name == "EMSUnitCallSign").Single().Value,
                  time_initialized:(incident_date_time_initialized.Split())[1],
                  time_of_alarm:(cells.Where(cell => cell.Name == "UnitNotifiedByDispatch").Single().Value.Contains(k.SPACE) ? (cells.Where(cell => cell.Name == "UnitNotifiedByDispatch").Single().Value.Split())[1] : k.EMPTY),
                  time_enroute:(cells.Where(cell => cell.Name == "UnitEnroute").Single().Value.Contains(k.SPACE) ? (cells.Where(cell => cell.Name == "UnitEnroute").Single().Value.Split())[1] : k.EMPTY),
                  time_on_scene:(cells.Where(cell => cell.Name == "UnitArrivedOnScene").Single().Value.Contains(k.SPACE) ? (cells.Where(cell => cell.Name == "UnitArrivedOnScene").Single().Value.Split())[1] : k.EMPTY),
                  time_transporting:(cells.Where(cell => cell.Name == "UnitLeftScene").Single().Value.Contains(k.SPACE) ? (cells.Where(cell => cell.Name == "UnitLeftScene").Single().Value.Split())[1] : k.EMPTY),
                  time_at_hospital:(cells.Where(cell => cell.Name == "PatientArrivedAtDestination").Single().Value.Contains(k.SPACE) ? (cells.Where(cell => cell.Name == "PatientArrivedAtDestination").Single().Value.Split())[1] : k.EMPTY),
                  time_available:(cells.Where(cell => cell.Name == "UnitBackInService").Single().Value.Contains(k.SPACE) ? (cells.Where(cell => cell.Name == "UnitBackInService").Single().Value.Split())[1] : k.EMPTY),
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

    readonly private NameValueCollection appSettings = null;
    readonly private ConnectionStringSettingsCollection connectionStrings = null;
    readonly private IClass_ss ss_cad_provider = null;

    }

  }
