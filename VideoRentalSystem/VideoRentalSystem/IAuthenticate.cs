namespace VideoRentalSystem
{
    public interface IAuthenticator
    {
        bool Authenticate(string username, string password);
    }
}
