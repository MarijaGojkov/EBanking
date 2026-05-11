using CommunityToolkit.Mvvm.Input;
using EBanking.Services;
using EBanking.Services.Models;
using EBanking.UI.Common.Validation;
using EBanking.UI.Models;
using GalaSoft.MvvmLight.Ioc;
using System;

namespace EBanking.UI.ViewModels.Windows
{
    public class PaymentViewModel : BaseViewModel<PaymentModel>
    {
        private readonly ITransactionService _transactionService;
        private readonly IAccountService _accountService;

        [PreferredConstructor]
        public PaymentViewModel()
        {
        }

        public PaymentViewModel(ITransactionService transactionService, IAccountService accountService, TransactionInfo transactionInfo)
        {
            Validator = new PaymentViewValidator<PaymentModel>();
            _transactionService = transactionService;
            _accountService = accountService;
            Model.Title = "New Payment";
            Model.PayerAccountNumber = transactionInfo.AccountNumber;
            Model.CurrentBalance = transactionInfo.CurrentBalance;
            PaymentCommand = new RelayCommand(Pay);
            TransactionInfo = transactionInfo;
        }

        public RelayCommand PaymentCommand { get; set; }
        public TransactionInfo TransactionInfo { get; set; }

        public void Pay()
        {
            if (Validator.ValidateModel(Model))
            {
                _transactionService.CreateTransaction(new TransactionModel
                {
                    AccountNumber = Model.PayerAccountNumber,
                    Amount = Model.Amount,
                    SecondaryPartyAccountNumber = Model.RecipientAccountNumber,
                    SecondaryPartyName = Model.RecipientName,
                    BalanceAfterTransaction = Model.CurrentBalance - Model.Amount,
                    Date = DateTime.UtcNow
                });

                _accountService.UpdateBalance(Model.CurrentBalance - Model.Amount, Model.PayerAccountNumber);

                if (_accountService.IsValidAccount(Model.RecipientAccountNumber))
                {
                    AddTransactionToRecipient();
                }
                Close();
            }
        }

        private async void AddTransactionToRecipient()
        {
            var account = await _accountService.GetAccountByAccountNumber(Model.RecipientAccountNumber);

            _transactionService.CreateTransaction(new TransactionModel
            {
                AccountNumber = Model.RecipientAccountNumber,
                Amount = Model.Amount,
                SecondaryPartyAccountNumber = Model.PayerAccountNumber,
                SecondaryPartyName = TransactionInfo.UserFullName,
                BalanceAfterTransaction = account.Balance + Model.Amount,
                Date = DateTime.UtcNow
            });

            _accountService.UpdateBalance(account.Balance + Model.Amount, Model.RecipientAccountNumber);
        }
    }
}
