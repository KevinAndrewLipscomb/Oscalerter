using Class_ss;
using kix;
using OscalertSvc.Scrape.Interface;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text.Json;
using static OscalertSvc.Scrape.Interface.IClass_ss;

namespace OscalertSvc.Scrape
  {
  public class TClass_ss_imagetrendelite : TClass_ss, IClass_ss
    {

    public void Login
      (
      string username,
      string password,
      CookieContainer cookie_container
      )
      {
      HttpWebResponse response;
      if (!Request_www_imagetrendelite_com_Signin
          (
          cookie_container: cookie_container,
          username: username,
          password: password,
          response: out response
          )
        )
        {
        throw new Exception("Request_www_imagetrendelite_com_Signin() returned FALSE.");
        }
      HtmlDocumentOf(ConsumedStreamOf(response));
      }

    public void Nudge(CookieContainer cookie_container)
      {
      HttpWebResponse response;
      if (!Request_www_imagetrendelite_com_Get1
          (
          cookie_container: cookie_container,
          response: out response
          )
        )
        {
        throw new Exception("Request_www_imagetrendelite_com_Get1() returned FALSE.");
        }
      HtmlDocumentOf(ConsumedStreamOf(response));
      //
      if (!Request_www_imagetrendelite_com_Get2
          (
          cookie_container: cookie_container,
          response: out response
          )
        )
        {
        throw new Exception("Request_www_imagetrendelite_com_Get2() returned FALSE.");
        }
      HtmlDocumentOf(ConsumedStreamOf(response));
      }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types")]
    public EmsCadList CurrentEmsCadList
      (
      CookieContainer cookie_container,
      string request_identifier
      )
      {
      HttpWebResponse response;
      if (!Request_www_imagetrendelite_com_Load
          (
          cookie_container: cookie_container as CookieContainer,
          request_identifier: request_identifier,
          response: out response
          )
        )
        {
        throw new Exception("Request_www_imagetrendelite_com_Load() returned FALSE.");
        }
      //
      EmsCadList current_ems_cad_list = null;
      var text = HtmlDocumentOf(ConsumedStreamOf(response)).DocumentNode.InnerText;
      if (text.Contains("Server Error"))
        {
        Report.Debug($"From TClass_ss_imagetrendelite.Request_www_imagetrendelite_com_Load, got: SERVER ERROR");
        }
      else if (text.Contains("Site Temporarily Offline"))
        {
        Report.Debug($"From TClass_ss_imagetrendelite.Request_www_imagetrendelite_com_Load, got: SITE TEMPORARILY OFFLINE");
        }
      else if (text.Contains("Service Unavailable"))
        {
        Report.Debug($"From TClass_ss_imagetrendelite.Request_www_imagetrendelite_com_Load, got: SERVICE UNAVAILABLE");
        }
      else if (text.Contains("Forbidden Access"))
        {
        Report.Debug($"From TClass_ss_imagetrendelite.Request_www_imagetrendelite_com_Load, got: FORBIDDEN ACCESS");
        }
      else if (text.Contains("Access Denied"))
        {
        Report.Debug($"From TClass_ss_imagetrendelite.Request_www_imagetrendelite_com_Load, got: ACCESS DENIED");
        }
      else
        {
        try
          {
          current_ems_cad_list = JsonSerializer.Deserialize<EmsCadList>(text);
          if (current_ems_cad_list.ErrorMessage != null)
            {
            Report.Debug($"TClass_ss_imagetrendelite.CurrentEmsCadList got an EmsCadList with ErrorMessage: " + current_ems_cad_list.ErrorMessage.ToString() + k.NEW_LINE);
            }
          }
        catch (Exception the_exception)
          {
          Report.Debug($"From TClass_ss_imagetrendelite.Request_www_imagetrendelite_com_Load, got: UNHANDLED " + the_exception.ToString() + k.NEW_LINE + text);
          }
        }
      return current_ems_cad_list;
      }

    //--
    //
    // BEGIN code generated initially by Fiddler extension "Request to Code"
    //
#pragma warning disable CA1031 // Do not catch general exception types
#pragma warning disable CA2234 // Pass system uri objects instead of strings
    //
    //--

    private bool Request_www_imagetrendelite_com_Signin
      (
      CookieContainer cookie_container,
      string username,
      string password,
      out HttpWebResponse response
      )
      {
      response = null;

      try
        {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://vbems.imagetrendelite.com/Elite/AuthAPI/Authenticate?organizationId=VBEMS");
        NormalizeWithCookie(request, cookie_container);

        request.Accept = "*/*";
        request.Headers.Add("X-Requested-With", @"XMLHttpRequest");
        request.ContentType = "application/json";
        request.Referer = "https://vbems.imagetrendelite.com/Elite/?organizationId=VBEMS";
        request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
        request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
        request.Headers.Set(HttpRequestHeader.CacheControl, "no-cache");

        request.Method = "POST";
        request.ServicePoint.Expect100Continue = false;

        string body = k.EMPTY
        + "{"
        + k.QUOTE + "identifier" + k.QUOTE + ":" + k.QUOTE + username + k.QUOTE + ","
        + k.QUOTE + "passkey" + k.QUOTE + ":" + k.QUOTE + password + k.QUOTE + ","
        + k.QUOTE + "organizationId" + k.QUOTE + ":" + k.QUOTE + "vbems" + k.QUOTE
        + "}";
        byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(body);
        request.ContentLength = postBytes.Length;
        Stream stream = request.GetRequestStream();
        stream.Write(postBytes, 0, postBytes.Length);
        stream.Close();

        response = (HttpWebResponse)request.GetResponse();
        }
      catch (WebException e)
        {
        if (e.Status == WebExceptionStatus.ProtocolError)
          response = (HttpWebResponse)e.Response;
        else
          return false;
        }
      catch
        {
        if (response != null)
          response.Close();
        return false;
        }

      return true;
      }

    private bool Request_www_imagetrendelite_com_Get1
      (
      CookieContainer cookie_container,
      out HttpWebResponse response
      )
      {
      response = null;

      try
        {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://vbems.imagetrendelite.com/Elite/Organizationvbems/Agency00404/DynamicListAPIController/GetDynamicListViews?dynamicListViewTypeName=ViewAllEMSCADList");
        NormalizeWithCookie(request, cookie_container);

        request.Accept = "*/*";
        request.Headers.Add("X-Requested-With", @"XMLHttpRequest");
        request.Referer = "https://vbems.imagetrendelite.com/Elite/Organizationvbems/Agency00404/RunForm/CadList?startingFilter=ems";
        request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
        request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");

        response = (HttpWebResponse)request.GetResponse();
        }
      catch (WebException e)
        {
        if (e.Status == WebExceptionStatus.ProtocolError)
          response = (HttpWebResponse)e.Response;
        else
          return false;
        }
      catch
        {
        if (response != null)
          response.Close();
        return false;
        }

      return true;
      }

    private bool Request_www_imagetrendelite_com_Get2
      (
      CookieContainer cookie_container,
      out HttpWebResponse response
      )
      {
      response = null;

      try
        {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://vbems.imagetrendelite.com/Elite/Organizationvbems/Agency00404/DynamicListAPIController/GetDynamicListViewByID?dynamicListViewModelID=910a358f-b03d-489a-bbe0-39d64ebc08cb");
        NormalizeWithCookie(request, cookie_container);

        request.Accept = "*/*";
        request.Headers.Add("X-Requested-With", @"XMLHttpRequest");
        request.Referer = "https://vbems.imagetrendelite.com/Elite/Organizationvbems/Agency00404/RunForm/CadList?startingFilter=ems";
        request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
        request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");

        response = (HttpWebResponse)request.GetResponse();
        }
      catch (WebException e)
        {
        if (e.Status == WebExceptionStatus.ProtocolError)
          response = (HttpWebResponse)e.Response;
        else
          return false;
        }
      catch
        {
        if (response != null)
          response.Close();
        return false;
        }

      return true;
      }

    private bool Request_www_imagetrendelite_com_Load
      (
      CookieContainer cookie_container,
      string request_identifier,
      out HttpWebResponse response
      )
      {
      response = null;

      try
        {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://vbems.imagetrendelite.com/Elite/Organizationvbems/Agency00404/DynamicListAPIController/LoadDynamicListRecords?skip=0&pageSize=200&search=&comparisonType=STARTSWITH&sortColumn=UnitNotifiedByDispatch&sortAscending=false&viewID=910a358f-b03d-489a-bbe0-39d64ebc08cb&includeTotalRecordCount=false&RequestIdentifier=" + request_identifier);
        NormalizeWithCookie(request, cookie_container);

        request.Accept = "*/*";
        request.Headers.Add("X-Requested-With", @"XMLHttpRequest");
        request.Referer = "https://vbems.imagetrendelite.com/Elite/Organizationvbems/Agency00404/RunForm/CadList?startingFilter=ems";
        request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
        request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");

        request.Timeout = int.Parse(ConfigurationManager.AppSettings["Request_www_imagetrendelite_com_Load_timeout_milliseconds"]);

        response = (HttpWebResponse)request.GetResponse();
        }
      catch (WebException e)
        {
        if (e.Status == WebExceptionStatus.ProtocolError)
          {
          response = (HttpWebResponse)e.Response;
          }
        else
          {
          Report.Debug($"TClass_ss_imagetrendelite.Request_www_imagetrendelite_com_Load encountered a WebException: " + e.ToString() + k.NEW_LINE);
          return false;
          }
        }
      catch (Exception e)
        {
        if (response != null)
          response.Close();
        Report.Debug($"TClass_ss_imagetrendelite.Request_www_imagetrendelite_com_Load encountered a non-web Exception: " + e.ToString() + k.NEW_LINE);
        return false;
        }

      return true;
      }

    //--
    //
#pragma warning restore CA1031 // Do not catch general exception types
#pragma warning restore CA2234 // Pass system uri objects instead of strings
    //
    // END code generated initially by Fiddler extension "Request to Code"
    //
    //--

    }
  }
