# XT.WinService

# 1、介绍
windows服务程序集，topshelf封装库。


# 2、使用方式
```
// 添加Log4net日志
ShareService.ConfigureLog4net();

builder.Services.AddSingleton(new AppSettings(builder.Environment.IsDevelopment()));
// 添加windows服务模块
ShareService.AddWinService(builder.Services);

// window服务注册，必须实现ILogService
builder.Services.AddSingleton<IWindowService, ServerWinService>();
// 启动
ShareService.RunWinService(app.Services, new ServiceInfo
{
    Description = "IotServer服务",
    DisplayName = "IotServer-"+ext,
    Name = "IotServer-"+ext
});
```

