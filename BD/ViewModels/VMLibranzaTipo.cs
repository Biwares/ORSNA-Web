using System;
using System.Collections.Generic;
using BD.Models;
using BD.Utilities;

namespace BD.ViewModels
{
    public class VMLibranzaTipo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public static VMLibranzaTipo Map(BD.Models.LibranzaTipo l, string con)
        {
            OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
            VMLibranzaTipo response = new VMLibranzaTipo();

            response.Id = l.Id;
            response.Nombre = l.Nombre;

            return response;
        }

        public static ICollection<VMLibranzaTipo> MapList(ICollection<BD.Models.LibranzaTipo> adj, string con)
        {
            ICollection<VMLibranzaTipo> listResponse = new System.Collections.ObjectModel.Collection<VMLibranzaTipo>();

            foreach (var lt in adj)
            {
                listResponse.Add(VMLibranzaTipo.Map(lt, con));
            }

            return listResponse;
        }
    }
}
