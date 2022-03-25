using System.ComponentModel.DataAnnotations.Schema;

namespace BD.Models
{
    public partial class RolModulo
    {
        public int Id { get; set; }
        [ForeignKey("Rol")]
        public int IdRol { get; set; }
        [ForeignKey("Modulo")]
        public int IdModulo { get; set; }
        public bool Ver { get; set; }
        public bool Editar { get; set; }
        public bool Eliminar { get; set; }

        public virtual Modulos Modulo { get; set; }
        public virtual Rol Rol { get; set; }
    }
}
