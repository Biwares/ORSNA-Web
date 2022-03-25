using BD.Models;
using System;
using System.Collections.Generic;

namespace BD.ViewModels
{
    public class VMBeneficiarioBancos
    {
        public int Id { get; set; }
        public int? IdBeneficiario { get; set; }
        public string TipoCuenta { get; set; }
        public string NroCuenta { get; set; }
        public string NombreBanco { get; set; }
        public string Sucursal { get; set; }
        public string Cbu { get; set; }
        public string Cuit { get; set; }
        public string Titular { get; set; }
        public DateTime? FechaVigencia { get; set; }
        public string Direccion { get; set; }
        public string BicSwift { get; set; }
        public double? EsNacional { get; set; }

        public static VMBeneficiarioBancos Map(BD.Models.BeneficiarioBancos b, string con)
        {
            OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
            VMBeneficiarioBancos response = new VMBeneficiarioBancos();

            response.Id = b.Id;
            response.IdBeneficiario = b.IdBeneficiario;
            response.TipoCuenta = b.TipoCuenta;
            response.NroCuenta = b.NroCuenta;
            response.NombreBanco = b.NombreBanco;
            response.Sucursal = b.Sucursal;
            response.Cbu = b.Cbu;
            response.Titular = b.Titular;
            response.FechaVigencia = b.FechaVigencia;
            response.Direccion = b.Direccion;
            response.BicSwift = b.BicSwift;
            response.EsNacional = b.EsNacional;
            response.Cuit = b.Cuit;


            return response;
        }

        public static ICollection<VMBeneficiarioBancos> MapList(ICollection<BD.Models.BeneficiarioBancos> bBancos, string con)
        {
            ICollection<VMBeneficiarioBancos> listResponse = new System.Collections.ObjectModel.Collection<VMBeneficiarioBancos>();

            foreach (var bb in bBancos)
            {
                listResponse.Add(Map(bb, con));
            }

            return listResponse;
        }
    }
}
