using System;
using System.ComponentModel.DataAnnotations;

namespace GestionalePoliziaComunale.Models
{
    public class Verbale
    {
        [ScaffoldColumn(false)]
        public int id_Verbale { get; set; }

        [Display(Name = "Data Violazione")]
        public DateTime Data_Violazione { get; set; }

        [Display(Name = "Data Trascrizione Verbale")]
        public DateTime Data_Trascrizione_Verbale { get; set; }

        [Display(Name = "Indirizzo Violazione")]
        public string Indirizzo_Violazione { get; set; }

        public decimal Importo { get; set; }

        [Display(Name = "Decurtamento Punti")]
        public int Decurtamento_Punti { get; set; }

        [Display(Name = "Nominativo Agente")]
        public string Nominativo_Agente { get; set; }

        [Display(Name = "Tipo Violazione")]
        public int id_Violazione { get; set; }

        [Display(Name = "Nome Verbalizzato")]
        public int id_Anagrafica { get; set; }

    }
}
