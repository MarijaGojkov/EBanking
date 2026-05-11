namespace EBanking.Services.Models
{
    public class TransactionModel
    {
        public int TransactionId { get; set; }
        public string AccountNumber { get; set; }
        public string CardNumber { get; set; }
        public double Amount { get; set; }
        public double BalanceAfterTransaction { get; set; }
        public DateTime Date { get; set; }
        public string SecondaryPartyName { get; set; }
        public string SecondaryPartyAccountNumber { get; set; }
    }
}
