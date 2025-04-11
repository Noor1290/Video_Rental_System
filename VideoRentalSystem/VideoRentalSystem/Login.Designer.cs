using System.Windows.Forms;

namespace VideoRentalSystem
{
    partial class Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            panel1 = new Panel();
            pictureBox1 = new PictureBox();
            Reset_button = new Button();
            button1 = new Button();
            txtUsername = new TextBox();
            label1 = new Label();
            txtPassword = new TextBox();
            label2 = new Label();
            label3 = new Label();
            button3 = new Button();
            button4 = new Button();
            lblMessage = new Label();
            lblPassword = new Label();
            lblTextFile = new Label();
            label5 = new Label();
            TextFile = new TextBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.LightBlue;
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(Reset_button);
            panel1.Controls.Add(button1);
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(2, 3, 2, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(178, 650);
            panel1.TabIndex = 11;
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
            Reset_button.Click += Reset_Click;
            // 
            // button1
            // 
            button1.Font = new Font("Lucida Handwriting", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.Location = new Point(22, 289);
            button1.Margin = new Padding(2, 3, 2, 3);
            button1.Name = "button1";
            button1.Size = new Size(134, 50);
            button1.TabIndex = 12;
            button1.Text = "Register";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Register_Click;
            // 
            // txtUsername
            // 
            txtUsername.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtUsername.Location = new Point(230, 211);
            txtUsername.Margin = new Padding(2, 3, 2, 3);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(347, 32);
            txtUsername.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Lucida Handwriting", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(225, 184);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(124, 24);
            label1.TabIndex = 7;
            label1.Text = "Username";
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPassword.Location = new Point(230, 342);
            txtPassword.Margin = new Padding(2, 3, 2, 3);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(347, 32);
            txtPassword.TabIndex = 8;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Lucida Handwriting", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(225, 315);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(116, 24);
            label2.TabIndex = 9;
            label2.Text = "Password";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Lucida Handwriting", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(235, 19);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(299, 36);
            label3.TabIndex = 10;
            label3.Text = "VideoRental Shop";
            // 
            // button3
            // 
            button3.BackColor = SystemColors.ActiveCaption;
            button3.Font = new Font("Lucida Handwriting", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button3.Location = new Point(300, 588);
            button3.Margin = new Padding(2, 3, 2, 3);
            button3.Name = "button3";
            button3.Size = new Size(210, 50);
            button3.TabIndex = 14;
            button3.Text = "Enter";
            button3.UseVisualStyleBackColor = false;
            button3.Click += Login_Click;
            // 
            // button4
            // 
            button4.BackColor = SystemColors.ActiveCaption;
            button4.Font = new Font("Lucida Handwriting", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button4.Location = new Point(626, 0);
            button4.Margin = new Padding(2, 3, 2, 3);
            button4.Name = "button4";
            button4.Size = new Size(74, 50);
            button4.TabIndex = 15;
            button4.Text = "X";
            button4.UseVisualStyleBackColor = false;
            button4.Click += Close_CLick;
            // 
            // lblMessage
            // 
            lblMessage.AutoSize = true;
            lblMessage.ForeColor = SystemColors.ButtonHighlight;
            lblMessage.Location = new Point(271, 258);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(50, 20);
            lblMessage.TabIndex = 16;
            lblMessage.Text = "label4";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.ForeColor = SystemColors.ButtonHighlight;
            lblPassword.Location = new Point(271, 393);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(50, 20);
            lblPassword.TabIndex = 17;
            lblPassword.Text = "label4";
            // 
            // lblTextFile
            // 
            lblTextFile.AutoSize = true;
            lblTextFile.ForeColor = SystemColors.ButtonHighlight;
            lblTextFile.Location = new Point(271, 520);
            lblTextFile.Name = "lblTextFile";
            lblTextFile.Size = new Size(50, 20);
            lblTextFile.TabIndex = 20;
            lblTextFile.Text = "label4";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Lucida Handwriting", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(230, 440);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(115, 24);
            label5.TabIndex = 19;
            label5.Text = "Text File ";
            // 
            // TextFile
            // 
            TextFile.Font = new Font("Verdana", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TextFile.Location = new Point(230, 467);
            TextFile.Margin = new Padding(2, 3, 2, 3);
            TextFile.Name = "TextFile";
            TextFile.Size = new Size(347, 23);
            TextFile.TabIndex = 18;
            TextFile.TextChanged += TextFile_TextChanged;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(711, 650);
            Controls.Add(lblTextFile);
            Controls.Add(label5);
            Controls.Add(TextFile);
            Controls.Add(lblPassword);
            Controls.Add(lblMessage);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(panel1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(txtPassword);
            Controls.Add(label1);
            Controls.Add(txtUsername);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(2, 3, 2, 3);
            Name = "Login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Reset_button;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private Label lblMessage;
        private Label lblPassword;
        private Label lblTextFile;
        private Label label5;
        private TextBox TextFile;
    }
}

