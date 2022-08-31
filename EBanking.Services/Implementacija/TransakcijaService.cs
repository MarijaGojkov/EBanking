using EBanking.DataAccess.Modeli;
using EBanking.DataAccess.Repozitorijumi;
using EBanking.Services.Modeli;

namespace EBanking.Services.Implementacija
{
    public class TransakcijaService : ITransakcijaService
    {
        private readonly IRepozitorijumTransakcija _repozitorijumTransakcija;

        public TransakcijaService(IRepozitorijumTransakcija repozitorijumTransakcija)
        {
            _repozitorijumTransakcija = repozitorijumTransakcija;
        }

        public void CreateTransakcija(TransakcijaModel transakcijaModel)
        {
            _repozitorijumTransakcija.CreateTransakcija(new Transakcija
            { 
                BrojKartice = transakcijaModel.BrojKartice ?? "/",
                BrojRacuna = transakcijaModel.BrojRacuna,
                BrojRacunaSekundarnogAktera = transakcijaModel.BrojRacunaSekundarnogAktera,
                NazivSekundarnogAktera = transakcijaModel.NazivSekundarnogAktera,
                BalansNakonTransakcije = transakcijaModel.BalansNakonTransakcije,
                KolicinaNovca = transakcijaModel.KolicinaNovca,
                Datum = transakcijaModel.Datum
            });
        }
    }
}
