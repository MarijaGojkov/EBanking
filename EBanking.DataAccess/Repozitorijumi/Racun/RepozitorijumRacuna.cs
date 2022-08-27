using EBanking.DataAccess.Modeli;
using System.Data.SqlClient;

namespace EBanking.DataAccess.Repozitorijumi.Racun
{
    public class RepozitorijumRacuna : IRepozitorijumRacuna
    {
        private const string _konekcioniString = @"Data Source = .\SQLEXPRESS;Initial Catalog=eBanking;Integrated Security=True";

        public void CreateRacun(TekuciRacun model)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_konekcioniString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "INSERT INTO TekuciRacun(brojRacuna, idKorisnika, balans, valuta, tip, datumKreiranja)" +
                        "VALUES(@brojRacuna, @idKorisnika, @balans, @valuta, @tip, @datumKreiranja)";
                    sqlCommand.Parameters.AddWithValue("@brojRacuna", model.BrojRacuna);
                    sqlCommand.Parameters.AddWithValue("@idKorisnika", model.IdKorisnika);
                    sqlCommand.Parameters.AddWithValue("@balans", model.Balans);
                    sqlCommand.Parameters.AddWithValue("@valuta", model.Valuta);
                    sqlCommand.Parameters.AddWithValue("@tip", model.Tip);
                    sqlCommand.Parameters.AddWithValue("@datumKreiranja", model.DatumKreiranja);

                    sqlCommand.ExecuteScalar();
                }
            }

        }

        public List<TekuciRacun> GetRacuniByKorsnikId(string idKorisnika)
        {
            List<TekuciRacun> racuni = new List<TekuciRacun>();

            using (SqlConnection sqlConnection = new SqlConnection(_konekcioniString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "SELECT * FROM TekuciRacun INNER JOIN Korisnik " +
                        "ON TekuciRacun.idKorisnika = Korisnik.idKorisnika" +
                        "WHERE TekuciRacun.idKorisnika = @idKorsnika";
                    sqlCommand.Parameters.AddWithValue("@idKorsnika", idKorisnika);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            racuni.Add(new TekuciRacun
                            {
                                IdKorisnika = (int)reader["idKorisnika"],
                                BrojRacuna = reader["brojRacuna"] as string,
                                Balans = Decimal.ToDouble((decimal)reader["balans"]),
                                DatumKreiranja = (DateTime)reader["datumKreiranja"],
                                Tip = reader["tip"] as string,
                                Valuta = reader["valuta"] as string,
                                Korisnik = new Modeli.Korisnik
                                {
                                    Ime = reader["ime"] as string,
                                    Prezime = reader["prezime"] as string,
                                    DatumRodjenja = (DateTime)reader["datumRodjenja"],
                                    Email = reader["email"] as string,
                                    Lozinka = reader["lozinka"] as string
                                }
                            });
                        }
                    }
                }

            }
            return racuni;
        }
        public TekuciRacun UpdateBalance(double balans)
        {
            throw new NotImplementedException();
        }
    }
}
