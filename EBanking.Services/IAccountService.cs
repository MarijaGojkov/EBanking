using EBanking.Services.Models;

namespace EBanking.Services
{
    public interface IAccountService
    {
        List<AccountModel> GetAccountsForUser(int userId);
        void UpdateBalance(double newBalance, string accountNumber);
        bool IsValidAccount(string accountNumber);
        Task<AccountModel> GetAccountByAccountNumber(string accountNumber);
    }
}
