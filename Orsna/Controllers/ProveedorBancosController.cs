using BD.Models;
using BD.ViewModels;
using BL.Beneficiario;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IO;

namespace Orsna.Controllers
{
    public class ProveedorBancosController : BaseController
    {
        public ProveedorBancosController(IConfiguration iConfig):base(iConfig)
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

                BLBeneficiarioBancos BussProveedorBancos = new BLBeneficiarioBancos(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
                GenericResponse<bool> response = BussProveedorBancos.Save(data);
                return Json(response);
            }
        }
        [HttpGet("[action]")]
        public JsonResult GetProveedorBancoById(int id)
        {
            BLBeneficiarioBancos BL = new BLBeneficiarioBancos(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            BeneficiarioBancos bank = BL.GetBancoById(id);
            return Json(bank);
        }
        [HttpDelete("[action]")]
        public JsonResult Delete(int id)
        {
            BLBeneficiarioBancos BussBancoProveedor = new BLBeneficiarioBancos(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            return Json(BussBancoProveedor.Delete(id));
        }
        [HttpGet("[action]")]
        public JsonResult GetBancosToProveedor(int idProveedor)
        {
            BLBeneficiarioBancos bbb = new BLBeneficiarioBancos(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            return Json(bbb.GetBancosToProveedor(idProveedor));
        }
    }
}