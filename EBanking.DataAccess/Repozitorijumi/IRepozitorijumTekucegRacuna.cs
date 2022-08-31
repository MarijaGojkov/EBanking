using EBanking.DataAccess.Modeli;

namespace EBanking.DataAccess.Repozitorijumi
{
    public interface IRepozitorijumTekucegRacuna
    {
        void CreateRacun(TekuciRacun model);
        List<TekuciRacun> GetRacuniByKorsnikId(int idKorisnika);
        TekuciRacun UpdateBalance(double balans);
    }
}
