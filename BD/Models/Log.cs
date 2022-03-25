using System;

namespace BD.Models
{
    public partial class Log
    {
        public int Id { get; set; }
        public string Ubicacion { get; set; }
        public string Mensaje { get; set; }
        public string Detalle { get; set; }
        public DateTime Fecha { get; set; }

        public string UserName { get; set; }

    }
}
