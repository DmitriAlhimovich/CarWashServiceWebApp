using CarWashService.Core.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarWashService.Reports
{
    public class SummaryReportGenerator : ISummaryReportGenerator
    {        
        private readonly IInDesignProvider provider;
        
        private const string StartDateParamKey = "StartDate";
        private const string EndDateParamKey = "EndDate";
        private const int PdfType = 1952403524;
        private const string ReportTemplatePath = "c:/Downloads/CarWashSummaryReport.indd";
        private const string PdfFilePathTemplate = "c:/Downloads/summary_{0}-{1}.pdf";

        public SummaryReportGenerator(IInDesignProvider provider)
        {
            this.provider = provider;
        }

        public void FillReport(Dictionary<string, object> paramsDict)
        {
            var startDate = (DateTime)paramsDict[StartDateParamKey];
            var endDate = (DateTime)paramsDict[EndDateParamKey];

            var doc = provider.GetReportDocument(ReportTemplatePath);
            InDesign.Page page = (InDesign.Page)doc.Pages[1];

            InDesign.TextFrame frame1 = (InDesign.TextFrame)page.TextFrames[4];
            frame1.Contents = GetSummaries(startDate, endDate);

            InDesign.TextFrame frame2 = (InDesign.TextFrame)page.TextFrames[2];
            frame2.Contents = $"{startDate.ToShortDateString()} - {endDate.ToShortDateString()}";

            doc.Export(PdfType, string.Format(PdfFilePathTemplate,
                startDate.ToShortDateString(), endDate.ToShortDateString()));
        }

        private string GetSummaries(DateTime startDate, DateTime endDate)
        {
            using var context = new CarWashServiceContext();
            var groups = context.Appointments.Include(a => a.Service)
               .Where(a => a.StartTime >= startDate && a.StartTime <= endDate).AsEnumerable()
               .GroupBy(a => a.Service);

            var reportRows = groups.Select(g => $"{g.Key.Title} - {g.Sum(a => a.Service.Price)}").ToList();

            return string.Join("\n", reportRows);
        }
    }
}
