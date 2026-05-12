using EBanking.UI.Models;

namespace EBanking.UI.Common.Validation
{
    public class RegistrationViewValidator<TModel> : IValidator<TModel> where TModel : RegistrationModel
    {
        public bool ValidateModel(TModel model)
        {
            var errors = 0;
            if (model.Email == null || model.Email == "")
            {
                model.EmailError = "You have not entered your email";
                errors++;
            }
            else
            {
                model.EmailError = null;
            }

            if (model.UserPin == null || model.UserPin == "")
            {
                model.UserPinError = "You have not entered your user PIN";
                errors++;
            }
            else
            {
                model.UserPinError = null;
            }

            if (model.Password == null || model.Password == "")
            {
                model.PasswordError = "You have not entered your password";
                errors++;
            }
            else
            {
                model.PasswordError = null;
            }

            if (model.ConfirmPassword == null || model.ConfirmPassword == "")
            {
                model.ConfirmPasswordError = "You have not confirmed your password";
                errors++;
            }
            else
            {
                model.ConfirmPasswordError = null;
            }

            return errors == 0;
        }
    }
}
