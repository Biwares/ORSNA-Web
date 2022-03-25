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
    public class ProyectoAdjuntosController : BaseController
    {
        private readonly IHostingEnvironment _environment;
        public ProyectoAdjuntosController(IHostingEnvironment hostingEnvironment, IConfiguration iConfig) : base(iConfig)
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

                var pA = new BLProyectoAdjuntos(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
                VMAdjunto pAdjunto = await pA.Post(vm, "Proyecto");

                var patch = configuration.GetValue<string>("MyConfig:UploadFolder") + "\\" + pAdjunto.Id;


                if (!Directory.Exists(patch))
                    Directory.CreateDirectory(patch);

                var filePath = Path.Combine(_environment.ContentRootPath, patch, vm.archivo.FileName);
                

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await vm.archivo.CopyToAsync(stream);
                }
                return Json("OK");
                //return CreatedAtAction("GetUser", new { id = pAdjunto.Id }, pAdjunto);
            }catch(Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return Json("no se enviaron datos de archivos correctos");
            }
        }

        // DELETE: 
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            BLProyectoAdjuntos bl = new BLProyectoAdjuntos(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            var resp = bl.Delete(id);
            return Json(resp);
        }
        // GET: 
        [HttpGet("{id}", Name = "GetProyectoAdjuntos")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var _context = new OrsnaDatabaseContext(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            var proyecto = await _context.ProyectoAdjuntos.SingleOrDefaultAsync(m => m.Id == id);

            if (proyecto == null)
            {
                return NotFound();
            }

            return Ok(proyecto);
        }

        [HttpGet("[action]")]
        public IActionResult GetAdjuntosByProyecto(int id)
        {
            BLProyectoAdjuntos bl = new BLProyectoAdjuntos(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);

            return Json(bl.GetAdjuntosByProyecto(id));
        }
    }
}