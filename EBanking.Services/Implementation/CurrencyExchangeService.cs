using EBanking.DataAccess.Repositories;
using EBanking.Services.Models;

namespace EBanking.Services.Implementation
{
    public class CurrencyExchangeService : ICurrencyExchangeService
    {
        private readonly ICurrencyExchangeRepository _currencyExchangeRepository;
        private readonly ITransactionRepository _transactionRepository;

        public CurrencyExchangeService(
            ICurrencyExchangeRepository currencyExchangeRepository,
            ITransactionRepository transactionRepository)
        {
            _currencyExchangeRepository = currencyExchangeRepository;
            _transactionRepository = transactionRepository;
        }

        public void Exchange(ExchangeRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            if (request.Amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero.", nameof(request));
            }
            if (string.Equals(request.SourceAccountNumber, request.DestinationAccountNumber, StringComparison.Ordinal))
            {
                throw new InvalidOperationException("Cannot exchange to the same account.");
            }

            var rates = _currencyExchangeRepository.GetExchangeRatesByCurrency(request.SourceCurrency);
            var rate = rates.FirstOrDefault(r =>
                string.Equals(r.Currency, request.DestinationCurrency, StringComparison.OrdinalIgnoreCase))
                ?? throw new InvalidOperationException(
                    $"Exchange rate from {request.SourceCurrency} to {request.DestinationCurrency} not found.");

            // The rate column is DECIMAL(18,4); Account.balance is DECIMAL(18,2).
            // Round the credit to 2dp before it crosses into the transaction so the
            // SqlParameter (Scale = 2) doesn't silently truncate at the driver.
            decimal destinationAmount = decimal.Round(request.Amount * rate.Value, 2, MidpointRounding.AwayFromZero);

            _transactionRepository.ExecuteExchange(
                sourceAccountNumber: request.SourceAccountNumber,
                destinationAccountNumber: request.DestinationAccountNumber,
                sourceAmount: request.Amount,
                destinationAmount: destinationAmount,
                userFullName: request.UserFullName,
                occurredAt: DateTime.UtcNow);
        }
    }
}
