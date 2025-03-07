// Derived from KiAspdotnetFramework/component/db/Class~db~template~kicrudhelped~items.cs~template

using Class_db;
using Class_db_trail;
using kix;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Class_db_field_situations
  {

  public class TClass_db_field_situations: TClass_db
    {

    internal class digest
      {
      internal string case_num;
      internal string address;
      internal string assignment;
      internal DateTime time_initialized;
      internal int num_ambulances;
      internal int num_zone_cars;
      internal int num_squad_trucks;
      internal int num_supervisors;
      internal int num_holds;
      internal int num_hzcs;
      internal int num_acarts;
      internal int num_matvs;
      internal int num_mbks;
      internal int num_lifeguards;
      internal int num_mci_trucks;
      internal int num_mrtks;
      internal int num_rbs;
      internal int num_tacs;
      internal int num_bats;
      internal int num_cars;
      internal int num_engines;
      internal int num_fboas;
      internal int num_frsqs;
      internal int num_hazs;
      internal int num_ladders;
      internal int num_safes;
      internal int num_sups;
      internal int num_tankers;
      internal int num_zods;
      internal bool be_emtals;
      internal bool be_etby;
      internal bool be_mrt;
      internal bool be_pio;
      internal bool be_pu;
      internal bool be_rescue_area;
      internal bool be_sqtm;
      internal bool be_ftby;
      internal bool be_mirt;
      internal bool be_stech;
      internal bool be_sart;
      internal string nature;
      }

    private class field_situation_summary
      {
      public string id;
      }

    private readonly TClass_db_trail db_trail = null;

    public TClass_db_field_situations() : base()
      {
      db_trail = new TClass_db_trail();
      }

    internal bool BeFireSurge()
      {
      Open();
      using var my_sql_command = new MySqlCommand
        (
        "select"
        + " IF(((sum(num_engines) + sum(num_ladders) + sum(num_frsqs) >= 10) and (sum(num_tacs) >= 2) and (sum(num_bats) >= 2) and (sum(num_ambulances) + sum(num_holds) >= 2) and (sum(num_supervisors) >= 2) and (sum(num_safes) >= 1))"
        +     " or (sum(field_situation_impression.description = 'WorkingFire') >= 2)"
        +   " , 1, 0)"
        + " from field_situation"
        +   " join field_situation_impression on (field_situation_impression.id=field_situation.impression_id)"
        ,
        Connection
        );
      var be_meta_surge_fire = "1" == my_sql_command.ExecuteScalar().ToString();
      Close();
      return be_meta_surge_fire;
      }

    internal bool BeMultAlsHolds()
      {
      Open();
      using var my_sql_command = new MySqlCommand("select IF(sum(num_hzcs) >= 2, 1, 0) from field_situation",Connection);
      var be_meta_surge_als = "1" == my_sql_command.ExecuteScalar().ToString();
      Close();
      return be_meta_surge_als;
      }

    internal bool BeMultAmbHolds()
      {
      Open();
      using var my_sql_command = new MySqlCommand("select IF(sum(num_holds) >= 2, 1, 0) from field_situation",Connection);
      var be_meta_surge_ems = "1" == my_sql_command.ExecuteScalar().ToString();
      Close();
      return be_meta_surge_ems;
      }

    public bool Delete(string id)
      {
      var result = true;
      Open();
      try
        {
        using var my_sql_command = new MySqlCommand(db_trail.Saved("delete from field_situation where id = \"" + id + "\""), Connection);
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
          throw ;
          }
        }
      Close();
      return result;
      }

    internal void DeleteAnyStillStale()
      {
      Open();
      using var my_sql_command = new MySqlCommand("delete from field_situation where be_stale",Connection);
      my_sql_command.ExecuteNonQuery();
      Close();
      }

    internal Queue<digest> DigestQ()
      {
      var digest_q = new Queue<digest>();
      //
      Open();
      using var my_sql_command = new MySqlCommand
        (
        "select IFNULL(MAX(incident_num),CONCAT('OFS',LEFT(MD5(RAND()),13))) as case_num" // the call to MAX() assures that an actual incident_num value will be favored over a NULL incident_num during the GROUP BY
        + " , incident_address as address"
        + " , GROUP_CONCAT(call_sign order by list_pecking_order,call_sign) as assignment"
        + " , DATE_FORMAT(TIMESTAMP(incident_date,time_initialized),'%Y-%m-%d %H:%i') as time_initialized"
        + " , sum(call_sign REGEXP '^A[[:digit:]]') as num_ambulances"
        + " , sum(call_sign REGEXP '^ZM[[:digit:]]') as num_zone_cars"
        + " , sum(call_sign REGEXP '^MSQ[[:digit:]]') as num_squad_trucks"
        + " , sum(call_sign REGEXP '^EMS[[:digit:]]') as num_supervisors"
        + " , sum(call_sign REGEXP '^HOLD[[:digit:]]') as num_holds"
        + " , sum(call_sign REGEXP '^HZC[[:digit:]]') as num_hzcs"
        + " , sum(call_sign REGEXP '^(CART|MRAT)[[:digit:]]') as num_acarts"
        + " , sum(call_sign REGEXP '^MATV[[:digit:]]') as num_matvs"
        + " , sum(call_sign REGEXP '^MBK[[:digit:]]') as num_mbks"
        + " , sum(call_sign REGEXP '^.LG') as num_lifeguards"
        + " , sum(call_sign REGEXP '^MCI[[:digit:]]') as num_mci_trucks"
        + " , sum(call_sign REGEXP '^MMTK[[:digit:]]') as num_mrtks"
        + " , sum(call_sign REGEXP '^MBOAT[[:digit:]]') as num_rbs"
        + " , sum(call_sign REGEXP '^TAC[[:digit:]]') as num_tacs"
        + " , sum(call_sign REGEXP '^BAT[[:digit:]]') as num_bats"
        + " , sum(call_sign REGEXP '^CAR[[:digit:]]') as num_cars"
        + " , sum(call_sign REGEXP '^N?E[[:digit:]]') as num_engines"
        + " , sum(call_sign REGEXP '^FBOA[[:digit:]]') as num_fboas"
        + " , sum(call_sign REGEXP '^FR[[:digit:]]') as num_frsqs"
        + " , sum(call_sign REGEXP '^HAZ[[:digit:]]') as num_hazs"
        + " , sum(call_sign REGEXP '^L[[:digit:]]') as num_ladders"
        + " , sum(call_sign REGEXP '^SAFE[[:digit:]]') as num_safes"
        + " , sum(call_sign REGEXP '^SUP[[:digit:]]') as num_sups"
        + " , sum(call_sign REGEXP '^T[[:digit:]]') as num_tankers"
        + " , sum(call_sign REGEXP '^ZOD[[:digit:]]') as num_zods"
        + " , sum(call_sign = 'EMTALS') as be_emtals"
        + " , sum(call_sign = 'ETBY') as be_etby"
        + " , sum(call_sign = 'MRT') as be_mrt"
        + " , sum(call_sign = 'PIO') as be_pio"
        + " , sum(call_sign = 'PU') as be_pu"
        + " , sum(call_sign REGEXP '^R[[:digit:]]') as be_rescue_area"
        + " , sum(call_sign in ('SQTM','TECH')) as be_sqtm"
        + " , sum(call_sign = 'FTBY') as be_ftby"
        + " , sum(call_sign = 'MIRT') as be_mirt"
        + " , sum(call_sign = 'STECH') as be_stech"
        + " , sum(call_sign REGEXP '^SART') as be_sart" // Both SART and SARTM have appeared in the CAD records.
        + " , nature"
        + " from"
        +   " ("
        +   " select incident_date"
        +   " , incident_num"
        +   " , incident_address"
        +   " , call_sign"
        +   " , IF(call_sign in ('EMTALS','ETBY','FTBY','MIRT','MRT','SQTM','SART','SARTM','TECH'),0," // especially informative indicators
        +        " IF(call_sign REGEXP '^R[[:digit:]]',10," // rescue area
        +           " IF(call_sign REGEXP '^TAC[[:digit:]]',20," // tactical channel
        +              " IF(call_sign REGEXP '^E[[:digit:]]',30," // engine
        +                 " IF(call_sign REGEXP '^NE[[:digit:]]',40," // navy engine
        +                    " IF(call_sign REGEXP '^L[[:digit:]]',50," // ladder
        +                       " IF(call_sign REGEXP '^FR[[:digit:]]',60," // fire squad
        +                          " IF(call_sign REGEXP '^T[[:digit:]]',70," // tanker
        +                             " IF(call_sign REGEXP '^HAZ[[:digit:]]',80," // hazmat truck
        +                                " IF(call_sign REGEXP '^BTRK[[:digit:]]',90," // brush truck
        +                                   " IF(call_sign REGEXP '^MSQ[[:digit:]]',100," // squad truck
        +                                      " IF(call_sign REGEXP '^A[[:digit:]]',110," // ambulance
        +                                         " IF(call_sign REGEXP '^NR[[:digit:]]',120," // navy rescue
        +                                            " IF(call_sign REGEXP '^HOLD[[:digit:]]',130," // holding for ambulance
        +                                               " IF(call_sign REGEXP '^ZM[[:digit:]]',140," // zone car
        +                                                  " IF(call_sign REGEXP '^HZC[[:digit:]]',150," // holding for zone car
        +                                                     " IF(call_sign REGEXP '^(CART|MRAT)[[:digit:]]',160," // ambulance cart
        +                                                        " IF(call_sign REGEXP '^MATV[[:digit:]]',170," // medical atv
        +                                                           " IF(call_sign REGEXP '^MBK[[:digit:]]',180," // medical bike team
        +                                                              " IF(call_sign REGEXP '^EMS[[:digit:]]',190," // EMS supervisor or chief
        +                                                                 " IF(call_sign REGEXP '^ECH[[:digit:]]',200," // EMS chief
        +                                                                    " IF(call_sign REGEXP '^BAT[[:digit:]]',210," // battalion chief
        +                                                                       " IF(call_sign REGEXP '^CAR[[:digit:]]',220," // fire >=div chief
        +                                                                          " 300" // anybody else, alphabetically
        +                                                                          " )"
        +                                                                       " )"
        +                                                                    " )"
        +                                                                 " )"
        +                                                              " )"
        +                                                           " )"
        +                                                        " )"
        +                                                     " )"
        +                                                  " )"
        +                                               " )"
        +                                            " )"
        +                                         " )"
        +                                      " )"
        +                                   " )"
        +                                " )"
        +                             " )"
        +                          " )"
        +                       " )"
        +                    " )"
        +                 " )"
        +              " )"
        +           " )"
        +        " ) as list_pecking_order"
        +   " , DATE_FORMAT(time_initialized,'%H:%i') as time_initialized"
        +   " , DATE_FORMAT(time_of_alarm,'%H:%i') as time_of_alarm"
        +   " , DATE_FORMAT(time_enroute,'%H:%i') as time_enroute"
        +   " , DATE_FORMAT(time_on_scene,'%H:%i') as time_onscene"
        +   " , DATE_FORMAT(time_transporting,'%H:%i') as time_transporting"
        +   " , DATE_FORMAT(time_at_hospital,'%H:%i') as time_at_hospital"
        +   " , IFNULL("
        +         " nature"
        +       " ,"
        +         " ("
        +         " select IFNULL(description,radio_dispatch.nature)"
        +         " from radio_dispatch"
        +           " left join incident_nature on (incident_nature.designator=radio_dispatch.nature)"
        +         " where incident_address like CONCAT(address,'%')"
        +           " or address like CONCAT(incident_address,'%')"
        +           " or incident_address like CONCAT(SUBSTRING_INDEX(address,' / ',-1),'% / ',SUBSTRING_INDEX(address,' / ',1))"
        +           " or address like CONCAT(SUBSTRING_INDEX(incident_address,' / ',-1),'% / ',SUBSTRING_INDEX(incident_address,' / ',1))"
                        // incidents at intersections, which generate multiple 911 calls, are sometimes dispatched as a transposition of how they appear in the final case of record:  B / A instead of A / B
        +         " order by transmission_datetime desc"
        +         " limit 1"
        +         " )"
        +       " ) as nature"
        +   " from cad_record"
        +   " where be_current"
        +   " )"
        +   " as active_case_assignment"
        + " group by address"
        + " order by time_initialized desc"
        ,
        Connection
        );
      var dr = my_sql_command.ExecuteReader();
      while (dr.Read())
        {
        digest_q.Enqueue
          (
          new digest
            {
            case_num = dr["case_num"].ToString(),
            address = dr["address"].ToString(),
            assignment = dr["assignment"].ToString(),
            time_initialized = DateTime.Parse(dr["time_initialized"].ToString()),
            num_ambulances = int.Parse(dr["num_ambulances"].ToString()),
            num_zone_cars = int.Parse(dr["num_zone_cars"].ToString()),
            num_squad_trucks = int.Parse(dr["num_squad_trucks"].ToString()),
            num_supervisors = int.Parse(dr["num_supervisors"].ToString()),
            num_holds = int.Parse(dr["num_holds"].ToString()),
            num_hzcs = int.Parse(dr["num_hzcs"].ToString()),
            num_acarts = int.Parse(dr["num_acarts"].ToString()),
            num_matvs = int.Parse(dr["num_matvs"].ToString()),
            num_mbks = int.Parse(dr["num_mbks"].ToString()),
            num_lifeguards = int.Parse(dr["num_lifeguards"].ToString()),
            num_mci_trucks = int.Parse(dr["num_mci_trucks"].ToString()),
            num_mrtks = int.Parse(dr["num_mrtks"].ToString()),
            num_rbs = int.Parse(dr["num_rbs"].ToString()),
            num_tacs = int.Parse(dr["num_tacs"].ToString()),
            num_bats = int.Parse(dr["num_bats"].ToString()),
            num_cars = int.Parse(dr["num_cars"].ToString()),
            num_engines = int.Parse(dr["num_engines"].ToString()),
            num_fboas = int.Parse(dr["num_fboas"].ToString()),
            num_frsqs = int.Parse(dr["num_frsqs"].ToString()),
            num_hazs = int.Parse(dr["num_hazs"].ToString()),
            num_ladders = int.Parse(dr["num_ladders"].ToString()),
            num_safes = int.Parse(dr["num_safes"].ToString()),
            num_sups = int.Parse(dr["num_sups"].ToString()),
            num_tankers = int.Parse(dr["num_tankers"].ToString()),
            num_zods = int.Parse(dr["num_zods"].ToString()),
            be_emtals = (dr["be_emtals"].ToString() == "1"),
            be_etby = (dr["be_etby"].ToString() == "1"),
            be_mrt = (dr["be_mrt"].ToString() == "1"),
            be_pio = (dr["be_pio"].ToString() == "1"),
            be_pu = (dr["be_pu"].ToString() == "1"),
            be_rescue_area = (dr["be_rescue_area"].ToString() == "1"),
            be_sqtm = (dr["be_sqtm"].ToString() == "1"),
            be_ftby = (dr["be_ftby"].ToString() == "1"),
            be_mirt = (dr["be_mirt"].ToString() == "1"),
            be_stech = (dr["be_stech"].ToString() == "1"),
            be_sart = (dr["be_sart"].ToString() == "1"),
            nature = dr["nature"].ToString()
            }
          );
        }
      dr.Close();
      Close();
      return digest_q;
      }

    public bool Get
      (
      string id,
      out string case_num,
      out string address,
      out string assignment,
      out DateTime time_initialized,
      out string nature,
      out string impression_id,
      out string num_ambulances,
      out string num_zone_cars,
      out string num_squad_trucks,
      out string num_supervisors,
      out bool be_emtals,
      out bool be_etby,
      out string num_holds,
      out string num_hzcs,
      out string num_lifeguards,
      out string num_mci_trucks,
      out bool be_mrt,
      out string num_mrtks,
      out bool be_pio,
      out bool be_pu,
      out bool be_rescue_area,
      out string num_rbs,
      out bool be_sqtm,
      out string num_tacs,
      out string num_bats,
      out string num_cars,
      out string num_engines,
      out string num_fboas,
      out string num_frsqs,
      out bool be_ftby,
      out string num_hazs,
      out string num_ladders,
      out bool be_mirt,
      out string num_safes,
      out bool be_stech,
      out string num_sups,
      out string num_tankers,
      out bool be_sart,
      out string num_zods,
      out string num_acarts,
      out string num_matvs,
      out string num_mbks
      )
      {
      case_num = k.EMPTY;
      address = k.EMPTY;
      assignment = k.EMPTY;
      time_initialized = DateTime.MinValue;
      nature = k.EMPTY;
      impression_id = k.EMPTY;
      num_ambulances = k.EMPTY;
      num_zone_cars = k.EMPTY;
      num_squad_trucks = k.EMPTY;
      num_supervisors = k.EMPTY;
      be_emtals = false;
      be_etby = false;
      num_holds = k.EMPTY;
      num_hzcs = k.EMPTY;
      num_lifeguards = k.EMPTY;
      num_mci_trucks = k.EMPTY;
      be_mrt = false;
      num_mrtks = k.EMPTY;
      be_pio = false;
      be_pu = false;
      be_rescue_area = false;
      num_rbs = k.EMPTY;
      be_sqtm = false;
      num_tacs = k.EMPTY;
      num_bats = k.EMPTY;
      num_cars = k.EMPTY;
      num_engines = k.EMPTY;
      num_fboas = k.EMPTY;
      num_frsqs = k.EMPTY;
      be_ftby = false;
      num_hazs = k.EMPTY;
      num_ladders = k.EMPTY;
      be_mirt = false;
      num_safes = k.EMPTY;
      be_stech = false;
      num_sups = k.EMPTY;
      num_tankers = k.EMPTY;
      be_sart = false;
      num_zods = k.EMPTY;
      num_acarts = k.EMPTY;
      num_matvs = k.EMPTY;
      num_mbks = k.EMPTY;
      var result = false;
      //
      Open();
      using var my_sql_command = new MySqlCommand("select * from field_situation where CAST(id AS CHAR) = \"" + id + "\"", Connection);
      var dr = my_sql_command.ExecuteReader();
      if (dr.Read())
        {
        case_num = dr["case_num"].ToString();
        address = dr["address"].ToString();
        assignment = dr["assignment"].ToString();
        time_initialized = DateTime.Parse(dr["time_initialized"].ToString());
        nature = dr["nature"].ToString();
        impression_id = dr["impression_id"].ToString();
        num_ambulances = dr["num_ambulances"].ToString();
        num_zone_cars = dr["num_zone_cars"].ToString();
        num_squad_trucks = dr["num_squad_trucks"].ToString();
        num_supervisors = dr["num_supervisors"].ToString();
        be_emtals = (dr["be_emtals"].ToString() == "1");
        be_etby = (dr["be_etby"].ToString() == "1");
        num_holds = dr["num_holds"].ToString();
        num_hzcs = dr["num_hzcs"].ToString();
        num_lifeguards = dr["num_lifeguards"].ToString();
        num_mci_trucks = dr["num_mci_trucks"].ToString();
        be_mrt = (dr["be_mrt"].ToString() == "1");
        num_mrtks = dr["num_mrtks"].ToString();
        be_pio = (dr["be_pio"].ToString() == "1");
        be_pu = (dr["be_pu"].ToString() == "1");
        be_rescue_area = (dr["be_rescue_area"].ToString() == "1");
        num_rbs = dr["num_rbs"].ToString();
        be_sqtm = (dr["be_sqtm"].ToString() == "1");
        num_tacs = dr["num_tacs"].ToString();
        num_bats = dr["num_bats"].ToString();
        num_cars = dr["num_cars"].ToString();
        num_engines = dr["num_engines"].ToString();
        num_fboas = dr["num_fboas"].ToString();
        num_frsqs = dr["num_frsqs"].ToString();
        be_ftby = (dr["be_ftby"].ToString() == "1");
        num_hazs = dr["num_hazs"].ToString();
        num_ladders = dr["num_ladders"].ToString();
        be_mirt = (dr["be_mirt"].ToString() == "1");
        num_safes = dr["num_safes"].ToString();
        be_stech = (dr["be_stech"].ToString() == "1");
        num_sups = dr["num_sups"].ToString();
        num_tankers = dr["num_tankers"].ToString();
        be_sart = (dr["be_sart"].ToString() == "1");
        num_zods = dr["num_zods"].ToString();
        num_acarts = dr["num_acarts"].ToString();
        num_matvs = dr["num_matvs"].ToString();
        num_mbks = dr["num_mbks"].ToString();
        result = true;
        }
      dr.Close();
      Close();
      return result;
      }

    internal string NumConsideredActive()
      {
      Open();
      using var my_sql_command = new MySqlCommand
        (
        "select count(*)"
        + " from field_situation"
        +   " join field_situation_impression on (field_situation_impression.id=field_situation.impression_id)"
        + " where assignment not like '%FAST%'"
        +   " and assignment not rlike '^R[[:digit:]]+$'"
        +   " and assignment not in ('ECOMM','FCOMM')"
        +   " and"
        +     " ("
        +       " time_initialized >= DATE_SUB(NOW(),INTERVAL 3 HOUR)"
        +     " or"
        +       " assignment like '%,%'"
        +     " )",
        Connection
        );
      var num_considered_active = my_sql_command.ExecuteScalar().ToString();
      Close();
      return num_considered_active;
      }

    internal k.int_nonnegative PriorImpressionPeckingOrder(string case_num)
      {
      Open();
      using var my_sql_command = new MySqlCommand
        ("select pecking_order from field_situation join field_situation_impression on (field_situation_impression.id=field_situation.impression_id) where case_num = '" + case_num + "'",Connection);
      var pecking_order_obj = my_sql_command.ExecuteScalar();
      Close();
      return new k.int_nonnegative((pecking_order_obj == null ? 0 : int.Parse(pecking_order_obj.ToString())));
      }

    internal void MarkAllStale()
      {
      Open();
      using var my_sql_command = new MySqlCommand("update field_situation set be_stale = TRUE",Connection);
      my_sql_command.ExecuteNonQuery();
      Close();
      }

    public void Set
      (
      string id,
      string case_num,
      string address,
      string assignment,
      DateTime time_initialized,
      string nature,
      string impression_id,
      int num_ambulances,
      int num_zone_cars,
      int num_squad_trucks,
      int num_supervisors,
      bool be_emtals,
      bool be_etby,
      int num_holds,
      int num_hzcs,
      int num_lifeguards,
      int num_mci_trucks,
      bool be_mrt,
      int num_mrtks,
      bool be_pio,
      bool be_pu,
      bool be_rescue_area,
      int num_rbs,
      bool be_sqtm,
      int num_tacs,
      int num_bats,
      int num_cars,
      int num_engines,
      int num_fboas,
      int num_frsqs,
      bool be_ftby,
      int num_hazs,
      int num_ladders,
      bool be_mirt,
      int num_safes,
      bool be_stech,
      int num_sups,
      int num_tankers,
      bool be_sart,
      int num_zods,
      int num_acarts,
      int num_matvs,
      int num_mbks
      )
      {
      var childless_field_assignments_clause = k.EMPTY
      + "be_stale = FALSE"
      + " , case_num = NULLIF('" + case_num + "','')"
      + " , address = NULLIF('" + address + "','')"
      + " , assignment = NULLIF('" + assignment + "','')"
      + " , time_initialized = NULLIF('" + time_initialized.ToString("yyyy-MM-dd HH:mm") + "','')"
      + " , nature = NULLIF('" + nature + "','')"
      + " , impression_id = NULLIF('" + impression_id + "','')"
      + " , num_ambulances = NULLIF('" + num_ambulances.ToString() + "','')"
      + " , num_zone_cars = NULLIF('" + num_zone_cars.ToString() + "','')"
      + " , num_squad_trucks = NULLIF('" + num_squad_trucks.ToString() + "','')"
      + " , num_supervisors = NULLIF('" + num_supervisors.ToString() + "','')"
      + " , be_emtals = " + be_emtals.ToString()
      + " , be_etby = " + be_etby.ToString()
      + " , num_holds = NULLIF('" + num_holds.ToString() + "','')"
      + " , num_hzcs = NULLIF('" + num_hzcs.ToString() + "','')"
      + " , num_lifeguards = NULLIF('" + num_lifeguards.ToString() + "','')"
      + " , num_mci_trucks = NULLIF('" + num_mci_trucks.ToString() + "','')"
      + " , be_mrt = " + be_mrt.ToString()
      + " , num_mrtks = NULLIF('" + num_mrtks.ToString() + "','')"
      + " , be_pio = " + be_pio.ToString()
      + " , be_pu = " + be_pu.ToString()
      + " , be_rescue_area = " + be_rescue_area.ToString()
      + " , num_rbs = NULLIF('" + num_rbs.ToString() + "','')"
      + " , be_sqtm = " + be_sqtm.ToString()
      + " , num_tacs = NULLIF('" + num_tacs.ToString() + "','')"
      + " , num_bats = NULLIF('" + num_bats.ToString() + "','')"
      + " , num_cars = NULLIF('" + num_cars.ToString() + "','')"
      + " , num_engines = NULLIF('" + num_engines.ToString() + "','')"
      + " , num_fboas = NULLIF('" + num_fboas.ToString() + "','')"
      + " , num_frsqs = NULLIF('" + num_frsqs.ToString() + "','')"
      + " , be_ftby = " + be_ftby.ToString()
      + " , num_hazs = NULLIF('" + num_hazs.ToString() + "','')"
      + " , num_ladders = NULLIF('" + num_ladders.ToString() + "','')"
      + " , be_mirt = " + be_mirt.ToString()
      + " , num_safes = NULLIF('" + num_safes.ToString() + "','')"
      + " , be_stech = " + be_stech.ToString()
      + " , num_sups = NULLIF('" + num_sups.ToString() + "','')"
      + " , num_tankers = NULLIF('" + num_tankers.ToString() + "','')"
      + " , be_sart = " + be_sart.ToString()
      + " , num_zods = NULLIF('" + num_zods.ToString() + "','')"
      + " , num_acarts = NULLIF('" + num_acarts.ToString() + "','')"
      + " , num_matvs = NULLIF('" + num_matvs.ToString() + "','')"
      + " , num_mbks = NULLIF('" + num_mbks.ToString() + "','')"
      + k.EMPTY;
      //
      var target_table_name = "field_situation";
      var key_field_name = "id";
      var key_field_value = id;
      var additional_match_condition = " or case_num = '" + case_num + "'";
      //
      // The following code is adapted from Class_db_trail.MimicTraditionalInsertOnDuplicateKeyUpdate, but does not journal its activity.
      //
      const string DELIMITER = "~";
      var procedure_name = "MTIODKU_" + ConfigurationManager.AppSettings["application_name"] + "_" + DateTime.Now.Ticks.ToString("D19");
      var code = "/* DELIMITER '" + DELIMITER + "' */"
      + " drop procedure if exists " + procedure_name
      + DELIMITER
      + " create procedure " + procedure_name + "() modifies sql data"
      +   " BEGIN"
      +   " start transaction;"
      +   " if (select 1 from " + target_table_name + " where " + key_field_name + " = '" + key_field_value + "'" + (additional_match_condition.Length > 0 ? additional_match_condition : k.EMPTY) + " limit 1 LOCK IN SHARE MODE) is null"
      +   " then"
      +     " insert " + target_table_name + " set " + key_field_name + " = NULLIF('" + key_field_value + "',''), " + childless_field_assignments_clause + ";"
      +   " else"
      +     " update " + target_table_name + " set " + childless_field_assignments_clause + " where " + key_field_name + " = '" + key_field_value + "'" + (additional_match_condition.Length > 0 ? additional_match_condition : k.EMPTY) + ";"
      +   " end if;"
      +   " commit;"
      +   " END"
      + DELIMITER
      + " call " + procedure_name + "()"
      + DELIMITER
      + " drop procedure if exists " + procedure_name;
      var my_sql_script = new MySqlScript();
      my_sql_script.Connection = Connection;
      my_sql_script.Delimiter = DELIMITER;
      my_sql_script.Query = code;
      Open();
      ExecuteOneOffProcedureScriptWithTolerance(procedure_name,my_sql_script);
      //
      Close();
      }

    internal object Summary(string id)
      {
      Open();
      using var my_sql_command = new MySqlCommand
        (
        "SELECT *"
        + " FROM field_situation"
        + " where id = '" + id + "'",
        Connection
        );
      var dr = my_sql_command.ExecuteReader();
      dr.Read();
      var the_summary = new field_situation_summary()
        {
        id = id
        };
      dr.Close();
      Close();
      return the_summary;
      }

    } // end TClass_db_field_situations

  }
