using CarWashServiceWebApp.Data;
using CarWashServiceWebApp.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace CarWashServiceWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            if (Configuration["GenerateTestData"] == "True")
                GenerateTestData();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }

        private void GenerateTestData()
        {
            GenerateCustomers();
            GenerateServices();

        }

        private void GenerateCustomers()
        {
            string[] firstNames = new[] { "John", "James", "Richard", "George", "Donald" };
            string[] lastNames = new[] { "Black", "Green", "Bing", "Paul", "Brams" };

            Random random1 = new Random(DateTime.Now.Second);
            Random random2 = new Random(DateTime.Now.Minute);

            using (var context = new CarWashServiceContext())
            {
                if (context.Customers.Any()) return;

                for (int i = 0; i < 20; i++)
                    context.Customers.Add(new Customer
                    {
                        FirstName = firstNames[random1.Next(firstNames.Length)],
                        LastName = lastNames[random2.Next(lastNames.Length)]
                    });

                context.SaveChanges();
            }
        }

        private void GenerateServices()
        {
            using (var context = new CarWashServiceContext())
            {
                if (context.Services.Any())
                    return;

                context.Services.Add(new Service { Title = "Hand wash", IsAvailable = true, Duration = new TimeSpan(0, 40, 0) });
                context.Services.Add(new Service { Title = "Wax polish", IsAvailable = true, Duration = new TimeSpan(0, 20, 0) });
                context.Services.Add(new Service { Title = "Wash and Vac", IsAvailable = true, Duration = new TimeSpan(0, 30, 0) });
                context.Services.Add(new Service { Title = "Interior cleaning", IsAvailable = true, Duration = new TimeSpan(0, 35, 0) });
                context.Services.Add(new Service { Title = "Interior and exterior", IsAvailable = true, Duration = new TimeSpan(0, 60, 0) });

                context.SaveChanges();
            }
        }
    }
}
