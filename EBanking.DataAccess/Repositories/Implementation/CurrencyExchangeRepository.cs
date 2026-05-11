using EBanking.DataAccess.Models;
using System.Data.SqlClient;

namespace EBanking.DataAccess.Repositories.Implementation
{
    public class CurrencyExchangeRepository : ICurrencyExchangeRepository
    {
        public List<CurrencyExchange> GetExchangeRatesByCurrency(string currency)
        {
            List<CurrencyExchange> exchangeRates = new List<CurrencyExchange>();

            using (SqlConnection sqlConnection = new SqlConnection(DatabaseAccess.ConnectionString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "SELECT * FROM CurrencyExchange";

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            exchangeRates.Add(new CurrencyExchange
                            {
                                Currency = reader["currency"] as string,
                                Value = decimal.ToDouble((decimal)reader["value"]),
                            });
                        }
                    }
                }
            }
            return exchangeRates;
        }
    }
}
