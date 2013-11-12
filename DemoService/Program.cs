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
            BasicServiceStarter.Run<MyService>(Name);
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
}
