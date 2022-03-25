using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BL.Area;
using BD.ViewModels;
using BD.Models;
using BL.Seguridad;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Extensions.Configuration;
using Orsna.Helpers;
using Microsoft.AspNetCore.Http;

namespace Orsna.Controllers
{
    public class AreaController : BaseController
    {
        public AreaController(IConfiguration iConfig) : base(iConfig)
        {
            configuration = iConfig;
        }
        [HttpGet]
        public IActionResult Get()
        {
            BLArea BL = new BLArea(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            ICollection<VMArea> getAll = BL.GetAll(); ;
            return Json(new ResultDto<VMArea>("success", getAll));
        }
        [HttpGet("[action]")]
        public IActionResult GetFiltradasPorUsuario()
        {
            BLSeguridad BLSeguridad = new BLSeguridad(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            List<int> getAreas = BLSeguridad.GetAreasDelUsuario(null, BasicAuthenticationHandler.getUserNameAndPasswordFromHeaders(Request.Headers["Authorization"]).Item1);

            BLArea BL = new BLArea(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            ICollection<Areas> getAll = BL.GetAll(0, "", "", "", "", true, getAreas); ;
            return Json(new ResultDto<Areas>("success", getAll));
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

                Areas data = (JsonConvert.DeserializeObject<Areas>(reader.ReadToEnd()));

                BLArea bl = new BLArea(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
                GenericResponse<bool> response = bl.Save(data);
                return Json(response);
            }
        }
        [HttpGet("[action]")]
        public JsonResult GetAll(int page, string FilterCodArea, string FilterNombre, string Order, string ColumnOrder)
        {
            BLArea bl = new BLArea(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            IList<Areas> data = bl.GetAll(page, FilterCodArea, FilterNombre, Order, ColumnOrder, false, null);
            return Json(data);
        }
        [HttpGet("[action]")]
        public int GetCountPages(int page, string FilterCodArea, string FilterNombre, string Order, string ColumnOrder)
        {
            BLArea bl = new BLArea(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            return bl.GetCountPages(page, FilterCodArea, FilterNombre, Order, ColumnOrder);
        }
        [HttpGet("[action]")]
        public int GetCountFilterElements(int page, string FilterCodArea, string FilterNombre, string Order, string ColumnOrder)
        {
            BLArea bl = new BLArea(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            return bl.GetCountFilterElements(page, FilterCodArea, FilterNombre, Order, ColumnOrder);
        }
        [HttpGet("[action]")]
        public JsonResult GetAreaById(int idArea)
        {
            BLArea bl = new BLArea(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            return Json(bl.GetAreaById(idArea));
        }
        [HttpDelete("[action]")]
        public JsonResult Delete(int id)
        {
            BLArea bl = new BLArea(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            return Json(bl.Delete(id));
        }
    }
}