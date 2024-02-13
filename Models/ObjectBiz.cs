using ki.mvc;
using System;

namespace OscalertConsoleApp.Models
  {
  public abstract class ObjectBiz
    {

    public ObjectBiz() // CONSTRUCTOR
      {
      Report = new()
        {
        Debug = ReportDebug,
        Warning = ReportWarning,
        Error = ReportError,
        Failure = ReportFailure
        };
      }

    protected bool BeQuitCommanded = false;
    //
    public class EventArgs
      {
      public string content = string.Empty;
      public EventArgs() {}
      public EventArgs(string content) { this.content = content; }
      }
    //
    public event EventHandler<EventArgs> OnProgress, OnCompletion;
    public event EventHandler<string> OnDebug, OnWarning, OnError, OnFailure;
    //
    protected virtual void ReportProgress(EventArgs e) => OnProgress?.Invoke(this,e);
    protected virtual void ReportCompletion(EventArgs e) => OnCompletion?.Invoke(this,e);
    protected virtual void ReportDebug(string text) => OnDebug?.Invoke(this,text);
    protected virtual void ReportWarning(string text) => OnWarning?.Invoke(this,text);
    protected virtual void ReportError(string text) => OnError?.Invoke(this,text);
    protected virtual void ReportFailure(string text) => OnFailure?.Invoke(this,text);

    protected ReportClass Report;

    public void Quit(object sender, System.EventArgs e)
      {
      BeQuitCommanded = true;
      }

    }
  }