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
            List<Anagrafica> listaPersone = getAnagrafica();
            return View(listaPersone);
        }

        // GET: Anagrafica/Details/5
        public ActionResult Details(int id)
        {
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
        public ActionResult Create(FormCollection collection)
        {
            using (SqlConnection conn = Connection.GetConn())
                try
                {
                    conn.Open();
                    // Creo il comando SQL per l'inserimento dei dati
                    SqlCommand cmd = new SqlCommand("" +
                        "INSERT INTO Anagrafica (Cognome, Nome, Indirizzo, Citta, CAP, Cod_Fisc) " +
                        "VALUES (@Cognome, @Nome, @Indirizzo, @Citta, @CAP, @Cod_Fisc)", conn);

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
            Anagrafica anagrafica = getAnagraficaByID(id);
            return View(anagrafica);
        }

        // POST: Anagrafica/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {

            using (SqlConnection conn = Connection.GetConn())
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

        public static List<Anagrafica> getAnagrafica()
        {
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
                    return null;
                }
                finally
                {
                    conn.Close();
                }

            return listaAnagrafica;
        }

        public static Anagrafica getAnagraficaByID(int id)
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
