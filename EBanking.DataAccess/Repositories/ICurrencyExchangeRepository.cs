using EBanking.DataAccess.Models;

namespace EBanking.DataAccess.Repositories
{
    public interface ICurrencyExchangeRepository
    {
        List<CurrencyExchange> GetExchangeRatesByCurrency(string currency);
    }
}
