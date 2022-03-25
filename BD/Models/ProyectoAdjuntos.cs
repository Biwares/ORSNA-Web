namespace BD.Models
{
    public partial class ProyectoAdjuntos : BaseAdjuntos
    {
        public int? IdProyecto { get; set; }
        public virtual Proyectos IdProyectoNavigation { get; set; }
    }
}
