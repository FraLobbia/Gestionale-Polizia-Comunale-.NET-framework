using System;
using System.ComponentModel.DataAnnotations;
namespace GestionalePoliziaComunale.Models
{
    public class Anagrafica
    {
        [ScaffoldColumn(false)]
        public int id_Anagrafica { get; set; }

        [Required(ErrorMessage = "Il campo Cognome è obbligatorio")]
        public String Cognome { get; set; }

        [Required(ErrorMessage = "Il campo Nome è obbligatorio")]
        public String Nome { get; set; }

        [ScaffoldColumn(false)]
        public String NomeCompleto
        {
            get
            {
                return Cognome + " " + Nome;
            }
        }

        [Required(ErrorMessage = "Il campo Indirizzo è obbligatorio")]
        public String Indirizzo { get; set; }

        [Required(ErrorMessage = "Il campo Città è obbligatorio")]
        public String Citta { get; set; }

        [MaxLength(5, ErrorMessage = "Il CAP deve essere di 5 caratteri")]
        [Required(ErrorMessage = "Il campo CAP è obbligatorio")]
        public String CAP { get; set; }

        //[StringLength(16, ErrorMessage = "Il Codice Fiscale deve essere di 16 caratteri")] // todo: commentato per praticità nelle prove
        [Required(ErrorMessage = "Il campo Codice Fiscale è obbligatorio")]
        [Display(Name = "Codice Fiscale")]
        public String Cod_Fisc { get; set; }

        // Costruttore
        public Anagrafica(int id_Anagrafica, String Cognome, String Nome, String Indirizzo,
                          String Citta, String CAP, String Cod_Fisc)
        {
            this.id_Anagrafica = id_Anagrafica;
            this.Cognome = Cognome;
            this.Nome = Nome;
            this.Indirizzo = Indirizzo;
            this.Citta = Citta;
            this.CAP = CAP;
            this.Cod_Fisc = Cod_Fisc;
        }

        public Anagrafica()
        {
        }
    }

}