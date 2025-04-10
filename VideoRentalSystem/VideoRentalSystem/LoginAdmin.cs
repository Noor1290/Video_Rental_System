using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoRentalSystem
{
    internal class LoginAdmin : Form
    {
        private Button button4;
        private Button button3;
        private Panel panel1;
        private PictureBox pictureBox1;
        private Button Reset_button;
        private Button button2;
        private Label lblPassword;
        private Label lblMessage;
        private Button GoBackBtn;
        private Button button5;
        private Panel panel2;
        private PictureBox pictureBox2;
        private Label label1;
        private Label label2;
        private TextBox txtPassword;
        private Label label4;
        private TextBox txtUsername;
        private Label label3;
        public LoginAdmin()
        {
            InitializeComponent();
        }




        private void button5_Click(object sender, EventArgs e)
        {
            string correctUsername = "admin";
            string correctPassword = "admin123"; // Replace with your desired password

            string enteredUsername = txtUsername.Text.Trim();
            string enteredPassword = txtPassword.Text.Trim();

            lblMessage.Text = "";
            lblPassword.Text = "";

            if (enteredUsername != correctUsername)
            {
                lblMessage.Text = "Invalid username.";
                lblMessage.ForeColor = Color.Red;
                return; // Stop further validation if username is incorrect
            }

            if (enteredPassword != correctPassword)
            {
                lblPassword.Text = "Invalid password.";
                lblPassword.ForeColor = Color.Red;
                return; // Stop further validation if password is incorrect
            }

            lblMessage.Text = "Login successful!";
            lblMessage.ForeColor = Color.Green;

            // Continue to main page or other actions after successful login
        }
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginAdmin));
            button4 = new Button();
            button3 = new Button();
            panel1 = new Panel();
            pictureBox1 = new PictureBox();
            Reset_button = new Button();
            button2 = new Button();
            label3 = new Label();
            lblPassword = new Label();
            lblMessage = new Label();
            GoBackBtn = new Button();
            button5 = new Button();
            panel2 = new Panel();
            pictureBox2 = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            txtPassword = new TextBox();
            label4 = new Label();
            txtUsername = new TextBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // button4
            // 
            button4.BackColor = SystemColors.ActiveCaption;
            button4.Font = new Font("Lucida Handwriting", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button4.Location = new Point(417, -199);
            button4.Margin = new Padding(2, 3, 2, 3);
            button4.Name = "button4";
            button4.Size = new Size(74, 50);
            button4.TabIndex = 28;
            button4.Text = "X";
            button4.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            button3.BackColor = SystemColors.ActiveCaption;
            button3.Font = new Font("Lucida Handwriting", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button3.Location = new Point(91, 389);
            button3.Margin = new Padding(2, 3, 2, 3);
            button3.Name = "button3";
            button3.Size = new Size(0, 0);
            button3.TabIndex = 27;
            button3.Text = "Enter";
            button3.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.LightBlue;
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(Reset_button);
            panel1.Controls.Add(button2);
            panel1.Location = new Point(-209, -199);
            panel1.Margin = new Padding(2, 3, 2, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(178, 650);
            panel1.TabIndex = 26;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(22, 39);
            pictureBox1.Margin = new Padding(2, 3, 2, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(139, 151);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.TabIndex = 14;
            pictureBox1.TabStop = false;
            // 
            // Reset_button
            // 
            Reset_button.Font = new Font("Lucida Handwriting", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Reset_button.Location = new Point(22, 393);
            Reset_button.Margin = new Padding(2, 3, 2, 3);
            Reset_button.Name = "Reset_button";
            Reset_button.Size = new Size(134, 50);
            Reset_button.TabIndex = 13;
            Reset_button.Text = "Reset";
            Reset_button.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Font = new Font("Lucida Handwriting", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button2.Location = new Point(22, 289);
            button2.Margin = new Padding(2, 3, 2, 3);
            button2.Name = "button2";
            button2.Size = new Size(134, 50);
            button2.TabIndex = 12;
            button2.Text = "Register";
            button2.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Lucida Handwriting", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(26, -180);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(299, 36);
            label3.TabIndex = 25;
            label3.Text = "VideoRental Shop";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.ForeColor = SystemColors.ButtonHighlight;
            lblPassword.Location = new Point(272, 442);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(50, 20);
            lblPassword.TabIndex = 38;
            lblPassword.Text = "label4";
            // 
            // lblMessage
            // 
            lblMessage.AutoSize = true;
            lblMessage.ForeColor = SystemColors.ButtonHighlight;
            lblMessage.Location = new Point(272, 307);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(50, 20);
            lblMessage.TabIndex = 37;
            lblMessage.Text = "label4";
            // 
            // GoBackBtn
            // 
            GoBackBtn.BackColor = SystemColors.ActiveCaption;
            GoBackBtn.Font = new Font("Lucida Handwriting", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            GoBackBtn.Location = new Point(22, 495);
            GoBackBtn.Margin = new Padding(2, 3, 2, 3);
            GoBackBtn.Name = "GoBackBtn";
            GoBackBtn.Size = new Size(123, 50);
            GoBackBtn.TabIndex = 36;
            GoBackBtn.Text = "Go Back";
            GoBackBtn.UseVisualStyleBackColor = false;
            GoBackBtn.Click += GoBackBtn_Click;
            // 
            // button5
            // 
            button5.BackColor = SystemColors.ActiveCaption;
            button5.Font = new Font("Lucida Handwriting", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button5.Location = new Point(298, 486);
            button5.Margin = new Padding(2, 3, 2, 3);
            button5.Name = "button5";
            button5.Size = new Size(210, 50);
            button5.TabIndex = 35;
            button5.Text = "Enter";
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // panel2
            // 
            panel2.BackColor = Color.LightBlue;
            panel2.Controls.Add(pictureBox2);
            panel2.Controls.Add(GoBackBtn);
            panel2.Location = new Point(-4, -9);
            panel2.Margin = new Padding(2, 3, 2, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(178, 650);
            panel2.TabIndex = 34;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(22, 39);
            pictureBox2.Margin = new Padding(2, 3, 2, 3);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(139, 151);
            pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox2.TabIndex = 14;
            pictureBox2.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Lucida Handwriting", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(231, 10);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(299, 36);
            label1.TabIndex = 33;
            label1.Text = "VideoRental Shop";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Lucida Handwriting", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(226, 364);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(116, 24);
            label2.TabIndex = 32;
            label2.Text = "Password";
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Verdana", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPassword.Location = new Point(231, 391);
            txtPassword.Margin = new Padding(2, 3, 2, 3);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(347, 28);
            txtPassword.TabIndex = 31;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Lucida Handwriting", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(226, 233);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(124, 24);
            label4.TabIndex = 30;
            label4.Text = "Username";
            // 
            // txtUsername
            // 
            txtUsername.Font = new Font("Verdana", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtUsername.Location = new Point(231, 260);
            txtUsername.Margin = new Padding(2, 3, 2, 3);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(347, 28);
            txtUsername.TabIndex = 29;
            // 
            // LoginAdmin
            // 
            ClientSize = new Size(693, 632);
            ControlBox = false;
            Controls.Add(lblPassword);
            Controls.Add(lblMessage);
            Controls.Add(button5);
            Controls.Add(panel2);
            Controls.Add(label1);
            Controls.Add(label2);
            Controls.Add(txtPassword);
            Controls.Add(label4);
            Controls.Add(txtUsername);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(panel1);
            Controls.Add(label3);
            Name = "LoginAdmin";
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void GoBackBtn_Click(object sender, EventArgs e)
        {
            WelcomeAdmin admin = new WelcomeAdmin();
            admin.Show();
            this.Close();
        }
    }
}
