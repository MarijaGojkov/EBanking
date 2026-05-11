using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EBanking.UI.Models
{
    public class CurrencyExchangeModel : BaseModel
    {
        public double Amount { get; set; }
        public double ConvertedValue { get; set; }
        public Services.Models.AccountModel Account { get; set; }
        public Services.Models.AccountModel SelectedAccount { get; set; }
        public List<Services.Models.AccountModel> UserAccounts { get; set; }
        public ObservableCollection<Services.Models.AccountModel> Accounts { get; set; }
    }
}
