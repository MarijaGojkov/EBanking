using EBanking.Services.Models;

namespace EBanking.Services
{
    public interface ICurrencyExchangeService
    {
        void Exchange(ExchangeRequest request);
    }
}
