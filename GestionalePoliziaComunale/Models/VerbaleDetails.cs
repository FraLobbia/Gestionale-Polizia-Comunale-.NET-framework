using System;
using System.ComponentModel.DataAnnotations;

namespace GestionalePoliziaComunale.Models
{
    public class VerbaleDetails : Verbale
    {

        [Display(Name = "Nome Verbalizzato")]
        public string nomeVerbalizzato { get; set; }


        [Display(Name = "Cognome Verbalizzato")]
        public string cognomeVerbalizzato { get; set; }


        [Display(Name = "Tipo Violazione")]
        public string tipoViolazione { get; set; }


        public VerbaleDetails()
        {
        }

        // Costruttore
        public VerbaleDetails(int idVerbale, DateTime dataViolazione, String indirizzoViolazione, String nominativoAgente,
                       DateTime dataTrascrizioneVerbale, decimal importo, int decurtamentoPunti, int id_Violazione,
                       int idAnagrafica)
        {
            this.id_Verbale = idVerbale;
            this.Data_Violazione = dataViolazione;
            this.Indirizzo_Violazione = indirizzoViolazione;
            this.Nominativo_Agente = nominativoAgente;
            this.Data_Trascrizione_Verbale = dataTrascrizioneVerbale;
            this.Importo = importo;
            this.Decurtamento_Punti = decurtamentoPunti;
            this.id_Violazione = id_Violazione;
            this.id_Anagrafica = idAnagrafica;
        }

        public VerbaleDetails(int idVerbale, string nomeVerbalizzato, string cognomeVerbalizzato,
               DateTime dataViolazione, DateTime dataTrascrizioneVerbale,
               string indirizzoViolazione, string nominativoAgente,
               decimal importo, int decurtamentoPunti)
        {
            this.id_Verbale = idVerbale;
            this.nomeVerbalizzato = nomeVerbalizzato;
            this.cognomeVerbalizzato = cognomeVerbalizzato;
            this.Data_Violazione = dataViolazione;
            this.Data_Trascrizione_Verbale = dataTrascrizioneVerbale;
            this.Indirizzo_Violazione = indirizzoViolazione;
            this.Nominativo_Agente = nominativoAgente;
            this.Importo = importo;
            this.Decurtamento_Punti = decurtamentoPunti;
        }


    }

}