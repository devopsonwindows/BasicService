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
