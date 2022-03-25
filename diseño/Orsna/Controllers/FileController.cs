using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using BD.ViewModels;
using BL.File;
using Microsoft.Extensions.FileProviders;
using BL.Libranza;
using System.Text;
using DinkToPdf.Contracts;
using DinkToPdf;

namespace Orsna.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : Controller
    {
        private IConfiguration configuration;
        private string fileName { get; set; }
        public FileController(IConfiguration iConfig)
        {
            configuration = iConfig;
        }
        [HttpGet("[action]")]
        public IActionResult GetFile(string id)
        {
            string uploadFolder = configuration.GetValue<string>("MyConfig:UploadFolder");

            BLFile bl = new BLFile(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));

            VMAdjunto adjunto = bl.GetAdjuntoById(id);

            //FileStream file = new FileStream(uploadFolder+"/"+adjunto.NombreArchivo, FileMode.Open);
            fileName = adjunto.NombreArchivo;

            return DownloadFile(uploadFolder + "\\" + adjunto.Id);
        }
        private FileResult DownloadFile(string filePath)
        {
            IFileProvider provider = new PhysicalFileProvider(filePath);
            IFileInfo fileInfo = provider.GetFileInfo(fileName);
            var readStream = fileInfo.CreateReadStream();
            var mimeType = "application/vnd.ms-excel";
            return File(readStream, mimeType, fileName);
        }


        [HttpGet("[action]")]
        public FileResult CreatePDF(string Id, string Tipo)
        {

            IConverter converter = new BasicConverter(new PdfTools());
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "Libranza Report"
            };
            string html = "";
            switch (Tipo)
            {
                case "Libranza":
                    html = this.GetHtmlLibranza(int.Parse(Id));
                    break;
                default:
                    return null;
            }

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = html,
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "stylesReport.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
            };

            HtmlToPdfDocument pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };
            try
            {
                byte[] file = converter.Convert(pdf);
                return File(file, "application/pdf");
            }catch(Exception ex)
            {//Unable to load DLL 'libwkhtmltox': The specified module could not be found. When using Nuget package
                var e = ex;
                return null;
            }

        }

        private string GetHtmlLibranza(int id)
        {
            BLLibranza bl = new BLLibranza(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            VMLibranza lb = bl.GetLibranzaById(id);

            var sb = new StringBuilder();
            sb.Append(@"
                    <html>
                    <table>
                    <tbody>
                    <tr>
                    <td style = 'border-top:0'><b> ID Libranza: </b><span>" + lb.Id +
                    "</span></td><td style='border-top:0'><b> Nro Libranza: </b><span>" + lb.NroLibranza +
                    "</span></td><td style='border-top:0'><b> Fecha: </b><span>" + lb.Fecha + @"</span></td>
                    <td style = 'border-top:0'><b> Nro.de Cuenta: </b><span>" + lb.Proyecto.Cuenta.NroCuenta + @"</span></td>
                    </tr>
                    <tr>
                    <td style='border-top:0'><b> Nro.de Expediente de Pago: </b><span>+" + lb.Proyecto.NroPago + @"</span></td>
                    <td style='border-top:0'><b>Código de Obra / Ubicación Geográfica: </b><span>" + lb.Proyecto.CodObra + @"</span></td>
                    < td style ='border-top:0'><b> Descripción: </b><span>" + lb.Proyecto.Descripcion + @"</span></td>
                    <td style='border-top:0'><b> Monto Neto: </b><span>" + lb.MontoNeto + @"</span></td>
                    </tr>"/*

                    < tr >

                    < td style = "border-top:0" >< b > Estado: </ b >< span * ngIf = "DetalleLibranza.libranzasEstado!=null" >{ { DetalleLibranza.libranzasEstado.nombre}
                                    }</ span ></ td >

                    < td style = "border-top:0" >< b > Fecha estado: </ b >< span >{ { DetalleLibranza.fecha | date :  "dd-MM-y"}
                                    }</ span ></ td >

                    < td style = "border-top:0" >< b > Fecha de Pago: </ b >< span ></ span ></ td >

                    < td style = "border-top:0" >< b > Retenciones: </ b >< span ></ span ></ td >

                    </ tr >

                    < tr >

                    < td style = "border-top:0" >< b > Extracto Bancario: </ b >< span ></ span ></ td >

                    < td style = "border-top:0" >< b > Observaciones: </ b >< span ></ span ></ td >

                    < td style = "border-top:0" >< b > Monto Bruto: </ b >< span >{ { DetalleLibranza.montoBruto | currency}
                                    }</ span ></ td >

                    < td style = "border-top:0" >< b ></ b >< span ></ span ></ td >

                    </ tr >*/
                );/*
                    < table class="table table-striped">
                                  <tbody* ngIf = "DetalleLibranza!=null" >
                                    < tr >
                                      < td colspan="4">
                                        <b>Beneficiarios</b>
                                      </td>
                                    </tr>                        

                    ");

                foreach (var f in lb.Factura)
                {
                    sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                  </tr>", emp.Name, emp.LastName, emp.Age, emp.Gender);
                }
                */
            sb.Append(@"
                                </table>
                            </body>
                        </html>");

            return sb.ToString();
        }
    }
}