namespace OscalertConsoleApp.Data
  {
  public interface IClassOneDb
    {
    void Delete(string username);
    bool Get
      (
      string username,
      out string encoded_password,
      out bool be_stale_password,
      out string password_reset_email_address,
      out bool be_active,
      out uint num_unsuccessful_login_attempts,
      out string last_login
      );
    string IdOf(string username);
    }
  }