using System.Configuration;

namespace EBanking.DataAccess
{
    public static class DatabaseAccess
    {
        private const string ConnectionStringName = "EBankingDb";

        private static readonly Lazy<string> _connectionString = new(() =>
        {
            var configString = ConfigurationManager.ConnectionStrings[ConnectionStringName]?.ConnectionString;
            if (string.IsNullOrWhiteSpace(configString))
            {
                throw new InvalidOperationException(
                    $"Connection string '{ConnectionStringName}' was not found in configuration. " +
                    "Ensure App.config defines <connectionStrings><add name=\"" + ConnectionStringName + "\" ... /></connectionStrings>.");
            }
            return configString;
        });

        public static string ConnectionString => _connectionString.Value;
    }
}
