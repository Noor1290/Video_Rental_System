using System;
using System.Drawing;
using System.Windows.Forms;
using Last;
using Timer = System.Windows.Forms.Timer;


namespace VideoRentalSystem
{
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    // Main form class for the welcome screen
    public partial class WelcomeForm : Form
    {
        // UI elements
        private Label lblWelcome;
        private Button btnLogin;
        private Button btnRegister;

        // Timers for animations
        private Timer typingTimer;
        private Timer fadeTimer;

        // Typing animation variables
        private string welcomeText = "Welcome To Video Rental Store!";
        private int charIndex = 0;

        // Constructor
        public WelcomeForm()
        {
            InitializeComponent(); // Set up form and controls
            StartTypingAnimation(); // Begin typing animation
        }

        // Initialize and configure form elements
        private void InitializeComponent()
        {
            // Form properties
            this.Text = "Welcome Window";
            this.AutoScaleDimensions = new SizeF(9F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(875, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.LightBlue; // LightBlue background

            // Welcome Label
            lblWelcome = new Label
            {
                Text = "", // Starts empty for typing effect
                ForeColor = Color.Black,
                Font = new Font("Lucida Handwriting", 18F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                AutoSize = true,
                Location = new Point(125, 45), // Position at the top center
                TextAlign = ContentAlignment.MiddleCenter,
            };
            this.Controls.Add(lblWelcome);

            // Login Button
            btnLogin = new Button
            {
                Text = "Login",
                Size = new Size(175, 90),
                Location = new Point(360, 230),
                BackColor = SystemColors.ActiveCaption,
                ForeColor = Color.White,
                Font = new Font("Lucida Handwriting", 15F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                Anchor = AnchorStyles.Bottom,
            };
            btnLogin.Click += BtnLogin_Click; // Attach click event
            this.Controls.Add(btnLogin);

            // Register Button
            btnRegister = new Button
            {
                Text = "Register",
                Size = new Size(175, 90),
                Location = new Point(360, 350),
                BackColor = SystemColors.ActiveCaption,
                ForeColor = Color.White,
                Font = new Font("Lucida Handwriting", 15F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                Anchor = AnchorStyles.Top,
            };

            btnRegister.Click += BtnRegister_Click; // Attach click event
            this.Controls.Add(btnRegister);
        }

        // Start the typing effect for the welcome message
        private void StartTypingAnimation()
        {
            typingTimer = new Timer { Interval = 100 }; // Typing speed (100ms per character)
            typingTimer.Tick += (s, e) =>
            {
                if (charIndex < welcomeText.Length)
                {
                    lblWelcome.Text += welcomeText[charIndex++]; // Add one character at a time
                }
                else
                {
                    typingTimer.Stop(); // Stop when text is fully typed
                    StartFadeOutAnimation(); // Start fading effect
                }
            };
            typingTimer.Start();
        }

        // Fade out the welcome message and replace it with new text
        private void StartFadeOutAnimation()
        {
            fadeTimer = new Timer { Interval = 50 }; // Smoother fading with shorter interval
            fadeTimer.Tick += (s, e) =>
            {
                if (lblWelcome.ForeColor.A > 20) // Ensure Alpha value doesn't go negative
                {
                    lblWelcome.ForeColor = Color.FromArgb(lblWelcome.ForeColor.A - 20, 255, 255);
                }
                else
                {
                    fadeTimer.Stop();
                    lblWelcome.Text = ""; // Clear text
                    lblWelcome.ForeColor = Color.White; // Reset color
                    charIndex = 0;
                    welcomeText = "Please login or register!"; // Update text
                    StartTypingAnimation(); // Restart typing effect with new text
                }
            };
            fadeTimer.Start();
        }

        // Event handler for Login button click
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        // Event handler for Register button click
        private void BtnRegister_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.Show();
            this.Hide();

        }
    }
}
