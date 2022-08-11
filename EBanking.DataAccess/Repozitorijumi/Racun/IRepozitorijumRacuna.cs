using EBanking.DataAccess.Modeli;

namespace EBanking.DataAccess.Repozitorijumi.Racun
{
    public interface IRepozitorijumRacuna
    {
        void CreateRacun(TekuciRacun model);
        TekuciRacun GetRacunById(string brojRacuna);
        TekuciRacun UpdateBalance(double balans);
    }
}
