using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace VideoRentalSystem
{
    public class ProfilePage : Form
    {
        private Label lblTitle;
        private Label lblName;
        private Label lblEmail;
        private PictureBox profilePictureBox;
        private Button btnUploadImage;
        private Button btnEditProfile;
        private DataGridView rentedVideosGridView;
        private User currentUser;
        private List<Video> rentedVideos;
        private Panel mainPanel;

        public ProfilePage()
        {
            InitializeComponent();
            LoadUserProfile();
            LoadRentedVideos();
            ApplyTheme();

        }

        private void InitializeComponent()
        {
            // Main Form Settings
            this.Text = "My Profile - Video Rental System";
            this.Size = new Size(700, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 240, 240);
            this.Font = new Font("Segoe UI", 9);

            // Main Panel for better layout control
            mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20)
            };
            this.Controls.Add(mainPanel);

            // Title Label
            lblTitle = new Label()
            {
                Text = "MY PROFILE",
                Location = new Point(20, 20),
                AutoSize = true,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(70, 130, 180)
            };



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
                new Video { Title = "Transformers", RentedDate = DateTime.Now.AddDays(-6) },
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