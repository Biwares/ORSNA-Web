using System;
using System.Collections.Generic;
using BD.Models;

namespace BD.ViewModels
{
    public class VMAeropuerto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime? Fechainicio { get; set; }
        public DateTime? Fechafin { get; set; }
        public VMAeropuertosGrupo AeropuertosGrupo { get; set; }
        public string NombreCorto { get; set; }
        public string CodIata { get; set; }

        public static VMAeropuerto Map(BD.Models.Aeropuertos p, string con)
        {
            OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
            VMAeropuerto response = new VMAeropuerto();

            response.Id = p.Id;
            response.Nombre = p.Nombre;
            response.Fechainicio = p.FechaInicio;
            response.Fechafin = p.Fechafin;
            response.AeropuertosGrupo = VMAeropuertosGrupo.Map(context.AeropuertosGrupo.Find(p.IdAeropuertosGrupo), con);
            response.NombreCorto = p.NombreCorto;
            response.CodIata = p.CodIata;

            return response;
        }

        public static List<VMAeropuerto> MapList(ICollection<BD.Models.Aeropuertos> aero, string con)
        {
            List<VMAeropuerto> listResponse = new List<VMAeropuerto>();

            foreach (var proj in aero)
            {
                listResponse.Add(VMAeropuerto.Map(proj, con));
            }

            return listResponse;
        }
    }
}
