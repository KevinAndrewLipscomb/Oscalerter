// Derived from KiAspdotnetFramework/component/db/Class~db~template~kicrudhelped~items.cs~template

using Class_db;
using Class_db_trail;
using kix;
using MySql.Data.MySqlClient;

namespace Class_db_field_situation_impressions
  {
  public class TClass_db_field_situation_impressions: TClass_db
    {
    private class field_situation_impression_summary
      {
      public string id;
      }

    private readonly TClass_db_trail db_trail = null;

    public TClass_db_field_situation_impressions() : base()
      {
      db_trail = new TClass_db_trail();
      }

    public bool Delete(string id)
      {
      var result = true;
      Open();
      try
        {
        using var my_sql_command = new MySqlCommand(db_trail.Saved("delete from field_situation_impression where id = \"" + id + "\""), connection);
        my_sql_command.ExecuteNonQuery();
        }
      catch(System.Exception e)
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

    internal string ElaborationOfDescription(string description)
      {
      Open();
      using var my_sql_command = new MySqlCommand("select elaboration from field_situation_impression where description = '" + description + "'",connection);
      var elaboration_of_description = my_sql_command.ExecuteScalar().ToString();
      Close();
      return elaboration_of_description;
      }

    public bool Get
      (
      string id,
      out string description,
      out string pecking_order
      )
      {
      description = k.EMPTY;
      pecking_order = k.EMPTY;
      var result = false;
      //
      Open();
      using var my_sql_command = new MySqlCommand("select * from field_situation_impression where CAST(id AS CHAR) = \"" + id + "\"", connection);
      var dr = my_sql_command.ExecuteReader();
      if (dr.Read())
        {
        description = dr["description"].ToString();
        pecking_order = dr["pecking_order"].ToString();
        result = true;
        }
      dr.Close();
      Close();
      return result;
      }

    public void GetIdDescriptionElaborationOfPeckingOrder
      (
      k.int_nonnegative pecking_order,
      out string id,
      out string description,
      out string elaboration
      )
      {
      Open();
      using var my_sql_command = new MySqlCommand("select id,description,elaboration from field_situation_impression where pecking_order = '" + pecking_order.val + "'",connection);
      var dr = my_sql_command.ExecuteReader();
      dr.Read();
      id = dr["id"].ToString();
      description = dr["description"].ToString();
      elaboration = dr["elaboration"].ToString();
      dr.Close();
      Close();
      }

    public int PeckingOrderValOfDescription(string description)
      {
      Open();
      using var my_sql_command = new MySqlCommand("select pecking_order from field_situation_impression where description = '" + description + "'",connection);
      var pecking_order_val_of_description = int.Parse(my_sql_command.ExecuteScalar().ToString());
      Close();
      return pecking_order_val_of_description;
      }

    public void Set
      (
      string id,
      string description,
      string pecking_order
      )
      {
      var childless_field_assignments_clause = k.EMPTY
      + "description = NULLIF('" + description + "','')"
      + " , pecking_order = NULLIF('" + pecking_order + "','')"
      + k.EMPTY;
      db_trail.MimicTraditionalInsertOnDuplicateKeyUpdate
        (
        target_table_name:"field_situation_impression",
        key_field_name:"id",
        key_field_value:id,
        childless_field_assignments_clause:childless_field_assignments_clause
        );
      }

    public object Summary(string id)
      {
      Open();
      using var my_sql_command = new MySqlCommand
        (
        "SELECT *"
        + " FROM field_situation_impression"
        + " where id = '" + id + "'",
        connection
        );
      var dr = my_sql_command.ExecuteReader();
      dr.Read();
      var the_summary = new field_situation_impression_summary()
        {
        id = id
        };
      Close();
      return the_summary;
      }

    } // end TClass_db_field_situation_impressions

  }
