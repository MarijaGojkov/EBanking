namespace EBanking.DataAccess.Repositories
{
    public interface IUserRepository
    {
        Models.User GetUserById(int id);

        int IsValidUser(string email, string password);

        List<Models.User> GetAllUsers();

        int AddUser(Models.User user);

        void UpdateUserPassword(string email, string userPin, string password);

        bool VerifyUser(string email, string userPin);
    }
}
