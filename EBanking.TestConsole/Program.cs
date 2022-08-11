using EBanking.DataAccess.Modeli;
using EBanking.DataAccess.Repozitorijumi.Korisnik;
using EBanking.DataAccess.Repozitorijumi.Racun;

RepozitorijumKorisnika repozitorijumKorisnika = new RepozitorijumKorisnika();
RepozitorijumRacuna repozitorijumRacuna = new RepozitorijumRacuna();

repozitorijumRacuna.CreateRacun(new TekuciRacun
{
    BrojRacuna = "1215745641",
    IdKorisnika = 2,
    Balans = 10000,
    DatumKreiranja = DateTime.UtcNow,
    Tip = "Dinarski",
    Valuta = "Dinar"
});
