using OscalertSvc.Data;
using System.Collections.Specialized;
using System.Threading;

namespace OscalertSvc.Models
  {
  public class ClassOneBiz : ObjectBiz
    {

    private readonly IClassOneDb classOneDb = null;
    private readonly NameValueCollection appSettings = null;

    public ClassOneBiz // CONSTRUCTOR
      (
      IClassOneDb classOneDb_imp,
      NameValueCollection appSettings_imp
      )
      {
      classOneDb = classOneDb_imp;
      appSettings = appSettings_imp;
      }

    public void Process
      (
      string parameterOne,
      string parameterTwo
      )
      {
      // --
      //
      // Perform this class of processing, monitoring for a commanded quit, and making reports as necessary.
      //
      // --
      var done = false;
      while (!BeQuitCommanded && !done)
        {
        ReportProgress(e:new("."));
        Thread.Sleep(millisecondsTimeout:1000);
        }
      //
      if (BeQuitCommanded)
        {
        ReportWarning(text:"Process interrupted.");
        }
      else
        {
        ReportCompletion(e:new("Process complete."));
        }
      }

    }
  }
