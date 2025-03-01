using System;
using System.Windows.Forms;

namespace VideoRentalSystem
{
    public class Welcome : Form
    {
        private Label welcomeLabel;

        public Welcome()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Form properties
            this.Text = "Welcome";
            this.Width = 900;
            this.Height = 500;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Welcome label
            welcomeLabel = new Label();
            welcomeLabel.Text = "Welcome to the BEST Video Rental Store!";
            welcomeLabel.AutoSize = true;
            welcomeLabel.Font = new System.Drawing.Font("Arial", 14);
            welcomeLabel.Location = new System.Drawing.Point(100, 80);

            // Add controls to the form
            this.Controls.Add(welcomeLabel);
        }


    }
}