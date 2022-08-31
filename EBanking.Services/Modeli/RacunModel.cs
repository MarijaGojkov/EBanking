namespace EBanking.Services.Modeli
{
    public class RacunModel
    {
        public string BrojRacuna { get; set; }
        public int IdKorisnika { get; set; }
        public double Balans { get; set; }
        public string Tip { get; set; }
        public string Valuta { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public KorisnikModel Korisnik { get; set; }
    }
}
