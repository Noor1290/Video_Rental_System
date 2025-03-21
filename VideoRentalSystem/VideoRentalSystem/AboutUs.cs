using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace VideoRentalSystem
{
    public partial class AboutUs : Form
    {

        private Label lblAboutUs;
        

        // Timers for animations
        private Timer typingTimer;
        private Timer fadeTimer;

        // Typing animation variables
        private string welcomeText = "About Us!";
        private int charIndex = 0;
        public AboutUs()
        {
            InitializeComponent();
            StartTypingAnimation(); // Begin typing animation
            Initialize();
        }


         private void Initialize()
         {
            lblAboutUs = new Label();
            SuspendLayout();
            //
            // lblAboutUs
            //

            lblAboutUs.Location = new Point(0, 0);
            lblAboutUs.Name = "lblAboutUs";
            lblAboutUs.Size = new Size(111, 29);
            lblAboutUs.TabIndex = 0;

            //
            // Form
            //
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(972, 812);
            Controls.Add(lblAboutUs);
            Margin = new Padding(3, 4, 3, 4);
            Name = "AboutUs";
            StartPosition = FormStartPosition.CenterScreen;
            Load += AboutUs_Load;
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        

        private void StartTypingAnimation()
        {
            typingTimer = new Timer { Interval = 100 }; // Typing speed (100ms per character)
            typingTimer.Tick += (s, e) =>
            {
                if (charIndex < welcomeText.Length)
                {
                    lblAboutUs.Text += welcomeText[charIndex++]; // Add one character at a time
                }
                else
                {
                    typingTimer.Stop(); // Stop when text is fully typed
                    StartFadeOutAnimation(); // Start fading effect
                }
            };
            typingTimer.Start();
        }

        private void StartFadeOutAnimation()
        {
            fadeTimer = new Timer { Interval = 50 }; // Smoother fading with shorter interval
            fadeTimer.Tick += (s, e) =>
            {
                if (lblAboutUs.ForeColor.A > 20) // Ensure Alpha value doesn't go negative
                {
                    lblAboutUs.ForeColor = Color.FromArgb(lblAboutUs.ForeColor.A - 20, 255, 255);
                }
                else
                {
                    fadeTimer.Stop();
                    lblAboutUs.Text = ""; // Clear text
                    lblAboutUs.ForeColor = Color.Black; // Reset color
                    charIndex = 0;
                    welcomeText = ""; // Update text
                    StartTypingAnimation(); // Restart typing effect with new text
                }
            };
            fadeTimer.Start();
        }


        private void AboutUs_Load(object sender, EventArgs e)
        {

        }
    }
}
