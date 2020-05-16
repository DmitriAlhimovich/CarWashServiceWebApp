using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CarWashService.Reports.WinApp
{
    public partial class MainForm : Form
    {
        private readonly IAppointmentsReportGenerator appointmentsReportGenerator;
        private readonly ISummaryReportGenerator summaryReportGenerator;

        public MainForm(IAppointmentsReportGenerator appointmentsReportGenerator,
            ISummaryReportGenerator summaryReportGenerator)
        {            
            this.appointmentsReportGenerator = appointmentsReportGenerator;
            this.summaryReportGenerator = summaryReportGenerator;
            InitializeComponent();
        }

        private void BtnWeekly_Click(object sender, EventArgs e)
        {
            StartReportGenerating();

            appointmentsReportGenerator.FillReport(new Dictionary<string, object>()
            {
                ["StartDate"] = DateTime.Now.AddDays(-7),
                ["EndDate"] = DateTime.Now
            });
            EndReportGenerating();
        }                

        private void BtnMonthly_Click(object sender, EventArgs e)
        {
            StartReportGenerating();

            summaryReportGenerator.FillReport(new Dictionary<string, object>()
            {
                ["StartDate"] = DateTime.Now.AddDays(-30),
                ["EndDate"] = DateTime.Now
            });

            EndReportGenerating();
        }

        private void StartReportGenerating()
        {
            Cursor = Cursors.WaitCursor;
            btnWeekly.Enabled = false;
            btnMonthly.Enabled = false;
        }

        private void EndReportGenerating()
        {
            btnWeekly.Enabled = true;
            btnMonthly.Enabled = true;
            Cursor = Cursors.Default;
        }
    }
}
