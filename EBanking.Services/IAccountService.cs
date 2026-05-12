using EBanking.Services.Models;

namespace EBanking.Services
{
    public interface IAccountService
    {
        List<AccountModel> GetAccountsForUser(int userId);
        void UpdateBalance(decimal newBalance, string accountNumber);
        bool IsValidAccount(string accountNumber);
        AccountModel GetAccountByAccountNumber(string accountNumber);
    }
}
