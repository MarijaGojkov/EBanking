namespace EBanking.Services
{
    public interface IUserService
    {
        int Login(string email, string password);

        void UpdateUserPassword(string email, string userPin, string password);

        bool VerifyUser(string email, string userPin);
    }
}
