using BD.Models;
using BD.ViewModels;
using BL;
using BL.Proyecto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Orsna.Controllers
{
    [Produces("application/json")]
    public class LibranzaAdjuntosController : BaseController
    {
        private IConfiguration configuration;
        private readonly IHostingEnvironment _environment;
        public LibranzaAdjuntosController(IHostingEnvironment hostingEnvironment, IConfiguration iConfig) : base(iConfig)
        {
            _environment = hostingEnvironment;

            configuration = iConfig;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromForm]FileB vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var pA = new BLLibranzaAdjuntos(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
                VMAdjunto pAdjunto = await pA.Post(vm, "Libranza");

                var patch = configuration.GetValue<string>("MyConfig:UploadFolder") + "\\" + pAdjunto.Id;


                if (!Directory.Exists(patch))
                    Directory.CreateDirectory(patch);

                var filePath = Path.Combine(_environment.ContentRootPath, patch, vm.archivo.FileName);
                

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await vm.archivo.CopyToAsync(stream);
                }
                return Json("OK");

            } catch(Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return Json("no se enviaron datos de archivos correctos");
            }
        }

        // DELETE: 
        [HttpDelete("[action]")]
        public IActionResult Delete(int id)
        {
            BLLibranzaAdjuntos bl = new BLLibranzaAdjuntos(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            var resp = bl.Delete(id);
            return Json(resp);
        }
        // GET: 
        [HttpGet("{id}", Name = "GetLibranzaAdjuntos")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var _context = new OrsnaDatabaseContext(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            var proyecto = await _context.LibranzaAdjuntos.SingleOrDefaultAsync(m => m.Id == id);

            if (proyecto == null)
            {
                return NotFound();
            }

            return Ok(proyecto);
        }

        [HttpGet("[action]")]
        public IActionResult GetAdjuntosByLibranza(int id)
        {
            BLLibranzaAdjuntos bl = new BLLibranzaAdjuntos(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);

            return Json(bl.GetAdjuntosByLibranza(id));
        }
    }
}