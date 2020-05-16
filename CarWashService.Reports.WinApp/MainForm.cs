using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CarWashService.Reports.WinApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void BtnWeekly_Click(object sender, EventArgs e)
        {
            AppointmentsReportGenerator reportGenerator =
                new AppointmentsReportGenerator(new InDesignProvider());

            reportGenerator.FillReport(new Dictionary<string, object>()
            {
                ["StartDate"] = DateTime.Now.AddDays(-7),
                ["EndDate"] = DateTime.Now
            });
        }

        private void BtnMonthly_Click(object sender, EventArgs e)
        {
            SummaryReportGenerator reportGenerator =
                new SummaryReportGenerator(new InDesignProvider());

            reportGenerator.FillReport(new Dictionary<string, object>()
            {
                ["StartDate"] = DateTime.Now.AddDays(-30),
                ["EndDate"] = DateTime.Now
            });
        }

    }
}
