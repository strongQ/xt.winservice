
using log4net.Config;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XT.Common.Models.WinService;

namespace XT.WinService
{

    public static class ShareService
    {
        /// <summary>
        /// 添加Windows服务注入
        /// </summary>
        /// <param name="services"></param>
        public static void AddWinService(this IServiceCollection services)
        {
            services.AddSingleton<StartupService>();
        }
        /// <summary>
        /// 启动WinServer
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="server"></param>
        public static void RunWinService(this IServiceProvider provider, ServiceInfo server)
        {
           var startup= provider.GetService<StartupService>();
           
             startup?.Start(server);
                
               
          

        }

       
        /// <summary>
        /// 配置Log4日志
        /// </summary>
        public static void ConfigureLog4net()
        {
            string location = Assembly.GetExecutingAssembly().Location;
            string directoryName = Path.GetDirectoryName(location);
            string fileName = directoryName + "\\log4net.config";
            XmlConfigurator.ConfigureAndWatch(new FileInfo(fileName));
        }
    }
}
