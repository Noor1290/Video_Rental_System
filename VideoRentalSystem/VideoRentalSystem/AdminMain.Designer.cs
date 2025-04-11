namespace VideoRentalSystem
{
    partial class AdminMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminMain));
            panel1 = new Panel();
            RemoveVideo = new Button();
            ModifyVideo = new Button();
            AddVideo = new Button();
            pictureBox2 = new PictureBox();
            button6 = new Button();
            pictureBox1 = new PictureBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.LightBlue;
            panel1.Controls.Add(RemoveVideo);
            panel1.Controls.Add(ModifyVideo);
            panel1.Controls.Add(AddVideo);
            panel1.Controls.Add(pictureBox2);
            panel1.Controls.Add(button6);
            panel1.Controls.Add(pictureBox1);
            panel1.Location = new Point(2, -7);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(213, 800);
            panel1.TabIndex = 51;
            // 
            // RemoveVideo
            // 
            RemoveVideo.BackColor = SystemColors.ActiveCaption;
            RemoveVideo.Font = new Font("Lucida Handwriting", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            RemoveVideo.Location = new Point(5, 300);
            RemoveVideo.Margin = new Padding(2, 3, 2, 3);
            RemoveVideo.Name = "RemoveVideo";
            RemoveVideo.Size = new Size(206, 74);
            RemoveVideo.TabIndex = 44;
            RemoveVideo.Text = "Remove";
            RemoveVideo.UseVisualStyleBackColor = false;
            RemoveVideo.Click += RemoveVideo_Click;
            // 
            // ModifyVideo
            // 
            ModifyVideo.BackColor = SystemColors.ActiveCaption;
            ModifyVideo.Font = new Font("Lucida Handwriting", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ModifyVideo.Location = new Point(5, 380);
            ModifyVideo.Margin = new Padding(2, 3, 2, 3);
            ModifyVideo.Name = "ModifyVideo";
            ModifyVideo.Size = new Size(206, 74);
            ModifyVideo.TabIndex = 43;
            ModifyVideo.Text = "Modify";
            ModifyVideo.UseVisualStyleBackColor = false;
            ModifyVideo.Click += ModifyVideo_Click;
            // 
            // AddVideo
            // 
            AddVideo.BackColor = SystemColors.ActiveCaption;
            AddVideo.Font = new Font("Lucida Handwriting", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            AddVideo.Location = new Point(2, 220);
            AddVideo.Margin = new Padding(2, 3, 2, 3);
            AddVideo.Name = "AddVideo";
            AddVideo.Size = new Size(206, 74);
            AddVideo.TabIndex = 42;
            AddVideo.Text = "Add";
            AddVideo.UseVisualStyleBackColor = false;
            AddVideo.Click += AddVideo_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(9, 572);
            pictureBox2.Margin = new Padding(2, 3, 2, 3);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(46, 51);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 41;
            pictureBox2.TabStop = false;
            // 
            // button6
            // 
            button6.BackColor = SystemColors.ActiveCaption;
            button6.Font = new Font("Lucida Handwriting", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button6.Location = new Point(2, 562);
            button6.Margin = new Padding(2, 3, 2, 3);
            button6.Name = "button6";
            button6.Size = new Size(206, 74);
            button6.TabIndex = 40;
            button6.Text = "Logout";
            button6.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(32, 11);
            pictureBox1.Margin = new Padding(2, 3, 2, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(139, 151);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.TabIndex = 36;
            pictureBox1.TabStop = false;
            // 
            // AdminMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(865, 641);
            ControlBox = false;
            Controls.Add(panel1);
            Name = "AdminMain";
            Load += AdminMain_Load;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Panel panel1;
        private PictureBox pictureBox2;
        private Button button6;
        private PictureBox pictureBox1;
        private Button RemoveVideo;
        private Button ModifyVideo;
        private Button AddVideo;
    }
}