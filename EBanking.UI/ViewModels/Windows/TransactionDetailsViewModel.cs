using EBanking.Services.Models;
using EBanking.UI.Models;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBanking.UI.ViewModels.Windows
{
    public class TransactionDetailsViewModel : BaseViewModel<TransactionDetailsModel>
    {
        [PreferredConstructor]
        public TransactionDetailsViewModel()
        {
            Model.Title = "Transaction Details";
        }

        public TransactionDetailsViewModel(TransactionModel transactionModel)
        {
            Model.Title = "Transaction Details";
            Model.Transaction = transactionModel;
            Model.AccountNumber = transactionModel.AccountNumber;
        }
    }
}
