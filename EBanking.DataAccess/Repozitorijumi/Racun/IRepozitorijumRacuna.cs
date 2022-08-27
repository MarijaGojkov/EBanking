using EBanking.DataAccess.Modeli;

namespace EBanking.DataAccess.Repozitorijumi.Racun
{
    public interface IRepozitorijumRacuna
    {
        void CreateRacun(TekuciRacun model);
        List<TekuciRacun> GetRacuniByKorsnikId(string idKorisnika);
        TekuciRacun UpdateBalance(double balans);
    }
}
