//imports for debugging
using System.Diagnostics;


namespace VideoRentalSystem
{
    public partial class AddVideo : Form
    {
        //Custom hash table to store video data.
        private CustomHashTable videoData;

        public AddVideo(CustomHashTable videoData)
        {
            InitializeComponent();
            this.videoData = videoData;//store reference to shared video data
            InitializeCustomUI();
        }

        // Method to initialize custom UI elements with style
        private void InitializeCustomUI()
        {
            // Set the form properties for better appearance
            this.Text = "Add Video - Admin";
            this.Size = new Size(600, 500);
            this.BackColor = Color.FromArgb(240, 240, 240); // Light grey background
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            // Create Labels
            Label lblTitle = CreateLabel("Add New Video", new Point(200, 20), 20, FontStyle.Bold);
            Label lblVideoTitle = CreateLabel("Video Title:", new Point(50, 80), 12, FontStyle.Regular);
            Label lblDuration = CreateLabel("Duration:", new Point(50, 180), 12, FontStyle.Regular);
            Label lblTimeLimit = CreateLabel("Time Limit:", new Point(50, 230), 12, FontStyle.Regular);
            Label lblPrice = CreateLabel("Price :", new Point(50, 280), 12, FontStyle.Regular);
            Label lblGenre = CreateLabel("Genre:", new Point(50, 330), 12, FontStyle.Regular);
            lblMessage = CreateLabel("", new Point(50, 380), 12, FontStyle.Regular);
            lblMessage.ForeColor = Color.Green;

            // Create Textboxes
            TextBox txtVideoTitle = CreateTextBox(new Point(200, 80));
            TextBox txtDuration = CreateTextBox(new Point(200, 180));
            TextBox txtTimeLimit = CreateTextBox(new Point(200, 230));
            TextBox txtPrice = CreateTextBox(new Point(200, 280));

            // Create ComboBox for Genre
            ComboBox cmbGenre = new ComboBox
            {
                Location = new Point(200, 330),
                Width = 200
            };
            cmbGenre.Items.AddRange(new object[] { "Action", "Adventure", "Animation", "Apocalyptic", "Biopic", "Comedy", "Crime", "Cyborg", "Default", "Documentary", "Drama", "Fantasy", "Fiction", "Horror", "Mindbearer", "Mystery", "Outlaw", "Political", "Psychological", "Romance", "Thriller" });

            // Create Buttons for Add Video action
            Button addButton = CreateButton("Add Video", new Point(50, 420));

            // Add event handlers for the button
            addButton.Click += (sender, e) => AddVideo_Click(sender, e, txtVideoTitle, txtDuration, txtTimeLimit, txtPrice, cmbGenre);

            // Add controls to the form
            this.Controls.Add(lblTitle);
            this.Controls.Add(lblVideoTitle);
            this.Controls.Add(lblDuration);
            this.Controls.Add(lblTimeLimit);
            this.Controls.Add(lblPrice);
            this.Controls.Add(lblGenre);
            this.Controls.Add(lblMessage);
            this.Controls.Add(txtVideoTitle);
            this.Controls.Add(txtDuration);
            this.Controls.Add(txtTimeLimit);
            this.Controls.Add(txtPrice);
            this.Controls.Add(cmbGenre);
            this.Controls.Add(addButton);
        }

        // Helper method to create labels
        private Label CreateLabel(string text, Point location, int fontSize, FontStyle fontStyle)
        {
            return new Label
            {
                Text = text,
                Location = location,
                Font = new Font("Arial", fontSize, fontStyle),
                AutoSize = true,
                ForeColor = Color.Black
            };
        }

        // Helper method to create textboxes
        private TextBox CreateTextBox(Point location)
        {
            return new TextBox
            {
                Location = location,
                Size = new Size(200, 25),
                Font = new Font("Arial", 10)
            };
        }

        // Helper method to create buttons
        private Button CreateButton(string text, Point location)
        {
            return new Button
            {
                Text = text,
                Location = location,
                Size = new Size(120, 40),
                BackColor = Color.LightSkyBlue,
                Font = new Font("Arial", 12, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White
            };
        }

        // Add video button click event
        private void AddVideo_Click(object sender, EventArgs e, TextBox txtVideoTitle, TextBox txtDuration, TextBox txtTimeLimit, TextBox txtPrice, ComboBox cmbGenre)
        {
            string videoTitle = txtVideoTitle.Text.Trim();
            string duration = txtDuration.Text.Trim();
            string timeLimit = txtTimeLimit.Text.Trim();
            string price = txtPrice.Text.Trim();
            string genre = cmbGenre.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(videoTitle) || string.IsNullOrEmpty(duration) || string.IsNullOrEmpty(timeLimit) || string.IsNullOrEmpty(price) || string.IsNullOrEmpty(genre))
            {
                lblMessage.Text = "All fields must be filled!";
                lblMessage.ForeColor = Color.Red;
                return;
            }

            // Check if the video already exists in the hashtable by VideoTitle
            foreach (KeyValuePair<string, object> entry in videoData)
            {
                // Renamed variable to avoid conflict
                CustomHashTable existingVideoDetails = (CustomHashTable)entry.Value;

                // Check if the VideoTitle matches
                if (existingVideoDetails["VideoTitle"].ToString() == videoTitle)
                {
                    lblMessage.Text = "Video with this title already exists!";
                    lblMessage.ForeColor = Color.Red;
                    return; // If video exists, stop and show error message
                }
            }

            // Generate a simple VideoID based on the last SQL VideoID or incremental logic
            int newVideoID = GetNextVideoID();  // Method to get the next VideoID (incremental logic)

            // Set UserID as NULL
            string userID = "NULL"; // As you specified, UserID should be recorded as NULL

            // Record UploadDate as the current time
            string uploadDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // Set VideoPath based on the Genre
            string videoPath = GetVideoPathForGenre(genre);

            // Add new video to the hash table
            CustomHashTable videoDetails = new CustomHashTable(10000);
            videoDetails.Add("VideoID", newVideoID.ToString());  // VideoID should be added as a string
            videoDetails.Add("VideoTitle", videoTitle);
            videoDetails.Add("Duration", duration);
            videoDetails.Add("TimeLimit", timeLimit);
            videoDetails.Add("Price", price);
            videoDetails.Add("Genre", genre);
            videoDetails.Add("UserID", userID);
            videoDetails.Add("UploadDate", uploadDate);
            videoDetails.Add("VideoPath", videoPath);

            // Debugging: Log the video details before adding to the hashtable
            Debug.WriteLine("Adding the following video to the hashtable:");
            Debug.WriteLine($"VideoID: {newVideoID}, Title: {videoTitle}, Duration: {duration}, TimeLimit: {timeLimit}, Price: {price}, Genre: {genre}");
            Debug.WriteLine($"UserID: {userID}, UploadDate: {uploadDate}, VideoPath: {videoPath}");

            // Add the new video to the videoData hashtable
            string videoID = newVideoID.ToString();
            videoData.Add(videoID, videoDetails);

            lblMessage.Text = $"Video '{videoTitle}' added successfully!";
            lblMessage.ForeColor = Color.Green;

            AdminMain admin = new AdminMain(videoData);
            admin.Show();
            this.Hide();
        }

        // Method to get the next VideoID based on the last used VideoID (sequential logic)
        private int GetNextVideoID()
        {

            // Otherwise, find the highest VideoID in the videoData hash table
            int maxVideoID = 0;

            foreach (KeyValuePair<string, object> entry in videoData)
            {
                // Get the video details for each entry in the hashtable
                CustomHashTable videoDetails = (CustomHashTable)entry.Value;

                // Parse the VideoID and find the highest one
                if (videoDetails.ContainsKey("VideoID"))
                {
                    int currentVideoID = Convert.ToInt32(videoDetails["VideoID"].ToString());
                    maxVideoID = Math.Max(maxVideoID, currentVideoID);
                }
            }

            // Return the next VideoID (incremented by 1)
            return maxVideoID + 1;
        }


        // Method to get the VideoPath based on Genre
        private string GetVideoPathForGenre(string genre)
        {
            // Map the genres to their corresponding paths (as per the table provided in your screenshot)
            Dictionary<string, string> genreToPath = new Dictionary<string, string>
    {
        {"Crime", "Image/Crime.jpeg" },
        { "Biopic", "Image/Biopic.jpeg" },
        { "Action", "Image/Action.jpeg" },
        { "Comedy", "Image/Comedy.jpeg" },
        { "Cyborg", "Image/Cyborg.jpeg" },
        { "Outlaw", "Image/Outlaw.jpeg" },
        { "Adventure", "Image/Adventure.jpeg" },
        { "Animation", "Image/Animation.jpeg" },
        { "Apocalyptic", "Image/Apocalyptic.jpeg" },
        { "Default", "Image/Default.jpg" },
        { "Documentary", "Image/Documentary.jpeg" },
        { "Drama", "Image/Drama.jpeg" },
        { "Fantasy", "Image/Fantasy.jpeg" },
        { "Fiction", "Image/Fiction.jpeg" },
        { "Horror", "Image/Horror.jpeg" },
        { "Mindbearer", "Image/Mindbearer.jpeg" },
        { "Mystery", "Image/Mystery.jpeg" },
        { "Political", "Image/Political.jpg" },
        { "Psychological", "Image/Psychological.jpeg" },
        { "Romance", "Image/Romance.jpeg" },
        { "Thriller", "Image/Thriller.jpeg" }
        };

            // Return the corresponding path for the selected genre, or a default path
            return genreToPath.ContainsKey(genre) ? genreToPath[genre] : "Image/Default.jpg";
        }


        private Label lblMessage; // To show the success/error message
    }
}
