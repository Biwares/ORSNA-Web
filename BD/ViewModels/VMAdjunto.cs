using System;
using System.Collections.Generic;
using BD.Models;
using BD.Utilities;

namespace BD.ViewModels
{
    public class VMAdjunto
    {
        public int Id { get; set; }
        public string Hash { get; set; }
        public string Modulo { get; set; }
        public DateTime FechaAlta { get; set; }
        public string NombreArchivo { get; set; }
        public string TipoAnexo { get; set; }

        public static VMAdjunto Map(BD.Models.Adjuntos p, string con)
        {
            OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
            VMAdjunto response = new VMAdjunto();

            response.Id = p.Id;
            response.Hash = DataEncode.Base64Encode(p.Id.ToString());
            response.FechaAlta = p.FechaAlta;
            response.Modulo = p.Modulo;
            response.NombreArchivo = p.NombreArchivo;
            response.TipoAnexo = p.TipoAnexo;

            return response;
        }

        public static ICollection<VMAdjunto> MapList(ICollection<BD.Models.Adjuntos> adj, string con)
        {
            ICollection<VMAdjunto> listResponse = new System.Collections.ObjectModel.Collection<VMAdjunto>();

            foreach (var proj in adj)
            {
                if(proj.Estado==true)
                    listResponse.Add(VMAdjunto.Map(proj, con));
            }

            return listResponse;
        }
    }
}
