using EBanking.DataAccess.Repozitorijumi.Korisnik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBanking.Services.Korisnik
{
    public class KorisnikService : IKorisnikService
    {
        private readonly IRepozitorijumKorisnika _repozitorijumKorisnika;

        public KorisnikService(IRepozitorijumKorisnika repozitorijumKorisnika)
        {
            _repozitorijumKorisnika = repozitorijumKorisnika;
        }

        public bool Login(string email, string password)
        {
            return _repozitorijumKorisnika.IsValidKorisnik(email, password);
        }
    }
}
