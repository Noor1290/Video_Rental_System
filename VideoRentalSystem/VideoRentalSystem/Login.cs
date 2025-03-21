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
            string connectionString = "Server=NOOR\\SQLEXPRESS;Database=VideoRentingSystem;Integrated Security=True;TrustServerCertificate=True;";

            await using var conn = new SqlConnection(connectionString);
            try
            {
                await conn.OpenAsync();

                string userQuery = "SELECT UserID, Username, Email, Password, ProfilePic FROM Users WHERE Username COLLATE Latin1_General_BIN = @Username AND Password = @Password";

                await using var userCmd = conn.CreateCommand();
                userCmd.CommandText = userQuery;
                userCmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                userCmd.Parameters.AddWithValue("@Password", txtPassword.Text);

                await using var userReader = await userCmd.ExecuteReaderAsync();
                if (await userReader.ReadAsync())
                {
                    // Store user data in the userInfo object (no need for profile_pic here)
                    userInfo["UserID"] = userReader["UserID"].ToString();
                    userInfo["Username"] = userReader["Username"].ToString();
                    userInfo["Email"] = userReader["Email"].ToString();
                    userInfo["Password"] = userReader["Password"].ToString();
                    // Optionally store profile_pic in case you need it later
                    byte[] profilePicData = userReader["ProfilePic"] as byte[];
                    if (profilePicData != null)
                    {
                        // You can store or ignore the profile_pic data here
                        userInfo["ProfilePic"] = profilePicData; // Store the binary data if needed
                    }

                }
                else
                {
                    lblMessage.Text = "Invalid username or password.";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                string videoDataFilePath = "test2.txt";
                Debug.WriteLine("Entered into ImportVideoDatabaseFromTxt");
                if (!File.Exists(videoDataFilePath))
                {
                    Console.WriteLine($"File {videoDataFilePath} not found in {Environment.CurrentDirectory}");
                    return;
                }
                Debug.WriteLine("Entered into ImportVideoDatabaseFromTxt");
                await ImportVideoDatabaseFromTxt(videoDataFilePath, connectionString);
                Debug.WriteLine("Entered into ImportVideoDatabaseFromTxt");

                var (videoData, uploads, videoRentals) = await LoadVideoDataAsync(userInfo["UserID"].ToString(), connectionString);

                Main mainPage = new Main(userInfo, videoData, videoRentals, uploads);
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
            // Check if the path contains typical file path characters
            // such as directory separators or drive letters (e.g., C:\)
            return path.Contains("\\") || path.Contains("/");
        }


        private async Task ImportVideoDatabaseFromTxt(string filePath, string connectionString)
        {
            Debug.WriteLine("=== Starting ImportVideoDatabaseFromTxt ===");
            Debug.WriteLine($"Current Directory: {Environment.CurrentDirectory}");
            Debug.WriteLine($"Looking for file: {filePath}");

            if (!File.Exists(filePath))
            {
                Debug.WriteLine($"File {filePath} not found in {Environment.CurrentDirectory}");
                return;
            }

            // Read all lines from the text file.
            string[] lines = await File.ReadAllLinesAsync(filePath);
            Debug.WriteLine($"File read successfully. Total lines: {lines.Length}");

            if (lines.Length < 4)
            {
                Debug.WriteLine("The file is empty or missing required header/data lines.");
                return;
            }

            // Open the SQL connection.
            await using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    await conn.OpenAsync();
                    Debug.WriteLine("Database connection opened successfully.");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error opening database connection: " + ex.Message);
                    return;
                }

                // File layout:
                // Line 0: Header row (column names)
                // Line 1: Separator (dashed line)
                // Lines 2 to (n-2): Data rows
                // Last line: Summary (e.g., "(5 rows affected)")
                for (int i = 2; i < lines.Length - 1; i++)
                {
                    Debug.WriteLine("---------------------------------------------------");
                    string line = lines[i].Trim();
                    Debug.WriteLine($"Processing line {i}: '{line}'");
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        Debug.WriteLine($"Line {i} is empty or whitespace. Skipping.");
                        continue;
                    }

                    // Split on two or more whitespace characters.
                    string[] tokens = System.Text.RegularExpressions.Regex.Split(line, @"\s{2,}");
                    Debug.WriteLine($"Line {i} split into {tokens.Length} tokens.");
                    for (int j = 0; j < tokens.Length; j++)
                    {
                        Debug.WriteLine($"  Token[{j}]: '{tokens[j]}'");
                    }

                    if (tokens.Length != 7)  // Correct token count should be 8 now
                    {
                        Debug.WriteLine($"Skipping malformed line {i} (expected 8 tokens, got {tokens.Length}): '{line}'");
                        continue;
                    }

                    string userId = tokens[1].Trim(); // may be "NULL"
                    string videoTitle = tokens[2].Trim();

                    // Process the combined UploadDate and TimeLimit in token[3].
                    string combinedDateTime = tokens[3].Trim();
                    string uploadDateStr;
                    string timeLimitStr;
                    int lastSpaceInDate = combinedDateTime.LastIndexOf(' ');
                    if (lastSpaceInDate > 0)
                    {
                        uploadDateStr = combinedDateTime.Substring(0, lastSpaceInDate);
                        timeLimitStr = combinedDateTime.Substring(lastSpaceInDate + 1);
                    }
                    else
                    {
                        uploadDateStr = combinedDateTime;
                        timeLimitStr = "";
                    }

                    string priceStr = tokens[4].Trim();

                    // Token[5] contains Genre
                    string genre = tokens[5].Trim();

                    // Token[7] contains the videoPath
                    string videoPath = tokens[6].Trim();

                    Debug.WriteLine("Parsed values:");
                    Debug.WriteLine($"  UserId: {userId}");
                    Debug.WriteLine($"  VideoTitle: {videoTitle}");
                    Debug.WriteLine($"  UploadDateStr: {uploadDateStr}");
                    Debug.WriteLine($"  TimeLimitStr: {timeLimitStr}");
                    Debug.WriteLine($"  PriceStr: {priceStr}");
                    Debug.WriteLine($"  Genre: {genre}");
                    Debug.WriteLine($"  VideoPath: {videoPath}");

                    string insertQuery = @"
                                            INSERT INTO VideoDatabase
                                                (UserID, VideoTitle, UploadDate, TimeLimit, Price, Genre, videoPath)
                                            VALUES
                                                (@UserID, @VideoTitle, @UploadDate, @TimeLimit, @Price, @Genre, @videoPath);
                                        ";

                    try
                    {
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

                            // Ignore VideoImage (do not insert it) by not including it in the query
                            cmd.Parameters.AddWithValue("@Genre", genre);
                            cmd.Parameters.AddWithValue("@videoPath", videoPath);

                            int rowsAffected = await cmd.ExecuteNonQueryAsync();
                            Debug.WriteLine($"Inserted video '{videoTitle}'. Rows affected: {rowsAffected}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Exception during insertion for line {i}: {ex.Message}");
                    }
                }
            }

            Debug.WriteLine("=== Import process completed ===");
        }




        private async Task<(CustomHashTable, CustomHashTable, CustomHashTable)> LoadVideoDataAsync(string userId, string connectionString)
        {
            CustomHashTable videoData = new CustomHashTable(10000);
            CustomHashTable uploads = new CustomHashTable(10000);
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
                            videoDetails.Add("videoPath", videoReader["videoPath"]);
                            var videoImageValue = videoReader["videoPath"];
                            Debug.WriteLine($"Type: {videoImageValue.GetType().Name}");
                            Debug.WriteLine($"Type: {videoImageValue}");

                            // Check the length if it's a string or byte array
                            if (videoImageValue is string videoImageString)
                            {
                                Debug.WriteLine($"Length of string: {videoImageString.Length}");

                                // Check if the string looks like a file path
                                if (IsValidPath(videoImageString))
                                {
                                    Debug.WriteLine("This is a valid file path.");
                                }
                                else
                                {
                                    Debug.WriteLine("This is a string, but not a valid file path.");
                                }
                            }
                            else if (videoImageValue is byte[] videoImageBytes)
                            {
                                Debug.WriteLine($"Length of bytes array: {videoImageBytes.Length}");
                            }
                            else
                            {
                                Debug.WriteLine("Unknown type or unsupported type for length checking.");
                            }





                            videoDetails.Add("Price", videoReader["Price"].ToString());
                            videoDetails.Add("Genre", videoReader["Genre"].ToString());

                            // Now store the entire videoDetails in the videoData hashtable
                            videoData[videoReader["VideoID"].ToString()] = videoDetails;

                            Console.WriteLine($"Stored video with ID: {videoReader["VideoTitle"].ToString()}");
                        }
                    }
                }


                // Get user-specific videos (uploads)
                string userVideoQuery = "SELECT * FROM VideoDatabase WHERE UserID = @UserID";
                using (SqlCommand userVideoCmd = new SqlCommand(userVideoQuery, conn))
                {
                    userVideoCmd.Parameters.AddWithValue("@UserID", userId);
                    using (SqlDataReader userVideoReader = await userVideoCmd.ExecuteReaderAsync()) // Make this async
                    {
                        try
                        {
                            while (await userVideoReader.ReadAsync()) // Make this async
                            {
                                // Create CustomHashTable for each upload record
                                var uploadDetails = new CustomHashTable(10000);

                                uploadDetails.Add("VideoID", userVideoReader["VideoID"].ToString());
                                uploadDetails.Add("UserID", userVideoReader["UserID"].ToString());
                                uploadDetails.Add("VideoTitle", userVideoReader["VideoTitle"].ToString());
                                uploadDetails.Add("UploadDate", userVideoReader["UploadDate"].ToString());
                                uploadDetails.Add("TimeLimit", userVideoReader["TimeLimit"].ToString());
                                uploadDetails.Add("videoPath", userVideoReader["videoPath"]);
                                uploadDetails.Add("Price", userVideoReader["Price"].ToString());
                                uploadDetails.Add("Genre", userVideoReader["Genre"].ToString());

                                // Store uploadDetails in uploads using VideoId as the key
                                uploads[userVideoReader["VideoID"].ToString()] = uploadDetails;
                                Console.WriteLine($"Stored upload with ID: {userVideoReader["VideoID"].ToString()}");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
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

            return (videoData, uploads, videoRentals); // Returning all three Hashtables
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
    }
}
