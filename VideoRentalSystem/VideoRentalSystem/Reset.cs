//imports
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

namespace VideoRentalSystem
{
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public partial class Reset : Form
    {
        public Reset()
        {
            InitializeComponent();
        }
        //navigate back to login form
        private void Login_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();//hide current form rather than closing it
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Reset_button_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string newPassword = txtPassword.Text.Trim();
            string confirmNewPassword = txtNewPassword.Text.Trim();

            // Validate inputs
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmNewPassword))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            if (!UserExists(username))
            {
                MessageBox.Show("User does not exist.");
                return;
            }

            if (newPassword != confirmNewPassword)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            // Optional: Validate password strength (e.g., length check)
            if (newPassword.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters long.");
                return;
            }


            // Update the password in the database
            if (UpdatePassword(username, newPassword))
            {
                MessageBox.Show("Password has been successfully reset!");
            }
            else
            {
                MessageBox.Show("Error resetting the password.");
            }
        }

        //real-time password validation
        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            // Capture the new password
            string newPassword = txtPassword.Text.Trim();

            // Optional: You can add validation for the password here, e.g., check length
            if (newPassword.Length < 6)
            {
                lblPasswordValidation.Text = "Password must be at least 6 characters long.";
                lblPasswordValidation.ForeColor = Color.Red;
            }
            else
            {
                lblPasswordValidation.Text = ""; // Clear the message if valid
            }
        }
        //password confirmation check
        private void txtNewPassword_TextChanged(object sender, EventArgs e)
        {
            // Capture the confirm new password
            string confirmNewPassword = txtNewPassword.Text.Trim();

            // Check if the passwords match
            string newPassword = txtPassword.Text.Trim();

            if (newPassword != confirmNewPassword)
            {
                lblPasswordMatchValidation.Text = "Passwords do not match!";
                lblPasswordMatchValidation.ForeColor = Color.Red;
            }
            else
            {
                lblPasswordMatchValidation.Text = "Passwords match.";
                lblPasswordMatchValidation.ForeColor = Color.Green;
            }
        }
        //username verification
        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            // Capture the username entered by the user
            string username = txtUsername.Text.Trim();

            // Check if the username exists in the database
            if (string.IsNullOrEmpty(username))
            {
                lblUsernameValidation.Text = "Please enter a username.";
                lblUsernameValidation.ForeColor = Color.Red;
            }
            else
            {
                if (UserExists(username))
                {
                    lblUsernameValidation.Text = "User exists.";
                    lblUsernameValidation.ForeColor = Color.Green;
                }
                else
                {
                    lblUsernameValidation.Text = "User does not exist.";
                    lblUsernameValidation.ForeColor = Color.Red;
                }
            }
        }

        // Method to check if the username exists in the database
        private bool UserExists(string username)
        {
            string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
            using (SqlConnection conn = new SqlConnection("Server=NOOR\\SQLEXPRESS01;Database=VideoRentalSystem;Integrated Security=True;TrustServerCertificate=True;"))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    int result = (int)cmd.ExecuteScalar();

                    return result > 0; // If the result is greater than 0, the user exists
                }
            }
        }
        // Method to update the password in the database
        private bool UpdatePassword(string username, string newPassword)
        {
            string query = "UPDATE Users SET Password = @NewPassword WHERE Username = @Username";
            using (SqlConnection conn = new SqlConnection("Server=NOOR\\SQLEXPRESS01;Database=VideoRentalSystem;Integrated Security=True;TrustServerCertificate=True;"))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@NewPassword", newPassword); // In practice, hash the password before storing it
                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0; // If rows were affected, the password was updated successfully
                }
            }
        }

    }
}
