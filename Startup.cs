﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Blog.Models;

namespace Blog
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
            services.AddMvc();

            services.AddDbContext<BlogContext>(options =>
                options.UseSqlite("Data Source=Blogging.db"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "admin",
                    template: "admin/{controller}/{action}",
                    defaults: new { controller = "Post", action = "Index" });
                
                routes.MapRoute(
                    name: "post",
                    template: "post/{id}",
                    defaults: new { controller = "Home", action = "Post" });

                routes.MapRoute(
                    name: "category",
                    template: "category/{id}",
                    defaults: new { controller = "Home", action = "Category" });

                routes.MapRoute(
                    name: "tag",
                    template: "tag/{id}",
                    defaults: new { controller = "Home", action = "Tag" });    
  
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
