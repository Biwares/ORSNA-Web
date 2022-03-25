using BD.Models;
using System.Collections.Generic;

namespace BD.ViewModels
{
    public class VMDestinos
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool RequiereAeropuerto { get; set; }

        public static VMDestinos Map(BD.Models.Destinos p, string con)
        {
            OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
            VMDestinos response = new VMDestinos();

            response.Id = p.Id;
            response.Nombre = p.Nombre;
            response.RequiereAeropuerto = p.RequiereAeropuerto;

            return response;
        }

        public static ICollection<VMDestinos> MapList(ICollection<BD.Models.Destinos> destinos, string con)
        {
            ICollection<VMDestinos> listResponse = new System.Collections.ObjectModel.Collection<VMDestinos>();

            foreach (var a in destinos)
            {
                listResponse.Add(VMDestinos.Map(a, con));
            }

            return listResponse;
        }
    }
}
