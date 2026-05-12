using EBanking.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace EBanking.UI.Models
{
    public class TransactionDetailsModel : BaseModel
    {
        public TransactionModel Transaction { get; set; }
        public string AccountNumber { get; set; }
    }
}
