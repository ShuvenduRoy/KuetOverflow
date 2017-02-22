using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace KuetOverflow_ASP.NET
{
    public class Startup
    {
        private readonly IConfigurationRoot _configuration;

        public Startup(IHostingEnvironment env)
        {
            _configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .AddJsonFile(env.ContentRootPath + "/config.json")
            .Build();

        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDbContext<KuetDataContext>(options =>
            {
                var connectinoString = _configuration.GetConnectionString("KuetDataContext");
                options.UseSqlServer(connectinoString);
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            app.UseExceptionHandler("/pages/error.html");


            if (_configuration.GetValue<bool>("EnableDeveloperExceptions"))
            {
                app.UseDeveloperExceptionPage();
            }
            app.Use(async (context, next) =>
            {
                if (context.Request.Path.Value.Contains("invalid"))
                    throw new Exception("Error!");
                await next();
            });

            app.UseMvc(routers =>
            {
                routers.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
            });


            app.UseFileServer();
        }
    }
}
