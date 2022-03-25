using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BD.Models;

namespace BD.ViewModels
{
    public class VMBeneficiario
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; }
        public string Descripcion { get; set; }
        public string Cuit { get; set; }
        public DateTime? FechaAlta { get; set; }
        public string NacionalExtranjero { get; set; }
        public string Email { set; get; }
        public string Telefono { get; set; }
        public ICollection<VMAdjunto> Adjuntos { get; set; }
        public ICollection<VMBeneficiarioBancos> Bancos { get; set; }
        public string nombreCuentaSeleccionada { get; set; }

        public static VMBeneficiario Map(BD.Models.Beneficiarios b, string con)
        {
            OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
            VMBeneficiario response = new VMBeneficiario();

            response.Id = b.Id;
            response.RazonSocial = b.RazonSocial;
            response.Descripcion = b.Descripcion;
            response.Cuit = b.Cuit;
            response.FechaAlta = b.FechaAlta;
            response.NacionalExtranjero = b.NacionalExtranjero.Trim();
            response.Email = b.Email;
            response.Telefono = b.Telefono;
            response.Adjuntos = VMAdjunto.MapList(context.Adjuntos.Where(
                a => a.BeneficiarioAdjuntos.Where(
                    ba => ba.IdBeneficiario == b.Id).Select(
                    ad => ad.IdBeneficiario).Contains(b.Id))
                    .ToList(), con);
            try {
            
                 response.Bancos = VMBeneficiarioBancos.MapList(context.BeneficiarioBancos.Where(
                a => a.IdBeneficiario == b.Id && a.Estado == true).ToList(), con);
                  }
            catch (Exception ex)
                {
                
                }
            return response;
        }

        public static ICollection<VMBeneficiario> MapList(ICollection<BD.Models.Beneficiarios> beneficiario, string con)
        {
            ICollection<VMBeneficiario> listResponse = new System.Collections.ObjectModel.Collection<VMBeneficiario>();

            foreach (var proj in beneficiario)
            {
                listResponse.Add(Map(proj,con));
            }

            return listResponse;
        }
        public static Beneficiarios Map(VMBeneficiario b, string con)
        {
            OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
            Beneficiarios response = new Beneficiarios();

            response.Id = b.Id;
            response.RazonSocial = b.RazonSocial;
            response.Descripcion = b.Descripcion;
            response.Cuit = b.Cuit;
            response.FechaAlta = b.FechaAlta.Value;
            response.NacionalExtranjero = b.NacionalExtranjero.Trim();
            response.Email = b.Email;
            response.Telefono = b.Telefono;
            
            return response;
        }

        public static ICollection<Beneficiarios> MapList(ICollection<VMBeneficiario> beneficiario, string con)
        {
            ICollection<Beneficiarios> listResponse = new System.Collections.ObjectModel.Collection<Beneficiarios>();

            foreach (var proj in beneficiario)
            {
                listResponse.Add(Map(proj, con));
            }

            return listResponse;
        }
    }
}
