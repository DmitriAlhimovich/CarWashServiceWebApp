using System;
using System.Collections.Generic;
using System.Text;

namespace CarWashService.Reports
{
    public class ReportsConfiguration
    {        
        public int PdfType { get; set; }
        public string AppointmentsReportTemplatePath { get; set; }
        public string AppointmentsPdfFilePathTemplate { get; set; }
        public string SummaryReportTemplatePath { get; set; }
        public string SummaryPdfFilePathTemplate { get; set; }
    }
}
