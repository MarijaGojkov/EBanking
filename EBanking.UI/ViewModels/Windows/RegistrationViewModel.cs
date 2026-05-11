using CommunityToolkit.Mvvm.Input;
using EBanking.Services;
using EBanking.UI.Common.Validation;
using EBanking.UI.Models;
using EBanking.UI.Views;

namespace EBanking.UI.ViewModels.Windows
{
    public class RegistrationViewModel : BaseViewModel<RegistrationModel>
    {
        private readonly IUserService _userService;

        public RegistrationViewModel(IUserService userService)
        {
            Validator = new RegistrationViewValidator<RegistrationModel>();
            Model.Title = "Account Activation";
            _userService = userService;
            RegistrationCommand = new RelayCommand(ActivateUser);
        }

        public RelayCommand RegistrationCommand { get; set; }

        public void ShowLoginWindow()
        {
            LoginView loginView = new LoginView();
            loginView.Show();
        }

        public void ActivateUser()
        {
            Model.FormError = "";

            if (!Validator.ValidateModel(Model))
            {
                return;
            }

            if (!_userService.VerifyUser(Model.Email, Model.UserPin))
            {
                Model.FormError = "Email or activation PIN is not recognized.";
                return;
            }

            if (!Model.Password.Equals(Model.ConfirmPassword))
            {
                Model.ConfirmPasswordError = "Passwords do not match";
                return;
            }

            _userService.UpdateUserPassword(Model.Email, Model.UserPin, Model.Password);
            ShowLoginWindow();
            Close();
        }
    }
}
