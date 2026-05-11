using EBanking.DataAccess;
using EBanking.DataAccess.Security;
using System.Data.SqlClient;

// One-shot utility for the password modernization pass.
//
// Usage:
//   dotnet run --project EBanking.TestConsole                 -> re-hash any plaintext rows in [User]
//   dotnet run --project EBanking.TestConsole -- hash <pwd>   -> print a BCrypt hash for <pwd>
//
// "Plaintext" is detected by the absence of the BCrypt marker '$2' at the
// start of the password column. The rehash pass is idempotent: running it
// again on an already-hashed table is a no-op.

if (args.Length >= 2 && args[0].Equals("hash", StringComparison.OrdinalIgnoreCase))
{
    Console.WriteLine(PasswordHasher.Hash(args[1]));
    return;
}

using var connection = new SqlConnection(DatabaseAccess.ConnectionString);
connection.Open();

var rows = new List<(int UserId, string Password)>();
using (var read = connection.CreateCommand())
{
    read.CommandText = "SELECT userId, password FROM [User] WHERE password NOT LIKE '$2%'";
    using var reader = read.ExecuteReader();
    while (reader.Read())
    {
        rows.Add(((int)reader["userId"], (string)reader["password"]));
    }
}

if (rows.Count == 0)
{
    Console.WriteLine("No plaintext passwords found. Nothing to do.");
    return;
}

Console.WriteLine($"Rehashing {rows.Count} user row(s)...");
foreach (var (userId, plaintext) in rows)
{
    using var update = connection.CreateCommand();
    update.CommandText = "UPDATE [User] SET password = @hash WHERE userId = @id";
    update.Parameters.AddWithValue("@hash", PasswordHasher.Hash(plaintext));
    update.Parameters.AddWithValue("@id", userId);
    update.ExecuteNonQuery();
    Console.WriteLine($"  userId={userId} updated");
}
Console.WriteLine("Done.");
