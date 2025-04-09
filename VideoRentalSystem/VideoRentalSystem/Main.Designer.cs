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
            button1 = new Button();
            button7 = new Button();
            pictureBox2 = new PictureBox();
            button6 = new Button();
            pictureBox1 = new PictureBox();
            button5 = new Button();
            SearchButton = new Button();
            label4 = new Label();
            label1 = new Label();
            button8 = new Button();
            label2 = new Label();
            button4 = new Button();
            ProductsName = new TextBox();
            Categories = new ComboBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
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
            panel1.Margin = new Padding(2, 3, 2, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(208, 987);
            panel1.TabIndex = 32;
            // 
            // button1
            // 
            button1.Font = new Font("Lucida Handwriting", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.Location = new Point(0, 398);
            button1.Margin = new Padding(2, 3, 2, 3);
            button1.Name = "button1";
            button1.Size = new Size(208, 50);
            button1.TabIndex = 43;
            button1.Text = "About Us";
            button1.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            button7.Font = new Font("Lucida Handwriting", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button7.Location = new Point(0, 342);
            button7.Margin = new Padding(2, 3, 2, 3);
            button7.Name = "button7";
            button7.Size = new Size(208, 50);
            button7.TabIndex = 42;
            button7.Text = "Profile";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(11, 925);
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
            button6.Location = new Point(2, 915);
            button6.Margin = new Padding(2, 3, 2, 3);
            button6.Name = "button6";
            button6.Size = new Size(206, 74);
            button6.TabIndex = 40;
            button6.Text = "Logout";
            button6.UseVisualStyleBackColor = false;
            button6.Click += Logout_CLick;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(33, 26);
            pictureBox1.Margin = new Padding(2, 3, 2, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(139, 151);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.TabIndex = 36;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // button5
            // 
            button5.Font = new Font("Lucida Handwriting", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button5.Location = new Point(0, 286);
            button5.Margin = new Padding(2, 3, 2, 3);
            button5.Name = "button5";
            button5.Size = new Size(208, 50);
            button5.TabIndex = 34;
            button5.Text = "Products";
            button5.UseVisualStyleBackColor = true;
            // 
            // SearchButton
            // 
            SearchButton.BackColor = SystemColors.ActiveCaption;
            SearchButton.Font = new Font("Lucida Handwriting", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            SearchButton.Location = new Point(1126, -1);
            SearchButton.Margin = new Padding(2, 3, 2, 3);
            SearchButton.Name = "SearchButton";
            SearchButton.Size = new Size(105, 50);
            SearchButton.TabIndex = 39;
            SearchButton.Text = "🔍";
            SearchButton.UseVisualStyleBackColor = false;
            SearchButton.Click += SearchButtonFunction;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Lucida Handwriting", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(285, 117);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(111, 24);
            label4.TabIndex = 41;
            label4.Text = "Products";
            label4.Click += label4_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Lucida Handwriting", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(786, 117);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(131, 24);
            label1.TabIndex = 43;
            label1.Text = "Categories";
            // 
            // button8
            // 
            button8.BackColor = SystemColors.ActiveCaption;
            button8.Font = new Font("Lucida Handwriting", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button8.Location = new Point(529, 211);
            button8.Margin = new Padding(2, 3, 2, 3);
            button8.Name = "button8";
            button8.Size = new Size(139, 50);
            button8.TabIndex = 43;
            button8.Text = "Enter";
            button8.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Lucida Handwriting", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(669, 323);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(111, 24);
            label2.TabIndex = 47;
            label2.Text = "Products";
            // 
            // button4
            // 
            button4.BackColor = SystemColors.ActiveCaption;
            button4.Font = new Font("Lucida Handwriting", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button4.Location = new Point(742, 211);
            button4.Margin = new Padding(2, 3, 2, 3);
            button4.Name = "button4";
            button4.Size = new Size(139, 50);
            button4.TabIndex = 48;
            button4.Text = "Delete";
            button4.UseVisualStyleBackColor = false;
            button4.Click += Order_Click;
            // 
            // ProductsName
            // 
            ProductsName.Location = new Point(288, 156);
            ProductsName.Name = "ProductsName";
            ProductsName.Size = new Size(281, 27);
            ProductsName.TabIndex = 49;
            ProductsName.TextChanged += ProductsName_TextChanged;
            // 
            // Categories
            // 
            Categories.FormattingEnabled = true;
            Categories.Items.AddRange(new object[] { "Action", "Adventure", "Animation", "Apocalyptic", "Biopic", "Comedy", "Crime", "Cyborg", "Default", "Documentary", "Drama", "Fantasy", "Fiction", "Horror", "Mindbearer", "Mystery", "Outlaw", "Political", "Psychological", "Romance", "Thriller" });
            Categories.Location = new Point(786, 155);
            Categories.Name = "Categories";
            Categories.Size = new Size(278, 28);
            Categories.TabIndex = 50;
            Categories.SelectedIndexChanged += Categories_SelectedIndexChanged;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1230, 987);
            Controls.Add(Categories);
            Controls.Add(ProductsName);
            Controls.Add(button4);
            Controls.Add(label2);
            Controls.Add(button8);
            Controls.Add(label1);
            Controls.Add(label4);
            Controls.Add(SearchButton);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(2, 3, 2, 3);
            Name = "Main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Main";
            Load += Main_Load;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button4;
        private Button button1;
        private TextBox ProductsName;
        private ComboBox Categories;
    }
}