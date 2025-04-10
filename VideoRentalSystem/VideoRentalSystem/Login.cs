#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using VideoRentalSystem;


namespace VideoRentalSystem
{

    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public partial class Login : Form
    {
        // Hashtable to store user information after successful login
        private CustomHashTable userInfo = new CustomHashTable(10000);

        public Login()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.Show();
            this.Hide();

        }

        private void Close_CLick(object sender, EventArgs e)
        {
            Close();
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            Reset reset = new Reset();
            reset.Show();
            this.Hide();

        }

        private async void Login_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=NOOR\\SQLEXPRESS01;Database=VideoRentalSystem;Integrated Security=True;TrustServerCertificate=True;";

            await using var conn = new SqlConnection(connectionString);
            try
            {
                await conn.OpenAsync();

                // First, authenticate the user
                string userQuery = "SELECT UserID, Username, Email, Password, ProfilePic FROM Users WHERE Username COLLATE Latin1_General_BIN = @Username";

                // Use the 'using' block to ensure resources are disposed properly
                await using var userCmd = conn.CreateCommand();
                userCmd.CommandText = userQuery;
                userCmd.Parameters.AddWithValue("@Username", txtUsername.Text);

                // Execute the query and read the result
                await using var userReader = await userCmd.ExecuteReaderAsync();

                bool isValidUser = false;
                string passwordFromDb = string.Empty;

                if (await userReader.ReadAsync())
                {
                    // Check if username exists
                    isValidUser = true;
                    passwordFromDb = userReader["Password"].ToString(); // Get the password from the DB
                }

                if (!isValidUser)
                {
                    // If the username is invalid
                    lblMessage.Text = "Invalid username.";
                    lblMessage.ForeColor = Color.Red;
                    lblPassword.Text = "";  // Clear the password-related error message
                    return;
                }

                // If username exists, now check for password
                if (txtPassword.Text != passwordFromDb)
                {
                    // If the password is invalid
                    lblPassword.Text = "Invalid password.";
                    lblPassword.ForeColor = Color.Red;
                    lblMessage.Text = "";  // Clear the username-related error message
                    return;
                }

                // If both username and password are valid
                userInfo["UserID"] = userReader["UserID"].ToString();
                userInfo["Username"] = userReader["Username"].ToString();
                userInfo["Email"] = userReader["Email"].ToString();
                userInfo["Password"] = userReader["Password"].ToString();

                byte[] profilePicData = userReader["ProfilePic"] as byte[];
                if (profilePicData != null)
                {
                    userInfo["ProfilePic"] = profilePicData; // Store the binary data if needed
                }

                userReader.Close(); // Close the DataReader

                // Now, check if a file path is provided
                string videoDataFilePath = TextFile.Text.Trim(); // Get the file path entered by the user

                // If no file is uploaded, check if there is data in the database
                if (string.IsNullOrWhiteSpace(videoDataFilePath)) // No file path provided
                {
                    string checkDatabaseQuery = "SELECT COUNT(*) FROM VideoDatabase";
                    await using var dbCmd = new SqlCommand(checkDatabaseQuery, conn);
                    int rowsInDatabase = (int)await dbCmd.ExecuteScalarAsync();

                    if (rowsInDatabase == 0)
                    {
                        lblTextFile.Text = "The database is empty. Please upload a valid file.";
                        lblTextFile.ForeColor = Color.Red;
                        return;
                    }
                }
                else // File path provided, process the uploaded file
                {
                    await ImportVideoDatabaseFromTxt(videoDataFilePath, connectionString);
                }

                // Load video data from the database for the current user
                var (videoData, videoRentals) = await LoadVideoDataAsync(userInfo["UserID"].ToString(), connectionString);

                // Show main page
                Main mainPage = new Main(userInfo, videoData, videoRentals);
                mainPage.Show();
                this.Hide();

                lblMessage.Text = "Login successful! Redirecting...";
                lblMessage.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Database error: " + ex.Message;
                lblMessage.ForeColor = Color.Red;
            }
        }



        // Helper method to convert a hex string (e.g. "0xFFD8FFE0...") to a byte array.
        public static byte[] HexStringToByteArray(string hex)
        {
            if (string.IsNullOrEmpty(hex))
                return null;

            // Remove 0x prefix if present.
            if (hex.StartsWith("0x", StringComparison.InvariantCultureIgnoreCase))
            {
                hex = hex.Substring(2);
            }

            int numberChars = hex.Length;
            byte[] bytes = new byte[numberChars / 2];
            for (int i = 0; i < numberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }
        // Helper method to check if the string looks like a valid file path
        public static bool IsValidPath(string path)
        {

            return path.Contains("\\") || path.Contains("/");
        }


        private async Task ImportVideoDatabaseFromTxt(string filePath, string connectionString)
        {
            if (!File.Exists(filePath))
            {
                return;
            }

            // Read all lines from the text file.
            string[] lines = await File.ReadAllLinesAsync(filePath);
            if (lines.Length < 4)
            {
                return;
            }

            // Open the SQL connection.
            await using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    await conn.OpenAsync();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error opening database connection: " + ex.Message);
                    return;
                }

                int totalExistingRows = 0; // Counter for existing rows

                for (int i = 2; i < lines.Length - 1; i++)
                {
                    string line = lines[i].Trim();
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    // Split on two or more whitespace characters.
                    string[] tokens = System.Text.RegularExpressions.Regex.Split(line, @"\s{2,}");
                    if (tokens.Length != 8)  // Correct token count should be 8 now
                    {
                        continue;
                    }

                    string userId = tokens[1].Trim(); // may be "NULL"
                    string videoTitle = tokens[2].Trim();
                    string combinedDateTime = tokens[3].Trim();
                    string uploadDateStr;
                    string DurationStr;
                    int lastSpaceInDate = combinedDateTime.LastIndexOf(' ');

                    if (lastSpaceInDate > 0)
                    {
                        uploadDateStr = combinedDateTime.Substring(0, lastSpaceInDate);
                        DurationStr = combinedDateTime.Substring(lastSpaceInDate + 1);
                    }
                    else
                    {
                        uploadDateStr = combinedDateTime;
                        DurationStr = "";
                    }

                    string timeLimitStr = tokens[4].Trim();
                    string priceStr = tokens[5].Trim();
                    string genre = tokens[6].Trim();
                    string videoPath = tokens[7].Trim();

                    // Log the values to check for consistency in format
                    Debug.WriteLine($"Checking for duplicate: VideoTitle={videoTitle}, Duration={DurationStr}");

                    // Check if the data already exists (excluding UserID, checking VideoTitle and Duration)
                    string checkQuery = @"
                                    SELECT COUNT(1) 
                                    FROM VideoDatabase 
                                    WHERE VideoTitle = @VideoTitle AND Duration = @Duration;
                                ";

                    try
                    {
                        await using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                        {
                            checkCmd.Parameters.AddWithValue("@VideoTitle", videoTitle.Trim().ToLower()); // Case-insensitive comparison
                            checkCmd.Parameters.AddWithValue("@Duration", DurationStr);

                            // Execute the query and retrieve the count
                            var result = await checkCmd.ExecuteScalarAsync();

                            // If result is DBNull, treat it as 0
                            int existingRecords = result != DBNull.Value ? Convert.ToInt32(result) : 0;

                            // Debug statement to show how many duplicates are found
                            Debug.WriteLine($"Found {existingRecords} duplicate(s) for VideoTitle={videoTitle} with Duration={DurationStr}.");

                            if (existingRecords > 0)  // Data exists, display a message
                            {
                                totalExistingRows += existingRecords; // Increment the counter
                                Debug.WriteLine($"Record(s) already exists for VideoTitle={videoTitle} with Duration={DurationStr}. Skipping insertion.");
                            }
                            else  // Data does not exist, proceed to insert
                            {
                                string insertQuery = @"
                            INSERT INTO VideoDatabase
                                (UserID, VideoTitle, UploadDate, Duration, TimeLimit, Price, Genre, VideoPath)
                            VALUES
                                (@UserID, @VideoTitle, @UploadDate, @Duration, @TimeLimit, @Price, @Genre, @VideoPath);
                        ";

                                await using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                                {
                                    // Handle UserID: if the token is "NULL" (or empty), use DBNull.Value.
                                    if (string.Equals(userId, "NULL", StringComparison.OrdinalIgnoreCase) || string.IsNullOrWhiteSpace(userId))
                                    {
                                        cmd.Parameters.AddWithValue("@UserID", DBNull.Value);
                                    }
                                    else if (int.TryParse(userId, out int userIdInt))
                                    {
                                        cmd.Parameters.AddWithValue("@UserID", userIdInt);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@UserID", DBNull.Value);
                                    }

                                    cmd.Parameters.AddWithValue("@VideoTitle", videoTitle);
                                    if (DateTime.TryParse(uploadDateStr, out DateTime uploadDate))
                                    {
                                        cmd.Parameters.AddWithValue("@UploadDate", uploadDate);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@UploadDate", DBNull.Value);
                                    }

                                    if (int.TryParse(timeLimitStr, out int timeLimit))
                                    {
                                        cmd.Parameters.AddWithValue("@TimeLimit", timeLimit);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@TimeLimit", DBNull.Value);
                                    }

                                    if (decimal.TryParse(priceStr, out decimal price))
                                    {
                                        cmd.Parameters.AddWithValue("@Price", price);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@Price", DBNull.Value);
                                    }

                                    cmd.Parameters.AddWithValue("@Duration", DurationStr);
                                    cmd.Parameters.AddWithValue("@Genre", genre);
                                    cmd.Parameters.AddWithValue("@VideoPath", videoPath);

                                    int rowsAffected = await cmd.ExecuteNonQueryAsync();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Exception during check or insertion for line {i}: {ex.Message}");
                    }
                }

                // After processing, display how many rows already exist
                if (totalExistingRows > 0)
                {
                    Debug.WriteLine($"{totalExistingRows} row(s) were already present in the database. Skipping those rows.");
                }
                else
                {
                    Debug.WriteLine("All data was successfully inserted into the database.");
                }
            }

            Debug.WriteLine("=== Import process completed ===");
        }









        private async Task<(CustomHashTable, CustomHashTable)> LoadVideoDataAsync(string userId, string connectionString)
        {
            CustomHashTable videoData = new CustomHashTable(10000);
            CustomHashTable videoRentals = new CustomHashTable(10000);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync(); // Make this async

                // Get all videos
                string videoQuery = "SELECT * FROM VideoDatabase";
                using (SqlCommand videoCmd = new SqlCommand(videoQuery, conn))
                {
                    using (SqlDataReader videoReader = await videoCmd.ExecuteReaderAsync()) // Make this async
                    {
                        while (await videoReader.ReadAsync())
                        {
                            var videoDetails = new CustomHashTable(10000);

                            videoDetails.Add("VideoID", videoReader["VideoID"].ToString());
                            videoDetails.Add("UserID", videoReader["UserID"].ToString());
                            videoDetails.Add("VideoTitle", videoReader["VideoTitle"].ToString());
                            videoDetails.Add("UploadDate", videoReader["UploadDate"].ToString());
                            videoDetails.Add("TimeLimit", videoReader["TimeLimit"].ToString());
                            videoDetails.Add("Duration", videoReader["Duration"].ToString());
                            videoDetails.Add("VideoPath", videoReader["VideoPath"]);
                            videoDetails.Add("Price", videoReader["Price"].ToString());
                            videoDetails.Add("Genre", videoReader["Genre"].ToString());

                            // Now store the entire videoDetails in the videoData hashtable
                            videoData[videoReader["VideoID"].ToString()] = videoDetails;

                        }
                    }
                }

                // Get user-specific rentals
                string rentalQuery = "SELECT * FROM VideoRentals WHERE UserID = @UserID";
                using (SqlCommand rentalCmd = new SqlCommand(rentalQuery, conn))
                {
                    rentalCmd.Parameters.AddWithValue("@UserID", userId);
                    using (SqlDataReader rentalReader = await rentalCmd.ExecuteReaderAsync()) // Make this async
                    {
                        // Assuming you have already initialized videoRentals

                        try
                        {
                            // Assuming rentalReader is already set up for the rental query
                            while (await rentalReader.ReadAsync())
                            {
                                // Create CustomHashTable for each rental record
                                var rentalDetails = new CustomHashTable(10000);
                                rentalDetails.Add("RentalID", rentalReader["RentalID"].ToString());
                                rentalDetails.Add("UserID", rentalReader["UserID"].ToString());
                                rentalDetails.Add("VideoTitle", rentalReader["VideoTitle"].ToString());
                                rentalDetails.Add("RentalDate", rentalReader["RentalDate"].ToString());
                                rentalDetails.Add("TimeLimit", rentalReader["TimeLimit"].ToString());
                                rentalDetails.Add("ReturnDate", rentalReader["ReturnDate"].ToString());
                                rentalDetails.Add("Status", rentalReader["Status"].ToString());
                                rentalDetails.Add("VideoID", rentalReader["VideoID"].ToString());

                                // Store rentalDetails in videoRentals using RentalId as the key
                                videoRentals[rentalReader["RentalID"].ToString()] = rentalDetails;
                                Console.WriteLine($"Stored rental with ID: {rentalReader["RentalID"].ToString()}");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }

                    }
                }
            }

            return (videoData, videoRentals);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lblMessage_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void TextFile_TextChanged(object sender, EventArgs e)
        {
            string filePath = TextFile.Text.Trim(); // Get and trim any leading/trailing spaces from the input

            // Clear previous messages in lblTextFile
            lblTextFile.Text = "";
            lblTextFile.ForeColor = Color.Black; // Reset text color to black

            if (string.IsNullOrWhiteSpace(filePath)) // Check if the file path is empty or just spaces
            {
                lblTextFile.Text = "Please enter a valid file path."; // Error message
                lblTextFile.ForeColor = Color.Red; // Set text color to red for error
                return;
            }

            // Check if the file exists at the specified location
            if (File.Exists(filePath))
            {
                lblTextFile.Text = "File is valid and accessible!"; // Success message
                lblTextFile.ForeColor = Color.Green; // Set text color to green for success
            }
            else
            {
                lblTextFile.Text = "File does not exist. Please check the path and try again."; // Error message
                lblTextFile.ForeColor = Color.Red; // Set text color to red for error
            }
        }


    }
}
