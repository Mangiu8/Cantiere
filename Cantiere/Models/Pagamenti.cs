using System;

namespace Cantiere.Models
{
    public class Pagamenti
    {
        public int IdPagamento { get; set; }
        public DateTime PeriodoPagamento { get; set; }
        public decimal Ammontare { get; set; }
        public bool Stipendio { get; set; }
        public bool Acconto { get; set; }
    }
}