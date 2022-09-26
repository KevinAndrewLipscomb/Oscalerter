using System;

namespace OscalertSvc.Models
  {
  public abstract class ObjectBiz
    {

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

    public void Quit(object sender, System.EventArgs e)
      {
      BeQuitCommanded = true;
      }

    }
  }