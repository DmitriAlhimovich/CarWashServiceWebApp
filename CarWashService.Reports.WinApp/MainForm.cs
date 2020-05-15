using CarWashService.Core.Data;
using CarWashService.Reports.WinApp.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarWashService.Reports.WinApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnWeekly_Click(object sender, EventArgs e)
        {
            using var context = new CarWashServiceContext();

            //new InDesignProvider().WriteRows(new[] { "one", "twokk"});

            var rows = context.Appointments
                .Where(a => a.StartTime >= DateTime.Today.AddDays(-7))
                .Select(a => $"{a.StartTime.ToShortDateString()} - {a.Customer.LastName}").ToList();

            new InDesignProvider().WriteRows(rows); ;
        }

        private void btnMonthly_Click(object sender, EventArgs e)
        {
            //CarWashService.Reports
        }

    }
}
