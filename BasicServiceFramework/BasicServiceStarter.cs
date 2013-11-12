using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace BasicServiceFramework
{
    public static class BasicServiceStarter
    {
        public static void Run<T>(string serviceName) where T : IDisposable, new()
        {
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
                {
                    if (EventLog.SourceExists(serviceName))
                    {
                        EventLog.WriteEntry(serviceName,
                            "Fatal Exception : " + Environment.NewLine +
                            e.ExceptionObject, EventLogEntryType.Error);
                    }
                };

            if (Environment.UserInteractive)
            {
                var cmd =
                    (Environment.GetCommandLineArgs().Skip(1).FirstOrDefault() ?? "")
                    .ToLower();
                switch (cmd)
                {
                    case "i":
                    case "install":
                        Console.WriteLine("Installing {0}", serviceName);
                        BasicServiceInstaller.Install(serviceName);
                        break;
                    case "u":
                    case "uninstall":
                        Console.WriteLine("Uninstalling {0}", serviceName);
                        BasicServiceInstaller.Uninstall(serviceName);
                        break;
                    default:
                        using (var service = new T())
                        {
                            Console.WriteLine(
                                "Running {0}, press any key to stop", serviceName);
                            Console.ReadKey();
                        }
                        break;
                }
            }
            else
            {
                ServiceBase.Run(new BasicService<T> { ServiceName = serviceName });
            }
        }
    }
}
