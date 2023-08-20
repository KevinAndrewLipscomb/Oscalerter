using Class_ss;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Web;

namespace Class_ss_broadcastify
  {
  public class TClass_ss_broadcastify : TClass_ss
    {

    private static class Static
      {
      public static string USER_AGENT_DESIGNATOR = ConfigurationManager.AppSettings["ss_user_agent_designator"];
      }

    public TClass_ss_broadcastify() : base()
      {      
      }

    //--
    //
    // BEGIN code generated initially by Fiddler extension "Request to Code"
    //
    #pragma warning disable CA1031 // Do not catch general exception types
    #pragma warning disable CA2234 // Pass system uri objects instead of strings
    //
    //--

    private bool Request_www_broadcastify_com_Login
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
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.broadcastify.com/login/");
        request.CookieContainer = cookie_container;
        request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
        request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

		    request.UserAgent = Static.USER_AGENT_DESIGNATOR;
		    request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US,en;q=0.5");
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
		    request.Referer = "https://www.broadcastify.com/login/";
		    request.ContentType = "application/x-www-form-urlencoded";
		    request.Headers.Add("Origin", @"https://www.broadcastify.com");
		    request.Headers.Add("DNT", @"1");
		    request.KeepAlive = true;
		    //request.Headers.Set(HttpRequestHeader.Cookie, @"_awl=2.1685219475.5-b9454e35fe76ad6f7616424baef91546-6763652d75732d6561737431-3; _admrla=2.2-812d9005777c6164-9b412013-1288-11ec-8d40-de534a2245bc");
		    request.Headers.Add("Upgrade-Insecure-Requests", @"1");
		    request.Headers.Add("Sec-Fetch-Dest", @"document");
		    request.Headers.Add("Sec-Fetch-Mode", @"navigate");
		    request.Headers.Add("Sec-Fetch-Site", @"same-origin");
		    request.Headers.Add("Sec-Fetch-User", @"?1");
		    request.Headers.Add("Sec-GPC", @"1");

		    request.Method = "POST";
		    request.ServicePoint.Expect100Continue = false;

		    string body = @"username=" + HttpUtility.UrlEncode(username) + "&password=" + HttpUtility.UrlEncode(password) + "&action=auth&redirect=https%3A%2F%2Fwww.broadcastify.com";
		    byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(body);
		    request.ContentLength = postBytes.Length;
		    Stream stream = request.GetRequestStream();
		    stream.Write(postBytes, 0, postBytes.Length);
		    stream.Close();

		    response = (HttpWebResponse)request.GetResponse();
	      }
	    catch (WebException e)
	      {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return false;
	      }
	    catch
	      {
		    if(response != null) response.Close();
		    return false;
	      }

	    return true;
      }

    private bool Request_www_broadcastify_com_MyBCFY
      (
      CookieContainer cookie_container,
      out HttpWebResponse response
      )
      {
	    response = null;

	    try
  	    {
    		HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.broadcastify.com/MyBCFY/");
        request.CookieContainer = cookie_container;
        request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
        request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

		    request.UserAgent = Static.USER_AGENT_DESIGNATOR;
		    request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US,en;q=0.5");
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
		    request.Referer = "https://www.broadcastify.com/";
		    request.Headers.Add("DNT", @"1");
		    request.KeepAlive = true;
		    //request.Headers.Set(HttpRequestHeader.Cookie, @"_awl=2.1685219475.5-b9454e35fe76ad6f7616424baef91546-6763652d75732d6561737431-3; _admrla=2.2-812d9005777c6164-9b412013-1288-11ec-8d40-de534a2245bc; bcfyuser1=ODEwMDA6S2V2aW5BbmRyZXdMaXBzY29tYjokMnkkMTAkbTdFd05WckNpTDlTU2dOcjZnTnhndVRld1RjVUVtb2diTlByVWdtVU9MZjRUZy9Cd0ZtMlM%3D");
		    request.Headers.Add("Upgrade-Insecure-Requests", @"1");
		    request.Headers.Add("Sec-Fetch-Dest", @"document");
		    request.Headers.Add("Sec-Fetch-Mode", @"navigate");
		    request.Headers.Add("Sec-Fetch-Site", @"same-origin");
		    request.Headers.Add("Sec-Fetch-User", @"?1");
		    request.Headers.Add("Sec-GPC", @"1");

		    response = (HttpWebResponse)request.GetResponse();
	      }
	    catch (WebException e)
	      {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return false;
	      }
	    catch
	      {
		    if(response != null) response.Close();
		    return false;
	      }

	    return true;
      }

    private bool Request_www_broadcastify_com_ManageFeed14744
      (
      CookieContainer cookie_container,
      out HttpWebResponse response
      )
      {
	    response = null;

	    try
  	    {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.broadcastify.com/manage/feed/14744");

		    request.UserAgent = Static.USER_AGENT_DESIGNATOR;
		    request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US,en;q=0.5");
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
		    request.Referer = "https://www.broadcastify.com/MyBCFY/";
		    request.Headers.Add("DNT", @"1");
		    request.KeepAlive = true;
		    //request.Headers.Set(HttpRequestHeader.Cookie, @"xyzzy=test; xyzzy=test; _awl=2.1685219475.5-b9454e35fe76ad6f7616424baef91546-6763652d75732d6561737431-3; _admrla=2.2-812d9005777c6164-9b412013-1288-11ec-8d40-de534a2245bc; bcfyuser1=ODEwMDA6S2V2aW5BbmRyZXdMaXBzY29tYjokMnkkMTAkbTdFd05WckNpTDlTU2dOcjZnTnhndVRld1RjVUVtb2diTlByVWdtVU9MZjRUZy9Cd0ZtMlM%3D");
		    request.Headers.Add("Upgrade-Insecure-Requests", @"1");
		    request.Headers.Add("Sec-Fetch-Dest", @"document");
		    request.Headers.Add("Sec-Fetch-Mode", @"navigate");
		    request.Headers.Add("Sec-Fetch-Site", @"same-origin");
		    request.Headers.Add("Sec-Fetch-User", @"?1");
		    request.Headers.Add("Sec-GPC", @"1");

		    response = (HttpWebResponse)request.GetResponse();
	      }
	    catch (WebException e)
	      {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return false;
	      }
	    catch
	      {
		    if(response != null) response.Close();
		    return false;
	      }

	    return true;
      }

    private bool Request_www_broadcastify_com_ManageAjaxphp_TabAlertsFeedid14744
      (
      CookieContainer cookie_container,
      out HttpWebResponse response
      )
      {
	    response = null;

	    try
  	    {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.broadcastify.com/manage/ajax.php?tab=alerts&feedId=14744");

		    request.UserAgent = Static.USER_AGENT_DESIGNATOR;
		    request.Accept = "*/*";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US,en;q=0.5");
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
		    request.Referer = "https://www.broadcastify.com/manage/feed/14744";
		    request.Headers.Add("X-Requested-With", @"XMLHttpRequest");
		    request.Headers.Add("DNT", @"1");
		    request.KeepAlive = true;
		    //request.Headers.Set(HttpRequestHeader.Cookie, @"xyzzy=test; _awl=2.1685219475.5-b9454e35fe76ad6f7616424baef91546-6763652d75732d6561737431-3; _admrla=2.2-812d9005777c6164-9b412013-1288-11ec-8d40-de534a2245bc; bcfyuser1=ODEwMDA6S2V2aW5BbmRyZXdMaXBzY29tYjokMnkkMTAkbTdFd05WckNpTDlTU2dOcjZnTnhndVRld1RjVUVtb2diTlByVWdtVU9MZjRUZy9Cd0ZtMlM%3D");
		    request.Headers.Add("Sec-Fetch-Dest", @"empty");
		    request.Headers.Add("Sec-Fetch-Mode", @"cors");
		    request.Headers.Add("Sec-Fetch-Site", @"same-origin");
		    request.Headers.Add("Sec-GPC", @"1");

		    response = (HttpWebResponse)request.GetResponse();
	      }
	    catch (WebException e)
	      {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return false;
	      }
	    catch
	      {
		    if(response != null) response.Close();
		    return false;
	      }

	    return true;
      }

    private bool Request_www_broadcastify_com_ManageFeed_Alert_Doupdatealerts
      (
      CookieContainer cookie_container,
      string alert,
      bool be_fire,
      out HttpWebResponse response
      )
      {
	    response = null;

	    try
	      {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.broadcastify.com/manage/");
        request.CookieContainer = cookie_container;
        request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
        request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

		    request.Accept = "text/html, application/xhtml+xml, */*";
        request.Referer = "http://www.broadcastify.com/manage/feed/14744";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.UserAgent = Static.USER_AGENT_DESIGNATOR;
		    request.ContentType = "application/x-www-form-urlencoded";
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
		    request.Headers.Add("DNT", "1");
		    request.Headers.Set(HttpRequestHeader.Pragma, "no-cache");
		    //request.Headers.Set(HttpRequestHeader.Cookie, @"__utma=245121750.887757798.1413752601.1428276293.1428451907.139; __utmz=245121750.1418498841.34.2.utmcsr=radioreference.com|utmccn=(referral)|utmcmd=referral|utmcct=/; __qca=P0-248757097-1413752601012; __utmc=245121750; bcfyuser1=ODEwMDA6a2V2aW5hbmRyZXdsaXBzY29tYjo0ZTdhMmM5MzI0YTE1NDJmNTIxZmIzYmVlMGQwZTkxMw%3D%3D");

		    request.Method = "POST";

		    string postString = @"incCat=3&incType=" + (be_fire ? "13" : "18") + "&alert=" + HttpUtility.UrlEncode(alert) + "&confirmRules=on&action=doUpdateAlerts&feedId=14744";
          //
          // incType values
          //   12 = 1-alarm fire
          //   13 = multi-alarm fire
          //   14 = building collapse
          //   15 = firefighter down
          //   16 = hazmat incident
          //   17 = major brush / wildland fire
          //   18 = MCI
          //   19 = SAR
          //   20 = technical rescue
          //
          // alert maxlength = 225
          //
		    byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(postString);
		    request.ContentLength = postBytes.Length;
		    Stream stream = request.GetRequestStream();
		    stream.Write(postBytes, 0, postBytes.Length);
		    stream.Close();

		    response = (HttpWebResponse)request.GetResponse();
	      }
	    catch (WebException e)
	      {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return false;
	      }
	    catch
	      {
		    if(response != null) response.Close();
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

    private void Login
      (
      CookieContainer cookie_container
      )
      {
      HttpWebResponse response;
      if(!Request_www_broadcastify_com_Login
          (
          cookie_container:cookie_container,
          username:ConfigurationManager.AppSettings["broadcastify_username"],
          password:ConfigurationManager.AppSettings["broadcastify_password"],
          response:out response
          )
        )
        {
        throw new Exception("Request_www_broadcastify_com() returned FALSE.");
        }
      response.Close();
      }

    internal void AddAlert
      (
      string alert,
      bool be_fire = false
      )
      {
      var cookie_container = new CookieContainer();
      //
      Login(cookie_container:cookie_container);
      //
      HttpWebResponse response;
      if(!Request_www_broadcastify_com_MyBCFY
          (
          cookie_container:cookie_container,
          response:out response
          )
        )
      //then
        {
        throw new Exception("Request_www_broadcastify_com_MyBCFY() returned FALSE.");
        }
      response.Close();
      if(!Request_www_broadcastify_com_ManageFeed14744
          (
          cookie_container:cookie_container,
          response:out response
          )
        )
      //then
        {
        throw new Exception("Request_www_broadcastify_com_ManageFeed14744() returned FALSE.");
        }
      response.Close();
      if(!Request_www_broadcastify_com_ManageAjaxphp_TabAlertsFeedid14744
          (
          cookie_container:cookie_container,
          response:out response
          )
        )
      //then
        {
        throw new Exception("Request_www_broadcastify_com_ManageAjaxphp_TabAlertsFeedid14744() returned FALSE.");
        }
      response.Close();
      if(!Request_www_broadcastify_com_ManageFeed_Alert_Doupdatealerts
          (
          cookie_container:cookie_container,
          alert:alert,
          be_fire:be_fire,
          response:out response
          )
        )
      //then
        {
        throw new Exception("Request_www_broadcastify_com_Manage_Alert_Doupdatealerts() returned FALSE.");
        }
      }

    }
  }
