using EBanking.DataAccess.Modeli;

namespace EBanking.DataAccess.Repozitorijumi.Racun
{
    public interface IRepozitorijumRacuna
    {
        void CreateRacun(TekuciRacun model);
        TekuciRacun GetRacun(TekuciRacun racun);
        TekuciRacun UpdateBalance(double balans);
    }
}
