using EBanking.DataAccess.Modeli;

namespace EBanking.DataAccess.Repozitorijumi.Racun
{
    public interface IRepozitorijumRacuna
    {
        void CreateRacun(TekuciRacun model);
        TekuciRacun GetRacunById(string brojRacuna);
        void UpdateBalance(double balans,string brojRacuna);
        double GetBalance(string brojRacuna);
    }
}
