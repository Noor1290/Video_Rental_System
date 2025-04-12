//imports
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace VideoRentalSystem
{
    public partial class LoginAdmin : Form
    {
        public LoginAdmin()
        {
            InitializeComponent();
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            string correctUsername = "admin";
            string correctPassword = "admin123"; // Replace with your desired password

            string enteredUsername = txtUsername.Text.Trim();
            string enteredPassword = txtPassword.Text.Trim();

            // Reset labels
            lblMessage.Text = "";
            lblPassword.Text = "";

            if (enteredUsername != correctUsername)
            {
                lblMessage.Text = "Invalid username.";
                lblMessage.ForeColor = Color.Red;
                Debug.WriteLine("Invalid username entered.");
                return; // Stop further validation if username is incorrect
            }

            if (enteredPassword != correctPassword)
            {
                lblPassword.Text = "Invalid password.";
                lblPassword.ForeColor = Color.Red;
                Debug.WriteLine("Invalid password entered.");
                return; // Stop further validation if password is incorrect
            }

            lblMessage.Text = "Login successful!";
            lblMessage.ForeColor = Color.Green;

            // Now load video data asynchronously
            string connectionString = "Server=NOOR\\SQLEXPRESS01;Database=VideoRentalSystem;Integrated Security=True;TrustServerCertificate=True;";

            CustomHashTable videoData = await LoadVideoDataAsync(connectionString);

            AdminMain main = new AdminMain(videoData);
            main.Show();
            this.Hide();

            // Continue to the main page or other actions after successful login
        }


        // Asynchronously load video data from the database and store it in the hash table
        private async Task<CustomHashTable> LoadVideoDataAsync(string connectionString)
        {
            Debug.WriteLine("Loading video data from the database...");

            CustomHashTable videoData = new CustomHashTable(10000);

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync(); // Open connection asynchronously
                    Debug.WriteLine("Database connection opened.");

                    // Get all videos
                    string videoQuery = "SELECT * FROM VideoDatabase";
                    Debug.WriteLine($"Executing query: {videoQuery}");

                    using (SqlCommand videoCmd = new SqlCommand(videoQuery, conn))
                    {
                        using (SqlDataReader videoReader = await videoCmd.ExecuteReaderAsync()) // Asynchronously execute query
                        {
                            Debug.WriteLine("Reading video data from the database...");

                            while (await videoReader.ReadAsync())
                            {
                                var videoDetails = new CustomHashTable(10000);

                                // Populate the videoDetails hash table with the data from the database
                                videoDetails.Add("VideoID", videoReader["VideoID"].ToString());
                                videoDetails.Add("UserID", videoReader["UserID"].ToString());
                                videoDetails.Add("VideoTitle", videoReader["VideoTitle"].ToString());
                                videoDetails.Add("UploadDate", videoReader["UploadDate"].ToString());
                                videoDetails.Add("TimeLimit", videoReader["TimeLimit"].ToString());
                                videoDetails.Add("Duration", videoReader["Duration"].ToString());
                                videoDetails.Add("VideoPath", videoReader["VideoPath"].ToString());
                                videoDetails.Add("Price", videoReader["Price"].ToString());
                                videoDetails.Add("Genre", videoReader["Genre"].ToString());

                                // Now store the entire videoDetails in the videoData hashtable
                                videoData.Add(videoReader["VideoID"].ToString(), videoDetails);
                            }
                        }
                    }
                }
            }
            //error messages
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading video data: {ex.Message}");
            }

            Debug.WriteLine("Finished loading video data.");
            return videoData;//return populated hash table
        }

        private void GoBackBtn_Click_1(object sender, EventArgs e)
        {
            WelcomeAdmin admin = new WelcomeAdmin();
            admin.Show();//show welcome page
            this.Close();//close login form
        }
    }
}
