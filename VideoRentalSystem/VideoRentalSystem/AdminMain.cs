//imports
using System.Diagnostics;
using Microsoft.Data.SqlClient;

namespace VideoRentalSystem
{
    public partial class AdminMain : Form
    {
        //store video data
        private CustomHashTable videoData;

        //initialises admin main with curretn video data
        public AdminMain(CustomHashTable videoData)
        {
            InitializeComponent();
            this.videoData = videoData;
        }
        private void AdminMain_Load(object sender, EventArgs e)
        {
            
        }

        //opens add video form and closes current admin interface
        private void button2_Click(object sender, EventArgs e)
        {
            AddVideo addVideo = new AddVideo(videoData);
            addVideo.Show();
            this.Close(); // release cuurent form resources
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                

                // Ensure that video data is saved before deleting or modifying anything
                LogoutAndDeleteFromDatabase();
                // Save video data to the database first
                SaveVideoDataToDatabase();

                // Provide feedback after successful operation
                MessageBox.Show("Logged out successfully, and video data has been saved!");

                // Close or hide the current form and show the login page
                this.Hide();
                // Initialize the dependencies
                IAuthenticator authenticator = new AdminAuthenticate.AdminAuthenticator();  // Create the authenticator instance
                IVideoDataLoader videoDataLoader = new VideoDataLoader(); // Create the video data loader instance

                // Pass dependencies to LoginAdmin
                LoginAdmin loginForm = new LoginAdmin(authenticator, videoDataLoader);
                loginForm.Show();
            }
            catch (Exception ex)
            {
                // Handle errors here, such as logging the error
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        // Method to save videos to the database
        private void SaveVideoDataToDatabase()
        {
            //connection string for SQL server
            string connectionString = "Server=NOOR\\SQLEXPRESS01;Database=VideoRentalSystem;Integrated Security=True;TrustServerCertificate=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open(); // Open the SQL connection synchronously

                // Loop through each entry in the videoData CustomHashTable
                foreach (KeyValuePair<string, object> entry in videoData)
                {

                    // Get the video details
                    CustomHashTable videoDetails = (CustomHashTable)entry.Value;
                   


                    // Extract the details from the videoDetails CustomHashTable
                    string videoID = videoDetails["VideoID"].ToString();
                    string videoTitle = videoDetails["VideoTitle"].ToString();
                    string duration = videoDetails["Duration"].ToString();
                    string timeLimit = videoDetails["TimeLimit"].ToString();
                    string price = videoDetails["Price"].ToString();
                    string genre = videoDetails["Genre"].ToString();
                    string userID = videoDetails["UserID"].ToString(); // It will be "NULL" or actual user ID
                    string uploadDate = videoDetails["UploadDate"].ToString();
                    string videoPath = videoDetails["VideoPath"].ToString();

                    // Set userID properly if it's "NULL" or empty
                    object userIdValue = string.IsNullOrEmpty(userID) || userID == "NULL" ? DBNull.Value : (object)userID;
               

                    // Insert or update video data in the database
                    string query = @"
                                    IF EXISTS (SELECT 1 FROM VideoDatabase WHERE VideoID = @VideoID)
                                    BEGIN
                                        UPDATE VideoDatabase 
                                        SET VideoTitle = @VideoTitle, Duration = @Duration, TimeLimit = @TimeLimit, 
                                            Price = @Price, Genre = @Genre, UserID = @UserID, UploadDate = @UploadDate, 
                                            VideoPath = @VideoPath
                                        WHERE VideoID = @VideoID;
                                    END
                                    ELSE
                                    BEGIN
                                        INSERT INTO VideoDatabase (VideoTitle, Duration, TimeLimit, Price, Genre, UserID, UploadDate, VideoPath)
                                        VALUES (@VideoTitle, @Duration, @TimeLimit, @Price, @Genre, @UserID, @UploadDate, @VideoPath);
                                    END";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        // Add parameters to the command
                        cmd.Parameters.AddWithValue("@VideoID", videoID);
                        cmd.Parameters.AddWithValue("@VideoTitle", videoTitle);
                        cmd.Parameters.AddWithValue("@Duration", duration);
                        cmd.Parameters.AddWithValue("@TimeLimit", timeLimit);
                        cmd.Parameters.AddWithValue("@Price", price);
                        cmd.Parameters.AddWithValue("@Genre", genre);

                        // Check if the UserID is "NULL" (i.e., no user is assigned), set it to DBNull.Value
                        var userIdValue1 = string.IsNullOrEmpty(userID) || userID == "NULL" ? (object)DBNull.Value : userID;
                        cmd.Parameters.AddWithValue("@UserID", userIdValue1);
                        cmd.Parameters.AddWithValue("@UploadDate", uploadDate);
                        cmd.Parameters.AddWithValue("@VideoPath", videoPath);


                        // Execute the command synchronously
                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch (SqlException sqlEx)
                        {
                            Debug.WriteLine($"SQL Error: {sqlEx.Message}");
                            Debug.WriteLine($"SQL Error Code: {sqlEx.Number}, Line Number: {sqlEx.LineNumber}");
                        }
                    }
                }
            }
        }


        // Method to handle logout and delete any videos from the database that are no longer in the videoData hashtable
        private void LogoutAndDeleteFromDatabase()
        {
            string connectionString = "Server=NOOR\\SQLEXPRESS01;Database=VideoRentalSystem;Integrated Security=True;TrustServerCertificate=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open(); // Open the SQL connection synchronously

                // Get all video IDs from the database
                string getVideoIDsQuery = "SELECT VideoID FROM VideoDatabase";
                using (SqlCommand getCmd = new SqlCommand(getVideoIDsQuery, connection))
                {
                    using (SqlDataReader reader = getCmd.ExecuteReader())
                    {
                        // Create a list to store the VideoIDs to delete after reading
                        List<string> videoIDsToDelete = new List<string>();

                        // Read all video IDs from the database
                        while (reader.Read())
                        {
                            string videoID = reader["VideoID"].ToString();

                            // Check if the video is in the videoData hashtable
                            if (!videoData.ContainsKey(videoID))
                            {
                                // If the video is not in the hashtable, add to the delete list
                                videoIDsToDelete.Add(videoID);
                            }
                        }

                        // Close the reader before executing any more SQL commands
                        reader.Close();

                        // Now delete all videos that were not found in the hashtable
                        foreach (string videoID in videoIDsToDelete)
                        {
                            string deleteQuery = "DELETE FROM VideoDatabase WHERE VideoID = @VideoID";
                            using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, connection))
                            {
                                deleteCmd.Parameters.AddWithValue("@VideoID", videoID);
                                deleteCmd.ExecuteNonQuery();
                                Debug.WriteLine($"Deleted video with VideoID: {videoID} from the database.");
                            }
                        }
                    }
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            DeleteVideo delete = new DeleteVideo(videoData);
            delete.Show();
            this.Hide(); //keeps admin instance for potential return
        }
    }
}
