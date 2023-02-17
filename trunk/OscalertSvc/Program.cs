using OscalertSvc.Models;
using OscalertSvc.Views;
using System;
using System.ServiceProcess;

namespace OscalertSvc
  {
  /// <summary>
  /// Provides automatic near-realtime cellphone notifications about certain VBRescue field situations.  Can be invoked as a console
  /// application or as a Windows Service.
  /// </summary>
  partial class Program
    {

    static readonly private Biz biz = new();
      // COMPOSITION ROOT; exposes elements of the MODEL

    static private ClassOneInteraction classOneInteraction;

    /// <summary>
    /// Serves as the CONTROLLER
    /// </summary>
    /// <param name="args">None</param>
    static void Main(string[] args)
      {
      classOneInteraction = new ClassOneInteraction();
        // An Interaction acts as a VIEW.

      if (Environment.UserInteractive)
        {
        //
        // running as console app
        //
        classOneInteraction.OnQuitCommanded += biz.cad_activity_notification_agent.Quit;
          // An Interaction used by the controller inside a loop must also expose BeQuitCommanded.  If any parameters are needed in
          // addition to the command line args, the Interaction's constructor prompts the user for, and returns, such parameters.

        Work(args);
          // This blocks until the biz layer (the model) is complete.  The model observes the interaction (the view), which offers
          // the user a way to command a quit, so the model may complete on its own or quit at the behest of the user.

        Stop();
        }
      else
        {
        //
        // running as service
        //
        using var service = new Service();
        ServiceBase.Run(service);
        }

      }

    static private void Work(string[] args)
      {
      //
      // Wire up the view to observe each public ObjectBiz field/class in the model.
      //
      foreach(var field_info in typeof(Biz).GetFields())
        {
        if (field_info.FieldType.IsSubclassOf(typeof(ObjectBiz)))
          {
          (field_info.GetValue(biz) as ObjectBiz).OnProgress += classOneInteraction.ShowProgress;
          (field_info.GetValue(biz) as ObjectBiz).OnCompletion += classOneInteraction.ShowCompletion;
          (field_info.GetValue(biz) as ObjectBiz).OnDebug += classOneInteraction.ShowDebug;
          (field_info.GetValue(biz) as ObjectBiz).OnWarning += classOneInteraction.ShowWarning;
          (field_info.GetValue(biz) as ObjectBiz).OnError += classOneInteraction.ShowError;
          (field_info.GetValue(biz) as ObjectBiz).OnFailure += classOneInteraction.ShowFailure;
          }
        }
      //
      // Execute the business processing.
      //
      biz.cad_activity_notification_agent.Work(biz);
      }

    static private void Stop()
      {
      // onstop code here
      }

    }
  }
