#nullable disable
using System;
using System.Data.Common;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Diagnostics;


namespace VideoRentalSystem
{
    public partial class Register : Form
    {
        private DatabaseConnection dbConnection;
        private byte[] profilePictureData;

        public Register()
        {
            InitializeComponent();
            ErrorTextImage.Text = "";
            EmailErrorMessage.Text = "";
            PasswordErrorMessage.Text = "";
            UsernameErrorMessage.Text = "";
            dbConnection = new DatabaseConnection("VANSHIKA", "VideoTestDatabase");
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Delete_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;

        }

        private void Upload_Click(object sender, EventArgs e)
        {
            try
            {

                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;";
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Load the selected image
                        Image originalImage = Image.FromFile(openFileDialog.FileName);

                        // Resize the image to fit PictureBox
                        pictureBox1.Image = ResizeImage(originalImage, pictureBox1.Width, pictureBox1.Height);
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage; // Ensures the image fills the box

                        // Convert image to byte array
                        profilePictureData = File.ReadAllBytes(openFileDialog.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage("Error uploading image: " + ex.Message, Color.Red);
            }
        }
        private Image ResizeImage(Image img, int width, int height)
        {
            Bitmap resizedImage = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(resizedImage))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(img, 0, 0, width, height);
            }
            return resizedImage;
        }


        private void Close_Click(object sender, EventArgs e)
        {
            WelcomeForm welcome = new WelcomeForm();
            welcome.Show();
            this.Hide();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Address_Text(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void Submit_Click(object sender, EventArgs e)
        {
            bool isValid = true;
            string email = EmailTextBox.Text;

            if (IsValidEmail(email))
            {
                EmailErrorMessage.Text = "";
            }
            else
            {
                EmailErrorMessage.Text = "Invalid Email";
                EmailErrorMessage.ForeColor = Color.Red;
                isValid = false;
            }

            string password = PasswordTextBox.Text;

            if (IsPasswordValid(password))
            {
                PasswordErrorMessage.Text = "";
            }
            else
            {
                PasswordErrorMessage.Text = "Invalid Password";
                PasswordErrorMessage.ForeColor = Color.Red;
                isValid = false;
            }

            string username = UsernameTextBox.Text;

            if (username.Length < 5)
            {
                UsernameErrorMessage.Text = "Must be 5 characters at most";
                UsernameErrorMessage.ForeColor = Color.Red;
                isValid = false;
            }
            else
            {
                UsernameErrorMessage.Text = "";
            }

            if (isValid)
            {
                Login login = new Login();
                login.Show();
                this.Hide();

            }
            else
            {
                return;
            }
            try
            {
                dbConnection.InsertUser(username, email, password, profilePictureData);
                DisplayMessage("Registered successfully!", Color.Green);
            }
            catch (SqlException sqlEx)
            {
                // Log sqlEx if needed
                UsernameErrorMessage.Text = "SQL Error: " + sqlEx.Message;
                UsernameErrorMessage.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                // Log ex if needed
                DisplayMessage("Error: " + ex.Message, Color.Red);
            }



        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private static bool IsPasswordValid(string password)
        {
            const int MIN_LENGTH = 5;
            const int MAX_LENGTH = 10;

            if (password == null) throw new ArgumentNullException();

            bool meetsLengthRequirements = password.Length >= MIN_LENGTH && password.Length <= MAX_LENGTH;
            bool hasUpperCaseLetter = false;
            bool hasLowerCaseLetter = false;
            bool hasDecimalDigit = false;

            if (meetsLengthRequirements)
            {
                foreach (char c in password)
                {
                    if (char.IsUpper(c)) hasUpperCaseLetter = true;
                    else if (char.IsLower(c)) hasLowerCaseLetter = true;
                    else if (char.IsDigit(c)) hasDecimalDigit = true;
                }
            }

            bool isPasswordStrong = meetsLengthRequirements
                        && hasUpperCaseLetter
                        && hasLowerCaseLetter
                        && hasDecimalDigit
                        ;
            return isPasswordStrong;

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void UsernameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click_1(object sender, EventArgs e)
        {

        }
        private void DisplayMessage(string message, Color color)
        {
            ErrorTextImage.Text = message;  // Ensure ErrorTextImage is a Label in your form
            ErrorTextImage.ForeColor = color;
        }
    }
    public class DatabaseConnection
    {
        private string connectionString;

        public DatabaseConnection(string server, string database)
        {
            connectionString = $"Server={server};Database={database};Integrated Security=True;TrustServerCertificate=True;";
        }

        public void InsertUser(string username, string email, string password, byte[] profilePicture)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {

                    // Now insert
                    string query = @"
                    INSERT INTO Users (Username, Email, Password, ProfilePic) 
                    VALUES (@Username, @Email, @Password, @ProfilePic)";

                    using (SqlCommand command = new SqlCommand(query, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", password);
                        command.Parameters.AddWithValue("@ProfilePic", profilePicture);  // Insert image as byte array
                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    Debug.WriteLine("User registered successfully!");
                }
                catch (SqlException sqlEx)
                {
                    Debug.WriteLine("SQL Error: " + sqlEx.Message);
                    Debug.WriteLine("SQL Error Number: " + sqlEx.Number);
                    Debug.WriteLine("SQL Error Line Number: " + sqlEx.LineNumber);

                    transaction.Rollback();
                    throw new Exception("Database insert failed: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error: " + ex.Message);
                }
            }
        }
    }
}
