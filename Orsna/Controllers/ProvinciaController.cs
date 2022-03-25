using BD.ViewModels;
using BL.Proyecto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Orsna.Helpers;
using System.Collections.Generic;

namespace Orsna.Controllers
{
    public class ProvinciaController : BaseController
    {
        public ProvinciaController(IConfiguration iConfig) : base(iConfig)
        {
            configuration = iConfig;
        }
        [HttpGet]
        public IActionResult Get()
        {
            BLProvincia BL = new BLProvincia(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            ICollection<VMProvincia> getAll = BL.GetAll();
            return Json(new ResultDto<VMProvincia>("success", getAll));
        }
    }
}