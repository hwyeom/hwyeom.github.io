using Hwyeom.Services.Database;
using Hwyeom.Services.Interfaces;
using Hwyeom.Services.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hwyeom.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region 의존성 주입
            //의존성 주입
            services.AddScoped<MariaDbInitializer>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            #endregion

            #region DBConnection

            /* DB Context 등록작업 */
            //Maria DB 패키지 = Pomelo.EntityFrameworkCore.MySql
            var serverVersion = new MySqlServerVersion(new Version(10, 11, 0));
            var connectionString = Configuration.GetConnectionString(name: "MariaDbConnectionString");
            /*services.AddDbContext<MariaDbContext>(
                dbContextOptions => dbContextOptions
                    .UseMySql(connectionString, serverVersion)
                    // The following three options help with debugging, but should
                    // be changed or removed for production.
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
            );*/
            services.AddDbContext<MariaDbContext>(options =>
                options.UseMySql(connectionString, serverVersion));

            #endregion

            #region Kestrel
            
            services.Configure<KestrelServerOptions>(Configuration.GetSection("Kestrel"));

            #endregion


            //HttpContext에 접근할수 있도록 함
            services.AddHttpContextAccessor();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            //WEBSOKETS
            app.UseWebSockets();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
