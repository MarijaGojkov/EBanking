namespace EBanking.Services.Models
{
    public class TransferRequest
    {
        public string PayerAccountNumber { get; set; } = "";
        public string RecipientAccountNumber { get; set; } = "";
        public decimal Amount { get; set; }
        public string RecipientName { get; set; } = "";
        public string PaymentPurpose { get; set; } = "";
        public string PayerFullName { get; set; } = "";
    }
}
