using EBanking.DataAccess.Modeli;
using EBanking.DataAccess.Repozitorijumi;
using EBanking.Services.Modeli;

namespace EBanking.Services.Implementacija
{
    public class RacunService : IRacunService
    {
        private readonly IRepozitorijumTekucegRacuna _repozitorijumRacuna;

        public RacunService(IRepozitorijumTekucegRacuna repozitorijumRacuna)
        {
            _repozitorijumRacuna = repozitorijumRacuna;
        }

        public async Task<RacunModel> GetRacunByBrojRacuna(string brojRacuna)
        {
            TekuciRacun racun = await _repozitorijumRacuna.GetRacunByBrojRacuna(brojRacuna);

            return new RacunModel
            {
                BrojRacuna = racun.BrojRacuna,
                Balans = racun.Balans,
                DatumKreiranja = racun.DatumKreiranja,
                IdKorisnika = racun.IdKorisnika,
                Tip = racun.Tip,
                Valuta = racun.Valuta
            };
        }

        //Pozivamo repo za izvlacenje racuna i mapiramo ih na modele u sloju servisa
        public List<RacunModel> GetRacunForKorsnik(int idKorisnika)
        {
            List<TekuciRacun> racuni = _repozitorijumRacuna.GetRacuniByKorsnikId(idKorisnika);

            List<RacunModel> result = new List<RacunModel>();

            foreach (var racun in racuni)
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

        public bool IsValidRacun(string brojRacuna)
        {
            return _repozitorijumRacuna.IsValidRacun(brojRacuna);
        }

        public void UpdateBalance(double noviBalans, string brojRacuna)
        {
            _repozitorijumRacuna.UpdateBalance(noviBalans, brojRacuna);
        }
    }
}
