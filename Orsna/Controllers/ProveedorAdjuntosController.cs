using BD.Models;
using BD.ViewModels;
using BL.Beneficiario;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace Orsna.Controllers
{
    [Produces("application/json")]
    public class ProveedorAdjuntosController : BaseController
    {
        private readonly IHostingEnvironment _environment;
        public ProveedorAdjuntosController(IHostingEnvironment hostingEnvironment, IConfiguration iConfig) : base(iConfig)
        {
            _environment = hostingEnvironment;

            configuration = iConfig;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromForm]FileB vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bA = new BLBeneficiarioAdjuntos(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            BeneficiarioAdjuntos bAdjunto = await bA.Post(vm, "Beneficiario");

            var patch = configuration.GetValue<string>("MyConfig:UploadFolder") + "\\" + bAdjunto.IdAdjunto;
            
            if (!Directory.Exists(patch))
                Directory.CreateDirectory(patch);

            var filePath = Path.Combine(_environment.ContentRootPath, patch, vm.archivo.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await vm.archivo.CopyToAsync(stream);
            }

            return Json("OK");
            //return CreatedAtAction("GetUser", new { id = bAdjunto.IdAdjunto }, bAdjunto);
        }

        /*[HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromForm]FileB vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var _context = new OrsnaDatabaseContext(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            var beneficiario = await _context.BeneficiarioAdjuntos.SingleOrDefaultAsync(m => m.Id == id);

            var filePath = Path.Combine(_environment.ContentRootPath, configuration.GetValue<string>("MyConfig:UploadFolder"), vm.archivo.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await vm.archivo.CopyToAsync(stream);
            }


            beneficiario.NombreArchivo = vm.archivo.FileName;
            beneficiario.IdBeneficiario = vm.idEntidad;

            _context.Entry(beneficiario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }

            return Ok(beneficiario);
        }*/
        // DELETE: 
        [HttpDelete("[action]")]
        public IActionResult Delete(int id)
        {
            BLBeneficiarioAdjuntos bl = new BLBeneficiarioAdjuntos(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            var resp = bl.Delete(id);
            return Json(resp);
        }
        // GET: 
        [HttpGet("{id}", Name = "GetProveedorAdjunto")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var _context = new OrsnaDatabaseContext(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            var beneficiario = await _context.BeneficiarioAdjuntos.SingleOrDefaultAsync(m => m.Id == id);

            if (beneficiario == null)
            {
                return NotFound();
            }

            return Ok(beneficiario);
        }

        [HttpGet("[action]")]
        public IActionResult GetAdjuntosByBeneficiario(int id)
        {
            BLBeneficiarioAdjuntos bl = new BLBeneficiarioAdjuntos(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);

            return Json(bl.GetAdjuntosByBeneficiario(id));
        }
    }
}