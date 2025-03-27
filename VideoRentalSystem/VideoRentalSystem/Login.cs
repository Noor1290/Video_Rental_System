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
            string connectionString = "Server=VANSHIKA;Database=VideoTestDatabase;Integrated Security=True;TrustServerCertificate=True;";

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

                string videoDataFilePath = "database.txt";

                await ImportVideoDatabaseFromTxt(videoDataFilePath, connectionString);


                var (videoData, videoRentals) = await LoadVideoDataAsync(userInfo["UserID"].ToString(), connectionString);

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

                    // Process the combined UploadDate and TimeLimit in token[3].
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

                    // Token[5] contains Genre
                    string genre = tokens[6].Trim();


                    string videoPath = tokens[7].Trim();

                    string insertQuery = @"
                                            INSERT INTO VideoDatabase
                                                (UserID, VideoTitle, UploadDate, Duration, TimeLimit, Price, Genre, VideoPath)
                                            VALUES
                                                (@UserID, @VideoTitle, @UploadDate, @Duration, @TimeLimit, @Price, @Genre, @VideoPath);
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

                            cmd.Parameters.AddWithValue("@Duration", DurationStr);
                            cmd.Parameters.AddWithValue("@Genre", genre);
                            cmd.Parameters.AddWithValue("@videoPath", videoPath);

                            int rowsAffected = await cmd.ExecuteNonQueryAsync();
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
    }
}
