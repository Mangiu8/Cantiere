namespace Cantiere.Models
{
    public class Dipendenti
    {
        public int IdDipendente { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string CF { get; set; }
        public string Indirizzo { get; set; }
        public bool Coniugato { get; set; }
        public int NumeroFigli { get; set; }
        public string Mansione { get; set; }
        public int IDPagamento { get; set; }
    }
}