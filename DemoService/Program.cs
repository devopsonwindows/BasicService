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
        static void Main(string[] args)
        {
            BasicServiceStarter.Run<MyService>("DemoService");
        }
    }

    class MyService : IDisposable
    {
        public void Start()
        {
        }

        public void Dispose()
        {
        }
    }
}
