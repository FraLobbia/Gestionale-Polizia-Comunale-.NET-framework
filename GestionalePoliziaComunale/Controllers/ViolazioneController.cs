using GestionalePoliziaComunale.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
namespace GestionalePoliziaComunale.Controllers
{
    public class ViolazioneController : Controller
    {
        // GET: Violazioni
        public ActionResult Index()
        {
            // Ottengo la lista delle violazioni dal metodo getViolazioni
            List<Violazione> listaViolazioni = getViolazioni();
            return View(listaViolazioni);
        }

        // GET: Violazioni/Details/5
        public ActionResult Details(int id)
        {
            // Ottengo la violazione con l'id specificato dal metodo getViolazioneByID
            Violazione violazione = getViolazioneByID(id);
            return View(violazione);
        }

        // GET: Violazioni/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Violazioni/Create
        [HttpPost]
        public ActionResult Create(Violazione formViolazione)
        {
            using (SqlConnection conn = Connection.GetConn())
                try
                {
                    // Apro la connessione al db
                    conn.Open();

                    // Creo il comando per l'inserimento
                    SqlCommand cmd = new SqlCommand("INSERT INTO Violazione (descrizione) VALUES (@descrizione)", conn);
                    // Aggiungo i parametri al comando
                    //cmd.Parameters.AddWithValue("@descrizione", collection["descrizione"]);
                    cmd.Parameters.AddWithValue("@descrizione", formViolazione.Descrizione);

                    // Eseguo il comando
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // In caso di errore stampo un messaggio di errore nella view che ritorno
                    Response.Write("Errore: " + ex.Message);
                }
                finally
                {
                    conn.Close(); // Chiudo la connessione al db, NECESSARIO
                }

            // redirect alla pagina Index
            return RedirectToAction("Index");
        }

        // GET: Violazioni/Edit/5
        public ActionResult Edit(int id)
        {
            // Ottengo la violazione con l'id specificato dal metodo getViolazioneByID
            Violazione violazione = getViolazioneByID(id);
            return View(violazione);
        }

        // POST: Violazioni/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                // Apro la connessione al db
                conn.Open();
                // Creo il comando per l'aggiornamento
                SqlCommand cmd = new SqlCommand("UPDATE Violazione SET " +
                    "descrizione = @descrizione " +
                    "WHERE id_Violazione = @id", conn);

                // Aggiungo i parametri al comando
                cmd.Parameters.AddWithValue("@descrizione", collection["descrizione"]);

                // Aggiungo il parametro per la clausola WHERE
                cmd.Parameters.AddWithValue("@id", id);

                // Eseguo il comando
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Response.Write("Errore: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            // redirect alla pagina Index
            return RedirectToAction("Index");
        }

        // GET: Violazioni/Delete/5
        public ActionResult Delete(int id)
        {
            // Ottengo la violazione con l'id specificato dal metodo getViolazioneByID
            Violazione violazione = getViolazioneByID(id);
            return View(violazione);
        }

        // POST: Violazioni/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            using (SqlConnection conn = Connection.GetConn())
                try
                {
                    // Apro la connessione al db
                    conn.Open();
                    // Creo il comando per l'eliminazione
                    SqlCommand cmd = new SqlCommand("DELETE FROM Violazione WHERE id_Violazione = @id", conn);
                    // Aggiungo i parametri al comando
                    cmd.Parameters.AddWithValue("@id", id);
                    // Eseguo il comando
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Response.Write("Errore: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }

            // redirect alla pagina Index
            return RedirectToAction("Index");
        }


        // Metodo per ottenere la lista delle violazioni
        // Non riceve parametri
        // Restituisce una lista di violazioni
        public List<Violazione> getViolazioni()
        {
            // Creo la lista di violazioni da popolare successivamente
            List<Violazione> listaViolazioni = new List<Violazione>();

            using (SqlConnection conn = Connection.GetConn())
                try
                {
                    // Apro la connessione al db
                    conn.Open();

                    // Creo il comando per l'estrazione dei dati
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Violazione", conn);

                    // Eseguo il comando
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Leggo i dati
                    while (reader.Read())
                    {
                        // Creo un oggetto Violazione da aggiungere alla lista
                        Violazione SingleViolazione = new Violazione(
                            id_Violazione: reader.GetInt32(0),
                            descrizione: reader.GetString(1)
                            );

                        // Aggiungo l'oggetto alla lista
                        listaViolazioni.Add(SingleViolazione);
                    }
                }
                catch
                {
                    return null; // Restituisco null in caso di errore perchè
                                 // non posso restituire una lista vuota dal metodo
                }
                finally
                {
                    conn.Close();
                }

            // Restituisco la lista
            return listaViolazioni;
        }

        // Metodo per ottenere una singola violazione
        // Riceve un parametro di tipo int
        // Restituisce un oggetto di tipo Violazione
        public Violazione getViolazioneByID(int id)
        {
            // Creo un oggetto Violazione da restituire
            Violazione singleViolazione = new Violazione();

            using (SqlConnection conn = Connection.GetConn())
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Violazione WHERE id_Violazione = @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        singleViolazione.id_Violazione = reader.GetInt32(0);
                        singleViolazione.Descrizione = reader.GetString(1);
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("Errore: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            // Restituisco la violazione
            return singleViolazione;
        }
    }
}
