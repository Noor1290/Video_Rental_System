#nullable disable

using System.Diagnostics;


namespace VideoRentalSystem
{
    public class ProfilePage : Form
    {
        private CustomHashTable userInfo;
        private CustomHashTable videoRentals;
        private CustomHashTable videoData;

        // UI Controls
        private Panel leftPanel;
        private Panel rightPanel;
        private PictureBox profilePicture;
        private Label lblUsername;
        private Label lblEmail;
        private FlowLayoutPanel rentedVideosPanel;
        private Button btnGoBack;

        public ProfilePage(CustomHashTable userInfo, CustomHashTable videoData, CustomHashTable videoRentals)
        {
            this.userInfo = userInfo;
            this.videoRentals = videoRentals;
            this.videoData = videoData;

            InitializeComponents();
            DisplayUserProfile();
            DisplayRentedVideos();
        }

        private void InitializeComponents()
        {
            // Set up the form
            this.Text = "Profile Page";
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(711, 650);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(2, 3, 2, 3);
            StartPosition = FormStartPosition.CenterScreen;

            // Create and configure the left panel (user profile info)
            leftPanel = new Panel
            {
                Dock = DockStyle.Left,
                Width = 300,
                BackColor = Color.LightBlue
            };

            // Create and configure the right panel (rented videos)
            rightPanel = new Panel
            {
                BackColor = Color.White,
                ForeColor = Color.Black,
                Location = new Point(leftPanel.Width, 0),  // Start the right panel after the left panel (300px)
                Size = new Size(this.ClientSize.Width - leftPanel.Width, this.ClientSize.Height)  // Make right panel fill the remaining width
            };

            // Add panels to the form
            this.Controls.Add(leftPanel);
            this.Controls.Add(rightPanel);
            Debug.WriteLine($"Right Panel Size: {rightPanel.ClientSize.Width}x{rightPanel.ClientSize.Height}");
            // Create and configure profile picture
            profilePicture = new PictureBox
            {
                Size = new Size(209, 160),
                Location = new Point(22, 39),
                SizeMode = PictureBoxSizeMode.CenterImage,
                Margin = new Padding(2, 3, 2, 3),
            };


            leftPanel.Controls.Add(profilePicture);

            // Create and configure username label
            lblUsername = new Label
            {
                Font = new Font("Lucida Handwriting", 10F, FontStyle.Regular, GraphicsUnit.Point, 0),
                Location = new Point(22, 289),
                Margin = new Padding(2, 3, 2, 3),
                Size = new Size(204, 50),
                TabIndex = 13,

            };
            leftPanel.Controls.Add(lblUsername);

            // Create and configure email label
            lblEmail = new Label
            {
                Font = new Font("Lucida Handwriting", 10F, FontStyle.Regular, GraphicsUnit.Point, 0),
                Location = new Point(22, 393),
                Margin = new Padding(2, 3, 2, 3),
                Size = new Size(204, 50),
                TabIndex = 12,
            };
            leftPanel.Controls.Add(lblEmail);
            btnGoBack = new Button
            {
                Text = "Go Back",
                Size = new Size(100, 40),
                Location = new Point(20, this.ClientSize.Height - 60), // Place the button near the bottom of the left panel
                BackColor = Color.LightCoral
            };

            // Add button click event handler
            btnGoBack.Click += BtnGoBack_Click;

            // Add button to the left panel
            leftPanel.Controls.Add(btnGoBack);

            // Create and configure rented videos panel
            rentedVideosPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                AutoScroll = true
            };
            rightPanel.Controls.Add(rentedVideosPanel);
        }

        private void DisplayUserProfile()
        {
            byte[] profileImageBytes = userInfo.Get("ProfilePic") as byte[];  // Get the profile image as a byte array
            Debug.WriteLine($"Profile image bytes length: {profileImageBytes?.Length ?? 0}");

            try
            {
                // Assuming userInfo contains the user data in key-value pairs
                string username = userInfo.Get("Username")?.ToString() ?? "No username available";
                string email = userInfo.Get("Email")?.ToString() ?? "No email available";


                // Display username and email
                lblUsername.Text = $"Username: {username}";
                lblEmail.Text = $"Email: {email}";
            }
            catch (Exception ex)
            {
                // Handle the exception gracefully and log it for debugging
                Debug.WriteLine($"Error while updating user profile: {ex.Message}");
                MessageBox.Show($"Error while updating user profile: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Display profile picture
            if (profileImageBytes != null && profileImageBytes.Length > 0)
            {
                Debug.WriteLine("Profile image found, converting byte array to image...");
                try
                {
                    using (MemoryStream ms = new MemoryStream(profileImageBytes))
                    {
                        // Attempt to create the image from the stream
                        var loadedImage = Image.FromStream(ms);



                        // Check if the image is valid (not null)
                        if (loadedImage != null)
                        {
                            profilePicture.SizeMode = PictureBoxSizeMode.Zoom;
                            profilePicture.Image = loadedImage;
                            profilePicture.Refresh();

                            Debug.WriteLine("Successfully uploaded profile picture");
                        }
                        else
                        {
                            Debug.WriteLine("The image is null or invalid.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error loading image: {ex.Message}");
                    MessageBox.Show($"Error loading image: {ex.Message}");
                }

            }
            else
            {
                Debug.WriteLine("No profile picture found, setting to null.");
                // If no profile picture is available, set a default image or handle accordingly
                profilePicture.Image = null; // Optionally, set a default placeholder image
            }
        }

        private void DisplayRentedVideos()
        {
            // Ensure that rentedVideosPanel has enough space and is visible
            rentedVideosPanel.Controls.Clear(); // Clear previous controls
            rentedVideosPanel.AutoScroll = true; // Enable scrolling if needed

            // Ensure the FlowLayoutPanel takes up enough space
            rentedVideosPanel.Width = rightPanel.Width - 20;  // Adjust the width to ensure the panel fits within the rightPanel
            rentedVideosPanel.Height = rightPanel.Height - 50; // Set a fixed height (adjust as needed)

            // Debugging to ensure we're adding rented videos
            Debug.WriteLine("Displaying rented videos...");

            foreach (KeyValuePair<string, object> entry in videoRentals)
            {

                if (entry.Value is CustomHashTable videoInfo)
                {
                    string videoTitle = videoInfo.Get("VideoTitle")?.ToString() ?? "";
                    string videoStatus = videoInfo.Get("Status")?.ToString() ?? ""; // Get status (expired or active)


                    // Truncate title if it's too long
                    if (videoTitle.Length > 15)
                    {
                        videoTitle = videoTitle.Substring(0, 15) + "...";
                    }

                    // Create a new panel to represent each video card
                    Panel videoCard = new Panel
                    {
                        Size = new Size(120, 120),  // Increase size for better visibility
                        Margin = new Padding(10),
                        BorderStyle = BorderStyle.FixedSingle,
                        BackColor = videoStatus.ToLower() == "expired" ? Color.AliceBlue : Color.BlanchedAlmond
                    };

                    // Create a label for the video title, centered inside the square
                    Label lblVideoTitle = new Label
                    {
                        Text = videoTitle,
                        ForeColor = Color.Black,
                        AutoSize = true,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Dock = DockStyle.Fill  // Fill the entire space of the panel
                    };

                    // Add the video title label to the video card
                    videoCard.Controls.Add(lblVideoTitle);

                    // Add the video card to the rented videos panel on the right
                    rentedVideosPanel.Controls.Add(videoCard);
                }
                else
                {
                    Debug.WriteLine("Invalid video entry.");
                }
            }

            // Force the layout to update and refresh the flow layout panel
            rentedVideosPanel.Refresh();
        }
        private void BtnGoBack_Click(object sender, EventArgs e)
        {
            // Close the current ProfilePage
            this.Close();

            // Optionally, you can show the main page again
            // Assuming MainPage is the form you want to go back to
            Main mainPage = new Main(userInfo, videoData, videoRentals);
            mainPage.Show();
        }



    }
}
