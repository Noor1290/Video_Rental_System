namespace VideoRentalSystem
{
    partial class DeleteVideo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeleteVideo));
            txtDeleteVideo = new TextBox();
            panel1 = new Panel();
            pictureBox1 = new PictureBox();
            GoBackbutton = new Button();
            label3 = new Label();
            label1 = new Label();
            lblMessage = new Label();
            button6 = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // txtDeleteVideo
            // 
            txtDeleteVideo.Location = new Point(224, 174);
            txtDeleteVideo.Name = "txtDeleteVideo";
            txtDeleteVideo.Size = new Size(334, 27);
            txtDeleteVideo.TabIndex = 0;
            txtDeleteVideo.TextChanged += txtDeleteVideo_TextChanged;
            // 
            // panel1
            // 
            panel1.BackColor = Color.LightBlue;
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(GoBackbutton);
            panel1.Location = new Point(2, 12);
            panel1.Margin = new Padding(2, 3, 2, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(178, 406);
            panel1.TabIndex = 12;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(34, 34);
            pictureBox1.Margin = new Padding(2, 3, 2, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(101, 108);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.TabIndex = 14;
            pictureBox1.TabStop = false;
            // 
            // GoBackbutton
            // 
            GoBackbutton.Font = new Font("Lucida Handwriting", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            GoBackbutton.Location = new Point(22, 312);
            GoBackbutton.Margin = new Padding(2, 3, 2, 3);
            GoBackbutton.Name = "GoBackbutton";
            GoBackbutton.Size = new Size(134, 50);
            GoBackbutton.TabIndex = 13;
            GoBackbutton.Text = "Go Back";
            GoBackbutton.UseVisualStyleBackColor = true;
            GoBackbutton.Click += GoBackbutton_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Lucida Handwriting", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(224, 30);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(299, 36);
            label3.TabIndex = 13;
            label3.Text = "VideoRental Shop";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Lucida Handwriting", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(224, 148);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(149, 23);
            label1.TabIndex = 14;
            label1.Text = "Delete Video";
            // 
            // lblMessage
            // 
            lblMessage.AutoSize = true;
            lblMessage.ForeColor = SystemColors.ButtonFace;
            lblMessage.Location = new Point(247, 224);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(12, 20);
            lblMessage.TabIndex = 15;
            lblMessage.Text = ".";
            // 
            // button6
            // 
            button6.BackColor = SystemColors.ActiveCaption;
            button6.Font = new Font("Lucida Handwriting", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button6.Location = new Point(289, 259);
            button6.Margin = new Padding(2, 3, 2, 3);
            button6.Name = "button6";
            button6.Size = new Size(206, 52);
            button6.TabIndex = 42;
            button6.Text = "Delete";
            button6.UseVisualStyleBackColor = false;
            button6.Click += Delete_Click;
            // 
            // DeleteVideo
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(593, 421);
            ControlBox = false;
            Controls.Add(button6);
            Controls.Add(lblMessage);
            Controls.Add(label1);
            Controls.Add(label3);
            Controls.Add(panel1);
            Controls.Add(txtDeleteVideo);
            Name = "DeleteVideo";
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtDeleteVideo;
        private Panel panel1;
        private PictureBox pictureBox1;
        private Button GoBackbutton;
        private Label label3;
        private Label label1;
        private Label lblMessage;
        private Button button6;
    }
}