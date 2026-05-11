using EBanking.DataAccess.Models;
using System.Data;
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
                    sqlCommand.Parameters.Add(new SqlParameter("@amount", SqlDbType.Decimal) { Precision = 18, Scale = 2, Value = transaction.Amount });
                    sqlCommand.Parameters.Add(new SqlParameter("@balanceAfterTransaction", SqlDbType.Decimal) { Precision = 18, Scale = 2, Value = transaction.BalanceAfterTransaction });
                    sqlCommand.Parameters.AddWithValue("@date", transaction.Date);
                    sqlCommand.Parameters.AddWithValue("@secondaryPartyName", (object)transaction.SecondaryPartyName ?? DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@secondaryPartyAccountNumber", (object)transaction.SecondaryPartyAccountNumber ?? DBNull.Value);

                    return sqlCommand.ExecuteNonQuery();
                }
            }
        }

        public void ExecuteTransfer(
            string payerAccountNumber,
            string recipientAccountNumber,
            decimal amount,
            string recipientName,
            string payerFullName,
            DateTime occurredAt)
        {
            using var connection = new SqlConnection(DatabaseAccess.ConnectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                decimal payerBalance = ReadBalanceWithUpdLock(connection, transaction, payerAccountNumber)
                    ?? throw new InvalidOperationException("Payer account not found.");

                if (payerBalance < amount)
                {
                    throw new InvalidOperationException("Insufficient funds.");
                }

                UpdateBalanceByDelta(connection, transaction, payerAccountNumber, -amount);
                decimal payerBalanceAfter = payerBalance - amount;

                decimal? recipientBalance = ReadBalanceWithUpdLock(connection, transaction, recipientAccountNumber);
                if (recipientBalance.HasValue)
                {
                    UpdateBalanceByDelta(connection, transaction, recipientAccountNumber, amount);
                    InsertTransactionRow(
                        connection, transaction,
                        accountNumber: recipientAccountNumber,
                        amount: amount,
                        balanceAfter: recipientBalance.Value + amount,
                        date: occurredAt,
                        secondaryPartyName: payerFullName,
                        secondaryPartyAccountNumber: payerAccountNumber);
                }

                InsertTransactionRow(
                    connection, transaction,
                    accountNumber: payerAccountNumber,
                    amount: amount,
                    balanceAfter: payerBalanceAfter,
                    date: occurredAt,
                    secondaryPartyName: recipientName,
                    secondaryPartyAccountNumber: recipientAccountNumber);

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        private static decimal? ReadBalanceWithUpdLock(SqlConnection conn, SqlTransaction tx, string accountNumber)
        {
            using var cmd = conn.CreateCommand();
            cmd.Transaction = tx;
            cmd.CommandText = "SELECT balance FROM Account WITH (UPDLOCK, ROWLOCK) WHERE accountNumber = @accountNumber";
            cmd.Parameters.AddWithValue("@accountNumber", accountNumber);
            var result = cmd.ExecuteScalar();
            return result is null or DBNull ? null : (decimal)result;
        }

        private static void UpdateBalanceByDelta(SqlConnection conn, SqlTransaction tx, string accountNumber, decimal delta)
        {
            using var cmd = conn.CreateCommand();
            cmd.Transaction = tx;
            cmd.CommandText = "UPDATE Account SET balance = balance + @delta WHERE accountNumber = @accountNumber";
            cmd.Parameters.Add(new SqlParameter("@delta", SqlDbType.Decimal) { Precision = 18, Scale = 2, Value = delta });
            cmd.Parameters.AddWithValue("@accountNumber", accountNumber);
            cmd.ExecuteNonQuery();
        }

        private static void InsertTransactionRow(
            SqlConnection conn,
            SqlTransaction tx,
            string accountNumber,
            decimal amount,
            decimal balanceAfter,
            DateTime date,
            string secondaryPartyName,
            string secondaryPartyAccountNumber)
        {
            using var cmd = conn.CreateCommand();
            cmd.Transaction = tx;
            cmd.CommandText =
                "INSERT INTO [Transaction](accountNumber, cardNumber, amount, balanceAfterTransaction, date, secondaryPartyName, secondaryPartyAccountNumber) " +
                "VALUES (@accountNumber, NULL, @amount, @balanceAfterTransaction, @date, @secondaryPartyName, @secondaryPartyAccountNumber)";
            cmd.Parameters.AddWithValue("@accountNumber", accountNumber);
            cmd.Parameters.Add(new SqlParameter("@amount", SqlDbType.Decimal) { Precision = 18, Scale = 2, Value = amount });
            cmd.Parameters.Add(new SqlParameter("@balanceAfterTransaction", SqlDbType.Decimal) { Precision = 18, Scale = 2, Value = balanceAfter });
            cmd.Parameters.AddWithValue("@date", date);
            cmd.Parameters.AddWithValue("@secondaryPartyName", (object?)secondaryPartyName ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@secondaryPartyAccountNumber", (object?)secondaryPartyAccountNumber ?? DBNull.Value);
            cmd.ExecuteNonQuery();
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
                                Amount = (decimal)reader["amount"],
                                BalanceAfterTransaction = (decimal)reader["balanceAfterTransaction"],
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
