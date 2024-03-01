using System;
using System.ComponentModel.DataAnnotations;
namespace GestionalePoliziaComunale.Models
{
    public class Violazione
    {
        [ScaffoldColumn(false)]
        public int id_Violazione { get; set; }

        [Required(ErrorMessage = "Il campo descrizione è obbligatorio")]
        public String Descrizione { get; set; }

        // Costruttore con parametri
        public Violazione(int id_Violazione, String descrizione)
        {
            this.id_Violazione = id_Violazione;
            this.Descrizione = descrizione;
        }

        // Costruttore senza parametri
        public Violazione()
        {
        }

    }
}
