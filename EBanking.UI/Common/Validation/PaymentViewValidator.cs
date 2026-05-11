using EBanking.UI.Models;

namespace EBanking.UI.Common.Validation
{
    public class PaymentViewValidator<TModel> : IValidator<TModel> where TModel : PaymentModel
    {
        public bool ValidateModel(TModel model)
        {
            int errors = 0;

            if (model.RecipientAccountNumber == null || model.RecipientAccountNumber == "")
            {
                model.RecipientAccountNumberError = "You have not entered the recipient's account number";
                errors++;
            }
            else
            {
                model.RecipientAccountNumberError = null;
            }

            if (model.RecipientName == null || model.RecipientName == "")
            {
                model.RecipientNameError = "You have not entered the recipient's name";
                errors++;
            }
            else
            {
                model.RecipientNameError = null;
            }

            if (model.Amount == null || model.Amount <= 0)
            {
                model.AmountError = "You have not entered the amount";
                errors++;
            }
            else
            {
                model.AmountError = null;
            }

            if (model.PaymentPurpose == null || model.PaymentPurpose == "")
            {
                model.PaymentPurposeError = "You have not entered the payment purpose";
                errors++;
            }
            else
            {
                model.PaymentPurposeError = null;
            }

            return errors == 0;
        }
    }
}
