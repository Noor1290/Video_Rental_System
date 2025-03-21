﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoRentalSystem
{
    public class ProfilePage : Form
    {
        private Label lblName;
        private Label lblEmail;
        private PictureBox profilePictureBox;
        private Button btnUploadImage;
        private DataGridView rentedVideosGridView;
        private User currentUser;
        private List<Video> rentedVideos;

        public ProfilePage()
        {
            InitializeComponent();
            LoadUserProfile();
            LoadRentedVideos();

        }

        private void InitializeComponent()
        {
            this.Text = "User profile";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            //Initialize UI components
            lblName = new Label() { Location = new Point(20, 20), AutoSize = true, Font = new Font("Arial", 10, FontStyle.Bold) };
            lblEmail = new Label() { Location = new Point(20, 50), AutoSize = true, Font = new Font("Arial", 10) };

            profilePictureBox = new PictureBox()
            {
                Location = new Point(20, 80),
                Size = new Size(100, 100),
                BorderStyle = BorderStyle.Fixed3D,
                SizeMode = PictureBoxSizeMode.StretchImage

            };

            btnUploadImage = new Button()
            {
                Text = "Upload Image",
                Location = new Point(20, 190),
                Size = new Size(100, 30)

            };

            btnUploadImage.Click += BtnUploadImage_Click;


            rentedVideosGridView = new DataGridView()
            {
                Location = new Point(20, 230),
                Size = new Size(450, 120),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false
            };


            //Add controls to form
            this.Controls.Add(lblName);
            this.Controls.Add(lblEmail);
            this.Controls.Add(profilePictureBox);
            this.Controls.Add(btnUploadImage);
            this.Controls.Add(rentedVideosGridView);
        }

        private void LoadUserProfile()
        {
            currentUser = new User
            {
                Name = "Keisha Bungchee",
                Email = "Keisha@gmail.com",
                ProfileImagePath = "profile_image.txt"
            };

            lblName.Text = $"Name: {currentUser.Name}";
            lblEmail.Text = $"Email: {currentUser.Email}";

            if (File.Exists(currentUser.ProfileImagePath))
            {
                string imagePath = File.ReadAllText(currentUser.ProfileImagePath);
                if (File.Exists(imagePath))
                {
                    profilePictureBox.Image = Image.FromFile(imagePath);
                }
            }
        }

        private void LoadRentedVideos()
        {
            rentedVideos = new List<Video>
            {
                new Video { Title = "Fast and Furious", RentedDate = DateTime.Now.AddDays(-2) },
                new Video { Title = "BattleField", RentedDate = DateTime.Now.AddDays(-5) },
            };
            rentedVideosGridView.DataSource = null;
            rentedVideosGridView.DataSource = rentedVideos;
        }


        private void BtnUploadImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string imagePath = openFileDialog.FileName;
                    profilePictureBox.Image = Image.FromFile(imagePath);
                    currentUser.ProfileImagePath = imagePath;

                    File.WriteAllText("profile_image.txt", imagePath);
                }
            }
        }

    }

    public class User
    {
        public string Name { get; set; } = "Default Name";
        public string Email { get; set; } = "default@example.com";
        public string ProfileImagePath { get; set; } = "";
    }

    public class Video
    {
        public string Title { get; set; } = "Untitled Video";
        public DateTime RentedDate { get; set; } = DateTime.Now;
    }


}