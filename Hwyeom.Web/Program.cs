using Hwyeom.Services.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hwyeom.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostBuilder = CreateHostBuilder(args).Build();
            using(var scope = hostBuilder.Services.CreateScope())
            {
                //등록된 서비스 중 DBInit 먼저 실행
                var dbInit = scope.ServiceProvider.GetService<MariaDbInitializer>();
                dbInit.PlantSeedData();
            }
            hostBuilder.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(serverOptions =>
                    {
                        //속성을 설정하고 옵션에 메서드를 호출합니다.
                    })
                    .UseStartup<Startup>();
                });
    }
}
