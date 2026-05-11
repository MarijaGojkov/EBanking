using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EBanking.UI.Models
{
    public class CurrencyExchangeModel : BaseModel
    {
        public decimal Amount { get; set; }
        public decimal ConvertedValue { get; set; }
        public Services.Models.AccountModel Account { get; set; }
        public Services.Models.AccountModel SelectedAccount { get; set; }
        public List<Services.Models.AccountModel> UserAccounts { get; set; }
        public ObservableCollection<Services.Models.AccountModel> Accounts { get; set; }
    }
}
