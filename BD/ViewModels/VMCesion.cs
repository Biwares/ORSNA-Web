using System;
using System.Collections.Generic;
using BD.Models;
using BD.Utilities;

namespace BD.ViewModels
{
    public class VMCesion
    {
        public int Id { get; set; }
        public int? IdLibranza { get; set; }
        public string Beneficiario { get; set; }
        public string Cuit { get; set; }
        public string RegistraCesion { get; set; }
        public string NroEscritura { get; set; }

        public static VMCesion Map(BD.Models.LibranzaCesiones l, string con)
        {
            OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
            VMCesion response = new VMCesion();

            response.Id = l.Id;
            response.IdLibranza = l.IdLibranza;
            response.Beneficiario = l.Beneficiario;
            response.Cuit = l.Cuit;
            response.RegistraCesion = l.RegistraCesion;
            response.NroEscritura = l.NroEscritura;
            
            return response;
        }

        public static ICollection<VMCesion> MapList(ICollection<BD.Models.LibranzaCesiones> adj, string con)
        {
            ICollection<VMCesion> listResponse = new System.Collections.ObjectModel.Collection<VMCesion>();

            foreach (var lc in adj)
            {
                listResponse.Add(Map(lc, con));
            }

            return listResponse;
        }
    }
}
