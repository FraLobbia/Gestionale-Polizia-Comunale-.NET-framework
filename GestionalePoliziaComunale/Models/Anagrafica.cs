using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
namespace GestionalePoliziaComunale.Models
{
    public class Anagrafica
    {
        [ScaffoldColumn(false)]
        public int id_Anagrafica { get; set; }

        [Required(ErrorMessage = "Il campo Cognome è obbligatorio")]
        public String Cognome { get; set; }
        [Required(ErrorMessage = "Il campo Nome è obbligatorio")]
        public String Nome { get; set; }

        [Required(ErrorMessage = "Il campo Indirizzo è obbligatorio")]
        public String Indirizzo { get; set; }

        [Required(ErrorMessage = "Il campo Città è obbligatorio")]
        public String Citta { get; set; }

        [Required(ErrorMessage = "Il campo CAP è obbligatorio")]
        public String CAP { get; set; }

        [Required(ErrorMessage = "Il campo Codice Fiscale è obbligatorio")]
        [Display(Name = "Codice Fiscale")]
        public String Cod_Fisc { get; set; }

        // Costruttore
        public Anagrafica(int id_Anagrafica, String Cognome, String Nome, String Indirizzo,
                          String Citta, String CAP, String Cod_Fisc)
        {
            this.id_Anagrafica = id_Anagrafica;
            this.Cognome = Cognome;
            this.Nome = Nome;
            this.Indirizzo = Indirizzo;
            this.Citta = Citta;
            this.CAP = CAP;
            this.Cod_Fisc = Cod_Fisc;
        }

        public static Anagrafica getAnagraficaByID(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Anagrafica WHERE id_Anagrafica = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int id_Anagrafica = reader.GetInt32(0);
                    string Cognome = reader.GetString(1);
                    string Nome = reader.GetString(2);
                    string Indirizzo = reader.GetString(3);
                    string Citta = reader.GetString(4);
                    string CAP = reader.GetString(5);
                    string Cod_Fisc = reader.GetString(6);
                    Anagrafica anagrafica = new Anagrafica(id_Anagrafica, Cognome, Nome, Indirizzo, Citta, CAP, Cod_Fisc);

                    // Restituisco l'oggetto Anagrafica
                    return anagrafica;
                }
                else
                {
                    return null;
                }
            }
            catch (SqlException ex)
            {
                throw ex; // Rilancio l'eccezione al chiamante (il controller)
                          // throw è una parola chiave che serve per rilanciare un'eccezione 
            }
            finally
            {
                conn.Close();
            }
        }
    }

}