using EBanking.UI.Models;

namespace EBanking.UI.Common.Validacija
{
    public class RegistracijaViewValidator<TModel> : IValidator<TModel> where TModel : RegistracijaModel
    {
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

            if (model.KorisnickiPin == null || model.KorisnickiPin == "")
            {
                model.KorisnickiPinError = "Niste uneli sifru";
                IsValidModel = false;
            }
            else
            {
                model.KorisnickiPinError = null;
                IsValidModel = true;
            }
            if (model.Lozinka == null || model.Lozinka == "")
            {
                model.LozinkaError = "Niste uneli E-mail";
                IsValidModel = false;
            }
            else
            {
                model.LozinkaError = null;
                IsValidModel = true;
            }

            if (model.PonoviLozinku == null || model.PonoviLozinku == "")
            {
                model.PonoviLozinkuError = "Niste uneli sifru";
                IsValidModel = false;
            }
            else
            {
                model.PonoviLozinkuError = null;
                IsValidModel = true;
            }

        }
    }
}
