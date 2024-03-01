using System;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
namespace GestionalePoliziaComunale.Models
{
    public class Violazione
    {
        [ScaffoldColumn(false)]
        public int id_Violazione { get; set; }
        public String descrizione { get; set; }

        public Violazione(int id_Violazione, String descrizione)
        {
            this.id_Violazione = id_Violazione;
            this.descrizione = descrizione;
        }

        public static Violazione getViolazioneByID(int id)
        {

            using (SqlConnection conn = Connection.GetConn())
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Violazione WHERE id_Violazione = @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        int id_Violazione = reader.GetInt32(0);
                        string descrizione = reader.GetString(1);

                        // Creo un oggetto Violazione da restituire
                        return new Violazione(id_Violazione, descrizione);
                    }
                    else
                    {
                        return null;
                    }
                }
                catch
                {
                    return null;
                }
                finally
                {
                    conn.Close();
                }

        }
    }
}
