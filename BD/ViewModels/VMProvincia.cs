using System;
using System.Collections.Generic;
using BD.Models;
using BD.Utilities;

namespace BD.ViewModels
{
    public class VMProvincia
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public static VMProvincia Map(BD.Models.Provincias p, string con)
        {
            OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
            VMProvincia response = new VMProvincia();

            response.Id = p.Id;
            response.Nombre = p.Nombre;

            return response;
        }

        public static ICollection<VMProvincia> MapList(ICollection<BD.Models.Provincias> adj, string con)
        {
            ICollection<VMProvincia> listResponse = new System.Collections.ObjectModel.Collection<VMProvincia>();

            foreach (var p in adj)
            {
                listResponse.Add(VMProvincia.Map(p, con));
            }

            return listResponse;
        }
        public static Provincias Map(VMProvincia p, string con)
        {
            OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
            Provincias response = new Provincias();

            response.Id = p.Id;
            response.Nombre = p.Nombre;

            return response;
        }

        public static ICollection<Provincias> MapList(ICollection<VMProvincia> adj, string con)
        {
            ICollection<Provincias> listResponse = new System.Collections.ObjectModel.Collection<Provincias>();

            foreach (var p in adj)
            {
                listResponse.Add(VMProvincia.Map(p, con));
            }

            return listResponse;
        }
    }
}
