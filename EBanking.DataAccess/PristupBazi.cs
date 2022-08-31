using System.Configuration;

namespace EBanking.DataAccess
{
    public static class PristupBazi
    {
        public static string KonekcioniString = ConfigurationManager.ConnectionStrings["eBankingDB"].ConnectionString;
    }
}
