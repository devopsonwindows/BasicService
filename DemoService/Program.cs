using BasicServiceFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DemoService
{
    class Program
    {
        public const string Name = "DemoService";

        static void Main(string[] args)
        {
            if (Environment.UserInteractive)
            {
                using (var service = new MyService())
                {
                    service.Start();
                    Console.WriteLine("Running {0}, press any key to stop", Name);
                    Console.ReadKey();
                }
            }
            else
            {
                ServiceBase.Run(new BasicService(new MyService(), Name));
            }
        }
    }

    class MyService : IService
    {
        private bool _stopped;
        
        public void Start()
        {
            ThreadPool.QueueUserWorkItem(
                o =>
                {
                    while (!_stopped)
                    {
                        Console.WriteLine("Still here!");
                        Thread.Sleep(1000);
                    }
                });
        }

        public void Dispose()
        {
            _stopped = true;
        }
    }

    [RunInstaller(true)]
    public class MyInstaller : Installer
    {
        public MyInstaller()
        {
            Installers.Add(new ServiceProcessInstaller
                {
                    Account = ServiceAccount.LocalSystem
                });
            Installers.Add(new ServiceInstaller
                {
                    ServiceName = Program.Name,
                    DisplayName = Program.Name,
                    Description = "This is a demo service"
                });
        }
    }
}
