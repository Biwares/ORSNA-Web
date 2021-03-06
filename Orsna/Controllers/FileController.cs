using BD.ViewModels;
using BL.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Orsna.ExcelUtility;
using BL;

namespace Orsna.Controllers
{
    [Produces("application/json")]
    public class FileController : BaseController
    {
        private string fileName { get; set; }
        public FileController(IConfiguration iConfig) : base(iConfig)
        {
            configuration = iConfig;
        }
        [HttpGet("[action]")]
        [AllowAnonymous]
        public IActionResult GetFile(string id)
        {
            string uploadFolder = configuration.GetValue<string>("MyConfig:UploadFolder");

            BLFile bl = new BLFile(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);

            VMAdjunto adjunto = bl.GetAdjuntoById(id);

            //FileStream file = new FileStream(uploadFolder+"/"+adjunto.NombreArchivo, FileMode.Open);
            fileName = adjunto.NombreArchivo;
            // Response...
            System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
            {
                FileName = fileName,
                Inline = true  // false = prompt the user for downloading;  true = browser to try to show the file inline
            };
            Response.Headers.Add("Content-Disposition", cd.ToString());
            Response.Headers.Add("X-Content-Type-Options", "nosniff");

            return DownloadFile(uploadFolder + "\\" + adjunto.Id);
        }
        private FileResult DownloadFile(string filePath)
        {
            IFileProvider provider = new PhysicalFileProvider(filePath);
            IFileInfo fileInfo = provider.GetFileInfo(fileName);
            var readStream = fileInfo.CreateReadStream();
            var mimeType = string.Empty;
            new FileExtensionContentTypeProvider().TryGetContentType(fileName, out mimeType);

            return File(readStream, mimeType);
        }

        [HttpGet("[action]")]
        [AllowAnonymous]
        public IActionResult ProcesarCargarMasivaFile(string filePath)
        {
            string LIBRANZASPATH = filePath + "libranzas.xls";
            string PROJECTOSPATH = filePath + "projectos.xls";

            var dataLibranzas = (new ExcelReader()).ReadExcel(LIBRANZASPATH);
            var dataProjectos = (new ExcelReader()).ReadExcel(LIBRANZASPATH);

            //Iteramos por proyecto
            foreach (var row in dataProjectos.DataRows)
            {
                int indice= dataLibranzas.Headers.IndexOf("NOMBRECOLUMNA");
                string campo = row[indice];
            }

            return Json("success");
        }

        [HttpGet("[action]")]
        [AllowAnonymous]
        public IActionResult ImagenLogo()
        {
            var logo = Utils.imagenLogoBytes();
            return File(logo.Item1, logo.Item2 == "svg" ? "image/svg+xml" : "image/png");
        }

    }
}