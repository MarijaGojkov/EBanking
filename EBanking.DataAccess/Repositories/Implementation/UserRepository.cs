using EBanking.DataAccess.Models;
using System.Data.SqlClient;

namespace EBanking.DataAccess.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        public User GetUserById(int id)
        {
            User user = new User();

            using (SqlConnection sqlConnection = new SqlConnection(DatabaseAccess.ConnectionString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "SELECT * FROM [User] WHERE userId = @id";
                    sqlCommand.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user.UserId = (int)reader["userId"];
                            user.FirstName = reader["firstName"] as string;
                            user.LastName = reader["lastName"] as string;
                            user.DateOfBirth = (DateTime)reader["dateOfBirth"];
                            user.IdCardNumber = reader["idCardNumber"] as string;
                            user.Phone = reader["phone"] as string;
                            user.Email = reader["email"] as string;
                            user.UserPin = reader["userPin"] as string;
                            user.Password = reader["password"] as string;
                        }
                    }
                }
            }
            return user;
        }

        public int IsValidUser(string email, string password)
        {
            object userId;

            using (SqlConnection sqlConnection = new SqlConnection(DatabaseAccess.ConnectionString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "SELECT * FROM [User] WHERE email = @email AND password = @password";
                    sqlCommand.Parameters.AddWithValue("@email", email);
                    sqlCommand.Parameters.AddWithValue("@password", password);

                    userId = sqlCommand.ExecuteScalar();
                }
            }
            var id = userId == null ? 0 : (int)userId;
            return id;
        }

        public List<User> GetAllUsers()
        {
            List<User> userList = new List<User>();

            using (SqlConnection sqlConnection = new SqlConnection(DatabaseAccess.ConnectionString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "SELECT * FROM [User]";

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            userList.Add(new User
                            {
                                UserId = (int)reader["userId"],
                                FirstName = reader["firstName"] as string,
                                LastName = reader["lastName"] as string,
                                DateOfBirth = (DateTime)reader["dateOfBirth"],
                                IdCardNumber = reader["idCardNumber"] as string,
                                Phone = reader["phone"] as string,
                                Email = reader["email"] as string,
                                UserPin = reader["userPin"] as string,
                                Password = reader["password"] as string
                            });
                        }
                    }
                }
            }
            return userList;
        }

        public int AddUser(User user)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DatabaseAccess.ConnectionString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "INSERT INTO [User](firstName, lastName, dateOfBirth, idCardNumber, phone, email, userPin, password) " +
                        "VALUES (@firstName, @lastName, @dateOfBirth, @idCardNumber, @phone, @email, @userPin, @password)";
                    sqlCommand.Parameters.AddWithValue("@firstName", user.FirstName);
                    sqlCommand.Parameters.AddWithValue("@lastName", user.LastName);
                    sqlCommand.Parameters.AddWithValue("@dateOfBirth", user.DateOfBirth);
                    sqlCommand.Parameters.AddWithValue("@idCardNumber", user.IdCardNumber);
                    sqlCommand.Parameters.AddWithValue("@phone", user.Phone);
                    sqlCommand.Parameters.AddWithValue("@email", user.Email);
                    sqlCommand.Parameters.AddWithValue("@userPin", user.UserPin);
                    sqlCommand.Parameters.AddWithValue("@password", user.Password);

                    return sqlCommand.ExecuteNonQuery();
                }
            }
        }

        public void UpdateUserPassword(string email, string userPin, string password)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DatabaseAccess.ConnectionString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "UPDATE [User] SET password = @password WHERE email = @email AND userPin = @userPin";
                    sqlCommand.Parameters.AddWithValue("@password", password);
                    sqlCommand.Parameters.AddWithValue("@email", email);
                    sqlCommand.Parameters.AddWithValue("@userPin", userPin);

                    sqlCommand.ExecuteScalar();
                }
            }
        }

        public bool VerifyUser(string email, string userPin)
        {
            object userId;

            using (SqlConnection sqlConnection = new SqlConnection(DatabaseAccess.ConnectionString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "SELECT * FROM [User] WHERE email = @email AND userPin = @userPin";
                    sqlCommand.Parameters.AddWithValue("@email", email);
                    sqlCommand.Parameters.AddWithValue("@userPin", userPin);

                    userId = sqlCommand.ExecuteScalar();
                }
            }

            return userId != null;
        }
    }
}
