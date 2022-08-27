using EBanking.UI.Models;

namespace EBanking.UI.Common.Validacija
{
    public class LoginViewValidator<TModel> : IValidator<TModel> where TModel : LoginModel // TModel je tipa LoginModel u ovom valiudatoru
    {
        //Stanje modela, ako je true, sve radi i ne izbacuje greske za validaciju
        public bool IsValidModel { get; set; } = true;

        public void ValidateModel(TModel model)
        {
            if (model.Email == null || model.Email == "")
            {
                model.EmailError = "Niste uneli E-mail";
                IsValidModel = false;
            }
            else
            {
                model.EmailError = null;
                IsValidModel = true;
            }

            if (model.Password == null || model.Password == "")
            {
                model.PasswordError = "Niste uneli sifru";
                IsValidModel = false;
            }
            else
            {
                model.PasswordError = null;
                IsValidModel = true;
            }

            //model.EmailError = model.Email == null || model.Email == "" ? model.EmailError = "Niste uneli E-mail" : model.EmailError = null;
            //IsValidModel = model.Email == null || model.Email == "" ? IsValidModel = false : IsValidModel = true;

            //model.PasswordError = model.Password == null || model.Password == "" ? model.PasswordError = "Niste uneli sifru" : model.PasswordError = null;
            //IsValidModel = model.Password == null || model.Password == "" ? IsValidModel = false : IsValidModel = true;
        }
    }
}
