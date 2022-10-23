using OscalertSvc.Models;
using OscalertSvc.Views;
using System;
using System.ServiceProcess;

namespace OscalertSvc
  {
  /// <summary>
  /// Performs such-and-such.  Can be invoked as a console application or as a Windows Service.
  /// </summary>
  partial class Program
    {

    static readonly private Biz biz = new();
      // COMPOSITION ROOT; exposes elements of the MODEL

    static private ClassOneInteraction classOneInteraction;

    /// <summary>
    /// Serves as the CONTROLLER
    /// </summary>
    /// <param name="args">Command line arguments</param>
    static void Main(string[] args)
      {
      if (Environment.UserInteractive)
        {
        //
        // running as console app
        //
        classOneInteraction = new ClassOneInteraction();
        classOneInteraction.OnQuitCommanded += biz.cad_activity_notification_agent.Quit;
          // An Interaction acts as a VIEW.  If any parameters are needed in addition to the command line args, the Interaction's
          // constructor prompts the user for, and returns, such parameters.  An Interaction used by the controller inside a loop
          // must also expose BeQuitCommanded.

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
      //--
      //
      // Wire up the view to observe the model and execute the business processing.
      //
      //--
      biz.cad_activity_notification_agent.OnProgress += classOneInteraction.ShowProgress;
      biz.cad_activity_notification_agent.OnCompletion += classOneInteraction.ShowCompletion;
      biz.cad_activity_notification_agent.OnDebug += classOneInteraction.ShowDebug;
      biz.cad_activity_notification_agent.OnWarning += classOneInteraction.ShowWarning;
      biz.cad_activity_notification_agent.OnError += classOneInteraction.ShowError;
      biz.cad_activity_notification_agent.OnFailure += classOneInteraction.ShowFailure;
      //
      biz.cad_activity_notification_agent.Work();
      }

    static private void Stop()
      {
      // onstop code here
      }

    }
  }
