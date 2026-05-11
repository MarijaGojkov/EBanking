using EBanking.DataAccess.Models;
using System.Data.SqlClient;

namespace EBanking.DataAccess.Repositories.Implementation
{
    public class AccountRepository : IAccountRepository
    {
        public void CreateAccount(Account model)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DatabaseAccess.ConnectionString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "INSERT INTO Account(accountNumber, userId, balance, currency, type, dateCreated)" +
                        "VALUES(@accountNumber, @userId, @balance, @currency, @type, @dateCreated)";
                    sqlCommand.Parameters.AddWithValue("@accountNumber", model.AccountNumber);
                    sqlCommand.Parameters.AddWithValue("@userId", model.UserId);
                    sqlCommand.Parameters.AddWithValue("@balance", model.Balance);
                    sqlCommand.Parameters.AddWithValue("@currency", model.Currency);
                    sqlCommand.Parameters.AddWithValue("@type", model.Type);
                    sqlCommand.Parameters.AddWithValue("@dateCreated", model.DateCreated);

                    sqlCommand.ExecuteScalar();
                }
            }
        }

        public async Task<Account> GetAccountByAccountNumber(string accountNumber)
        {
            Account account = new Account();

            using (SqlConnection sqlConnection = new SqlConnection(DatabaseAccess.ConnectionString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "SELECT * FROM Account WHERE accountNumber = @accountNumber";
                    sqlCommand.Parameters.AddWithValue("@accountNumber", accountNumber);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            account.UserId = (int)reader["userId"];
                            account.AccountNumber = reader["accountNumber"] as string;
                            account.Balance = decimal.ToDouble((decimal)reader["balance"]);
                            account.DateCreated = (DateTime)reader["dateCreated"];
                            account.Type = reader["type"] as string;
                            account.Currency = reader["currency"] as string;
                        }
                    }
                }
            }

            return account;
        }

        public List<Account> GetAccountsByUserId(int userId)
        {
            List<Account> accounts = new List<Account>();

            using (SqlConnection sqlConnection = new SqlConnection(DatabaseAccess.ConnectionString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "SELECT * FROM Account INNER JOIN [User] ON Account.userId = [User].userId WHERE [User].userId = @userId";
                    sqlCommand.Parameters.AddWithValue("@userId", userId);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            accounts.Add(new Account
                            {
                                UserId = (int)reader["userId"],
                                AccountNumber = reader["accountNumber"] as string,
                                Balance = decimal.ToDouble((decimal)reader["balance"]),
                                DateCreated = (DateTime)reader["dateCreated"],
                                Type = reader["type"] as string,
                                Currency = reader["currency"] as string,
                                User = new User
                                {
                                    FirstName = reader["firstName"] as string,
                                    LastName = reader["lastName"] as string,
                                    DateOfBirth = (DateTime)reader["dateOfBirth"],
                                    Email = reader["email"] as string,
                                    Password = reader["password"] as string
                                }
                            });
                        }
                    }
                }
            }
            return accounts;
        }

        public bool IsValidAccount(string accountNumber)
        {
            object accountId;

            using (SqlConnection sqlConnection = new SqlConnection(DatabaseAccess.ConnectionString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "SELECT * FROM Account WHERE accountNumber = @accountNumber";
                    sqlCommand.Parameters.AddWithValue("@accountNumber", accountNumber);

                    accountId = sqlCommand.ExecuteScalar();
                }
            }

            return accountId as string is not null;
        }

        public void UpdateBalance(double balance, string accountNumber)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DatabaseAccess.ConnectionString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "UPDATE Account SET balance = @balance WHERE accountNumber = @accountNumber";
                    sqlCommand.Parameters.AddWithValue("@balance", balance);
                    sqlCommand.Parameters.AddWithValue("@accountNumber", accountNumber);

                    sqlCommand.ExecuteScalar();
                }
            }
        }
    }
}
