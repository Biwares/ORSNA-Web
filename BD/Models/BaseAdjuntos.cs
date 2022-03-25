namespace BD.Models
{
    public partial class BaseAdjuntos
    {
        public int Id { get; set; }
        public int? IdAdjunto { get; set; }
        public bool? Estado { get; set; }

        public virtual Adjuntos IdAdjuntoNavigation { get; set; }
    }
}
