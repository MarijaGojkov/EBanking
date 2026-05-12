using CommunityToolkit.Mvvm.Input;
using EBanking.Services;
using EBanking.Services.Models;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace EBanking.UI.ViewModels.Windows
{
    public class CurrencyExchangeViewModel : BaseViewModel<UI.Models.CurrencyExchangeModel>
    {
        private readonly ICurrencyExchangeService _currencyExchangeService;

        [PreferredConstructor]
        public CurrencyExchangeViewModel()
        {
            Model.Title = "Currency Exchange";
        }

        public CurrencyExchangeViewModel(
            ICurrencyExchangeService currencyExchangeService,
            AccountModel account,
            List<AccountModel> userAccounts,
            string userFullName)
        {
            Model.Title = "Currency Exchange";
            Model.Account = account;
            Model.UserAccounts = userAccounts;
            Model.Accounts = new System.Collections.ObjectModel.ObservableCollection<AccountModel>(Model.UserAccounts);
            _currencyExchangeService = currencyExchangeService;
            ConvertCommand = new RelayCommand(Convert);
            ExecuteTransactionCommand = new RelayCommand(ExecuteTransaction);
            UserFullName = userFullName;
        }

        public RelayCommand ConvertCommand { get; set; }
        public RelayCommand ExecuteTransactionCommand { get; set; }
        public string UserFullName { get; set; }

        public void Convert()
        {
            if (Model.SelectedAccount == null)
            {
                MessageBox.Show("You have not selected an account");
                return;
            }

            var rate = Model.Account.ExchangeRates
                .FirstOrDefault(x => x.Currency.Equals(Model.SelectedAccount.Currency));

            if (rate == null)
            {
                MessageBox.Show("Exchange rate not found for the selected account currency.");
                return;
            }

            Model.ConvertedValue = Model.Amount * rate.Value;
        }

        public void ExecuteTransaction()
        {
            if (Model.SelectedAccount is null)
            {
                MessageBox.Show("You have not selected an account");
                return;
            }
            if (Model.Amount <= 0)
            {
                MessageBox.Show("Enter an amount greater than zero");
                return;
            }
            if (Model.Account.Balance < Model.Amount)
            {
                MessageBox.Show("Insufficient funds on the account");
                return;
            }

            try
            {
                _currencyExchangeService.Exchange(new ExchangeRequest
                {
                    SourceAccountNumber = Model.Account.AccountNumber,
                    DestinationAccountNumber = Model.SelectedAccount.AccountNumber,
                    Amount = Model.Amount,
                    SourceCurrency = Model.Account.Currency,
                    DestinationCurrency = Model.SelectedAccount.Currency,
                    UserFullName = UserFullName,
                });
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exchange failed");
            }
        }
    }
}
