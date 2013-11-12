using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace BasicServiceFramework
{
    class BasicService<T> : ServiceBase where T : IDisposable, new()
    {
        private IDisposable _service;

        protected override void OnStart(string[] args)
        {
            try
            {
                _service = new T();
            }
            catch
            {
                ExitCode = 1064;
                throw;
            }
        }

        protected override void OnStop()
        {
            _service.Dispose();
        }
    }
}
