// Derived from KiAspdotnetFramework/component/biz/Class~biz~~template~kicrudhelped~item.cs~template

using Class_db_cad_records;
using MySql.Data.MySqlClient;
using Oscalerter.Models;
using System;

namespace Class_biz_cad_records
  {
  public class TClass_biz_cad_records : ObjectBiz
    {

    private readonly TClass_db_cad_records db_cad_records;

    public TClass_biz_cad_records
      (
      TClass_db_cad_records db_cad_records_imp
      )
      : base()
      {
      db_cad_records = db_cad_records_imp;
      }

    internal void Augment()
      {
      db_cad_records.Augment();
      }

    internal string LocalRenditionOf
      (
      Biz biz,
      string nature
      )
      {
      var active_case_board_rendition_of = nature;
      var local_rendition = biz.incident_nature_translations.LocalOfForeign(nature);
      if (local_rendition.Length > 0)
        {
        active_case_board_rendition_of = local_rendition;
        }
      return active_case_board_rendition_of;
      }

    public bool Delete(string id)
      {
      return db_cad_records.Delete(id);
      }

    public bool Get
      (
      string id,
      out DateTime incident_date,
      out string incident_num,
      out string incident_address,
      out string call_sign,
      out DateTime time_initialized,
      out DateTime time_of_alarm,
      out DateTime time_enroute,
      out DateTime time_on_scene,
      out DateTime time_transporting,
      out DateTime time_at_hospital,
      out DateTime time_available,
      out DateTime time_downloaded
      )
      {
      return db_cad_records.Get
        (
        id,
        out incident_date,
        out incident_num,
        out incident_address,
        out call_sign,
        out time_initialized,
        out time_of_alarm,
        out time_enroute,
        out time_on_scene,
        out time_transporting,
        out time_at_hospital,
        out time_available,
        out time_downloaded
        );
      }

    public void Set
      (
      string id,
      string incident_date,
      string incident_num,
      string incident_address,
      string call_sign,
      string time_initialized,
      string time_of_alarm,
      string time_enroute,
      string time_on_scene,
      string time_transporting,
      string time_at_hospital,
      string time_available,
      string time_downloaded,
      string nature
      )
      {
      try
        {
        db_cad_records.Set
          (
          id,
          incident_date,
          incident_num,
          incident_address,
          call_sign,
          time_initialized,
          time_of_alarm,
          time_enroute,
          time_on_scene,
          time_transporting,
          time_at_hospital,
          time_available,
          time_downloaded,
          nature,
          incident_date_parse_format:"%Y-%m-%d" // the dominant format used by the provider
          );
        }
      catch (MySqlException e)
        {
        if (e.Message.Contains("Incorrect datetime value")) 
          {
          db_cad_records.Set
            (
            id,
            incident_date,
            incident_num,
            incident_address,
            call_sign,
            time_initialized,
            time_of_alarm,
            time_enroute,
            time_on_scene,
            time_transporting,
            time_at_hospital,
            time_available,
            time_downloaded,
            nature,
            incident_date_parse_format:"%m/%d/%Y" // the format used by the provider occassionally, for unknown reasons
            );
          }
        else
          {
          throw e;
          }
        }
      }

    public object Summary(string id)
      {
      return db_cad_records.Summary(id);
      }

    public void ValidateAndTrim()
      {
      db_cad_records.ValidateAndTrim();
      }

    } // end TClass_biz_cad_records

  }
