using System;

namespace EBanking.UI.Models
{
    public class RacunModel : BaseModel
    {
        public string BrojRacuna { get; set; }
        public string BrojKartice { get; set; }
        public double Balans { get; set; }
        public string Tip { get; set; }
        public string Valuta { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }

    }
}
