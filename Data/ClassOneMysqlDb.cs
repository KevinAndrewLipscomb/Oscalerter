using kix;
using MySql.Data.MySqlClient;

namespace OscalertSvc.Data
  {
  class ClassOneMysqlDb : ClassMysqlDb, IClassOneDb
    {

    private readonly ClassDbTrail dbTrail = null;

    public void Delete(string username)
      {
      Open();
      using var my_sql_command = new MySqlCommand(dbTrail.Saved("delete from user where username = \"" + username + "\""), connection);
      my_sql_command.ExecuteNonQuery();
      Close();
      }

    public bool Get
      (
      string username,
      out string encoded_password,
      out bool be_stale_password,
      out string password_reset_email_address,
      out bool be_active,
      out uint num_unsuccessful_login_attempts,
      out string last_login
      )
      {
      bool result;
      MySqlDataReader dr;

      encoded_password = k.EMPTY;
      be_stale_password = false;
      password_reset_email_address = k.EMPTY;
      be_active = false;
      num_unsuccessful_login_attempts = 0;
      last_login = k.EMPTY;
      result = false;
      Open();
      using var my_sql_command = new MySqlCommand("select username" + " , IFNULL(encoded_password_hash,\"\") as encoded_password" + " , be_stale_password" + " , password_reset_email_address" + " , be_active" + " , num_unsuccessful_login_attempts" + " , IFNULL(last_login,\"\") as last_login" + " from user" + " where username = \"" + username + "\"", connection);
      dr = my_sql_command.ExecuteReader();
      if (dr.Read())
        {
        username = dr["username"].ToString();
        encoded_password = dr["encoded_password"].ToString();
        be_stale_password = (dr["be_stale_password"].ToString() == "1");
        password_reset_email_address = dr["password_reset_email_address"].ToString();
        be_active = (dr["be_active"].ToString() == "1");
        num_unsuccessful_login_attempts = uint.Parse(dr["num_unsuccessful_login_attempts"].ToString());
        last_login = dr["last_login"].ToString();
        result = true;
        }
      dr.Close();
      Close();
      return result;
      }

    public string IdOf(string username)
      {
      string result;
      Open();
      using var my_sql_command = new MySqlCommand("select id from user where username = \"" + username + "\"", connection);
      result = my_sql_command.ExecuteScalar().ToString();
      Close();
      return result;
      }

    }
  }
