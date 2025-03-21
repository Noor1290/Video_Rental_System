#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            string connectionString = "Server=DESKTOP-6GUDQI9;Database=VideoRentingSystem;Integrated Security=True;TrustServerCertificate=True;";

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
                            videoDetails.Add("VideoId", videoReader["VideoId"].ToString());
                            videoDetails.Add("UserId", videoReader["UserId"].ToString());
                            videoDetails.Add("VideoTitle", videoReader["VideoTitle"].ToString());
                            videoDetails.Add("UploadDate", videoReader["UploadDate"].ToString());
                            videoDetails.Add("TimeLimit", videoReader["TimeLimit"].ToString());
                            videoDetails.Add("VideoImage", videoReader["VideoImage"].ToString());
                            videoDetails.Add("Price", videoReader["Price"].ToString());
                            videoDetails.Add("Genre", videoReader["Genre"].ToString());


                            videoData[videoReader["VideoId"].ToString()] = videoDetails;
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
                                uploadDetails.Add("VideoImage", userVideoReader["VideoImage"].ToString());
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
                    rentalCmd.Parameters.AddWithValue("@UserId", userId);
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
                                rentalDetails.Add("RentalID", rentalReader["RentalId"].ToString());
                                rentalDetails.Add("UserID", rentalReader["UserId"].ToString());
                                rentalDetails.Add("VideoTitle", rentalReader["VideoTitle"].ToString());
                                rentalDetails.Add("RentalDate", rentalReader["RentalDate"].ToString());
                                rentalDetails.Add("TimeLimit", rentalReader["TimeLimit"].ToString());
                                rentalDetails.Add("ReturnDate", rentalReader["ReturnDate"].ToString());
                                rentalDetails.Add("Status", rentalReader["Status"].ToString());
                                rentalDetails.Add("VideoID", rentalReader["VideoUrl"].ToString());

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
