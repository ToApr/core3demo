using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Service;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using System.Reflection;
using Autofac.Extras.DynamicProxy;

namespace AspectCoreDemo
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
            
            services.AddTransient<IStudentService, StudentService>();
            services.AddControllersWithViews(optinons=> {
               // optinons.Filters.Add(typeof(CustomInterceptorAttribute));
            });
          
        }
        //Auto fac ×¢Èë
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<CustomInterceptorAttribute>();  //×¢²áÀ¹½ØÆ÷
            builder.RegisterAssemblyTypes(Assembly.Load("Service"))
             .AsImplementedInterfaces()
             .EnableInterfaceInterceptors();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
              .AsImplementedInterfaces()
              .EnableInterfaceInterceptors();
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
            }
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
