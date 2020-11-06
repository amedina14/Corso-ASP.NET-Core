using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MyCourse.Models.Infrastructure;
using MyCourse.Models.Services.Application;
using MyCourse.Models.Services.Infrastructure;

namespace MyCourse
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(); // .SetCompatibilityVersion(CompatibilityVersion.Version_2_2); using Microsoft.AspNetCore.Mvc; //

            // Quando si registra un servizio si indicano interfaccia e implementazione concreta (servizio).
            // services.AddTransient<ICourseService, AdoNetCourseService>(); // Sostituiamo CourseService
            services.AddTransient<ICourseService, EfCoreCourseService>(); // Sostituiamo CourseService
            services.AddTransient<IDatabaseAccessor, SqliteDatabaseAccessor>();

            // services.AddScoped<MyCourseDbContext>();
            services.AddDbContext<MyCourseDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
        {
            if (env.IsEnvironment("Development"))
            {
                app.UseDeveloperExceptionPage();

                // Browser sync con C#
                lifetime.ApplicationStarted.Register(() =>
                {
                    // unisce il path dell'app + il file.
                    string filePath = Path.Combine(env.ContentRootPath, "bin/reload.txt");
                    // inserisce la data aggiornata nel file.
                    File.WriteAllText(filePath, DateTime.Now.ToString());
                });
            }

            app.UseStaticFiles();

            app.UseMvc(routeBuilder =>
            {
                routeBuilder.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
