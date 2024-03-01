using System;

namespace GestionalePoliziaComunale.Models
{
    public class Verbale
    {
        public int id_Verbale { get; set; }
        public String data_Violazione { get; set; }
        public String indirizzo_Violazione { get; set; }
        public String nominativo_Agente { get; set; }
        public String data_Trascrizione_Verbale { get; set; }
        public decimal importo { get; set; }
        public int decurtamento_Punti { get; set; }
        public int id_Violazione { get; set; }
        public int id_Anagrafica { get; set; }

        // Costruttore
        public Verbale(int idVerbale, String dataViolazione, String indirizzoViolazione, String nominativoAgente,
                       String dataTrascrizioneVerbale, decimal importo, int decurtamentoPunti, int idViolazione,
                       int idAnagrafica)
        {
            this.id_Verbale = idVerbale;
            this.data_Violazione = dataViolazione;
            this.indirizzo_Violazione = indirizzoViolazione;
            this.nominativo_Agente = nominativoAgente;
            this.data_Trascrizione_Verbale = dataTrascrizioneVerbale;
            this.importo = importo;
            this.decurtamento_Punti = decurtamentoPunti;
            this.id_Violazione = idViolazione;
            this.id_Anagrafica = idAnagrafica;
        }

    }

}