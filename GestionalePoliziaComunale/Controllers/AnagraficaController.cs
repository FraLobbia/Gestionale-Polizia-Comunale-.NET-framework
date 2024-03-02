using GestionalePoliziaComunale.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace GestionalePoliziaComunale.Controllers
{
    public class AnagraficaController : Controller
    {
        // GET: Anagrafica
        public ActionResult Index()
        {
            // Ottengo la lista delle persone dal metodo getAnagrafica
            List<Anagrafica> listaPersone = getAnagrafica();
            return View(listaPersone);
        }

        // GET: Anagrafica/Details/5
        public ActionResult Details(int id)
        {
            // Ottengo la persona dal metodo getAnagraficaByID
            Anagrafica anagrafica = getAnagraficaByID(id);
            return View(anagrafica);
        }

        // GET: Anagrafica/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Anagrafica/Create
        [HttpPost]
        public ActionResult Create(Anagrafica formAnagrafica)
        {
            using (SqlConnection conn = Connection.GetConn())
                try
                {
                    conn.Open();

                    // Creo il comando SQL per l'inserimento dei dati
                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Anagrafica (Cognome, Nome, Indirizzo, Citta, CAP, Cod_Fisc) " +
                        "VALUES (@Cognome, @Nome, @Indirizzo, @Citta, @CAP, @Cod_Fisc)", conn);

                    cmd.Parameters.AddWithValue("@Cognome", formAnagrafica.Cognome);
                    cmd.Parameters.AddWithValue("@Nome", formAnagrafica.Nome);
                    cmd.Parameters.AddWithValue("@Indirizzo", formAnagrafica.Indirizzo);
                    cmd.Parameters.AddWithValue("@Citta", formAnagrafica.Citta);
                    cmd.Parameters.AddWithValue("@CAP", formAnagrafica.CAP);
                    cmd.Parameters.AddWithValue("@Cod_Fisc", formAnagrafica.Cod_Fisc);

                    // Eseguo il comando SQL
                    cmd.ExecuteNonQuery();

                    ViewBag.Message = "Utente Registrato con Successo";
                }
                catch (Exception ex)
                {
                    Response.Write("Errore: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }

            // Reindirizzo alla pagina Index
            return RedirectToAction("Index");

        }

        // GET: Anagrafica/Edit/5
        public ActionResult Edit(int id)
        {
            // Ottengo la persona dal metodo getAnagraficaByID
            Anagrafica anagrafica = getAnagraficaByID(id);
            return View(anagrafica);
        }

        // POST: Anagrafica/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Anagrafica formAnagrafica)
        {
            using (SqlConnection conn = Connection.GetConn())
                try
                {
                    conn.Open();

                    // Creo il comando SQL per l'aggiornamento dei dati
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
                    cmd.Parameters.AddWithValue("@Cognome", formAnagrafica.Cognome);
                    cmd.Parameters.AddWithValue("@Nome", formAnagrafica.Nome);
                    cmd.Parameters.AddWithValue("@Indirizzo", formAnagrafica.Indirizzo);
                    cmd.Parameters.AddWithValue("@Citta", formAnagrafica.Citta);
                    cmd.Parameters.AddWithValue("@CAP", formAnagrafica.CAP);
                    cmd.Parameters.AddWithValue("@Cod_Fisc", formAnagrafica.Cod_Fisc);
                    cmd.Parameters.AddWithValue("@id", id);

                    // Eseguo il comando SQL
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
            // Reindirizzo alla pagina Index
            return RedirectToAction("Index");
        }

        // GET: Anagrafica/Delete/5
        public ActionResult Delete(int id)
        {
            // Ottengo la persona dal metodo getAnagraficaByID
            Anagrafica anagrafica = getAnagraficaByID(id);
            return View(anagrafica);
        }

        // POST: Anagrafica/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            using (SqlConnection conn = Connection.GetConn())
                try
                {
                    conn.Open();

                    // Creo il comando SQL per l'eliminazione dei dati
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
                    Response.Write("Errore: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            // Reindirizzo alla pagina Index passando un messaggio di errore
            return RedirectToAction("Index");
        }


        // Metodo per ottenere la lista delle persone
        // Non riceve parametri in ingresso
        // Restituisce una lista di oggetti Anagrafica
        public List<Anagrafica> getAnagrafica()
        {
            // Creo una lista di oggetti Anagrafica da popolare in seguito
            List<Anagrafica> listaAnagrafica = new List<Anagrafica>();

            using (SqlConnection conn = Connection.GetConn())
                try
                {
                    conn.Open();

                    // Creo la query per il db
                    string query = "SELECT * FROM Anagrafica";

                    // Creo il comando per il db
                    SqlCommand cmd = new SqlCommand(query, conn);

                    // Eseguo il comando e ottengo il risultato
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        // Creo un oggetto Anagrafica per ogni riga del db
                        Anagrafica anagrafica = new Anagrafica(
                            id_Anagrafica: reader.GetInt32(reader.GetOrdinal("id_Anagrafica")),
                            Cognome: reader.GetString(reader.GetOrdinal("Cognome")),
                            Nome: reader.GetString(reader.GetOrdinal("Nome")),
                            Indirizzo: reader.GetString(reader.GetOrdinal("Indirizzo")),
                            Citta: reader.GetString(reader.GetOrdinal("Citta")),
                            CAP: reader.GetString(reader.GetOrdinal("CAP")),
                            Cod_Fisc: reader.GetString(reader.GetOrdinal("Cod_Fisc"))
                            );

                        // Aggiungo l'oggetto Anagrafica alla lista per ogni riga del db
                        listaAnagrafica.Add(anagrafica);
                    }
                }
                catch
                {
                    return null; // restituisco null perchè c'è stato un errore e
                                 // non posso restituire la lista dal metodo
                }
                finally
                {
                    conn.Close();
                }
            // Restituisco la lista di oggetti Anagrafica
            return listaAnagrafica;
        }

        // Metodo per ottenere una persona dato il suo id
        // Riceve in ingresso l'id della persona
        // Restituisce un singolo oggetto Anagrafica
        public Anagrafica getAnagraficaByID(int id)
        {
            using (SqlConnection conn = Connection.GetConn())
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
