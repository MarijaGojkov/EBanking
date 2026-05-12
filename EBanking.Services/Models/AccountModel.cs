namespace EBanking.Services.Models
{
    public class AccountModel
    {
        public string AccountNumber { get; set; }
        public int UserId { get; set; }
        public decimal Balance { get; set; }
        public string Type { get; set; }
        public string Currency { get; set; }
        public DateTime DateCreated { get; set; }
        public UserModel User { get; set; }
        public List<TransactionModel> Transactions { get; set; }
        public List<CurrencyExchangeModel> ExchangeRates { get; set; }
    }
}
