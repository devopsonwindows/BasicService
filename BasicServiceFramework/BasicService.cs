using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace BasicServiceFramework
{
    public interface IService : IDisposable
    {
        void Start();
    }

    public class BasicService : ServiceBase
    {
        private readonly IService _service;

        public BasicService(IService service, string name)
        {
            _service = service;
            ServiceName = name;
        }

        protected override void OnStart(string[] args)
        {
            _service.Start();
        }

        protected override void OnStop()
        {
            _service.Dispose();
        }
    }
}
