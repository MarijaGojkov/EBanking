namespace EBanking.UI.Models
{
    public class PaymentModel : BaseModel
    {
        public string PayerAccountNumber { get; set; }
        public decimal CurrentBalance { get; set; }
        public string RecipientName { get; set; }
        public string RecipientAccountNumber { get; set; }
        public string ReferenceNumber { get; set; }
        public string PaymentPurpose { get; set; }
        public decimal Amount { get; set; }

        #region Validation
        public string PayerAccountNumberError { get; set; }
        public string RecipientNameError { get; set; }
        public string RecipientAccountNumberError { get; set; }
        public string ReferenceNumberError { get; set; }
        public string PaymentPurposeError { get; set; }
        public string AmountError { get; set; }
        #endregion
    }
}
