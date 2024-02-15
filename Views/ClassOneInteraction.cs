using kix;
using log4net;
using log4net.Config;
using Oscalerter.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;

namespace Oscalerter.Views
{
  class ClassOneInteraction
    {
    // If any parameters are needed in addition to the command line args, provide a Get() method that prompts the user
    // for, and returns, such parameters.  If used by the controller inside a loop, expose BeQuitRequested.

    static readonly private ILog log = LogManager.GetLogger(typeof(ClassOneInteraction));

    private readonly bool BeUsingProgressWriteLines = false;
    private readonly List<ConsoleKey> quitKeyList = new()
      {
      ConsoleKey.Enter,
      ConsoleKey.Escape,
      ConsoleKey.Q,
      ConsoleKey.Spacebar
      };

    public event EventHandler OnQuitCommanded;
    protected virtual void ReportQuitCommanded() => OnQuitCommanded?.Invoke(this,null);

    public ClassOneInteraction() // CONSTRUCTOR
      {
      XmlConfigurator.Configure(); // reads log4net configuration
      var message = "To quit, press any of ";
      foreach (var quitKey in quitKeyList)
        {
        message += $"{quitKey}{k.SPACE}|{k.SPACE}";
        }
      AnsiConsole.WriteLine(message.TrimEnd(new char[] {'|',k.SPACE[0]}));
      //
      if (BeQuitKeyPressed()) ReportQuitCommanded();
      }

    private bool BeQuitKeyPressed()
      {
      return !Console.IsInputRedirected && Console.KeyAvailable && quitKeyList.Contains(Console.ReadKey(intercept:true).Key);
      }

    public void ShowEntityOne
      (
      string entityOne // trivial
      //
      // or complex
      //
      //string entityOne.AttributeFirst,
      //string entityOne.AttributeSecond
      )
      {
      AnsiConsole.WriteLine($"entityOne = {entityOne}");
      if (BeQuitKeyPressed()) ReportQuitCommanded();
      }

    public void ShowProgress
      (
      object source,
      ClassOneBiz.EventArgs e
      )
      {
      AnsiConsole.Write($"{e.content}");
      if (BeQuitKeyPressed()) ReportQuitCommanded();
      }

    public void ShowCompletion
      (
      object source,
      ClassOneBiz.EventArgs e
      )
      {
      AnsiConsole.WriteLine($"{e.content}");
      AnsiConsole.WriteLine("Program done.");
      }

    public void ShowFailure
      (
      object source,
      string text
      )
      {
      if (!BeUsingProgressWriteLines) Console.WriteLine();
      log.Fatal($"{source}: {text}");
      log.Fatal("Program done.");
      }

    private void ShowExtraNormal
      (
      Action<object> logAction,
      object source,
      string text
      )
      {
      if(
          !BeUsingProgressWriteLines
        &&
          (
            (logAction == log.Debug && log.IsDebugEnabled)
          ||
            (logAction == log.Warn && log.IsWarnEnabled)
          ||
            (logAction == log.Error && log.IsErrorEnabled)
          )
        )
        {
        Console.WriteLine();
        }
      logAction($"{source}: {text}");
      if (BeQuitKeyPressed())
        {
        ReportQuitCommanded();
        }
      }

    public void ShowDebug(object source, string text) => ShowExtraNormal(log.Debug, source, text);
    public void ShowWarning(object source, string text) => ShowExtraNormal(log.Warn, source, text);
    public void ShowError(object source, string text) => ShowExtraNormal(log.Error, source, text);

    }
  }
