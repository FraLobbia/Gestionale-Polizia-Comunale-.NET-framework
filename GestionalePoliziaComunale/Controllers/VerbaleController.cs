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
            // Ottengo la lista di Verbali di tipo VerbaleDetails (che estende Verbale)
            List<VerbaleDetails> listaVerbali = getVerbali();
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
            ViewBag.ListaAnagrafica = getAnagrafica();
            ViewBag.ListaViolazioni = getViolazioni();
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
            ViewBag.ListaAnagrafica = getAnagrafica();
            ViewBag.ListaViolazioni = getViolazioni();
            VerbaleDetails verbale = getVerbaleByID(id);
            return View(verbale);
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

        // Metodo per ottenere la lista di Anagrafica dal db
        // Non riceve nulla
        // Ritorna una lista di oggetti Anagrafica
        public List<Anagrafica> getAnagrafica()
        {
            // Creo una lista di oggetti Anagrafica da popolare successivamente
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
                    return null; // Ritorno null perchè non posso ritornare una lista vuota dal metodo
                }
                finally
                {
                    conn.Close();
                }
            // Ritorno la lista popolata
            return listaAnagrafica;
        }

        // Metodo per ottenere la lista di Violazioni dal db
        // Non riceve nulla
        // Ritorna una lista di oggetti Violazione
        public List<Violazione> getViolazioni()
        {
            List<Violazione> listaViolazioni = new List<Violazione>();

            using (SqlConnection conn = Connection.GetConn())
                try
                {
                    conn.Open();

                    // Creo la query per il db
                    string query = "SELECT * FROM Violazione";

                    // Creo il comando per il db
                    SqlCommand cmd = new SqlCommand(query, conn);

                    // Eseguo il comando e ottengo il risultato
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Violazione violazione = new Violazione(
                            id_Violazione: reader.GetInt32(reader.GetOrdinal("id_Violazione")),
                            descrizione: reader.GetString(reader.GetOrdinal("descrizione"))
                            );

                        // Aggiungo l'oggetto Violazione alla lista per ogni riga del db
                        listaViolazioni.Add(violazione);
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

            return listaViolazioni;
        }

        // Metodo per ottenere la lista di Verbali dal db
        // Non riceve nulla
        // Ritorna una lista di oggetti VerbaleDetails (che estende Verbale)
        public List<VerbaleDetails> getVerbali()
        {
            // Creo una lista di oggetti Verbale da popolare successivamente
            List<VerbaleDetails> listaVerbali = new List<VerbaleDetails>();

            using (SqlConnection conn = Connection.GetConn())
                try
                {
                    conn.Open();

                    // Creo la query per il db
                    string query = "SELECT V.id_Verbale," +
                        " A.Nome," +
                        " A.Cognome," +
                        " V.Data_Violazione," +
                        " V.Data_Trascrizione_Verbale," +
                        " v.Indirizzo_Violazione," +
                        " v.Nominativo_Agente," +
                        " v.Importo," +
                        "v.Decurtamento_Punti " +
                        "FROM Verbali AS V " +
                        "INNER JOIN Anagrafica AS A " +
                        "ON V.id_Anagrafica = A.id_Anagrafica";

                    // Creo il comando per il db
                    SqlCommand cmd = new SqlCommand(query, conn);

                    // Eseguo il comando e ottengo il risultato
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        VerbaleDetails verbaleDetailed = new VerbaleDetails(
                            idVerbale: reader.GetInt32(reader.GetOrdinal("id_Verbale")),
                            nomeVerbalizzato: reader.GetString(reader.GetOrdinal("Nome")),
                            cognomeVerbalizzato: reader.GetString(reader.GetOrdinal("Cognome")),
                            dataViolazione: reader.GetDateTime(reader.GetOrdinal("Data_Violazione")),
                            dataTrascrizioneVerbale: reader.GetDateTime(reader.GetOrdinal("Data_Trascrizione_Verbale")),
                            indirizzoViolazione: reader.GetString(reader.GetOrdinal("Indirizzo_Violazione")),
                            nominativoAgente: reader.GetString(reader.GetOrdinal("Nominativo_Agente")),
                            importo: reader.GetDecimal(reader.GetOrdinal("Importo")),
                            decurtamentoPunti: reader.GetInt32(reader.GetOrdinal("Decurtamento_Punti"))
                            );
                        // Aggiungo l'oggetto Verbale alla lista per ogni riga del db
                        listaVerbali.Add(verbaleDetailed);
                    }
                }
                catch
                {
                    return null; // Ritorno null perchè non posso ritornare una lista vuota dal metodo
                }
                finally
                {
                    conn.Close();
                }

            return listaVerbali;
        }

        // Metodo per ottenere un Verbale dal db
        // Riceve un id di tipo int
        // Ritorna un oggetto VerbaleDetails (che estende Verbale)
        public VerbaleDetails getVerbaleByID(int id)
        {
            // Creo un oggetto VerbaleDetails da popolare successivamente
            VerbaleDetails verbale = new VerbaleDetails();

            using (SqlConnection conn = Connection.GetConn())
                try
                {
                    conn.Open();

                    // Creo la query per il db
                    string query = "SELECT V.id_Verbale," +
                        " A.Nome," +
                        " A.Cognome," +
                        " V.Data_Violazione," +
                        " V.Data_Trascrizione_Verbale," +
                        " v.Indirizzo_Violazione," +
                        " v.Nominativo_Agente," +
                        " v.Importo," +
                        "v.Decurtamento_Punti " +
                        "FROM Verbali AS V " +
                        "INNER JOIN Anagrafica AS A " +
                        "ON V.id_Anagrafica = A.id_Anagrafica " +
                        "WHERE V.id_Verbale = @id";

                    // Creo il comando per il db
                    SqlCommand cmd = new SqlCommand(query, conn);

                    // Aggiungo il parametro al comando SQL
                    cmd.Parameters.AddWithValue("@id", id);

                    // Eseguo il comando e ottengo il risultato
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        verbale = new VerbaleDetails(
                            idVerbale: reader.GetInt32(reader.GetOrdinal("id_Verbale")),
                            nomeVerbalizzato: reader.GetString(reader.GetOrdinal("Nome")),
                            cognomeVerbalizzato: reader.GetString(reader.GetOrdinal("Cognome")),
                            dataViolazione: reader.GetDateTime(reader.GetOrdinal("Data_Violazione")),
                            dataTrascrizioneVerbale: reader.GetDateTime(reader.GetOrdinal("Data_Trascrizione_Verbale")),
                            indirizzoViolazione: reader.GetString(reader.GetOrdinal("Indirizzo_Violazione")),
                            nominativoAgente: reader.GetString(reader.GetOrdinal("Nominativo_Agente")),
                            importo: reader.GetDecimal(reader.GetOrdinal("Importo")),
                            decurtamentoPunti: reader.GetInt32(reader.GetOrdinal("Decurtamento_Punti"))
                            );
                    }
                }
                catch
                {
                    return null; // Ritorno null perchè non posso ritornare un oggetto vuoto dal metodo
                }
                finally
                {
                    conn.Close();
                }
            // Ritorno l'oggetto popolato di tipo VerbaleDetails
            return verbale;
        }

    }
}
