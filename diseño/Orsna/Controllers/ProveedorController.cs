using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BL.Beneficiario;
using BD.ViewModels;
using BD.Models;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Extensions.Configuration;
using Orsna.Helpers;

namespace Orsna.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : Controller
    {
        private IConfiguration configuration;
        public ProveedorController(IConfiguration iConfig)
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

                 BLBeneficiario bl = new BLBeneficiario(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
                GenericResponse<int> response = bl.Save(data);
                return Json(response);
            }
        }
        [HttpGet("[action]")]
        public IActionResult GetAll(int page, string FilterRazonSocial, string FilterCuit, string NacionalExtranjero, string Order, string ColumnOrder)
        {
            BLBeneficiario bl = new BLBeneficiario(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            IList<Beneficiarios> data = bl.GetAll(page, FilterRazonSocial, FilterCuit, NacionalExtranjero, Order, ColumnOrder);
            return Json(data);
        }
        [HttpGet("[action]")]
        public IActionResult GetFilter(string FilterRazonSocial, string FilterCuit)
        {
            BLBeneficiario bl = new BLBeneficiario(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            ICollection<VMBeneficiario> data = bl.GetAll(FilterRazonSocial, FilterCuit);
            return Json(new ResultDto<VMBeneficiario>("success", data));
        }
        [HttpGet]
        public IActionResult Get()
        {
            BLBeneficiario bl = new BLBeneficiario(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            ICollection<VMBeneficiario> data = bl.GetAll();
            return Json(new ResultDto<VMBeneficiario>("success", data));
        }
        [HttpGet("[action]")]
        public int GetCountPages(int page, string FilterRazonSocial, string FilterCuit, string NacionalExtranjero, string Order, string ColumnOrder)
        {
            BLBeneficiario BussProveedor = new BLBeneficiario(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            return BussProveedor.GetCountPages(page, FilterRazonSocial, FilterCuit, NacionalExtranjero, Order, ColumnOrder);
        }
        [HttpGet("[action]")]
        public JsonResult GetProveedorById(int idProveedor)
        {
            BLBeneficiario bb = new BLBeneficiario(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
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
            BLBeneficiario BussProveedor = new BLBeneficiario(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            return Json(BussProveedor.Delete(id));
        }
    }
}