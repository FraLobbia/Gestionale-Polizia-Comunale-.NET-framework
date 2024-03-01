using GestionalePoliziaComunale.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
namespace GestionalePoliziaComunale.Controllers
{
    public class AnagraficaController : Controller
    {
        // GET: Anagrafica
        public ActionResult Index()
        {

            // Connessione al db tramite la stringa di connessione presente nel file Web.config     
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString.ToString();

            // Creo la connessione al db tramite la stringa di connessione 
            SqlConnection conn = new SqlConnection(connectionString);

            List<Anagrafica> listaPersone = new List<Anagrafica>();
            try
            {
                // Apro la connessione al db
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Anagrafica", conn);

                // Eseguo il comando e ottengo il risultato
                SqlDataReader reader = cmd.ExecuteReader();

                // Leggo il risultato
                while (reader.Read())
                {
                    // Leggo i valori delle colonne 
                    int id_Anagrafica = reader.GetInt32(0);
                    string Cognome = reader.GetString(1);
                    string Nome = reader.GetString(2);
                    string Indirizzo = reader.GetString(3);
                    string Citta = reader.GetString(4);
                    string CAP = reader.GetString(5);
                    string Cod_Fisc = reader.GetString(6);

                    // Creo un oggetto Anagrafica da aggiungere alla lista "listaPersone"
                    Anagrafica anagrafica = new Anagrafica(id_Anagrafica, Cognome, Nome, Indirizzo, Citta, CAP, Cod_Fisc);

                    // Aggiungo l'oggetto Anagrafica alla lista per ogni riga del db
                    listaPersone.Add(anagrafica);
                }

            }
            // Gestione dell'eccezione
            catch (Exception ex) // ex è l'oggetto che rappresenta l'eccezione
            {
                Response.Write("Errore");
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close(); // Chiudo la connessione al db, NECESSARIO
            }

            return View(listaPersone);
        }

        // GET: Anagrafica/Details/5
        public ActionResult Details(int id)
        {
            Anagrafica anagrafica = Anagrafica.getAnagraficaByID(id);
            return View(anagrafica);
        }

        // GET: Anagrafica/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Anagrafica/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                // Creo il comando SQL per l'inserimento dei dati
                SqlCommand cmd = new SqlCommand("INSERT INTO Anagrafica (Cognome, Nome, Indirizzo, Citta, CAP, Cod_Fisc) VALUES (@Cognome, @Nome, @Indirizzo, @Citta, @CAP, @Cod_Fisc)", conn);

                // Aggiungo i parametri al comando SQL
                cmd.Parameters.AddWithValue("@Cognome", collection["Cognome"]);
                cmd.Parameters.AddWithValue("@Nome", collection["Nome"]);
                cmd.Parameters.AddWithValue("@Indirizzo", collection["Indirizzo"]);
                cmd.Parameters.AddWithValue("@Citta", collection["Citta"]);
                cmd.Parameters.AddWithValue("@CAP", collection["CAP"]);
                cmd.Parameters.AddWithValue("@Cod_Fisc", collection["Cod_Fisc"]);

                // Eseguo il comando SQL
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Response.Write("Errore");
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return RedirectToAction("Index");
        }

        // GET: Anagrafica/Edit/5
        public ActionResult Edit(int id)
        {
            Anagrafica anagrafica = Anagrafica.getAnagraficaByID(id);
            return View(anagrafica);
        }

        // POST: Anagrafica/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "UPDATE Anagrafica SET" +
                    " Cognome = @Cognome," +
                    " Nome = @Nome," +
                    " Indirizzo = @Indirizzo," +
                    " Citta = @Citta," +
                    " CAP = @CAP," +
                    " Cod_Fisc = @Cod_Fisc" +
                    " WHERE id_Anagrafica = @id", conn);

                // Aggiungo i parametri al comando SQL
                cmd.Parameters.AddWithValue("@Cognome", collection["Cognome"]);
                cmd.Parameters.AddWithValue("@Nome", collection["Nome"]);
                cmd.Parameters.AddWithValue("@Indirizzo", collection["Indirizzo"]);
                cmd.Parameters.AddWithValue("@Citta", collection["Citta"]);
                cmd.Parameters.AddWithValue("@CAP", collection["CAP"]);
                cmd.Parameters.AddWithValue("@Cod_Fisc", collection["Cod_Fisc"]);
                cmd.Parameters.AddWithValue("@id", id);

                // Eseguo il comando SQL
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Response.Write("Errore");
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return RedirectToAction("Index");
        }

        // GET: Anagrafica/Delete/5
        public ActionResult Delete(int id)
        {
            Anagrafica anagrafica = Anagrafica.getAnagraficaByID(id);
            return View(anagrafica);
        }

        // POST: Anagrafica/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM Anagrafica " +
                    "WHERE id_Anagrafica = @id", conn);
                // Aggiungo i parametri al comando SQL
                cmd.Parameters.AddWithValue("@id", id);

                // Eseguo il comando SQL
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Response.Write("Errore");
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return RedirectToAction("Index");
        }
    }
}
