using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarWashService.Reports.WinApp
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var host = Host.CreateDefaultBuilder()
             .ConfigureAppConfiguration((context, builder) =>
             {                 
                 builder.AddJsonFile("appsettings.json", optional: true);
             })
             .ConfigureServices((context, services) =>
             {
                 ConfigureServices(context.Configuration, services);
             })             
             .Build();

            var services = host.Services;
            var mainForm = services.GetRequiredService<MainForm>();
                                                
            Application.Run(mainForm);
        }

        private static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddSingleton<IReportProvider, InDesignProvider>();
            services.AddSingleton<IAppointmentsReportGenerator, AppointmentsReportGenerator>();
            services.AddSingleton<ISummaryReportGenerator, SummaryReportGenerator>();
            services.AddScoped<MainForm>();
        }        
    }
}
