namespace EBanking.DataAccess.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public string AccountNumber { get; set; }
        public string CardNumber { get; set; }
        public decimal Amount { get; set; }
        public decimal BalanceAfterTransaction { get; set; }
        public DateTime Date { get; set; }
        public string SecondaryPartyName { get; set; }
        public string SecondaryPartyAccountNumber { get; set; }
    }
}
