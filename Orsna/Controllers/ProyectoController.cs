using BD.ViewModels;
using BL.Proyecto;
using BL.Seguridad;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Orsna.Helpers;
using System;
using System.Collections.Generic;
using System.IO;

namespace Orsna.Controllers
{
    [Produces("application/json")]
    public class ProyectoController : BaseController
    {
        public const int AREA_GAP = 8;
        public ProyectoController(IConfiguration iConfig) : base(iConfig)
        {
            configuration = iConfig;
        }

        [HttpGet]
        public IActionResult Get()
        {
            BLProyecto BL = new BLProyecto(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            ICollection<VMProyecto> getAll = BL.GetAll();
            return Json(new ResultDto<VMProyecto>("success", getAll));
        }
        [HttpGet("[action]")]
        public IActionResult GetAllReducido()
        {
            BLProyecto BL = new BLProyecto(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            ICollection<VMProyecto> getAll = BL.GetAllReducido();
            return Json(new ResultDto<VMProyecto>("success", getAll));
        }
        [HttpGet("[action]")]
        public IActionResult GetAnios()
        {
            BLProyecto BL = new BLProyecto(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            List<int> getAll = BL.GetAnios();
            return Json(getAll);
        }
        [HttpGet("[action]")]
        public IActionResult GetDestinos(int? idCuenta)
        {
            BLProyecto BL = new BLProyecto(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            ICollection<VMDestinos> getAll = BL.GetDestinos(idCuenta);
            return Json(getAll);
        }
        [HttpGet("[action]")]
        public IActionResult GetProyectosIds()
        {
            BLProyecto BL = new BLProyecto(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            BLSeguridad BLSeguridad = new BLSeguridad(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            List<int> getAreas = BLSeguridad.GetAreasDelUsuario(null, BasicAuthenticationHandler.getUserNameAndPasswordFromHeaders(Request.Headers["Authorization"]).Item1);

            ICollection<VMProyecto> getAll = BL.GetProyectosIds(getAreas);
            return Json(getAll);
        }
        [HttpGet("[action]")]
        public JsonResult GetAll(int page, string FilterAeropuerto, string FilterIdProyecto, string FilterArea,
            int? FilterEstado, int FilterFecha, string FilterObra, string Order, string ColumnOrder, string FilterCuentas, int FilterDestino)
        {
            BLSeguridad BLSeguridad = new BLSeguridad(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            List<int> getAreas = BLSeguridad.GetAreasDelUsuario(null, BasicAuthenticationHandler.getUserNameAndPasswordFromHeaders(Request.Headers["Authorization"]).Item1);

            BLProyecto proyecto = new BLProyecto(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            ICollection<VMProyecto> data = proyecto.GetAll(page, FilterAeropuerto, FilterIdProyecto, FilterArea,
            FilterEstado, FilterFecha, FilterObra, getAreas, Order, ColumnOrder, FilterCuentas, FilterDestino);
            
            return Json(new ResultDto<VMProyecto>("success", data));
        }
        [HttpGet("[action]")]
        public JsonResult GetAllResumido(int page, string FilterAeropuerto, string FilterIdProyecto, string FilterArea,
            int? FilterEstado, int FilterFecha, string FilterObra, string Order, string ColumnOrder, string FilterCuentas, int FilterDestino)
        {
            BLSeguridad BLSeguridad = new BLSeguridad(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            List<int> getAreas = BLSeguridad.GetAreasDelUsuario(null, BasicAuthenticationHandler.getUserNameAndPasswordFromHeaders(Request.Headers["Authorization"]).Item1);

            BLProyecto proyecto = new BLProyecto(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            ICollection<VMProyecto> data = proyecto.GetAllResumido(page, FilterAeropuerto, FilterIdProyecto, FilterArea,
            FilterEstado, FilterFecha, FilterObra, getAreas, Order, ColumnOrder, FilterCuentas, FilterDestino);

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

                    VMProyecto data = (JsonConvert.DeserializeObject<VMProyecto>(reader.ReadToEnd(), new JsonSerializerSettings
                    {
                        Culture = new System.Globalization.CultureInfo("es-AR")
                    }));

                    BLProyecto bl = new BLProyecto(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
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
            int? FilterEstado, int FilterFechaCreacion, string FilterObra, string Order, string ColumnOrder, string FilterCuentas, int FilterDestino)
        {
            BLSeguridad BLSeguridad = new BLSeguridad(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            List<int> getAreas = BLSeguridad.GetAreasDelUsuario(null, BasicAuthenticationHandler.getUserNameAndPasswordFromHeaders(Request.Headers["Authorization"]).Item1);

            BLProyecto BussProyecto = new BLProyecto(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            return BussProyecto.GetCountPages(FilterAeropuerto, FilterIdProyecto, FilterArea,
            FilterEstado, FilterFechaCreacion, FilterObra, getAreas, Order, ColumnOrder, FilterCuentas, FilterDestino);
        }

        [HttpGet("[action]")]
        public int GetCountFilterElements(int page, string FilterAeropuerto, string FilterIdProyecto, string FilterArea,
            int? FilterEstado, int FilterFechaCreacion, string FilterObra, string Order, string ColumnOrder, string FilterCuentas, int FilterDestino)
        {
            BLSeguridad BLSeguridad = new BLSeguridad(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            List<int> getAreas = BLSeguridad.GetAreasDelUsuario(null, BasicAuthenticationHandler.getUserNameAndPasswordFromHeaders(Request.Headers["Authorization"]).Item1);

            BLProyecto BussProyecto = new BLProyecto(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            return BussProyecto.GetCountFilterElements( FilterAeropuerto, FilterIdProyecto, FilterArea,
            FilterEstado, FilterFechaCreacion, FilterObra, getAreas, Order, ColumnOrder, FilterCuentas, FilterDestino);
        }
        [HttpGet("[action]")]
        public JsonResult GetProyectoById(int idProyecto)
        {
            BLProyecto proyecto = new BLProyecto(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            BLSeguridad BLSeguridad = new BLSeguridad(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            List<int> getAreas = BLSeguridad.GetAreasDelUsuario(null, BasicAuthenticationHandler.getUserNameAndPasswordFromHeaders(Request.Headers["Authorization"]).Item1);

            return Json(proyecto.GetProyectoById(idProyecto, getAreas.Contains(AREA_GAP)));
        }
        [HttpGet("[action]")]
        public JsonResult PuedeEditarMonto()
        {
            BLSeguridad BLSeguridad = new BLSeguridad(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            List<int> getAreas = BLSeguridad.GetAreasDelUsuario(null, BasicAuthenticationHandler.getUserNameAndPasswordFromHeaders(Request.Headers["Authorization"]).Item1);

            return Json(getAreas.Contains(AREA_GAP));
        }
        [HttpGet("[action]")]
        public JsonResult GetIdTentativo()
        {
            BLProyecto proyecto = new BLProyecto(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            var codigo = proyecto.GetIdTentativo();
            return Json(codigo);
        }
        [HttpDelete("[action]")]
        public JsonResult Delete(int id)
        {
            BLProyecto proyecto = new BLProyecto(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            return Json(proyecto.Delete(id));
        }
        [HttpGet("[action]")]
        public JsonResult GetEstados()
        {
            BLLibranzaEstado pe = new BLLibranzaEstado(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            return Json(pe.GetAll());
        }
    }
}