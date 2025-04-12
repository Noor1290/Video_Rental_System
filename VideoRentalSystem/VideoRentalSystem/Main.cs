//imports
#nullable disable
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using Timer = System.Windows.Forms.Timer;

namespace VideoRentalSystem
{
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public partial class Main : Form
    {
        private CustomHashTable userInfo;// store logged-in user information
        private CustomHashTable videoData;// contains available video data
        private CustomHashTable videoRentals;//tracks active video rentals
        private FlowLayoutPanel flowLayoutPanel;// UI container for video cards

        public Main(CustomHashTable userInfo, CustomHashTable videoData, CustomHashTable videoRentals)
        {
            // initialise data structures
            this.userInfo = userInfo;
            this.videoData = videoData;
            this.videoRentals = videoRentals;
            InitializeComponent();
            DisplayVideoDataAsCards(); //populate UI with video cards on startup

        }

        // handles user logout process
        private void Logout_CLick(object sender, EventArgs e)
        {
            //sync data with database before logout
            var rentalManager = new VideoRentalManager(videoRentals, userInfo);
            rentalManager.SyncRentalsToDatabase();
            rentalManager.UpdateAllRentalTimersInDB();
            rentalManager.ClearVideoDatabaseAndUnlinkChildren();
            //close cuurent form and return to login  
            this.Close();
            Login login = new Login();
            login.Show();
        }



        // Helper method to check if the string looks like a valid file path
        public static bool IsValidPath(string path)
        {
            // Check if the path contains typical file path characters
            // such as directory separators or drive letters (e.g., C:\)
            return path.Contains("\\") || path.Contains("/");
        }

        //generates and displays video cards in a scrollable layout
        private void DisplayVideoDataAsCards()
        {
            // Create a FlowLayoutPanel to hold the video cards
            if (flowLayoutPanel == null)
            {
                flowLayoutPanel = new FlowLayoutPanel
                {
                    Dock = DockStyle.None, // Ensure the panel is not docked to fill the container
                    FlowDirection = FlowDirection.LeftToRight, // Make the cards appear side by side
                    WrapContents = true, // Allow the cards to wrap to the next row
                    AutoScroll = true, // Allow scrolling if content exceeds the panel size
                    Padding = new Padding(10),
                    MaximumSize = new Size(Width, 600), // Set the max height to control overflow
                    Size = new Size(1000, 1000) // Example size, adjust based on your design
                };
                // Adjust the height to accommodate rows dynamically if required
                int rowHeight = 1320; // Assuming the height of each video card including padding
                int totalHeight = rowHeight * 2; // For example, two rows
                flowLayoutPanel.Size = new Size(1000, totalHeight); // Adjust the height to fit the content
                flowLayoutPanel.Margin = new Padding(100, 0, 100, 200);
                // Position the FlowLayoutPanel below the "Products" section
                flowLayoutPanel.Location = new Point(250, 350); // Adjust X and Y values as needed

                // Add the FlowLayoutPanel to the Form (or any container)
                this.Controls.Add(flowLayoutPanel);

                int cardCount = 0; // To keep track of how many cards we've added

                foreach (KeyValuePair<string, object> entry in videoData)
                {
                    if (cardCount >= 8) break; // Limit to 4 cards

                    if (entry.Value is CustomHashTable videoInfo)
                    {
                        // Access videoPath 
                        string videoImageObj = videoInfo.Get("VideoPath")?.ToString() ?? "";
                        string videoTitle = videoInfo.Get("VideoTitle")?.ToString() ?? "Untitled";

                        // Create a new Panel to represent the video card
                        Panel videoCard = new Panel
                        {
                            Size = new Size(200, 300),
                            BackColor = Color.LightBlue,
                            Margin = new Padding(10),
                            Cursor = Cursors.Hand
                        };

                        // Create the PictureBox for the video image
                        PictureBox videoImageControl = new PictureBox
                        {
                            SizeMode = PictureBoxSizeMode.StretchImage,
                            Size = new Size(180, 250),
                            Location = new Point(10, 10),
                            Tag = videoInfo
                        };

                        // Load image from the path (if valid)
                        if (IsValidPath(videoImageObj))
                        {
                            try
                            {
                                videoImageControl.Image = Image.FromFile(videoImageObj);
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine("Error loading image: " + ex.Message);
                            }
                        }

                        // Add Click event to show the video popup when clicked
                        videoImageControl.Click += (s, e) => ShowVideoPopup(videoInfo);

                        // Create a label for the video title
                        Label lblTitle = new Label
                        {
                            Text = videoTitle,
                            ForeColor = Color.White,
                            Font = new Font("Lucida Handwriting", 12F, FontStyle.Regular, GraphicsUnit.Point, 0),
                            AutoSize = true,
                            Location = new Point(10, 270),
                            Tag = videoInfo
                        };

                        // Add Click event for the label as well
                        lblTitle.Click += (s, e) => ShowVideoPopup(videoInfo);

                        // Add the PictureBox and Label to the video card (Panel)
                        videoCard.Controls.Add(videoImageControl);
                        videoCard.Controls.Add(lblTitle);

                        // Add the video card to the FlowLayoutPanel
                        flowLayoutPanel.Controls.Add(videoCard);

                        cardCount++; // Increment the card count
                    }
                }
            }
        }

        //dsplays detailed video information in a popup dialog
        private void ShowVideoPopup(CustomHashTable videoData)
        {
            Form popup = new Form
            {
                Text = "Video Details",
                Size = new Size(900, 500),
                StartPosition = FormStartPosition.CenterParent,
                BackColor = Color.FromArgb(0, 31, 63)
            };

            // Left panel for Image
            Panel leftPanel = new Panel
            {
                Size = new Size(400, 500),
                Dock = DockStyle.Left,
                BackColor = Color.Black
            };

            PictureBox pictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.StretchImage,
                BorderStyle = BorderStyle.FixedSingle
            };
            string videoImageObj = videoData.Get("VideoPath")?.ToString() ?? "";
            if (videoImageObj is string imagePath && IsValidPath(imagePath))
            {
                try
                {
                    pictureBox.Image = Image.FromFile(imagePath); // Load image from file
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error loading image from file: " + ex.Message);
                }
            }

            leftPanel.Controls.Add(pictureBox);
            popup.Controls.Add(leftPanel);

            // Right panel for details
            Panel rightPanel = new Panel
            {
                Size = new Size(500, 500),
                Dock = DockStyle.Right,
                BackColor = Color.LightBlue
            };

            Label lblTitle = new Label
            {
                Text = $"Title: {videoData["VideoTitle"]}",
                ForeColor = Color.White,
                Font = new Font("Lucida Handwriting", 12F, FontStyle.Regular, GraphicsUnit.Point, 0),
                AutoSize = true,
                Location = new Point(20, 30)
            };

            Label lblDescription = new Label
            {
                Text = $"Duration: {videoData["Duration"]}",
                ForeColor = Color.White,
                Font = new Font("Lucida Handwriting", 12F, FontStyle.Regular, GraphicsUnit.Point, 0),
                AutoSize = true,
                Location = new Point(20, 80)
            };

            Label lblTimeLimit = new Label
            {
                Text = $"Time Limit: {videoData["TimeLimit"]}",
                ForeColor = Color.White,
                Font = new Font("Lucida Handwriting", 12F, FontStyle.Regular, GraphicsUnit.Point, 0),
                AutoSize = true,
                Location = new Point(20, 130)
            };

            Label lblPrice = new Label
            {
                Text = $"Price: {videoData["Price"]}",
                ForeColor = Color.White,
                Font = new Font("Lucida Handwriting", 12F, FontStyle.Regular, GraphicsUnit.Point, 0),
                AutoSize = true,
                Location = new Point(20, 180)
            };

            Button btnRent = new Button
            {
                Text = "Rent",
                ForeColor = Color.White,
                BackColor = SystemColors.ActiveCaption,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(150, 80),
                Location = new Point(100, 220)
            };
            //rent button click handler
            btnRent.Click += (sender, e) =>
            {
                try
                {
                    var rentalManager = new VideoRentalManager(videoRentals, userInfo);
                    rentalManager.RentVideo(videoData, userInfo);
                }
                catch (Exception ex)//error message
                {
                    MessageBox.Show("Error renting video: " + ex.Message);
                }
            };

            //assemble and show popup
            rightPanel.Controls.Add(lblTitle);
            rightPanel.Controls.Add(lblDescription);
            rightPanel.Controls.Add(lblTimeLimit);
            rightPanel.Controls.Add(lblPrice);
            rightPanel.Controls.Add(btnRent);
            popup.Controls.Add(rightPanel);

            popup.ShowDialog();
        }

        private void ProductsName_TextChanged(object sender, EventArgs e)
        {
            // Get the search query from the textbox (convert to lowercase for case-insensitive search)
            string searchQuery = ProductsName.Text.ToLower();

            // Clear the current display (reset the flowLayoutPanel)
            flowLayoutPanel.Controls.Clear();

            int cardCount = 0; // Reset the card count to limit the number of displayed cards

            // Loop through the video data and display cards based on search query
            foreach (KeyValuePair<string, object> entry in videoData)
            {
                if (cardCount >= 8) break; // Limit to 8 cards (or as per your need)

                if (entry.Value is CustomHashTable videoInfo)
                {
                    // Get the video title and check if it contains the search query
                    string videoTitle = videoInfo.Get("VideoTitle")?.ToString() ?? "Untitled";

                    // Check if the title contains the search query letter (case-insensitive)
                    if (!videoTitle.ToLower().StartsWith(searchQuery)) continue; // Skip if title doesn't contain the search query

                    // Create a new Panel to represent the video card
                    Panel videoCard = new Panel
                    {
                        Size = new Size(200, 300),
                        BackColor = Color.LightBlue,
                        Margin = new Padding(10),
                        Cursor = Cursors.Hand
                    };

                    // Create the PictureBox for the video image
                    PictureBox videoImageControl = new PictureBox
                    {
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Size = new Size(180, 250),
                        Location = new Point(10, 10),
                        Tag = videoInfo
                    };

                    // Access the video image path
                    string videoImageObj = videoInfo.Get("VideoPath")?.ToString() ?? "";
                    if (IsValidPath(videoImageObj)) // Make sure you have a valid path
                    {
                        try
                        {
                            videoImageControl.Image = Image.FromFile(videoImageObj); // Load image from file
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("Error loading image: " + ex.Message);
                        }
                    }

                    // Add Click event to show the video popup when clicked
                    videoImageControl.Click += (s, e) => ShowVideoPopup(videoInfo);

                    // Create a label for the video title
                    Label lblTitle = new Label
                    {
                        Text = videoTitle,
                        ForeColor = Color.White,
                        Font = new Font("Lucida Handwriting", 12F, FontStyle.Regular, GraphicsUnit.Point, 0),
                        AutoSize = true,
                        Location = new Point(10, 270),
                        Tag = videoInfo
                    };

                    // Add Click event for the label as well
                    lblTitle.Click += (s, e) => ShowVideoPopup(videoInfo);

                    // Add the PictureBox and Label to the video card (Panel)
                    videoCard.Controls.Add(videoImageControl);
                    videoCard.Controls.Add(lblTitle);

                    // Add the video card to the FlowLayoutPanel
                    flowLayoutPanel.Controls.Add(videoCard);

                    cardCount++; // Increment the card count for each added video
                }
            }
        }
        private void Categories_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected category from the ComboBox
            string selectedCategory = Categories.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedCategory)) return;

            // Filter and display videos based on the selected category
            DisplayVideosByCategory(selectedCategory);
        }
        private void DisplayVideosByCategory(string selectedCategory)
        {
            // Clear the current display (reset the flowLayoutPanel)
            flowLayoutPanel.Controls.Clear();

            int cardCount = 0; // Reset the card count to limit the number of displayed cards

            // Loop through the video data and display cards based on the selected category
            foreach (KeyValuePair<string, object> entry in videoData)
            {
                if (cardCount >= 8) break; // Limit to 8 cards (or as per your need)

                if (entry.Value is CustomHashTable videoInfo)
                {
                    // Get the video category
                    string videoCategory = videoInfo.Get("Genre")?.ToString() ?? "";

                    // Check if the video category matches the selected category
                    if (videoCategory.Equals(selectedCategory, StringComparison.OrdinalIgnoreCase))
                    {
                        // Create and add the video card (same as before)
                        CreateVideoCard(videoInfo);
                        cardCount++; // Increment the card count for each added video
                    }
                }
            }
        }
        private void CreateVideoCard(CustomHashTable videoInfo)
        {
            // Create a new Panel to represent the video card
            Panel videoCard = new Panel
            {
                Size = new Size(200, 300),
                BackColor = Color.LightBlue,
                Margin = new Padding(10),
                Cursor = Cursors.Hand
            };

            // Create the PictureBox for the video image
            PictureBox videoImageControl = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.StretchImage,
                Size = new Size(180, 250),
                Location = new Point(10, 10),
                Tag = videoInfo
            };

            // Access the video image path
            string videoImageObj = videoInfo.Get("VideoPath")?.ToString() ?? "";
            if (IsValidPath(videoImageObj))
            {
                try
                {
                    videoImageControl.Image = Image.FromFile(videoImageObj);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error loading image: " + ex.Message);
                }
            }

            // Add Click event to show the video popup when clicked
            videoImageControl.Click += (s, e) => ShowVideoPopup(videoInfo);

            // Create a label for the video title
            Label lblTitle = new Label
            {
                Text = videoInfo.Get("VideoTitle")?.ToString() ?? "Untitled",
                ForeColor = Color.White,
                Font = new Font("Lucida Handwriting", 12F, FontStyle.Regular, GraphicsUnit.Point, 0),
                AutoSize = true,
                Location = new Point(10, 270),
                Tag = videoInfo
            };

            // Add Click event for the label as well
            lblTitle.Click += (s, e) => ShowVideoPopup(videoInfo);

            // Add the PictureBox and Label to the video card (Panel)
            videoCard.Controls.Add(videoImageControl);
            videoCard.Controls.Add(lblTitle);

            // Add the video card to the FlowLayoutPanel
            flowLayoutPanel.Controls.Add(videoCard);
        }

        // manage video rental operations and database synchronisation
        public class VideoRentalManager
        {
            private CustomHashTable videoRentals;
            private CustomHashTable userInfo;
            private CustomHashTable rentalTimers = new CustomHashTable(10000);

            private string connectionString = "Server=NOOR\\SQLEXPRESS01;Database=VideoRentalSystem;Integrated Security=True;TrustServerCertificate=True;";

            public VideoRentalManager(CustomHashTable VideoRentals, CustomHashTable userInfo)
            {
                this.videoRentals = VideoRentals;
                this.userInfo = userInfo;
            }

            // Method to rent a video (updates only CustomHashTable)
            public void RentVideo(CustomHashTable videoData, CustomHashTable userInfo)
            {
                //validate and parse rental duration
                if (!videoData.ContainsKey("TimeLimit") || !int.TryParse(videoData["TimeLimit"]?.ToString(), out int timeLimit))
                {
                    //error message
                    MessageBox.Show("Invalid or missing time limit.");
                    return;
                }
                //create rental record
                string rentalKey = Guid.NewGuid().ToString();

                var rentalDetails = new CustomHashTable(10000)
                {
                    ["VideoID"] = videoData["VideoID"],
                    ["UserID"] = userInfo["UserID"],
                    ["VideoTitle"] = videoData["VideoTitle"],
                    ["RentalDate"] = DateTime.Now.ToString(),
                    ["TimeLimit"] = timeLimit.ToString(),
                    ["ReturnDate"] = DateTime.Now.AddSeconds(timeLimit).ToString(),
                    ["Status"] = "rented"
                };

                videoRentals[rentalKey] = rentalDetails;

                //start countdown timer for rental
                Timer rentalTimer = new Timer { Interval = 1000 };
                rentalTimer.Tick += (sender, e) => UpdateRentalTimer(rentalKey);
                rentalTimer.Start();

                rentalTimers[rentalKey] = rentalTimer;

                MessageBox.Show("Video rented successfully!");
            }
            //synchronises in-memory rentals with database
            public void SyncRentalsToDatabase()
            {
                // 1. Update in-database rentals that are still marked as "rented"
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string updateQuery = @"
                                            UPDATE VideoRentals
                                            SET TimeLimit = CASE 
                                                                WHEN DATEDIFF(SECOND, GETDATE(), ReturnDate) > 0 
                                                                THEN DATEDIFF(SECOND, GETDATE(), ReturnDate)
                                                                ELSE 0
                                                            END,
                                                Status = CASE 
                                                                WHEN GETDATE() >= ReturnDate THEN 'expired'
                                                                ELSE Status
                                                         END
                                            WHERE Status = 'rented'";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        int affectedRows = cmd.ExecuteNonQuery();
                    }
                }

                // 2. Synchronize the in-memory rentals with the database.
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    foreach (KeyValuePair<string, object> entry in videoRentals)
                    {
                        CustomHashTable rentalDetails = entry.Value as CustomHashTable;
                        if (rentalDetails == null)
                            continue;

                        // Use helper method for VideoID conversion to handle NULL values.
                        object videoIDObj = GetNullableInt(rentalDetails["VideoID"]);
                        object userIDObj = int.Parse(rentalDetails["UserID"].ToString());
                        object rentalDateObj = DateTime.Parse(rentalDetails["RentalDate"].ToString());
                        object timeLimitObj = int.Parse(rentalDetails["TimeLimit"].ToString());
                        string status = rentalDetails["Status"].ToString();
                        string videoTitle = rentalDetails["VideoTitle"].ToString();

                        // Existence check using composite key (handling NULL for VideoID)
                        string checkQuery = @"
                                                SELECT COUNT(*) 
                                                FROM VideoRentals
                                                WHERE ((@VideoID IS NULL AND VideoID IS NULL) OR (VideoID = @VideoID))
                                                  AND UserID = @UserID
                                                  AND RentalDate = @RentalDate";
                        using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                        {
                            checkCmd.Parameters.AddWithValue("@VideoID", videoIDObj ?? DBNull.Value);
                            checkCmd.Parameters.AddWithValue("@UserID", userIDObj);
                            checkCmd.Parameters.AddWithValue("@RentalDate", rentalDateObj);

                            int exists = Convert.ToInt32(checkCmd.ExecuteScalar());

                            if (exists == 0)
                            {
                                // Insert new rental if it doesn't exist.
                                string insertQuery = @"
                                                        INSERT INTO VideoRentals
                                                            (VideoID, UserID, VideoTitle, RentalDate, TimeLimit, Status)
                                                        VALUES 
                                                            (@VideoID, @UserID, @VideoTitle, @RentalDate, @TimeLimit, @Status)";
                                using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                                {
                                    insertCmd.Parameters.AddWithValue("@VideoID", videoIDObj ?? DBNull.Value);
                                    insertCmd.Parameters.AddWithValue("@UserID", userIDObj);
                                    insertCmd.Parameters.AddWithValue("@VideoTitle", videoTitle);
                                    insertCmd.Parameters.AddWithValue("@RentalDate", rentalDateObj);
                                    insertCmd.Parameters.AddWithValue("@TimeLimit", timeLimitObj);
                                    insertCmd.Parameters.AddWithValue("@Status", status);

                                    insertCmd.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                // Update the existing rental.
                                string updateQuery = @"
                                                        UPDATE VideoRentals
                                                        SET TimeLimit = @TimeLimit,
                                                            Status = @Status
                                                        WHERE ((@VideoID IS NULL AND VideoID IS NULL) OR (VideoID = @VideoID))
                                                          AND UserID = @UserID
                                                          AND RentalDate = @RentalDate";
                                using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                                {
                                    updateCmd.Parameters.AddWithValue("@TimeLimit", timeLimitObj);
                                    updateCmd.Parameters.AddWithValue("@Status", status);
                                    updateCmd.Parameters.AddWithValue("@VideoID", videoIDObj ?? DBNull.Value);
                                    updateCmd.Parameters.AddWithValue("@UserID", userIDObj);
                                    updateCmd.Parameters.AddWithValue("@RentalDate", rentalDateObj);

                                    updateCmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
            }

            private object GetNullableInt(object val)
            {
                if (val == null || val == DBNull.Value)
                    return DBNull.Value;
                string s = val.ToString();
                if (string.Equals(s, "NULL", StringComparison.OrdinalIgnoreCase) || string.IsNullOrWhiteSpace(s))
                    return DBNull.Value;
                if (int.TryParse(s, out int i))
                    return i;
                return DBNull.Value;
            }

            //update remaining time for a single rental
            private void UpdateRentalTimer(string rentalKey)
            {
                // Check if the rental exists in the hashtable.
                if (videoRentals.ContainsKey(rentalKey) && videoRentals[rentalKey] is CustomHashTable rental)
                {
                    // Parse the ReturnDate.
                    DateTime returnDate = DateTime.Parse(rental["ReturnDate"].ToString());
                    // Calculate the remaining time.
                    TimeSpan remaining = returnDate - DateTime.Now;

                    if (remaining.TotalSeconds > 0)
                    {
                        // Update the TimeLimit with the remaining seconds in memory.
                        rental["TimeLimit"] = ((int)remaining.TotalSeconds).ToString();
                        rental["Status"] = "rented";
                    }
                    else
                    {
                        // When time runs out, set TimeLimit to 0 and mark the rental as expired.
                        rental["TimeLimit"] = "0";
                        rental["Status"] = "expired";
                    }

                    // OPTIONAL: Immediately sync this single rental's updated TimeLimit to the DB
                    SyncSingleRentalToDatabase(rentalKey);
                }
            }

            private void SyncSingleRentalToDatabase(string rentalKey)
            {
                if (!videoRentals.ContainsKey(rentalKey)) return;
                CustomHashTable rental = videoRentals[rentalKey] as CustomHashTable;
                if (rental == null) return;

                // Parse fields from the in-memory rental
                object videoIDObj = GetNullableInt(rental["VideoID"]);
                object userIDObj = int.Parse(rental["UserID"].ToString());
                object rentalDateObj = DateTime.Parse(rental["RentalDate"].ToString());
                object timeLimitObj = int.Parse(rental["TimeLimit"].ToString());
                string status = rental["Status"].ToString();
                // No ReturnDate, since it's computed in your table.

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Just do an UPDATE; we assume the row already exists in VideoRentals.
                    // If you need to handle "row doesn't exist," replicate the existence check from SyncRentalsToDatabase.
                    string updateQuery = @"
                                            UPDATE VideoRentals
                                            SET TimeLimit = @TimeLimit,
                                                Status = @Status
                                            WHERE ((@VideoID IS NULL AND VideoID IS NULL) OR (VideoID = @VideoID))
                                              AND UserID = @UserID
                                              AND RentalDate = @RentalDate";

                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@TimeLimit", timeLimitObj);
                        updateCmd.Parameters.AddWithValue("@Status", status);
                        updateCmd.Parameters.AddWithValue("@VideoID", videoIDObj ?? DBNull.Value);
                        updateCmd.Parameters.AddWithValue("@UserID", userIDObj);
                        updateCmd.Parameters.AddWithValue("@RentalDate", rentalDateObj);

                        updateCmd.ExecuteNonQuery();
                    }
                }
            }
            // 1. Add this to VideoRentalManager:
            public void UpdateAllRentalTimersInDB()
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string updateQuery = @"
                                            UPDATE VideoRentals
                                            SET TimeLimit = CASE 
                                                                WHEN DATEDIFF(SECOND, GETDATE(), ReturnDate) > 0 
                                                                THEN DATEDIFF(SECOND, GETDATE(), ReturnDate)
                                                                ELSE 0
                                                            END,
                                                Status = CASE
                                                            WHEN GETDATE() >= ReturnDate THEN 'expired'
                                                            ELSE 'rented'
                                                         END
                                        ";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        int affectedRows = cmd.ExecuteNonQuery();
                        MessageBox.Show($"{affectedRows} rentals updated in the database.");
                    }
                }
            }

            public void ClearVideoDatabaseAndUnlinkChildren()
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        string unlinkQuery = @"
                                                UPDATE VR
                                                SET VR.VideoID = NULL
                                                FROM [VideoRentalSystem].[dbo].[VideoRentals] VR
                                                INNER JOIN [VideoRentalSystem].[dbo].[VideoDatabase] VD
                                                    ON VR.VideoID = VD.VideoID;
                                            ";

                        using (SqlCommand unlinkCmd = new SqlCommand(unlinkQuery, conn))
                        {
                            int unlinkRows = unlinkCmd.ExecuteNonQuery();
                        }

                        // Now clear the VideoDatabase table.
                        //string clearQuery = "DELETE FROM [VideoRentalSystem].[dbo].[VideoDatabase];";
                        //using (SqlCommand clearCmd = new SqlCommand(clearQuery, conn))
                        //{
                        //    int rowsAffected = clearCmd.ExecuteNonQuery();
                        //}
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error clearing VideoDatabase: " + ex.Message);
                }
            }
        }
        private void Main_Load(object sender, EventArgs e)
        {

        }

        //search button to display another window that displays the list 
        private void SearchButtonFunction(object sender, EventArgs e)
        {
            SearchForm search = new SearchForm(userInfo,videoData, videoRentals);
            search.Show();
            this.Hide();
        }
        //move to profile page
        private void button7_Click(object sender, EventArgs e)
        {
            ProfilePage profile = new ProfilePage(userInfo, videoData, videoRentals);
            profile.Show();
            this.Hide();
        }
    }
}

