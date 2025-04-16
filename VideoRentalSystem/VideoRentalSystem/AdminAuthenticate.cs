namespace VideoRentalSystem
{
    internal class AdminAuthenticate
    {
        public class AdminAuthenticator : IAuthenticator
        {
            public bool Authenticate(string username, string password)
            {
                string correctUsername = "admin";
                string correctPassword = "admin123";

                return username == correctUsername && password == correctPassword;
            }
        }

    }
}
