using CarWashService.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarWashService.Reports
{
    public class SummaryReportGenerator : ISummaryReportGenerator
    {
        private readonly IConfiguration configuration;
        private readonly ReportsConfiguration reportsConfiguration;
        private readonly IReportProvider provider;
        
        private const string StartDateParamKey = "StartDate";
        private const string EndDateParamKey = "EndDate";       

        public SummaryReportGenerator(IConfiguration configuration, IReportProvider provider)
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

            var doc = provider.GetReportDocument(reportsConfiguration.SummaryReportTemplatePath);
            InDesign.Page page = (InDesign.Page)doc.Pages[1];

            InDesign.TextFrame frame1 = (InDesign.TextFrame)page.TextFrames[4];
            frame1.Contents = GetSummaries(startDate, endDate);

            InDesign.TextFrame frame2 = (InDesign.TextFrame)page.TextFrames[2];
            frame2.Contents = $"{startDate.ToShortDateString()} - {endDate.ToShortDateString()}";

            doc.Export(reportsConfiguration.PdfType, string.Format(reportsConfiguration.SummaryPdfFilePathTemplate,
                startDate.ToShortDateString(), endDate.ToShortDateString()));
        }

        private string GetSummaries(DateTime startDate, DateTime endDate)
        {
            using var context = new CarWashServiceContext();
            var groups = context.Appointments.Include(a => a.Service)
               .Where(a => a.StartTime >= startDate && a.StartTime <= endDate).AsEnumerable()
               .GroupBy(a => a.Service);

            var reportRows = groups.OrderBy(g => g.Key.Title).Select(g => $"{g.Key.Title} - {g.Sum(a => a.Service.Price)}").ToList();

            return string.Join("\n", reportRows);
        }
    }
}
