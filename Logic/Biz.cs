﻿using Class_biz_cad_activity_notification_agent;
using Class_biz_cad_records;
using Class_biz_field_situations;
using Class_biz_incident_nature_translations;
using Class_db_cad_records;
using Class_db_trail;
using Oscalerter.Scrape;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;

namespace Oscalerter.Logic
  {
  /// <summary>
  /// The composition root of the application
  /// </summary>
  public class Biz
    {

    static Biz()
      {
      var appBase = AppDomain.CurrentDomain.BaseDirectory;
      var ConnectionStringsFilePath = Path.Combine(appBase, "Config\\Sensitive\\connectionStrings.config");
      if (!File.Exists(ConnectionStringsFilePath))
        {
        var message = $"'{ConnectionStringsFilePath}' is missing. You may have to create it from scratch using '{appBase}Config\\Template\\connectionStrings.config' as a basis.";
        throw new Exception(message);
        }
      }

    public TClass_biz_cad_activity_notification_agent cad_activity_notification_agent = new
      (
      ss_cad_provider_imp:ss_cad_provider,
      appSettings_imp:appSettings,
      connectionStrings_imp:connectionStrings
      );
    public TClass_biz_cad_records cad_records = new
      (
      db_cad_records_imp:db_cad_records
      );
    public TClass_biz_field_situations field_situations = new();
    public TClass_biz_incident_nature_translations incident_nature_translations = new();

    static readonly private NameValueCollection appSettings = ConfigurationManager.AppSettings;
    static readonly private ConnectionStringSettingsCollection connectionStrings = ConfigurationManager.ConnectionStrings;
    static readonly private TClass_db_cad_records db_cad_records = new
      (
      db_trail_imp:db_trail
      );
    static readonly private TClass_db_trail db_trail = new();
    static readonly private TClass_ss_imagetrendelite ss_cad_provider = new();

    }
  }
