using System;
using System.Collections.Generic;
using BD.Models;
using BD.Utilities;

namespace BD.ViewModels
{
    public class VMAeropuertosGrupo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public static VMAeropuertosGrupo Map(BD.Models.AeropuertosGrupo a, string con)
        {
            OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
            VMAeropuertosGrupo response = new VMAeropuertosGrupo();

            response.Id = a.Id;
            response.Nombre = a.Nombre;

            return response;
        }

        public static ICollection<VMAeropuertosGrupo> MapList(ICollection<BD.Models.AeropuertosGrupo> adj, string con)
        {
            ICollection<VMAeropuertosGrupo> listResponse = new System.Collections.ObjectModel.Collection<VMAeropuertosGrupo>();

            foreach (var ag in adj)
            {
                listResponse.Add(VMAeropuertosGrupo.Map(ag, con));
            }

            return listResponse;
        }
    }
}