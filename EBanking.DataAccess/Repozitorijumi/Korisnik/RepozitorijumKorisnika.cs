using EBanking.DataAccess.Modeli;
using System.Data.SqlClient;

namespace EBanking.DataAccess.Repozitorijumi.Korisnik
{
    public class RepozitorijumKorisnika : IRepozitorijumKorisnika
    {
        private const string _konekcioniString = @"Data Source = .\SQLEXPRESS;Initial Catalog=eBanking;Integrated Security=True";

        public Modeli.Korisnik GetKorisnikById(int id)
        {
            Modeli.Korisnik korisnik = new Modeli.Korisnik();

            using (SqlConnection sqlConnection = new SqlConnection(_konekcioniString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "SELECT * FROM Korisnik WHERE idKorisnika = @id";
                    sqlCommand.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            korisnik.IdKorisnika = (int)reader["idKorisnika"];
                            korisnik.Ime = reader["ime"] as string;
                            korisnik.Prezime = reader["prezime"] as string;
                            korisnik.DatumRodjenja = (DateTime)reader["datumRodjenja"];
                            korisnik.BrojLicneKarte = reader["brojLicneKarte"] as string;
                            korisnik.Telefon = reader["telefon"] as string;
                            korisnik.Email = reader["email"] as string;
                            korisnik.KorisnickiPin = reader["korisnickiPin"] as string;
                            korisnik.Lozinka = reader["lozinka"] as string;
                        }
                    }
                }

            }
            return korisnik;
        }

        public bool IsValidKorisnik(string email, string password)
        {
            //int idKorisnika;
            object idKorisnika;

            using (SqlConnection sqlConnection = new SqlConnection(_konekcioniString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "SELECT * FROM Korisnik WHERE email = @email AND lozinka = @lozinka";
                    sqlCommand.Parameters.AddWithValue("@email", email);
                    sqlCommand.Parameters.AddWithValue("@lozinka", password);

                    idKorisnika = sqlCommand.ExecuteScalar();
                }

            }

            return idKorisnika != null;
        }
    }
}
