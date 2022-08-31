namespace EBanking.UI.Models
{
    public class PlacanjeModel : BaseModel
    {
        public string BrojRacunaPlatioca { get; set; }
        public double TrenutnoStanje { get; set; }

        public string NazivPrimaoca { get; set; }
        public string BrojRacunaPrimaoca { get; set; }
        public string PozivNaBroj { get; set; }
        public string SvrhaPlacanja { get; set; }
        public double KolicinaNovca { get; set; }
    }
}
