using BD.ViewModels;
using BL.Seguridad;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Orsna.Helpers;

namespace Orsna.Controllers
{
    public class SeguridadController : BaseController
    {
        public SeguridadController(IConfiguration iConfig) : base(iConfig)
        {
            configuration = iConfig;
        }

        [HttpGet("[action]")]
        public IActionResult GetPermisos(string modulo)
        {
            BLSeguridad BLSeguridad = new BLSeguridad(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            VMPermisos getPermisos = BLSeguridad.getPermisos(null, BasicAuthenticationHandler.getUserNameAndPasswordFromHeaders(Request.Headers["Authorization"]).Item1, modulo);
            return Json(getPermisos);
        }

        [HttpGet("[action]")]
        public IActionResult GetPermisosVer()
        {
            BLSeguridad BLSeguridad = new BLSeguridad(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            var result = BLSeguridad.getPermisosVer(null, BasicAuthenticationHandler.getUserNameAndPasswordFromHeaders(Request.Headers["Authorization"]).Item1);

            return Json(result);
        }
    }
}