namespace EBanking.DataAccess.Models
{
    public class Account
    {
        public string AccountNumber { get; set; }
        public int UserId { get; set; }
        public double Balance { get; set; }
        public string Type { get; set; }
        public string Currency { get; set; }
        public DateTime DateCreated { get; set; }
        public User User { get; set; }

        public List<Transaction> Transactions { get; set; }
    }
}
