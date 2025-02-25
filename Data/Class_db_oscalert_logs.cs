// Derived from KiAspdotnetFramework/component/db/Class~db~template~kicrudhelped~items.cs~template

using Class_db;
using Class_db_trail;
using kix;
using MySql.Data.MySqlClient;
using System;

namespace Class_db_oscalert_logs
  {
  public class TClass_db_oscalert_logs: TClass_db
    {
    private class oscalert_log_summary
      {
      public string id;
      }

    private readonly TClass_db_trail db_trail = null;

    public TClass_db_oscalert_logs() : base()
      {
      db_trail = new TClass_db_trail();
      }

    public bool Delete(string id)
      {
      var result = true;
      Open();
      try
        {
        using var my_sql_command = new MySqlCommand(db_trail.Saved("delete from oscalert_log where id = \"" + id + "\""), Connection);
        my_sql_command.ExecuteNonQuery();
        }
      catch(Exception e)
        {
        if (e.Message.StartsWith("Cannot delete or update a parent row: a foreign key constraint fails", true, null))
          {
          result = false;
          }
        else
          {
          throw;
          }
        }
      Close();
      return result;
      }

    internal void Enter(string content)
      {
      Open();
      using var my_sql_command = new MySqlCommand("insert oscalert_log set content = '" + content + "'",Connection);
      my_sql_command.ExecuteNonQuery();
      Close();
      }

    public bool Get
      (
      string id,
      out string timestamp,
      out string content
      )
      {
      timestamp = k.EMPTY;
      content = k.EMPTY;
      var result = false;
      //
      Open();
      using var my_sql_command = new MySqlCommand("select * from oscalert_log where CAST(id AS CHAR) = \"" + id + "\"", Connection);
      var dr = my_sql_command.ExecuteReader();
      if (dr.Read())
        {
        timestamp = dr["timestamp"].ToString();
        content = dr["content"].ToString();
        result = true;
        }
      dr.Close();
      Close();
      return result;
      }

    public void Set
      (
      string id,
      string timestamp,
      string content
      )
      {
      var childless_field_assignments_clause = k.EMPTY
      + "timestamp = NULLIF('" + timestamp + "','')"
      + " , content = NULLIF('" + content + "','')"
      + k.EMPTY;
      db_trail.MimicTraditionalInsertOnDuplicateKeyUpdate
        (
        target_table_name:"oscalert_log",
        key_field_name:"id",
        key_field_value:id,
        childless_field_assignments_clause:childless_field_assignments_clause
        );
      }

    internal object Summary(string id)
      {
      Open();
      using var my_sql_command = new MySqlCommand
        (
        "SELECT *"
        + " FROM oscalert_log"
        + " where id = '" + id + "'",
        Connection
        );
      var dr = my_sql_command.ExecuteReader();
      dr.Read();
      var the_summary = new oscalert_log_summary()
        {
        id = id
        };
      Close();
      return the_summary;
      }

    } // end TClass_db_oscalert_logs

  }
