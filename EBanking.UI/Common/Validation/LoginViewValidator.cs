using EBanking.UI.Models;

namespace EBanking.UI.Common.Validation
{
    public class LoginViewValidator<TModel> : IValidator<TModel> where TModel : LoginModel
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

            if (model.Password == null || model.Password == "")
            {
                model.PasswordError = "You have not entered your password";
                errors++;
            }
            else
            {
                model.PasswordError = null;
            }

            return errors == 0;
        }
    }
}
