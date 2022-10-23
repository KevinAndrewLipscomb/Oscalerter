using Class_biz_cad_activity_notification_agent;
using OscalertSvc.Scrape;
using System.Configuration;

namespace OscalertSvc.Models
  {
  /// <summary>
  /// The composition root of the application
  /// </summary>
  public class Biz
    {

    static readonly private TClass_ss_imagetrendelite ss_cad_provider = new();

    public TClass_biz_cad_activity_notification_agent cad_activity_notification_agent = new
      (
      ss_cad_provider_imp:ss_cad_provider,
      appSettings_imp:ConfigurationManager.AppSettings
      );

    }
  }
