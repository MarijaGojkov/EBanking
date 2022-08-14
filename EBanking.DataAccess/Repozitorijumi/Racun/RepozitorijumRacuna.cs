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

        public TekuciRacun GetRacunById(string brojRacuna)
        {
            TekuciRacun racun = new TekuciRacun();

            using (SqlConnection sqlConnection = new SqlConnection(_konekcioniString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "SELECT * FROM TekuciRacun WHERE brojRacuna = @brojRacuna";
                    sqlCommand.Parameters.AddWithValue("@brojRacuna", brojRacuna);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            racun.IdKorisnika = (int)reader["idKorisnika"];
                            racun.BrojRacuna = reader["brojRacuna"] as string;
                            racun.Balans = Decimal.ToDouble((decimal)reader["balans"]);
                            racun.DatumKreiranja = (DateTime)reader["datumKreiranja"];
                            racun.Tip = reader["tip"] as string;
                            racun.Valuta = reader["valuta"] as string;
                        }
                    }
                }

            }
            return racun;
        }
        public TekuciRacun UpdateBalance(double balans)
        {
            throw new NotImplementedException();
        }
    }
}
