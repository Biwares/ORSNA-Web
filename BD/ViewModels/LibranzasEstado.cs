using System;
using System.Collections.Generic;
using BD.Models;
using BD.Utilities;

namespace BD.ViewModels
{
    public class VMLibranzasEstado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public static VMLibranzasEstado Map(BD.Models.LibranzasEstado l, string con)
        {
            OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
            VMLibranzasEstado response = new VMLibranzasEstado();

            response.Id = l.Id;
            response.Nombre = l.Nombre;

            return response;
        }

        public static ICollection<VMLibranzasEstado> MapList(ICollection<BD.Models.LibranzasEstado> adj, string con)
        {
            ICollection<VMLibranzasEstado> listResponse = new System.Collections.ObjectModel.Collection<VMLibranzasEstado>();

            foreach (var le in adj)
            {
                listResponse.Add(VMLibranzasEstado.Map(le, con));
            }

            return listResponse;
        }
    }
}
