using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BD.Models
{
    public partial class Rol
    {
        public Rol()
        {
            UsuarioRol = new HashSet<UsuarioRol>();
            Modulos = new HashSet<RolModulo>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool? Estado { get; set; }

        [NotMapped]
        public virtual ICollection<UsuarioRol> UsuarioRol { get; set; }
        public virtual ICollection<RolModulo> Modulos { get; set; }
    }
}
