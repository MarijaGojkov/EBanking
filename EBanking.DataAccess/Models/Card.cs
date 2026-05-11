namespace EBanking.DataAccess.Models
{
    public class Card
    {
        public string CardNumber { get; set; }
        public string AccountNumber { get; set; }
        public string PinCode { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime DateCreated { get; set; }
        public string CVC { get; set; }
        public bool IsValid { get; set; }
    }
}
