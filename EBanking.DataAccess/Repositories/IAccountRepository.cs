using EBanking.DataAccess.Models;

namespace EBanking.DataAccess.Repositories
{
    public interface IAccountRepository
    {
        void CreateAccount(Account model);
        List<Account> GetAccountsByUserId(int userId);
        Task<Account> GetAccountByAccountNumber(string accountNumber);
        void UpdateBalance(double balance, string accountNumber);
        bool IsValidAccount(string accountNumber);
    }
}
