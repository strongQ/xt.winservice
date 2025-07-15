
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using Topshelf.HostConfigurators;
using Topshelf.ServiceConfigurators;
using XT.Common.Models.WinService;
using XT.Common.Services;

namespace XT.WinService
{
    public class StartupService
    {
        

        private readonly ILogService _logService;

        private readonly IWindowService _windowService;

        public StartupService(ILogService logService, IWindowService windowService)
        {
            _logService = logService;
            _windowService = windowService;
        }

        public void Start(ServiceInfo serviceInfo)
        {
            ServiceInfo serviceInfo2 = serviceInfo;
            if (_windowService == null)
            {
                throw new Exception("请先注入IWindowService或者TimeService的实现!");
            }

          
            TopshelfExitCode topshelfExitCode = HostFactory.Run(delegate (HostConfigurator config)
            {
                config.SetServiceName(serviceInfo2.Name);
                config.SetDisplayName(serviceInfo2.DisplayName);
                config.SetDescription(serviceInfo2.Description);
                config.Service(delegate (ServiceConfigurator<IWindowService> s)
                {
                    s.ConstructUsing(() => _windowService);
                    s.WhenStarted<IWindowService>(async delegate (IWindowService x)
                    {
                        _logService.Log("Service " + serviceInfo2.Name + " started.");
                        await x.Start();
                    });
                    s.WhenStopped<IWindowService>(async delegate (IWindowService x)
                    {
                        _logService.Log("Service " + serviceInfo2.Name + " stopped.");
                        await x.Stop();
                    });
                });
                config.RunAsLocalSystem();
                config.StartAutomatically();
            });
            int num2 = (Environment.ExitCode = (int)Convert.ChangeType(topshelfExitCode, topshelfExitCode.GetTypeCode()));
        }
    }
}
