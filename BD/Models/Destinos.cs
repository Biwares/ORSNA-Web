namespace BD.Models
{
    public partial class Destinos
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool RequiereAeropuerto { get; set; }
        public bool Estado { get; set; }
    }
}
