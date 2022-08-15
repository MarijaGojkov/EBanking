namespace EBanking.Services.Korisnik
{
    public interface IKorisnikService
    {
        bool Login(string email, string password);

        void UpdateLozinkeKorisnika(string email, string korisnickiPin, string lozinka);

        bool ProveraKorisnika(string email, string korisnickiPin);
    }
}
