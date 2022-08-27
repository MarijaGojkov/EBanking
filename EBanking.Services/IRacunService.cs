using EBanking.Services.Modeli;

namespace EBanking.Services
{
    public interface IRacunService
    {
        List<RacunModel> GetRacunForKorsnik(string IdKorsnika);
    }
}
