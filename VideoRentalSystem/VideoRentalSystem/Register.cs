using System;
using System.Drawing;
using System.Windows.Forms;

namespace VideoRentalSystem
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
            ErrorTextImage.Text = "";
            EmailErrorMessage.Text = "";
            PasswordErrorMessage.Text = "";
            UsernameErrorMessage.Text = "";
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
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            dialog.Multiselect = false;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try 
                { 
                    string picPath = dialog.FileName.ToString();
                    pictureBox1.ImageLocation = picPath;
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                catch (Exception ex)
                {
                    ErrorTextImage.Text = "An error occured while uploading the image";
                }
            }

        }

        private void Close_Click(object sender, EventArgs e)
        {
           Login login = new Login();
            login.Show();
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

            if(IsPasswordValid(password))
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
    }
}
