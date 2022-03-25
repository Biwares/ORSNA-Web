using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD.ViewModels
{
    public class LoginResponse
    {
        public string Response { get; set; }
        public bool Estado { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int IdUsuario { get; set; }
    }
}