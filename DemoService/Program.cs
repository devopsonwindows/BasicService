using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace DemoService
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceBase.Run(new MyService());
        }
    }

    class MyService : ServiceBase
    {
        public const string Name = "DemoService";
        public MyService()
        {
            ServiceName = Name;
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
                    ServiceName = MyService.Name,
                    DisplayName = MyService.Name,
                    Description = "This is a demo service"
                });
        }
    }
}
