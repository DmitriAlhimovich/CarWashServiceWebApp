namespace CarWashService.Reports.WinApp
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnWeekly = new System.Windows.Forms.Button();
            this.btnMonthly = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnWeekly
            // 
            this.btnWeekly.Location = new System.Drawing.Point(12, 12);
            this.btnWeekly.Name = "btnWeekly";
            this.btnWeekly.Size = new System.Drawing.Size(152, 32);
            this.btnWeekly.TabIndex = 0;
            this.btnWeekly.Text = "Weekly Report";
            this.btnWeekly.UseVisualStyleBackColor = true;
            this.btnWeekly.Click += new System.EventHandler(this.btnWeekly_Click);
            // 
            // btnMonthly
            // 
            this.btnMonthly.Location = new System.Drawing.Point(12, 72);
            this.btnMonthly.Name = "btnMonthly";
            this.btnMonthly.Size = new System.Drawing.Size(152, 32);
            this.btnMonthly.TabIndex = 1;
            this.btnMonthly.Text = "Monthly Report";
            this.btnMonthly.UseVisualStyleBackColor = true;
            this.btnWeekly.Click += new System.EventHandler(this.btnMonthly_Click);

            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Car Wash Reports";
            this.components.Add(this.btnMonthly);
            this.components.Add(this.btnWeekly);
            this.Controls.Add(this.btnMonthly);
            this.Controls.Add(this.btnWeekly);
        }

        #endregion

        private System.Windows.Forms.Button btnWeekly;
        private System.Windows.Forms.Button btnMonthly;
    }
}

