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

    public class BasicService<T> : ServiceBase where T : IService, new()
    {
        private IService _service;

        protected override void OnStart(string[] args)
        {
            _service = new T();
            _service.Start();
        }

        protected override void OnStop()
        {
            _service.Dispose();
        }
    }
}
