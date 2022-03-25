using System;
using System.Collections.Generic;

namespace BD.Models
{
    public partial class Usuarios
    {
        public Usuarios()
        {
            UsuariosAreas = new HashSet<UsuariosAreas>();
            UsuarioRol = new HashSet<UsuarioRol>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool? Estado { get; set; }
        public bool CheckAD { get; set; }


        public virtual ICollection<UsuariosAreas> UsuariosAreas { get; set; }
        public virtual ICollection<UsuarioRol> UsuarioRol { get; set; }
    }
}
