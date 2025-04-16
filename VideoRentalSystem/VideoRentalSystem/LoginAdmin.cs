namespace VideoRentalSystem
{
    public partial class LoginAdmin : Form
    {
        private readonly IAuthenticator _authenticator;
        private readonly IVideoDataLoader _videoDataLoader;

        public LoginAdmin(IAuthenticator authenticator, IVideoDataLoader videoDataLoader)
        {
            _authenticator = authenticator;
            _videoDataLoader = videoDataLoader;
            InitializeComponent();
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            string enteredUsername = txtUsername.Text.Trim();
            string enteredPassword = txtPassword.Text.Trim();

            // Reset labels
            lblMessage.Text = "";
            lblPassword.Text = "";

            // Authentication
            if (!_authenticator.Authenticate(enteredUsername, enteredPassword))
            {
                lblMessage.Text = "Invalid username or password.";
                lblMessage.ForeColor = Color.Red;
                return;
            }

            lblMessage.Text = "Login successful!";
            lblMessage.ForeColor = Color.Green;

            // Load video data
            string connectionString = "Server=NOOR\\SQLEXPRESS01;Database=VideoRentalSystem;Integrated Security=True;TrustServerCertificate=True;";
            CustomHashTable videoData = await _videoDataLoader.LoadVideoDataAsync(connectionString);

            // Show Admin Main page
            AdminMain main = new AdminMain(videoData);
            main.Show();
            this.Hide();
        }

        private void GoBackBtn_Click_1(object sender, EventArgs e)
        {
            WelcomeAdmin admin = new WelcomeAdmin();
            admin.Show();
            this.Close();
        }
    }
}
