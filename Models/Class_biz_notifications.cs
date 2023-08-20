using Class_db_notifications;
using Class_db_oscalert_logs;
using kix;
using System;
using System.Configuration;

namespace Class_biz_notifications
  {

  public class TClass_biz_notifications
    {

    private class Static
      {
      public static char[] BreakChars = new char[3 + 1];
      static Static() // constructor
        {
        BreakChars[1] = Convert.ToChar(k.SPACE);
        BreakChars[2] = Convert.ToChar(k.TAB);
        BreakChars[3] = Convert.ToChar(k.HYPHEN);
        }
      }

    private readonly string application_name = k.EMPTY;
    private readonly TClass_db_notifications db_notifications = null;
    private readonly TClass_db_oscalert_logs db_oscalert_logs = null;
    private readonly string host_domain_name = k.EMPTY;
    private readonly string runtime_root_fullspec = k.EMPTY;

    public TClass_biz_notifications() : base()
      {
      application_name = ConfigurationManager.AppSettings["application_name"];
      db_notifications = new();
      db_oscalert_logs = new();
      host_domain_name = ConfigurationManager.AppSettings["host_domain_name"];
      runtime_root_fullspec = ConfigurationManager.AppSettings["runtime_root_fullspec"];
      }

    internal void IssueOscalert
      (
      string description,
      string elaboration
      )
      {
      k.SmtpMailSend
        (
        from:ConfigurationManager.AppSettings["sender_email_address"],
        to:k.EMPTY,
        subject:k.EMPTY,
        message_string:elaboration,
        be_html:false,
        cc:k.EMPTY,
        bcc:db_notifications.TargetOfOscalert(description:description),
        reply_to:ConfigurationManager.AppSettings["bouncer_email_address"]
        );
      db_oscalert_logs.Enter(content:elaboration);
      }

    internal void IssueOscalertForAddressOfParticlarInterest(string elaboration)
      {
      k.SmtpMailSend
        (
        from:ConfigurationManager.AppSettings["sender_email_address"],
        to:ConfigurationManager.AppSettings["sysadmin_sms_address"],
        subject:k.EMPTY,
        message_string:"*" + elaboration,
        be_html:false,
        cc:k.EMPTY,
        bcc:k.EMPTY,
        reply_to:ConfigurationManager.AppSettings["bouncer_email_address"]
        );
      }

    } // end TClass_biz_notifications

  }
