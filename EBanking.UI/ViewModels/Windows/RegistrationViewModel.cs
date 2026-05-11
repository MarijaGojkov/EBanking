using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EBanking.Services;
using EBanking.UI.Common.Validation;
using EBanking.UI.Models;
using EBanking.UI.Views;
using System;
using System.Windows;

namespace EBanking.UI.ViewModels.Windows
{
    public class RegistrationViewModel : BaseViewModel<RegistrationModel>
    {
        private readonly IUserService _userService;

        public RegistrationViewModel(IUserService userService)
        {
            Validator = new RegistrationViewValidator<RegistrationModel>();
            Model.Title = "Registration";
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
            if (Validator.ValidateModel(Model))
            {
                if (_userService.VerifyUser(Model.Email, Model.UserPin))
                {
                    if (Model.Password.Equals(Model.ConfirmPassword))
                    {
                        _userService.UpdateUserPassword(Model.Email, Model.UserPin, Model.Password);

                        ShowLoginWindow();

                        Close();
                    }
                    else
                    {
                        Model.Label = "Passwords do not match";
                    }
                }
                else
                {
                    MessageBox.Show("User PIN and email do not match.", "Error");
                }
            }
        }
    }
}
