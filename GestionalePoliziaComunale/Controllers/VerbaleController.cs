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
            List<VerbaleDetails> listaVerbaliDetailed = getVerbaliDetailed();
            return View(listaVerbaliDetailed);
        }

        public ActionResult VerbaliSuperioriA10Punti()
        {
            // Ottengo la lista di Verbali di tipo VerbaleDetails (che estende Verbale) con decurtamento punti > 10
            List<VerbaleDetails> listaVerbaliDetailed = getVerbaliDetailedSuperioriA10Punti();
            return View(listaVerbaliDetailed);
        }

        public ActionResult VerbaliSuperioriA400Euro()
        {
            // Ottengo la lista di Verbali di tipo VerbaleDetails (che estende Verbale) con importo > 400 euro
            List<VerbaleDetails> listaVerbaliDetailed = getVerbaliDetailedSuperioriA400Euro();
            return View(listaVerbaliDetailed);
        }

        // GET: Verbale/Details/5
        public ActionResult Details(int id)
        {
            // Ottengo un Verbale di tipo VerbaleDetails (che estende Verbale) tramite l'id
            VerbaleDetails verbaleDetailed = getVerbaleDetailedByID(id);
            return View(verbaleDetailed);
        }

        // GET: Verbale/Create
        public ActionResult Create()
        {
            // Ottengo le liste di Anagrafica e Violazioni per popolare le dropdownlist
            ViewBag.ListaAnagrafica = getAnagrafica();
            ViewBag.ListaViolazioni = getViolazioni();
            return View();
        }

        // POST: Verbale/Create
        [HttpPost]
        public ActionResult Create(Verbale formVerbale)
        {
            using (SqlConnection conn = Connection.GetConn())
                try
                {
                    conn.Open();

                    // Creo il comando SQL per l'inserimento dei dati
                    string query = "INSERT INTO Verbali (Data_Violazione, Indirizzo_Violazione, Nominativo_Agente, Data_Trascrizione_Verbale, Importo, Decurtamento_Punti, id_Violazione, id_Anagrafica) VALUES (@Data_Violazione, @Indirizzo_Violazione, @Nominativo_Agente, @Data_Trascrizione_Verbale, @Importo, @Decurtamento_Punti, @id_Violazione, @id_Anagrafica)";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    // Aggiungo i parametri al comando SQL
                    cmd.Parameters.AddWithValue("@Data_Violazione", formVerbale.Data_Violazione);
                    cmd.Parameters.AddWithValue("@Indirizzo_Violazione", formVerbale.Indirizzo_Violazione);
                    cmd.Parameters.AddWithValue("@Nominativo_Agente", formVerbale.Nominativo_Agente);
                    cmd.Parameters.AddWithValue("@Data_Trascrizione_Verbale", formVerbale.Data_Trascrizione_Verbale);
                    cmd.Parameters.AddWithValue("@Importo", formVerbale.Importo);
                    cmd.Parameters.AddWithValue("@Decurtamento_Punti", formVerbale.Decurtamento_Punti);
                    cmd.Parameters.AddWithValue("@id_Violazione", formVerbale.id_Violazione);
                    cmd.Parameters.AddWithValue("@id_Anagrafica", formVerbale.id_Anagrafica);

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
            VerbaleDetails verbaleDetailed = getVerbaleDetailedByID(id);
            return View(verbaleDetailed);
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
            // Ottengo un Verbale di tipo VerbaleDetails (che estende Verbale) tramite l'id
            Verbale verbaleDetailed = getVerbaleDetailedByID(id);
            return View(verbaleDetailed);
        }

        // POST: Verbale/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            using (SqlConnection conn = Connection.GetConn())
                try
                {
                    conn.Open();

                    // Creo il comando SQL per l'eliminazione dei dati
                    string query = "DELETE FROM Verbali WHERE id_Verbale = @id";

                    // Creo il comando per il db
                    SqlCommand cmd = new SqlCommand(query, conn);

                    // Aggiungo il parametro al comando SQL
                    cmd.Parameters.AddWithValue("@id", id);

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
            // Ritorno alla Index
            return RedirectToAction("Index");
        }

        // GET: Verbale/PerPersona/5
        public ActionResult PerPersona(int id)
        {
            // Ottengo la lista di Verbali di tipo VerbaleDetails (che estende Verbale) per una persona tramite l'id
            List<VerbaleDetails> listaVerbaliDetailed = getVerbaliDetailedByPerson(id);
            ViewBag.NomeCompleto = getAnagrafica().Find(persona => persona.id_Anagrafica == id).NomeCompleto;
            ViewBag.SommaPunti = sommaPuntiDecurtati(id);
            return View(listaVerbaliDetailed);
        }

        // Metodo per ottenere la lista di Anagrafica dal db
        // Non riceve nulla
        // Ritorna una lista di oggetti di tipo Anagrafica
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
        // Ritorna una lista di oggetti di tipo Violazione
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
        public List<VerbaleDetails> getVerbaliDetailed()
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
        // Ritorna un oggetto di tipo VerbaleDetails (che estende Verbale)
        public VerbaleDetails getVerbaleDetailedByID(int id)
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

        // Metodo per ottenere un Verbale dal db
        // Riceve un id di tipo int
        // Ritorna un oggetto di tipo Verbale
        public Verbale getVerbaleByID(int id)
        {
            // Creo un oggetto Verbale da popolare successivamente
            Verbale verbale = new Verbale();

            using (SqlConnection conn = Connection.GetConn())
                try
                {
                    conn.Open();

                    // Creo la query per il db
                    string query = "SELECT * FROM Verbali WHERE id_Verbale = @id";

                    // Creo il comando per il db
                    SqlCommand cmd = new SqlCommand(query, conn);

                    // Aggiungo il parametro al comando SQL
                    cmd.Parameters.AddWithValue("@id", id);

                    // Eseguo il comando e ottengo il risultato
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        verbale.id_Verbale = reader.GetInt32(reader.GetOrdinal("id_Verbale"));
                        verbale.Data_Violazione = reader.GetDateTime(reader.GetOrdinal("Data_Violazione"));
                        verbale.Data_Trascrizione_Verbale = reader.GetDateTime(reader.GetOrdinal("Data_Trascrizione_Verbale"));
                        verbale.Indirizzo_Violazione = reader.GetString(reader.GetOrdinal("Indirizzo_Violazione"));
                        verbale.Importo = reader.GetDecimal(reader.GetOrdinal("Importo"));
                        verbale.Decurtamento_Punti = reader.GetInt32(reader.GetOrdinal("Decurtamento_Punti"));
                        verbale.Nominativo_Agente = reader.GetString(reader.GetOrdinal("Nominativo_Agente"));
                        verbale.id_Violazione = reader.GetInt32(reader.GetOrdinal("id_Violazione"));
                        verbale.id_Anagrafica = reader.GetInt32(reader.GetOrdinal("id_Anagrafica"));
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
            // Ritorno l'oggetto popolato di tipo Verbale
            return verbale;
        }

        // Metodo per ottenere la lista di Verbali dal db per una persona
        // Riceve un id di tipo int
        // Ritorna una lista di oggetti di tipo VerbaleDetails (che estende Verbale)
        public List<VerbaleDetails> getVerbaliDetailedByPerson(int id)
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
                        "ON V.id_Anagrafica = A.id_Anagrafica " +
                        "WHERE A.id_Anagrafica = @id";

                    // Creo il comando per il db
                    SqlCommand cmd = new SqlCommand(query, conn);

                    // Aggiungo il parametro al comando SQL
                    cmd.Parameters.AddWithValue("@id", id);

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

        // Metodo per sommare i punti decurtati per una persona
        // Riceve un id di tipo int
        // Ritorna un int che rappresenta la somma dei punti decurtati
        public int sommaPuntiDecurtati(int id)
        {
            int sommaPunti = 0;

            using (SqlConnection conn = Connection.GetConn())
                try
                {
                    conn.Open();

                    // Creo la query per il db
                    string query = "SELECT SUM(Decurtamento_Punti) AS SommaPunti FROM Verbali WHERE id_Anagrafica = @id";

                    // Creo il comando per il db
                    SqlCommand cmd = new SqlCommand(query, conn);

                    // Aggiungo il parametro al comando SQL
                    cmd.Parameters.AddWithValue("@id", id);

                    // Eseguo il comando e ottengo il risultato
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        sommaPunti = reader.GetInt32(reader.GetOrdinal("SommaPunti"));
                    }
                }
                catch
                {
                    return 0;
                }
                finally
                {
                    conn.Close();
                }
            return sommaPunti;
        }

        // metodo per ottenere la lista di Verbali detailed che superino 10 punti
        // Non riceve nulla
        // Ritorna una lista di oggetti di tipo VerbaleDetails (che estende Verbale)
        public List<VerbaleDetails> getVerbaliDetailedSuperioriA10Punti()
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
                        "ON V.id_Anagrafica = A.id_Anagrafica " +
                        "WHERE V.Decurtamento_Punti > 10";

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
                    return null; // Ritorno null
                }
                finally
                {
                    conn.Close();
                }
            return listaVerbali;
        }

        // metodo per ottenere la lista di Verbali detailed che abbiano un importo superiore a 400 euro
        // Non riceve nulla
        // Ritorna una lista di oggetti di tipo VerbaleDetails (che estende Verbale)
        public List<VerbaleDetails> getVerbaliDetailedSuperioriA400Euro()
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
                        "ON V.id_Anagrafica = A.id_Anagrafica " +
                        "WHERE V.Importo > 400" +
                        "ORDER BY V.Importo";

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
                    return null; // Ritorno null
                }
                finally
                {
                    conn.Close();
                }
            return listaVerbali;
        }
    }
}
