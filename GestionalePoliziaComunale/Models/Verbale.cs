using System;
using System.ComponentModel.DataAnnotations;

namespace GestionalePoliziaComunale.Models
{
    public class Verbale
    {
        [ScaffoldColumn(false)]
        public int id_Verbale { get; set; }


        [Display(Name = "Nome Verbalizzato")]
        public string nomeVerbalizzato { get; set; }

        [Display(Name = "Cognome Verbalizzato")]
        public string cognomeVerbalizzato { get; set; }


        [Display(Name = "Data Violazione")]
        public string data_Violazione { get; set; }



        [Display(Name = "Data Trascrizione Verbale")]
        public string data_Trascrizione_Verbale { get; set; }


        [Display(Name = "Indirizzo Violazione")]
        public String indirizzo_Violazione { get; set; }


        [Display(Name = "Tipo Violazione")]
        public string tipoViolazione { get; set; }

        public decimal importo { get; set; }


        [Display(Name = "Decurtamento Punti")]
        public int decurtamento_Punti { get; set; }

        [Display(Name = "Nominativo Agente")]
        public String nominativo_Agente { get; set; }



        [ScaffoldColumn(false)]
        public int id_Violazione { get; set; }

        [ScaffoldColumn(false)]
        public int id_Anagrafica { get; set; }



        public Verbale()
        {
        }


        // Costruttore
        public Verbale(int idVerbale, string dataViolazione, String indirizzoViolazione, String nominativoAgente,
                       string dataTrascrizioneVerbale, decimal importo, int decurtamentoPunti, int idViolazione,
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

        public Verbale(int idVerbale, string nomeVerbalizzato, string cognomeVerbalizzato,
               string dataViolazione, string dataTrascrizioneVerbale,
               string indirizzoViolazione, string nominativoAgente,
               decimal importo, int decurtamentoPunti)
        {
            this.id_Verbale = idVerbale;
            this.nomeVerbalizzato = nomeVerbalizzato;
            this.cognomeVerbalizzato = cognomeVerbalizzato;
            this.data_Violazione = dataViolazione;
            this.data_Trascrizione_Verbale = dataTrascrizioneVerbale;
            this.indirizzo_Violazione = indirizzoViolazione;
            this.nominativo_Agente = nominativoAgente;
            this.importo = importo;
            this.decurtamento_Punti = decurtamentoPunti;
        }


    }

}