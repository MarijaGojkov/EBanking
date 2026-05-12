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

        public void TransferFunds(TransferRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            if (request.Amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero.", nameof(request));
            }
            if (string.Equals(request.PayerAccountNumber, request.RecipientAccountNumber, StringComparison.Ordinal))
            {
                throw new InvalidOperationException("Cannot transfer to the same account.");
            }

            _transactionRepository.ExecuteTransfer(
                payerAccountNumber: request.PayerAccountNumber,
                recipientAccountNumber: request.RecipientAccountNumber,
                amount: request.Amount,
                recipientName: request.RecipientName,
                payerFullName: request.PayerFullName,
                occurredAt: DateTime.UtcNow);
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
