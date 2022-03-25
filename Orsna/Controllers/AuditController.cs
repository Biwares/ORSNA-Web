using BD.Models;
using BD.ViewModels;
using BL.Audit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Orsna.Helpers;
using System.Collections.Generic;

namespace Orsna.Controllers
{
    public class AuditController : BaseController
    {
        public AuditController(IConfiguration iConfig):base(iConfig)
        {
            configuration = iConfig;
        }
        [HttpGet]
        public IActionResult Get()
        {
            BLAudit BL = new BLAudit(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"),userId);
            ICollection<VMLog> getAll = BL.GetAll();
            return Json(new ResultDto<VMLog>("success", getAll));
        }
        
        [HttpGet("[action]")]
        public JsonResult GetAll(int page, string FilterFechaDesde, string FilterFechaHasta, string FilterMensaje, string FilterDetalle, string FilterUserName, string Order, string ColumnOrder)
        {
            BLAudit bl = new BLAudit(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            IList<Log> data = bl.GetAll(page, FilterFechaDesde, FilterFechaHasta, FilterMensaje, FilterDetalle, FilterUserName, Order, ColumnOrder);
            return Json(data);
        }
        [HttpGet("[action]")]
        public int GetCountPages(int page, string FilterFechaDesde, string FilterFechaHasta, string FilterMensaje, string FilterDetalle, string FilterUserName, string Order, string ColumnOrder)
        {
            BLAudit bl = new BLAudit(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            return bl.GetCountPages(page, FilterFechaDesde, FilterFechaHasta, FilterMensaje, FilterDetalle, FilterUserName, Order, ColumnOrder);
        }

        [HttpGet("[action]")]
        public int GetCountFilterElements(int page, string FilterFechaDesde, string FilterFechaHasta, string FilterMensaje, string FilterDetalle, string FilterUserName, string Order, string ColumnOrder)
        {
            BLAudit bl = new BLAudit(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            return bl.GetCountFilterElements(page, FilterFechaDesde, FilterFechaHasta, FilterMensaje, FilterDetalle, FilterUserName, Order, ColumnOrder);
        }

        [HttpGet("[action]")]
        public JsonResult GetLogById(int idLog)
        {
            BLAudit bl = new BLAudit(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            return Json(bl.GetLogById(idLog));
        }
    }
}