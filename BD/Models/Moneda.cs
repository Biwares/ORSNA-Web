namespace BD.Models
{
    public partial class Moneda
    {
        public Moneda()
        {
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreCorto { get; set; }
        public bool Estado { get; set; }
    }
}
