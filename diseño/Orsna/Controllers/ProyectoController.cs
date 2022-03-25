using System;
using BD.Models;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using BL.Proyecto;
using BD.ViewModels;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Newtonsoft.Json;
using Orsna.Helpers;
using Microsoft.Extensions.Configuration;

namespace Orsna.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectoController : Controller
    {
        private IConfiguration configuration;
        public ProyectoController(IConfiguration iConfig)
        {
            configuration = iConfig;
        }

        [HttpGet]
        public IActionResult Get()
        {
            BLProyecto BL = new BLProyecto(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            ICollection<VMProyecto> getAll = BL.GetAll();
            return Json(new ResultDto<VMProyecto>("success", getAll));
        }
        [HttpGet("[action]")]
        public JsonResult GetAll(int page, string FilterAeropuerto, string FilterIdProyecto, string FilterArea,
            int? FilterEstado, string FilterFecha, string Order, string ColumnOrder)
        {
            BLProyecto proyecto = new BLProyecto(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            ICollection<VMProyecto> data = proyecto.GetAll(page, FilterAeropuerto, FilterIdProyecto, FilterArea,
            FilterEstado, FilterFecha, Order, ColumnOrder);
            
            return Json(new ResultDto<VMProyecto>("success", data));
        }
        [HttpPost("[action]")]
        public JsonResult Save()
        {
            try
            {
                using (var mem = new MemoryStream())
                using (var reader = new StreamReader(mem))
                {
                    Request.Body.CopyTo(mem);

                    var body = reader.ReadToEnd();

                    mem.Seek(0, SeekOrigin.Begin);

                    VMProyecto data = (JsonConvert.DeserializeObject<VMProyecto>(reader.ReadToEnd()));

                    BLProyecto bl = new BLProyecto(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
                    GenericResponse<int> response = bl.Save(data);
                    return Json(response);
                }
            }catch(Exception ex)
            {
                 throw new Exception(ex.ToString());
            }
        }
        [HttpGet("[action]")]
        public int GetCountPages(int page, string FilterAeropuerto, string FilterIdProyecto, string FilterArea,
            int? FilterEstado, string FilterFechaCreacion, string Order, string ColumnOrder)
        {
            BLProyecto BussProyecto = new BLProyecto(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            return BussProyecto.GetCountPages( FilterAeropuerto, FilterIdProyecto, FilterArea,
            FilterEstado, FilterFechaCreacion, Order, ColumnOrder);
        }
        [HttpGet("[action]")]
        public JsonResult GetProyectoById(int idProyecto)
        {
            BLProyecto proyecto = new BLProyecto(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            return Json(proyecto.GetProyectoById(idProyecto));
        }
        [HttpDelete("[action]")]
        public JsonResult Delete(int id)
        {
            BLProyecto proyecto = new BLProyecto(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            return Json(proyecto.Delete(id));
        }
        [HttpGet("[action]")]
        public JsonResult GetEstados()
        {
            BLLibranzaEstado pe = new BLLibranzaEstado(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            return Json(pe.GetAll());
        }
    }
}