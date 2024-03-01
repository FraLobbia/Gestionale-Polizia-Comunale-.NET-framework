using System;

namespace GestionalePoliziaComunale.Models
{
    public class TipoViolazione
    {
        public int id_Violazione { get; set; }
        public String descrizione { get; set; }

        public TipoViolazione(int idViolazione, String descrizione)
        {
            this.id_Violazione = idViolazione;
            this.descrizione = descrizione;
        }
    }
}