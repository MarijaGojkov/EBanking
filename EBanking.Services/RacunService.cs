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

        public List<RacunModel> GetRacunForKorsnik(string idKorisnika)
        {
            List<TekuciRacun> racuni = _repozitorijumRacuna.GetRacuniByKorsnikId(idKorisnika);

            List<RacunModel> result = new List<RacunModel>();

            foreach(var racun in racuni)
            {
                result.Add(new RacunModel
                {
                    BrojRacuna = racun.BrojRacuna,
                    Balans = racun.Balans,
                    DatumKreiranja = racun.DatumKreiranja,
                    IdKorisnika = racun.IdKorisnika,
                    Tip = racun.Tip,
                    Valuta = racun.Valuta,
                    Korisnik = new KorisnikModel
                    {
                        BrojLicneKarte = racun.Korisnik.BrojLicneKarte,
                        DatumRodjenja = racun.Korisnik.DatumRodjenja,
                        Email = racun.Korisnik.Email,
                        Ime = racun.Korisnik.Ime,
                        Prezime = racun.Korisnik.Prezime,
                        Telefon = racun.Korisnik.Telefon,
                    }
                });
            }

            return result;
        }
    }
}
