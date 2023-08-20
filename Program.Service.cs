using System.ServiceProcess;
using System.Threading.Tasks;

namespace OscalertSvc
  {
  partial class Program
    {

    public const string ServiceName = "OscalertSvc";

    public class Service : ServiceBase
      {

      public Service() // CONSTRUCTOR
        {
        ServiceName = Program.ServiceName;
        }

      protected override void OnStart(string[] args) => Task.Run(() => Work(args));

      protected override void OnStop() => Stop();

      }

    }
  }
