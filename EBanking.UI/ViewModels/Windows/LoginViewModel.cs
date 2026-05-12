using CommunityToolkit.Mvvm.Input;
using EBanking.Services;
using EBanking.UI.Common.Validation;
using EBanking.UI.Models;
using EBanking.UI.Views;
using System.ComponentModel;
using System.Windows;

namespace EBanking.UI.ViewModels.Windows
{
    public class LoginViewModel : BaseViewModel<LoginModel>
    {
        private readonly IUserService _userService;
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;
        private readonly ICurrencyExchangeService _currencyExchangeService;

        public LoginViewModel(IUserService userService, IAccountService accountService, ITransactionService transactionService, ICurrencyExchangeService currencyExchangeService)
        {
            Validator = new LoginViewValidator<LoginModel>();
            _userService = userService;
            _accountService = accountService;
            _transactionService = transactionService;

            Model.Title = "Login";

            LoginCommand = new RelayCommand(Login);
            OpenRegistrationCommand = new RelayCommand(Registration);
            _transactionService = transactionService;
            _currencyExchangeService = currencyExchangeService;
        }

        public RelayCommand LoginCommand { get; set; }

        public RelayCommand OpenRegistrationCommand { get; set; }

        public void Login()
        {
            var userId = _userService.Login(Model.Email, Model.Password);
            if (userId == 0)
            {
                MessageBox.Show("Username and password do not match.", "Error");
            }
            if (Validator.ValidateModel(Model) && userId is not 0)
            {
                AccountView accountView = new AccountView
                {
                    DataContext = new AccountViewModel(_accountService, _transactionService, _currencyExchangeService, userId)
                };
                void OnAccountViewClosing(object? sender, CancelEventArgs e)
                {
                    accountView.Closing -= OnAccountViewClosing;
                    LoginView view = new LoginView
                    {
                        DataContext = this
                    };
                    view.Show();
                }
                accountView.Closing += OnAccountViewClosing;
                accountView.Show();
                Close();
            }
        }

        public void Registration()
        {
            RegistrationView registrationView = new RegistrationView();
            registrationView.Show();
            Close();
        }
    }
}
