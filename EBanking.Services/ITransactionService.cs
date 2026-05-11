using EBanking.Services.Models;

namespace EBanking.Services
{
    public interface ITransactionService
    {
        void CreateTransaction(TransactionModel transactionModel);
        List<TransactionModel> GetTransactionsByAccountNumber(string accountNumber);
    }
}
