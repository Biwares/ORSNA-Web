using System;
using System.Collections.Generic;
using BD.Models;

namespace BD.ViewModels
{
    public class VMRolModulo
    {
        public int Id { get; set; }
        public int IdRol { get; set; }
        public int IdModulo { get; set; }
        public string RolNombre { get; set; }
        public string ModuloNombre { get; set; }
        public bool ver { get; set; }
        public bool editar { get; set; }
        public bool eliminar { get; set; }

        public static VMRolModulo Map(BD.Models.RolModulo p, string con)
        {
            OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
            VMRolModulo response = new VMRolModulo();
 
            response.Id = p.Id;
            response.IdRol = p.IdRol;
            response.RolNombre = context.Rol.Find(p.IdRol).Nombre;
            response.IdModulo = p.IdModulo;
            response.ModuloNombre = context.Modulos.Find(p.IdModulo).Nombre;
            response.ver = p.Ver;
            response.editar = p.Editar;
            response.eliminar = p.Eliminar;

            return response;
        }

        public static VMRolModulo MapModulo(BD.Models.RolModulo p, string con)
        {
            OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
            VMRolModulo response = new VMRolModulo();

            response.IdModulo = p.IdModulo;
            response.ModuloNombre = context.Modulos.Find(p.IdModulo).Nombre;
            response.ver = p.Ver;
            response.editar = p.Editar;
            response.eliminar = p.Eliminar;

            return response;
        }

        public static ICollection<VMRolModulo> MapList(ICollection<BD.Models.RolModulo> rol, string con)
        {
            ICollection<VMRolModulo> listResponse = new System.Collections.ObjectModel.Collection<VMRolModulo>();

            foreach (var a in rol)
            {                
                listResponse.Add(VMRolModulo.Map(a, con));
            }

            return listResponse;
        }

        public static ICollection<VMRolModulo> MapListModulo(List<BD.Models.RolModulo> modulo, string con)
        {
            ICollection<VMRolModulo> listResponse = new System.Collections.ObjectModel.Collection<VMRolModulo>();

            foreach (var a in modulo)
            {
                listResponse.Add(VMRolModulo.MapModulo(a, con));
            }

            return listResponse;
        }
    }
}
