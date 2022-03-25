using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BL.Beneficiario;
using BD.ViewModels;
using BD.Models;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Orsna.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorBancosController : Controller
    {
        private IConfiguration configuration;
        public ProveedorBancosController(IConfiguration iConfig)
        {
            configuration = iConfig;
        }
        [HttpPost("[action]")]
        public JsonResult Save()
        {
            using (var mem = new MemoryStream())
            using (var reader = new StreamReader(mem))
            {
                Request.Body.CopyTo(mem);

                var body = reader.ReadToEnd();

                mem.Seek(0, SeekOrigin.Begin);

                BeneficiarioBancos data = (JsonConvert.DeserializeObject<BeneficiarioBancos>(reader.ReadToEnd()));

                BLBeneficiarioBancos BussProveedorBancos = new BLBeneficiarioBancos(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
                GenericResponse<bool> response = BussProveedorBancos.Save(data);
                return Json(response);
            }
        }
        [HttpGet("[action]")]
        public JsonResult GetProveedorBancoById(int id)
        {
            BLBeneficiarioBancos BL = new BLBeneficiarioBancos(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            BeneficiarioBancos bank = BL.GetBancoById(id);
            return Json(bank);
        }
        [HttpDelete("[action]")]
        public JsonResult Delete(int id)
        {
            BLBeneficiarioBancos BussBancoProveedor = new BLBeneficiarioBancos(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            return Json(BussBancoProveedor.Delete(id));
        }
        [HttpGet("[action]")]
        public JsonResult GetBancosToProveedor(int idProveedor)
        {
            BLBeneficiarioBancos bbb = new BLBeneficiarioBancos(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            return Json(bbb.GetBancosToProveedor(idProveedor));
        }
    }
}