using EBanking.DataAccess.Modeli;
using EBanking.DataAccess.Repozitorijumi.Racun;
using EBanking.Services.Modeli;

namespace EBanking.Services
{
    public class RacunService : IRacunService
    {
        private readonly IRepozitorijumRacuna _repozitorijumRacuna;

        public RacunService(IRepozitorijumRacuna repozitorijumRacuna)
        {
            _repozitorijumRacuna = repozitorijumRacuna;
        }

        public RacunModel GetRacun(string brojRacuna)
        {
            TekuciRacun model = _repozitorijumRacuna.GetRacunById(brojRacuna);

            RacunModel result = new RacunModel
            {
                Balans = model.Balans,
                BrojRacuna = model.BrojRacuna
            };

            return result;
        }
    }
}
