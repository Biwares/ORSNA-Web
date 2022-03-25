namespace BD.Models
{
    public partial class LibranzaAdjuntos : BaseAdjuntos
    {
        public int? IdLibranza { get; set; }
        public virtual Libranzas IdLibranzaNavigation { get; set; }
    }
}
