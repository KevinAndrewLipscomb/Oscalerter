﻿using Class_ss;
using ki.mvc;
using System.Collections.Generic;
using System.Net;

namespace OscalertConsoleApp.Scrape.Interface
  {
  public interface IClass_ss
    {

    //--
    //
    // BEGIN code generated by pasting the received JSON data into a JSON-to-C# converter (like at https://jsonutils.com/), making
    // sure the root object is named "EmsCadList", and making sure the accessibilities are at least "public".
    //
#pragma warning disable CA1034 // Nested types should not be visible
#pragma warning disable CA2227 // Collection properties should be read only
    //
    //--
    //
    public class Column
      {
      public string Name { get; set; }
      public string Value { get; set; }
      public string DataType { get; set; }
      }

    public class Record
      {
      public IList<Column> Columns { get; set; }
      }

    public class EmsCadList
      {
      public object ErrorMessage { get; set; }
      public int TotalRecordCount { get; set; }
      public IList<Record> Records { get; set; }
      public object ChildView { get; set; }
      public string RequestIdentifier { get; set; }
      public int NewDynamicListEngineUsed { get; set; }
      }
    //
    //--
    //
#pragma warning restore CA2227 // Collection properties should be read only
#pragma warning restore CA1034 // Nested types should not be visible
    //
    // END
    //
    //--

    EmsCadList CurrentEmsCadList
      (
      CookieContainer cookie_container,
      string request_identifier
      );
    void Login(string username, string password, CookieContainer cookie_container);
    void Nudge(CookieContainer cookie_container);
    IClass_ss WithReport(ReportClass Report);

    }
  }