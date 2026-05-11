using System.Collections.Generic;

namespace EBanking.UI.Models
{
    public class AccountModel : BaseModel
    {
        public string AccountNumber { get; set; }
        public double Balance { get; set; }
        public string UserFullName { get; set; }
        public Services.Models.AccountModel SelectedAccount { get; set; }
        public List<Services.Models.AccountModel> Accounts { get; set; }
        public List<Services.Models.TransactionModel> Transactions { get; set; }
        public Services.Models.TransactionModel SelectedTransaction { get; set; }
    }
}
