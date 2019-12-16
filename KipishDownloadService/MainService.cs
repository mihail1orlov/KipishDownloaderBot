using System.Collections.Generic;
using System.ServiceProcess;
using ServiceCommon;

namespace KipishDownloadService
{
    internal class MainService : ServiceBase
    {
        private readonly IEnumerable<IService> _service;

        public MainService(IEnumerable<IService> service)
        {
            _service = service;
        }

        public void StartSvc()
        {
            foreach (var service in _service)
            {
                service.Start();
            }
        }

        public void StopSvc()
        {
            foreach (var service in _service)
            {
                service.Stop();
            }
        }

        protected override void OnStart(string[] args)
        {
            StartSvc();
            base.OnStart(args);
        }

        protected override void OnStop()
        {
            StopSvc();
            base.OnStop();
        }
    }
}