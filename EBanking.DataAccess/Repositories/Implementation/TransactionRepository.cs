using EBanking.DataAccess.Models;
using System.Data.SqlClient;

namespace EBanking.DataAccess.Repositories.Implementation
{
    public class TransactionRepository : ITransactionRepository
    {
        public int CreateTransaction(Transaction transaction)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DatabaseAccess.ConnectionString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "INSERT INTO [Transaction](accountNumber, cardNumber, amount, balanceAfterTransaction, date, secondaryPartyName, secondaryPartyAccountNumber)" +
                        " VALUES (@accountNumber, @cardNumber, @amount, @balanceAfterTransaction, @date, @secondaryPartyName, @secondaryPartyAccountNumber)";
                    sqlCommand.Parameters.AddWithValue("@accountNumber", transaction.AccountNumber);
                    sqlCommand.Parameters.AddWithValue("@cardNumber", (object)transaction.CardNumber ?? DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@amount", transaction.Amount);
                    sqlCommand.Parameters.AddWithValue("@balanceAfterTransaction", transaction.BalanceAfterTransaction);
                    sqlCommand.Parameters.AddWithValue("@date", transaction.Date);
                    sqlCommand.Parameters.AddWithValue("@secondaryPartyName", (object)transaction.SecondaryPartyName ?? DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@secondaryPartyAccountNumber", (object)transaction.SecondaryPartyAccountNumber ?? DBNull.Value);

                    return sqlCommand.ExecuteNonQuery();
                }
            }
        }

        public List<Transaction> GetTransactionsByAccountNumber(string accountNumber)
        {
            List<Transaction> transactionList = new List<Transaction>();

            using (SqlConnection sqlConnection = new SqlConnection(DatabaseAccess.ConnectionString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "SELECT * FROM [Transaction] WHERE accountNumber = @accountNumber";
                    sqlCommand.Parameters.AddWithValue("@accountNumber", accountNumber);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            transactionList.Add(new Transaction
                            {
                                TransactionId = (int)reader["transactionId"],
                                CardNumber = reader["cardNumber"] as string,
                                AccountNumber = reader["accountNumber"] as string,
                                Amount = decimal.ToDouble((decimal)reader["amount"]),
                                BalanceAfterTransaction = decimal.ToDouble((decimal)reader["balanceAfterTransaction"]),
                                Date = (DateTime)reader["date"],
                                SecondaryPartyName = reader["secondaryPartyName"] as string,
                                SecondaryPartyAccountNumber = reader["secondaryPartyAccountNumber"] as string
                            });
                        }
                    }
                }
            }
            return transactionList;
        }
    }
}
