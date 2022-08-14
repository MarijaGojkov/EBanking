namespace EBanking.Services.Korisnik
{
    public interface IKorisnikService
    {
        bool Login(string email, string password);
    }
}
