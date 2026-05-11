using CommunityToolkit.Mvvm.Input;
using EBanking.Services;
using EBanking.Services.Models;
using EBanking.UI.Common.Validation;
using EBanking.UI.Models;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Windows;

namespace EBanking.UI.ViewModels.Windows
{
    public class PaymentViewModel : BaseViewModel<PaymentModel>
    {
        private readonly ITransactionService _transactionService;

        [PreferredConstructor]
        public PaymentViewModel()
        {
        }

        public PaymentViewModel(ITransactionService transactionService, TransactionInfo transactionInfo)
        {
            Validator = new PaymentViewValidator<PaymentModel>();
            _transactionService = transactionService;
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
            if (!Validator.ValidateModel(Model))
            {
                return;
            }

            try
            {
                _transactionService.TransferFunds(new TransferRequest
                {
                    PayerAccountNumber = Model.PayerAccountNumber,
                    RecipientAccountNumber = Model.RecipientAccountNumber,
                    Amount = Model.Amount,
                    RecipientName = Model.RecipientName,
                    PaymentPurpose = Model.PaymentPurpose,
                    PayerFullName = TransactionInfo.UserFullName,
                });
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Payment failed");
            }
        }
    }
}
