using CarWashService.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarWashService.Reports
{
    public class AppointmentsReportGenerator : IAppointmentsReportGenerator
    {
        private readonly IReportProvider provider;
        private readonly IConfiguration configuration;
        private readonly ReportsConfiguration reportsConfiguration;
        private const string StartDateParamKey = "StartDate";
        private const string EndDateParamKey = "EndDate";         

        public AppointmentsReportGenerator(IConfiguration configuration, IReportProvider provider)
        {
            this.configuration = configuration;
            reportsConfiguration = new ReportsConfiguration();
            this.configuration.GetSection("ReportsConfiguration").Bind(reportsConfiguration);

            this.provider = provider;
        }

        public void FillReport(Dictionary<string, object> paramsDict)
        {
            var startDate = (DateTime)paramsDict[StartDateParamKey];
            var endDate = (DateTime)paramsDict[EndDateParamKey];

            var doc = provider.GetReportDocument(reportsConfiguration.AppointmentsReportTemplatePath);
            InDesign.Page page = (InDesign.Page)doc.Pages[1];

            InDesign.TextFrame dataFrame = (InDesign.TextFrame)page.TextFrames[4];
            dataFrame.Contents = GetAppointments(startDate, endDate);

            InDesign.TextFrame dateRangeFrame = (InDesign.TextFrame)page.TextFrames[2];
            dateRangeFrame.Contents = $"{startDate.ToShortDateString()} - {endDate.ToShortDateString()}";

            doc.Export(reportsConfiguration.PdfType, string.Format(reportsConfiguration.AppointmentsPdfFilePathTemplate,
                startDate.ToShortDateString(), endDate.ToShortDateString()));
        }

        private string GetAppointments(DateTime startDate, DateTime endDate)
        {
            using var context = new CarWashServiceContext();
            var reportRows = context.Appointments.Include(a => a.Service)
                .Where(a => a.StartTime >= startDate && a.StartTime <= endDate)
                .OrderBy(a => a.StartTime)
                .Select(a => $"{a.StartTime.ToShortDateString()} {a.Service.Title} ({a.Customer.FirstName} {a.Customer.LastName})").ToList();

            return string.Join("\n", reportRows);
        }
    }
}
