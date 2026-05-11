using EBanking.DataAccess.Models;

namespace EBanking.DataAccess.Repositories
{
    public interface ITransactionRepository
    {
        int CreateTransaction(Transaction transaction);
        List<Transaction> GetTransactionsByAccountNumber(string accountNumber);
    }
}
