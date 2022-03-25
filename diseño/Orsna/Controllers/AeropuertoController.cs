using System;
using BD.Models;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using BL.Aeropuerto;
using BD.ViewModels;
using System.Collections.Generic;
using Newtonsoft.Json;
using Orsna.Helpers;
using Microsoft.Extensions.Configuration;

namespace Orsna.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AeropuertoController : Controller
    {
        private IConfiguration configuration;
        public AeropuertoController(IConfiguration iConfig)
        {
            configuration = iConfig;
        }

        [HttpGet]
        public IActionResult Get()
        {
            BLAeropuerto BL = new BLAeropuerto(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            ICollection<VMAeropuerto> getAll = BL.GetAll();
            return Json(new ResultDto<VMAeropuerto>("success", getAll));
        }
        [HttpGet("[action]")]
        public JsonResult GetAll(int page, string FilterNombre, int? FilterGrupo, int? FilterProvincia, string Order, string ColumnOrder)
        {
            BLAeropuerto aero = new BLAeropuerto(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            ICollection<VMAeropuerto> data = aero.GetAll(page, FilterNombre, FilterGrupo, FilterProvincia, Order, ColumnOrder);

            return Json(data);
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

                Aeropuertos data = (JsonConvert.DeserializeObject<Aeropuertos>(reader.ReadToEnd()));

                BLAeropuerto bl = new BLAeropuerto(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
                GenericResponse<bool> response = bl.Save(data);
                return Json(response);
            }
        }
        [HttpGet("[action]")]
        public int GetCountPages(int page, string FilterNombre, int? FilterGrupo, int? FilterProvincia, string Order, string ColumnOrder)
        {
            BLAeropuerto ba = new BLAeropuerto(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            return ba.GetCountPages(page, FilterNombre, FilterGrupo, FilterProvincia, Order, ColumnOrder);
        }
        [HttpGet("[action]")]
        public JsonResult GetAeroById(int id)
        {
            BLAeropuerto ba = new BLAeropuerto(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            return Json(ba.GetAeropuertoById(id));
        }
        [HttpDelete("[action]")]
        public JsonResult Delete(int id)
        {
            BLAeropuerto aer = new BLAeropuerto(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            return Json(aer.Delete(id));
        }
        [HttpGet("[action]")]
        public JsonResult GetAeropuertosGrupo()
        {
            BLAeropuertosGrupo aer = new BLAeropuertosGrupo(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            return Json(aer.GetAll());
        }
    }
}