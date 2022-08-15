namespace EBanking.UI.Models
{
    public class RegistracijaModel : BaseModel
    {
        public string Email { get; set; }
        public string KorisnickiPin { get; set; }
        public string Lozinka { get; set; }
        public string PonoviLozinku { get; set; }
    }
}
