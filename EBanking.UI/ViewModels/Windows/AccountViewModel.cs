using CommunityToolkit.Mvvm.Input;
using EBanking.Services;
using EBanking.Services.Models;
using EBanking.UI.Views;
using GalaSoft.MvvmLight.Ioc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace EBanking.UI.ViewModels.Windows
{
    public class AccountViewModel : BaseViewModel<EBanking.UI.Models.AccountModel>
    {
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;
        private readonly ICurrencyExchangeService _currencyExchangeService;

        [PreferredConstructor]
        public AccountViewModel()
        {
            Model.Title = "Current Account";
        }

        public AccountViewModel(IAccountService accountService, ITransactionService transactionService, ICurrencyExchangeService currencyExchangeService, int userId)
        {
            Model.Title = "Current Account";
            _accountService = accountService;
            _transactionService = transactionService;
            _currencyExchangeService = currencyExchangeService;
            UserId = userId;
            GetAccount(userId);
            PaymentCommand = new RelayCommand(Payment);
            OpenCurrencyExchangeCommand = new RelayCommand(OpenCurrencyExchange);
            LogoutCommand = new RelayCommand(Logout);
            TransactionDetailsCommand = new RelayCommand(TransactionDetails);
        }

        public RelayCommand PaymentCommand { get; set; }
        public RelayCommand OpenCurrencyExchangeCommand { get; set; }
        public RelayCommand LogoutCommand { get; set; }
        public RelayCommand TransactionDetailsCommand { get; set; }
        public int UserId { get; set; }

        public void GetAccount(int userId)
        {
            Model.Accounts = _accountService.GetAccountsForUser(userId);

            Model.UserFullName = Model.Accounts.First().User.FirstName + " " + Model.Accounts.First().User.LastName;
        }

        public void OpenCurrencyExchange()
        {
            if (Model.SelectedAccount is not null)
            {
                var accounts = Model.Accounts.Where(x => x.AccountNumber != Model.SelectedAccount.AccountNumber).ToList();
                CurrencyExchangeView view = new CurrencyExchangeView
                {
                    DataContext = new CurrencyExchangeViewModel(_transactionService, _accountService, _currencyExchangeService, Model.SelectedAccount, accounts, Model.UserFullName)
                };
                view.Closing += (s, o) =>
                {
                    GetAccount(UserId);
                };
                view.Show();
            }
            else
            {
                MessageBox.Show("You must select an account");
            }
        }

        public void Logout()
        {
            Close();
        }

        public async void Payment()
        {
            if (Model.SelectedAccount is not null)
            {
                PaymentView view = new PaymentView
                {
                    DataContext = new PaymentViewModel(_transactionService,
                                new TransactionInfo { AccountNumber = Model.SelectedAccount.AccountNumber, CurrentBalance = Model.SelectedAccount.Balance,
                                    UserFullName = Model.Accounts.First().User.FirstName + " " + Model.Accounts.First().User.LastName })
                };
                view.Closing += (s, o) =>
                {
                    GetAccount(UserId);
                };
                view.Show();
            }
            else
            {
                MessageBox.Show("You must select an account", "Error");
            }
        }

        public void TransactionDetails()
        {
            if (Model.SelectedAccount is not null)
            {
                TransactionDetailsView view = new TransactionDetailsView
                {
                    DataContext = new TransactionDetailsViewModel(Model.SelectedTransaction)
                };
                view.Show();
            }
            else
            {
                MessageBox.Show("You must select a transaction");
            }
        }
    }
}
