using EBanking.DataAccess.Models;

namespace EBanking.DataAccess.Repositories
{
    public interface ITransactionRepository
    {
        int CreateTransaction(Transaction transaction);
        List<Transaction> GetTransactionsByAccountNumber(string accountNumber);
        void ExecuteTransfer(
            string payerAccountNumber,
            string recipientAccountNumber,
            decimal amount,
            string recipientName,
            string payerFullName,
            DateTime occurredAt);
        void ExecuteExchange(
            string sourceAccountNumber,
            string destinationAccountNumber,
            decimal sourceAmount,
            decimal destinationAmount,
            string userFullName,
            DateTime occurredAt);
    }
}
