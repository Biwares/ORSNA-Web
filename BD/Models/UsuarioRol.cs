using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BD.Models
{
    public partial class UsuarioRol
    {
        public UsuarioRol()
        {
            roles = new HashSet<Rol>();
        }

        public int Id { get; set; }
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        [ForeignKey("Rol")]
        public int IdRol { get; set; }

        public virtual Rol Rol { get; set; }
        public virtual Usuarios Usuario { get; set; }

        [NotMapped]
        public virtual ICollection<Rol> roles { get; set; }
    }
}
