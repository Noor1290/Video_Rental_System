//imports
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
        //database connection handler and profile picture storage
        private DatabaseConnection dbConnection;
        private byte[] profilePictureData;

        public Register()
        {
            InitializeComponent();
            //initialises error messages
            ErrorTextImage.Text = "";
            EmailErrorMessage.Text = "";
            PasswordErrorMessage.Text = "";
            UsernameErrorMessage.Text = "";
            //create database connection with SQL server
            dbConnection = new DatabaseConnection("NOOR\\SQLEXPRESS01", "VideoRentalSystem");
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }
        //clears the profile picture from form
        private void Delete_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;

        }
        //handles image upload and processing
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
            catch (Exception ex) // error message
            {
                DisplayMessage("Error uploading image: " + ex.Message, Color.Red);
            }
        }
        //to resize image
        private Image ResizeImage(Image img, int width, int height)
        {
            //create a new bitmap with target dimensions
            Bitmap resizedImage = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(resizedImage))
            {
                //high quality image scaling
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(img, 0, 0, width, height);
            }
            return resizedImage;
        }

        //close registration and return to welcome screen
        private void Close_Click(object sender, EventArgs e)
        {
            WelcomeForm welcome = new WelcomeForm();
            welcome.Show();
            this.Hide();

        }

        // Modify the Submit_Click method to use the DoesUserExist method.
        private void Submit_Click(object sender, EventArgs e)
        {
            bool isValid = true;
            string email = EmailTextBox.Text;
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Text;

            // Validate email format
            if (IsValidEmail(email))
            {
                EmailErrorMessage.Text = "";  // Clear error message if email is valid
            }
            else
            {
                EmailErrorMessage.Text = "Email should be unique, contains @ and . symbols";
                EmailErrorMessage.ForeColor = Color.Red;
                isValid = false;  // Set isValid to false if email is invalid
            }

            // Validate if the email already exists in the database
            if (isValid && dbConnection.DoesUserExistByEmail(email))
            {
                EmailErrorMessage.Text = "Email is already registered!";
                EmailErrorMessage.ForeColor = Color.Red;
                isValid = false;  // Set isValid to false if email already exists
            }

            // Validate username length
            if (username.Length < 5)
            {
                UsernameErrorMessage.Text = "Username must be at least 5 characters.";
                UsernameErrorMessage.ForeColor = Color.Red;
                isValid = false;
            }
            else
            {
                UsernameErrorMessage.Text = "";  // Clear error message if username is valid
            }

            // Validate if the username already exists in the database
            if (isValid && dbConnection.DoesUserExistByUsername(username))
            {
                UsernameErrorMessage.Text = "Username already exists!";
                UsernameErrorMessage.ForeColor = Color.Red;
                isValid = false;  // Set isValid to false if username already exists
            }

            // Validate password complexity
            if (IsPasswordValid(password))
            {
                PasswordErrorMessage.Text = "";  // Clear error message if password is valid
            }
            else
            {
                PasswordErrorMessage.Text = "Password should contain\n1 Uppercase, 1 Lowercase,\n1 decimal digit and\n be 5-10 characters long.";
                PasswordErrorMessage.ForeColor = Color.Red;
                isValid = false;  // Set isValid to false if password is invalid
            }

            // Proceed with registration if all validations pass
            if (isValid)
            {
                try
                {
                    dbConnection.InsertUser(username, email, password, profilePictureData);
                    DisplayMessage("Registered successfully!", Color.Green);
                    Login login = new Login();
                    login.Show();
                    this.Hide();
                }
                catch (SqlException sqlEx)
                {
                    DisplayMessage("SQL Error: " + sqlEx.Message, Color.Red);
                }
                catch (Exception ex)
                {
                    DisplayMessage("Error: " + ex.Message, Color.Red);
                }
            }
            else
            {
                return;
            }
        }


        //email validation
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
        //check password complexity
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
        //universal message display method
        private void DisplayMessage(string message, Color color)
        {
            ErrorTextImage.Text = message;  // Ensure ErrorTextImage is a Label in your form
            ErrorTextImage.ForeColor = color;
        }
    }
    //database interaction class
    public class DatabaseConnection
    {
        private string connectionString;
        //builds conneciton string from server/database name
        public DatabaseConnection(string server, string database)
        {
            connectionString = $"Server={server};Database={database};Integrated Security=True;TrustServerCertificate=True;";
        }
        //safe user insertion method with transaction handling
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
                catch (Exception ex)// error message
                {
                    transaction.Rollback();
                    throw new Exception("Error: " + ex.Message);
                }
            }
        }
        public bool DoesUserExistByUsername(string username)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
            SELECT COUNT(*) FROM Users
            WHERE Username = @Username";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    int userCount = (int)command.ExecuteScalar();
                    return userCount > 0;  // Returns true if a user with the same username exists
                }
            }
        }

        public bool DoesUserExistByEmail(string email)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
            SELECT COUNT(*) FROM Users
            WHERE Email = @Email";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    int userCount = (int)command.ExecuteScalar();
                    return userCount > 0;  // Returns true if a user with the same email exists
                }
            }
        }
    }
}
