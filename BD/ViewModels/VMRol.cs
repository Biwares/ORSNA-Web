using System;
using System.Collections.Generic;
using BD.Models;

namespace BD.ViewModels
{
    public class VMRol
    {
        public int Id { get; set; }
        public int IdRol { get; set; }
        public string Nombre { get; set; }

        public static VMRol Map(BD.Models.Rol p, string con)
        {
            OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
            VMRol response = new VMRol();

            response.IdRol = p.Id;
            response.Nombre = p.Nombre;

            return response;
        }

        public static ICollection<VMRol> MapList(ICollection<BD.Models.Rol> rol, string con)
        {
            ICollection<VMRol> listResponse = new System.Collections.ObjectModel.Collection<VMRol>();

            foreach (var a in rol)
            {
                listResponse.Add(VMRol.Map(a, con));
            }

            return listResponse;
        }
    }
}
