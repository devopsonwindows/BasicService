using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace BasicServiceFramework
{
    public static class BasicServiceStarter
    {
        public static void Run<T>(string serviceName) where T : IService, new()
        {
            if (Environment.UserInteractive)
            {
                using (var service= new T())
                {
                    service.Start();
                    Console.WriteLine("Running {0}, press any key to stop", serviceName);
                    Console.ReadKey();
                }
            }
            else
            {
                ServiceBase.Run(new BasicService<T> { ServiceName = serviceName });
            }
        }
    }
}
