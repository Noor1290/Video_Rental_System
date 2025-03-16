using System.Windows.Forms;

namespace VideoRentalSystem
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            panel1 = new Panel();
            button7 = new Button();
            pictureBox2 = new PictureBox();
            button6 = new Button();
            pictureBox1 = new PictureBox();
            button5 = new Button();
            button3 = new Button();
            label4 = new Label();
            label1 = new Label();
            button8 = new Button();
            comboBox1 = new ComboBox();
            comboBox2 = new ComboBox();
            dataGridView1 = new DataGridView();
            label2 = new Label();
            button4 = new Button();
            button1 = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.LightBlue;
            panel1.Controls.Add(button1);
            panel1.Controls.Add(button7);
            panel1.Controls.Add(pictureBox2);
            panel1.Controls.Add(button6);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(button5);
            panel1.Location = new Point(0, -1);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(260, 892);
            panel1.TabIndex = 32;
            // 
            // button7
            // 
            button7.Font = new Font("Lucida Handwriting", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button7.Location = new Point(0, 428);
            button7.Margin = new Padding(3, 4, 3, 4);
            button7.Name = "button7";
            button7.Size = new Size(260, 62);
            button7.TabIndex = 42;
            button7.Text = "Profile";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(13, 812);
            pictureBox2.Margin = new Padding(3, 4, 3, 4);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(58, 64);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 41;
            pictureBox2.TabStop = false;
            // 
            // button6
            // 
            button6.BackColor = SystemColors.ActiveCaption;
            button6.Font = new Font("Lucida Handwriting", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button6.Location = new Point(3, 800);
            button6.Margin = new Padding(3, 4, 3, 4);
            button6.Name = "button6";
            button6.Size = new Size(257, 92);
            button6.TabIndex = 40;
            button6.Text = "Logout";
            button6.UseVisualStyleBackColor = false;
            button6.Click += Logout_CLick;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(41, 32);
            pictureBox1.Margin = new Padding(3, 4, 3, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(174, 189);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.TabIndex = 36;
            pictureBox1.TabStop = false;
            // 
            // button5
            // 
            button5.Font = new Font("Lucida Handwriting", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button5.Location = new Point(0, 358);
            button5.Margin = new Padding(3, 4, 3, 4);
            button5.Name = "button5";
            button5.Size = new Size(260, 62);
            button5.TabIndex = 34;
            button5.Text = "Products";
            button5.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.BackColor = SystemColors.ActiveCaption;
            button3.Font = new Font("Lucida Handwriting", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button3.Location = new Point(1407, -1);
            button3.Margin = new Padding(3, 4, 3, 4);
            button3.Name = "button3";
            button3.Size = new Size(131, 62);
            button3.TabIndex = 39;
            button3.Text = "🔍";
            button3.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Lucida Handwriting", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(356, 146);
            label4.Name = "label4";
            label4.Size = new Size(128, 28);
            label4.TabIndex = 41;
            label4.Text = "Products";
            label4.Click += label4_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Lucida Handwriting", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(983, 146);
            label1.Name = "label1";
            label1.Size = new Size(153, 28);
            label1.TabIndex = 43;
            label1.Text = "Categories";
            // 
            // button8
            // 
            button8.BackColor = SystemColors.ActiveCaption;
            button8.Font = new Font("Lucida Handwriting", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button8.Location = new Point(661, 264);
            button8.Margin = new Padding(3, 4, 3, 4);
            button8.Name = "button8";
            button8.Size = new Size(174, 62);
            button8.TabIndex = 43;
            button8.Text = "Enter";
            button8.UseVisualStyleBackColor = false;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Movies", "Documentories" });
            comboBox1.Location = new Point(361, 185);
            comboBox1.Margin = new Padding(3, 4, 3, 4);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(352, 33);
            comboBox1.TabIndex = 44;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "Action", "", "Adventure", "", "Animation", "", "Biography", "", "Comedy", "", "Crime", "", "Documentary", "", "Drama", "", "Family", "", "Fantasy", "", "History", "", "Horror", "", "Musical", "", "Mystery", "", "Romance", "", "Sci-Fi", "", "Sports", "", "Thriller", "", "War", "Western" });
            comboBox2.Location = new Point(989, 185);
            comboBox2.Margin = new Padding(3, 4, 3, 4);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(352, 33);
            comboBox2.TabIndex = 45;
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(357, 442);
            dataGridView1.Margin = new Padding(3, 4, 3, 4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.RowTemplate.Height = 28;
            dataGridView1.Size = new Size(1092, 432);
            dataGridView1.TabIndex = 46;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Lucida Handwriting", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(836, 404);
            label2.Name = "label2";
            label2.Size = new Size(128, 28);
            label2.TabIndex = 47;
            label2.Text = "Products";
            // 
            // button4
            // 
            button4.BackColor = SystemColors.ActiveCaption;
            button4.Font = new Font("Lucida Handwriting", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button4.Location = new Point(927, 264);
            button4.Margin = new Padding(3, 4, 3, 4);
            button4.Name = "button4";
            button4.Size = new Size(174, 62);
            button4.TabIndex = 48;
            button4.Text = "Delete";
            button4.UseVisualStyleBackColor = false;
            button4.Click += Order_Click;
            // 
            // button1
            // 
            button1.Font = new Font("Lucida Handwriting", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.Location = new Point(0, 498);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(260, 62);
            button1.TabIndex = 43;
            button1.Text = "About Us";
            button1.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1537, 890);
            Controls.Add(button4);
            Controls.Add(label2);
            Controls.Add(dataGridView1);
            Controls.Add(comboBox2);
            Controls.Add(comboBox1);
            Controls.Add(button8);
            Controls.Add(label1);
            Controls.Add(label4);
            Controls.Add(button3);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "Main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Main";
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button4;
        private Button button1;
    }
}