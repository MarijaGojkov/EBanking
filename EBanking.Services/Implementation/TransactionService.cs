using EBanking.DataAccess.Models;
using EBanking.DataAccess.Repositories;
using EBanking.Services.Models;

namespace EBanking.Services.Implementation
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public void CreateTransaction(TransactionModel transactionModel)
        {
            _transactionRepository.CreateTransaction(new Transaction
            {
                CardNumber = transactionModel.CardNumber ?? "/",
                AccountNumber = transactionModel.AccountNumber,
                SecondaryPartyAccountNumber = transactionModel.SecondaryPartyAccountNumber,
                SecondaryPartyName = transactionModel.SecondaryPartyName,
                BalanceAfterTransaction = transactionModel.BalanceAfterTransaction,
                Amount = transactionModel.Amount,
                Date = transactionModel.Date
            });
        }

        public List<TransactionModel> GetTransactionsByAccountNumber(string accountNumber)
        {
            List<Transaction> transactionList = new List<Transaction>();
            List<TransactionModel> result = new List<TransactionModel>();
            transactionList = _transactionRepository.GetTransactionsByAccountNumber(accountNumber);
            foreach (Transaction transaction in transactionList)
            {
                result.Add(new TransactionModel
                {
                    TransactionId = transaction.TransactionId,
                    AccountNumber = transaction.AccountNumber,
                    CardNumber = transaction.CardNumber,
                    Amount = transaction.Amount,
                    BalanceAfterTransaction = transaction.BalanceAfterTransaction,
                    Date = transaction.Date,
                    SecondaryPartyName = transaction.SecondaryPartyName,
                    SecondaryPartyAccountNumber = transaction.SecondaryPartyAccountNumber,
                });
            }
            return result;
        }
    }
}
