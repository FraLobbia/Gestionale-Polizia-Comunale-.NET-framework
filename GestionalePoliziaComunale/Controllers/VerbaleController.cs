using GestionalePoliziaComunale.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;
namespace GestionalePoliziaComunale.Controllers
{
    public class VerbaleController : Controller
    {
        // GET: Verbale
        public ActionResult Index()
        {
            List<Verbale> listaVerbali = new List<Verbale>();

            using (SqlConnection conn = Connection.GetConn())
                try
                {
                    conn.Open();

                    // Creo la query per il db
                    string query = "SELECT V.id_Verbale, A.Nome, A.Cognome, V.Data_Violazione, V.Data_Trascrizione_Verbale, v.Indirizzo_Violazione, v.Nominativo_Agente, v.Importo,v.Decurtamento_Punti\r\nFROM Verbali AS V\r\nINNER JOIN Anagrafica AS A ON V.id_Anagrafica = A.id_Anagrafica";

                    // Creo il comando per il db
                    SqlCommand cmd = new SqlCommand(query, conn);

                    // Eseguo il comando e ottengo il risultato
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Verbale verbale = new Verbale(
                            idVerbale: reader.GetInt32(reader.GetOrdinal("id_Verbale")),
                            nomeVerbalizzato: reader.GetString(reader.GetOrdinal("Nome")),
                            cognomeVerbalizzato: reader.GetString(reader.GetOrdinal("Cognome")),
                            dataViolazione: reader.GetDateTime(reader.GetOrdinal("Data_Violazione")).ToString("dd/MM/yyyy"),
                            dataTrascrizioneVerbale: reader.GetDateTime(reader.GetOrdinal("Data_Trascrizione_Verbale")).ToString("dd/MM/yyyy"),
                            indirizzoViolazione: reader.GetString(reader.GetOrdinal("Indirizzo_Violazione")),
                            nominativoAgente: reader.GetString(reader.GetOrdinal("Nominativo_Agente")),
                            importo: reader.GetDecimal(reader.GetOrdinal("Importo")),
                            decurtamentoPunti: reader.GetInt32(reader.GetOrdinal("Decurtamento_Punti"))
                            );


                        // Aggiungo l'oggetto Verbale alla lista per ogni riga del db
                        listaVerbali.Add(verbale);
                    }

                }
                catch (SqlException ex)
                {
                    Response.Write("Errore ");
                    Response.Write(ex.Message);
                    return null;
                }
                finally
                {
                    conn.Close();
                }

            return View(listaVerbali);
        }

        // GET: Verbale/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Verbale/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Verbale/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            using (SqlConnection conn = Connection.GetConn())
                try
                {
                    conn.Open();

                    // Creo il comando SQL per l'inserimento dei dati
                    string query = "INSERT INTO Verbali (Data_Violazione, Indirizzo_Violazione, Nominativo_Agente, Data_Trascrizione_Verbale, Importo, Decurtamento_Punti, id_Violazione, id_Anagrafica) VALUES (@Data_Violazione, @Indirizzo_Violazione, @Nominativo_Agente, @Data_Trascrizione_Verbale, @Importo, @Decurtamento_Punti, @id_Violazione, @id_Anagrafica)";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    // Aggiungo i parametri al comando SQL
                    cmd.Parameters.AddWithValue("@Data_Violazione", collection["Data_Violazione"]);
                    cmd.Parameters.AddWithValue("@Indirizzo_Violazione", collection["Indirizzo_Violazione"]);
                    cmd.Parameters.AddWithValue("@Nominativo_Agente", collection["Nominativo_Agente"]);
                    cmd.Parameters.AddWithValue("@Data_Trascrizione_Verbale", collection["Data_Trascrizione_Verbale"]);
                    cmd.Parameters.AddWithValue("@Importo", collection["Importo"]);
                    cmd.Parameters.AddWithValue("@Decurtamento_Punti", collection["Decurtamento_Punti"]);
                    cmd.Parameters.AddWithValue("@id_Violazione", collection["id_Violazione"]);
                    cmd.Parameters.AddWithValue("@id_Anagrafica", collection["id_Anagrafica"]);

                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    return View();
                }
                finally
                {
                    conn.Close();
                }

            return RedirectToAction("Index");
        }

        // GET: Verbale/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Verbale/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Verbale/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Verbale/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
