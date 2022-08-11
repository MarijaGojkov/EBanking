namespace EBanking.DataAccess.Repozitorijumi.Korisnik
{
    public interface IRepozitorijumKorisnika
    {
        Modeli.Korisnik GetKorisnikById(int id);

        bool IsValidKorisnik(string email, string password);
    }
}
