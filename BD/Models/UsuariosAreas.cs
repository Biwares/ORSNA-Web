using System;
using System.Collections.Generic;

namespace BD.Models
{
    public partial class UsuariosAreas
    {
        public int Id { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdArea { get; set; }
        public bool? Estado { get; set; }

        public virtual Areas IdAreaNavigation { get; set; }
        public virtual Usuarios IdUsuarioNavigation { get; set; }
    }
}
