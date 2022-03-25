using BD.Models;
using BD.ViewModels;
using BL.Beneficiario;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Orsna.Helpers;
using System.Collections.Generic;
using System.IO;

namespace Orsna.Controllers
{
    public class ProveedorController : BaseController
    {
        
        public ProveedorController(IConfiguration iConfig) : base(iConfig)
        {
            configuration = iConfig;
        }
        [HttpPost("[action]")]
        public IActionResult Save()
        {
            using (var mem = new MemoryStream())
            using (var reader = new StreamReader(mem))
            {
                Request.Body.CopyTo(mem);

                var body = reader.ReadToEnd();

                mem.Seek(0, SeekOrigin.Begin);

                VMBeneficiario data = (JsonConvert.DeserializeObject<VMBeneficiario>(reader.ReadToEnd()));

                 BLBeneficiario bl = new BLBeneficiario(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
                GenericResponse<int> response = bl.Save(data);
                return Json(response);
            }
        }
        [HttpGet("[action]")]
        public IActionResult GetAll(int page, string FilterRazonSocial, string FilterCuit, string NacionalExtranjero, string Order, string ColumnOrder)
        {
            BLBeneficiario bl = new BLBeneficiario(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            IList<Beneficiarios> data = bl.GetAll(page, FilterRazonSocial, FilterCuit, NacionalExtranjero, Order, ColumnOrder);

            return Json(new ResultDto<Beneficiarios>("success", data));

        }
        [HttpGet("[action]")]
        public IActionResult GetFilter(string FilterRazonSocial, string FilterCuit)
        {
            BLBeneficiario bl = new BLBeneficiario(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            ICollection<VMBeneficiario> data = bl.GetAll(FilterRazonSocial, FilterCuit);
            return Json(new ResultDto<VMBeneficiario>("success", data));
        }
        [HttpGet]
        public IActionResult Get()
        {
            BLBeneficiario bl = new BLBeneficiario(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            ICollection<VMBeneficiario> data = bl.GetAll();
            return Json(new ResultDto<VMBeneficiario>("success", data));
        }
        [HttpGet("[action]")]
        public int GetCountPages(int page, string FilterRazonSocial, string FilterCuit, string NacionalExtranjero, string Order, string ColumnOrder)
        {
            BLBeneficiario BussProveedor = new BLBeneficiario(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            return BussProveedor.GetCountPages(page, FilterRazonSocial, FilterCuit, NacionalExtranjero, Order, ColumnOrder);
        }

        [HttpGet("[action]")]
        public int GetCountFilterElements(int page, string FilterRazonSocial, string FilterCuit, string NacionalExtranjero, string Order, string ColumnOrder)
        {
            BLBeneficiario BussProveedor = new BLBeneficiario(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            return BussProveedor.GetCountFilterElements(page, FilterRazonSocial, FilterCuit, NacionalExtranjero, Order, ColumnOrder);
        }

        [HttpGet("[action]")]
        public JsonResult GetProveedorById(int idProveedor)
        {
            BLBeneficiario bb = new BLBeneficiario(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            VMBeneficiario beneficiario = bb.GetBeneficiarioById(idProveedor);

            var json = JsonConvert.SerializeObject(beneficiario, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
            return Json(json);
        }
        [HttpDelete("[action]")]
        public IActionResult Delete(int id)
        {
            BLBeneficiario BussProveedor = new BLBeneficiario(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            return Json(BussProveedor.Delete(id));
        }
    }
}