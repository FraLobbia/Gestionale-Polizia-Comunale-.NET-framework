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
            // Creo la lista di oggetti Violazione da popolare con i dati del db
            List<Violazione> listaViolazioni = new List<Violazione>();

            using (SqlConnection conn = Connection.GetConn())
                try
                {
                    // Apro la connessione al db
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM Violazione", conn);

                    // Eseguo il comando e ottengo il risultato
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Leggo il risultato
                    while (reader.Read())
                    {
                        // Creo un oggetto Violazione da popolare con i dati del db
                        Violazione violazione = new Violazione(
                            id_Violazione: reader.GetInt32(reader.GetOrdinal("id_Violazione")),
                            descrizione: reader.GetString(reader.GetOrdinal("descrizione"))
                            );

                        // Aggiungo l'oggetto Violazione alla lista per ogni riga del db
                        listaViolazioni.Add(violazione);
                    }

                }
                catch (Exception ex) // ex è l'oggetto che rappresenta l'eccezione
                {
                    Response.Write("Errore");
                    Response.Write(ex.Message);
                }
                finally
                {
                    conn.Close(); // Chiudo la connessione al db, NECESSARIO
                }

            return View(listaViolazioni);
        }

        // GET: Violazioni/Details/5
        public ActionResult Details(int id)
        {
            Violazione violazione = Violazione.getViolazioneByID(id);
            return View(violazione);
        }

        // GET: Violazioni/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Violazioni/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                // Apro la connessione al db
                conn.Open();

                // Creo il comando per l'inserimento
                SqlCommand cmd = new SqlCommand("INSERT INTO Violazione (descrizione) VALUES (@descrizione)", conn);
                // Aggiungo i parametri al comando
                cmd.Parameters.AddWithValue("@descrizione", collection["descrizione"]);

                // Eseguo il comando
                cmd.ExecuteNonQuery();

            }
            catch
            {
                return View();
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
            Violazione violazione = Violazione.getViolazioneByID(id);
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
                SqlCommand cmd = new SqlCommand("UPDATE Violazione SET descrizione = @descrizione WHERE id_Violazione = @id", conn);

                // Aggiungo i parametri al comando
                cmd.Parameters.AddWithValue("@descrizione", collection["descrizione"]);
                // Aggiungo il parametro per la clausola WHERE
                cmd.Parameters.AddWithValue("@id", id);
                // Eseguo il comando
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
            // redirect alla pagina Index
            return RedirectToAction("Index");
        }

        // GET: Violazioni/Delete/5
        public ActionResult Delete(int id)
        {
            Violazione violazione = Violazione.getViolazioneByID(id);
            return View(violazione);
        }

        // POST: Violazioni/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
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
            catch
            {
                return View();
            }
            finally
            {
                conn.Close();
            }
            // redirect alla pagina Index
            return RedirectToAction("Index");
        }
    }
}
