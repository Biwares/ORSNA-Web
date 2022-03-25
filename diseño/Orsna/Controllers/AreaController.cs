using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BL.Area;
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
    public class AreaController : Controller
    {
        private IConfiguration configuration;
        public AreaController(IConfiguration iConfig)
        {
            configuration = iConfig;
        }
        [HttpGet]
        public IActionResult Get()
        {
            BLUsuario BL = new BLUsuario(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            ICollection<VMArea> getAll = BL.GetAll();
            return Json(new ResultDto<VMArea>("success", getAll));
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

                BLUsuario BussArea = new BLUsuario(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
                GenericResponse<bool> response = BussArea.Save(data);
                return Json(response);
            }
        }
        [HttpGet("[action]")]
        public JsonResult GetAll(int page, string FilterCodArea, string FilterNombre, string Order, string ColumnOrder)
        {
            BLUsuario BussArea = new BLUsuario(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            IList<Areas> data = BussArea.GetAll(page, FilterCodArea, FilterNombre, Order, ColumnOrder);
            return Json(data);
        }
        [HttpGet("[action]")]
        public int GetCountPages(int page, string FilterCodArea, string FilterNombre, string Order, string ColumnOrder)
        {
            BLUsuario BussArea = new BLUsuario(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            return BussArea.GetCountPages(page, FilterCodArea, FilterNombre, Order, ColumnOrder);
        }
        [HttpGet("[action]")]
        public JsonResult GetAreaById(int idArea)
        {
            BLUsuario BussArea = new BLUsuario(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            return Json(BussArea.GetAreaById(idArea));
        }
        [HttpDelete("[action]")]
        public JsonResult Delete(int id)
        {
            BLUsuario bussArea = new BLUsuario(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            return Json(bussArea.Delete(id));
        }
        /*[HttpGet("[action]")]
        public JsonResult Prueba()
        {
            return Json("200");
        }*/
    }
}