using EBanking.DataAccess.Repositories;

namespace EBanking.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public int Login(string email, string password)
        {
            return _userRepository.IsValidUser(email, password);
        }

        public bool VerifyUser(string email, string userPin)
        {
            return _userRepository.VerifyUser(email, userPin);
        }

        public void UpdateUserPassword(string email, string userPin, string password)
        {
            _userRepository.UpdateUserPassword(email, userPin, password);
        }
    }
}
