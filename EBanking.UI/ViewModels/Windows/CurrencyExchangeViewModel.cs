using CommunityToolkit.Mvvm.Input;
using EBanking.Services;
using EBanking.Services.Models;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;

namespace EBanking.UI.ViewModels.Windows
{
    public class CurrencyExchangeViewModel : BaseViewModel<UI.Models.CurrencyExchangeModel>
    {
        private readonly ITransactionService _transactionService;
        private readonly IAccountService _accountService;
        private readonly ICurrencyExchangeService _currencyExchangeService;

        [PreferredConstructor]
        public CurrencyExchangeViewModel()
        {
            Model.Title = "Currency Exchange";
        }

        public CurrencyExchangeViewModel(ITransactionService transactionService,
            IAccountService accountService,
            ICurrencyExchangeService currencyExchangeService,
            AccountModel account,
            List<AccountModel> userAccounts,
            string userFullName)
        {
            Model.Title = "Currency Exchange";
            Model.Account = account;
            Model.UserAccounts = userAccounts;
            Model.Accounts = new System.Collections.ObjectModel.ObservableCollection<AccountModel>(Model.UserAccounts);
            _transactionService = transactionService;
            _accountService = accountService;
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
            if (Model.Account.Balance > Model.Amount)
            {
                _transactionService.CreateTransaction(new TransactionModel
                {
                    AccountNumber = Model.Account.AccountNumber,
                    Amount = Model.Amount,
                    SecondaryPartyAccountNumber = Model.SelectedAccount.AccountNumber,
                    SecondaryPartyName = UserFullName,
                    BalanceAfterTransaction = Model.Account.Balance - Model.Amount,
                    Date = DateTime.UtcNow
                });

                _accountService.UpdateBalance(Model.Account.Balance - Model.Amount, Model.Account.AccountNumber);

                AddTransactionToRecipient();
                Close();
            }
            else
            {
                MessageBox.Show("Insufficient funds on the account");
            }
        }

        private void AddTransactionToRecipient()
        {
            _transactionService.CreateTransaction(new TransactionModel
            {
                AccountNumber = Model.SelectedAccount.AccountNumber,
                Amount = Model.ConvertedValue,
                SecondaryPartyAccountNumber = Model.Account.AccountNumber,
                SecondaryPartyName = UserFullName,
                BalanceAfterTransaction = Model.SelectedAccount.Balance + Model.ConvertedValue,
                Date = DateTime.UtcNow
            });

            _accountService.UpdateBalance(Model.SelectedAccount.Balance + Model.ConvertedValue, Model.SelectedAccount.AccountNumber);
        }
    }


}
