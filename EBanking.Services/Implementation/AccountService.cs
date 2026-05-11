using EBanking.DataAccess.Models;
using EBanking.DataAccess.Repositories;
using EBanking.Services.Models;

namespace EBanking.Services.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICurrencyExchangeRepository _currencyExchangeRepository;

        public AccountService(IAccountRepository accountRepository, ITransactionRepository transactionRepository, ICurrencyExchangeRepository currencyExchangeRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
            _currencyExchangeRepository = currencyExchangeRepository;
        }

        public async Task<AccountModel> GetAccountByAccountNumber(string accountNumber)
        {
            Account account = await _accountRepository.GetAccountByAccountNumber(accountNumber);

            return new AccountModel
            {
                AccountNumber = account.AccountNumber,
                Balance = account.Balance,
                DateCreated = account.DateCreated,
                UserId = account.UserId,
                Type = account.Type,
                Currency = account.Currency
            };
        }

        public List<AccountModel> GetAccountsForUser(int userId)
        {
            List<Account> accounts = _accountRepository.GetAccountsByUserId(userId);

            List<AccountModel> result = new List<AccountModel>();
            List<TransactionModel> transactions = new List<TransactionModel>();

            foreach (var account in accounts)
            {
                var transactionResults = _transactionRepository.GetTransactionsByAccountNumber(account.AccountNumber);

                transactions = new List<TransactionModel>();

                foreach (var transaction in transactionResults)
                {
                    transactions.Add(new TransactionModel
                    {
                        TransactionId = transaction.TransactionId,
                        AccountNumber = transaction.AccountNumber,
                        CardNumber = transaction.CardNumber,
                        Amount = transaction.Amount,
                        BalanceAfterTransaction = transaction.BalanceAfterTransaction,
                        Date = transaction.Date,
                        SecondaryPartyName = transaction.SecondaryPartyName,
                        SecondaryPartyAccountNumber = transaction.SecondaryPartyAccountNumber
                    });
                }

                result.Add(new AccountModel
                {
                    AccountNumber = account.AccountNumber,
                    Balance = account.Balance,
                    DateCreated = account.DateCreated,
                    UserId = account.UserId,
                    Type = account.Type,
                    Currency = account.Currency,
                    User = new UserModel
                    {
                        IdCardNumber = account.User.IdCardNumber,
                        DateOfBirth = account.User.DateOfBirth,
                        Email = account.User.Email,
                        FirstName = account.User.FirstName,
                        LastName = account.User.LastName,
                        Phone = account.User.Phone,
                    },
                    Transactions = transactions,
                    ExchangeRates = MapExchangeRates(account.Currency)
                });
            }

            return result;
        }

        public List<CurrencyExchangeModel> MapExchangeRates(string accountCurrency)
        {
            var results = _currencyExchangeRepository.GetExchangeRatesByCurrency(accountCurrency);

            var models = new List<CurrencyExchangeModel>();

            foreach (var result in results)
            {
                models.Add(new CurrencyExchangeModel
                {
                    Currency = result.Currency,
                    Value = result.Value
                });
            }

            return models;
        }

        public bool IsValidAccount(string accountNumber)
        {
            return _accountRepository.IsValidAccount(accountNumber);
        }

        public void UpdateBalance(double newBalance, string accountNumber)
        {
            _accountRepository.UpdateBalance(newBalance, accountNumber);
        }
    }
}
