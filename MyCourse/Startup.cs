﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace MyCourse
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2); // indica la versione da utilizzare
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // if (env.IsDevelopment())
            if (env.IsEnvironment("Development")) {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            /*
                Middleware di routing
            */
            // app.UseMvcWithDefaultRoute();
            app.UseMvc(routeBuilder => 
            {
                // /courses/detail/5
                routeBuilder.MapRoute("default", "{Controller=Home}/{action=index}/{id?}");
            });

            /*
            app.Run(async (context) =>
            {
                string nome = context.Request.Query["nome"];
                await context.Response.WriteAsync($"Hello {nome.ToUpper()}!");
            });
            */
        }
    }
}
