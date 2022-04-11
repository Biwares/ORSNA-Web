using BL.Seguridad;
using BD.ViewModels;
using BL.Libranza;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Orsna.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BD.Models;

namespace Orsna.Controllers
{
    [Produces("application/json")]
    public class LibranzaController : BaseController
    {
        
        public LibranzaController(IConfiguration iConfig) : base(iConfig)
        {
            configuration = iConfig;
        }
        [HttpGet("[action]")]
        public IActionResult ActualizarNumeroLibranzas(string ceros)
        {
            BLLibranza BL = new BLLibranza(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            BL.ActualizarNroLibranza("D" + ceros);
            return Json("success");
        }
        [HttpGet]
        public IActionResult Get()
        {
            BLLibranza BL = new BLLibranza(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            ICollection<VMLibranza> getAll = BL.GetAll();
            return Json(new ResultDto<VMLibranza>("success", getAll));
        }
        [HttpGet("[action]")]
        public JsonResult GetAll(string page, string FilterAeropuerto, string FilterEstado, string FilterBeneficiario,
            string FilterProyecto, string FilterTipoLibranza, string FilterFactura, string FilterDesde,
            string FilterHasta, string FilterIdsProyecto, string Order, string ColumnOrder)
        {
            BLSeguridad BLSeguridad = new BLSeguridad(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            List<int> getAreas = BLSeguridad.GetAreasDelUsuario(null, BasicAuthenticationHandler.getUserNameAndPasswordFromHeaders(Request.Headers["Authorization"]).Item1);

            if (string.IsNullOrEmpty(FilterProyecto))
                FilterProyecto = "0";
            if (string.IsNullOrEmpty(FilterTipoLibranza))
                FilterTipoLibranza = "0";
            BLLibranza lib = new BLLibranza(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            ICollection<VMLibranza> data = lib.GetAll(string.IsNullOrEmpty(page) ? 0 : int.Parse(page), FilterAeropuerto, FilterEstado, FilterBeneficiario,
            int.Parse(FilterProyecto), int.Parse(FilterTipoLibranza), FilterFactura, FilterDesde,
            FilterHasta, FilterIdsProyecto, getAreas, Order, ColumnOrder, 8);

            return Json(new ResultDto<VMLibranza>("success", data));
        }
        [HttpGet("[action]")]
        public JsonResult GetAllMoneda()
        {
            BLLibranza lib = new BLLibranza(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            ICollection<Moneda> data = lib.GetAllMoneda();
            return Json(new ResultDto<Moneda>("success", data));
        }
        [HttpPost("[action]")]
        public JsonResult Save(int IdUsuario)
        {
            try
            {
                using (var mem = new MemoryStream())
                using (var reader = new StreamReader(mem))
                {
                    Request.Body.CopyTo(mem);

                    var body = reader.ReadToEnd();

                    mem.Seek(0, SeekOrigin.Begin);

                    VMLibranza data = (JsonConvert.DeserializeObject<VMLibranza>(reader.ReadToEnd(), new JsonSerializerSettings
                    {
                        Culture = new System.Globalization.CultureInfo("es-AR")
                    }));

                    BLLibranza bl = new BLLibranza(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
                    var pattern = configuration.GetValue<string>("MyConfig:NroLibranzaCeros");
                    if (string.IsNullOrEmpty(pattern))
                        pattern = "3";
                    GenericResponse<int> response = bl.Save(data, IdUsuario, "D" + pattern);
                    return Json(response);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        [HttpGet("[action]")]
        public IActionResult GetSiguientesEstado(int IdLibranza)
        {
            var bl = new BLLibranza(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            return Ok(bl.GetSiguientesEstado(IdLibranza));
        }
        [HttpGet("[action]")]
        public int GetCountPages(int page, string FilterAeropuerto, string FilterEstado, string FilterBeneficiario,
            string FilterProyecto, string FilterTipoLibranza, string FilterFactura, string FilterDesde,
            string FilterHasta, string FilterIdsProyecto, string Order, string ColumnOrder)
        {
            if (string.IsNullOrEmpty(FilterProyecto))
                FilterProyecto = "0";
            if (string.IsNullOrEmpty(FilterTipoLibranza))
                FilterTipoLibranza = "0";
            BLLibranza bl = new BLLibranza(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            BLSeguridad BLSeguridad = new BLSeguridad(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            List<int> getAreas = BLSeguridad.GetAreasDelUsuario(null, BasicAuthenticationHandler.getUserNameAndPasswordFromHeaders(Request.Headers["Authorization"]).Item1);

            return bl.GetCountPages(FilterAeropuerto, FilterEstado, FilterBeneficiario,
            int.Parse(FilterProyecto), int.Parse(FilterTipoLibranza), FilterFactura, FilterDesde,
            FilterHasta, FilterIdsProyecto, getAreas, Order, ColumnOrder);
        }
        [HttpGet("[action]")]
        public int GetCountFilterElements(int page, string FilterAeropuerto, string FilterEstado, string FilterBeneficiario,
            string FilterProyecto, string FilterTipoLibranza, string FilterFactura, string FilterDesde,
            string FilterHasta, string FilterIdsProyecto, string Order, string ColumnOrder)
        {
            if (string.IsNullOrEmpty(FilterProyecto))
                FilterProyecto = "0";
            if (string.IsNullOrEmpty(FilterTipoLibranza))
                FilterTipoLibranza = "0";
            BLLibranza bl = new BLLibranza(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            BLSeguridad BLSeguridad = new BLSeguridad(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            List<int> getAreas = BLSeguridad.GetAreasDelUsuario(null, BasicAuthenticationHandler.getUserNameAndPasswordFromHeaders(Request.Headers["Authorization"]).Item1);

            return bl.GetCountFilterElements(FilterAeropuerto, FilterEstado, FilterBeneficiario,
            int.Parse(FilterProyecto), int.Parse(FilterTipoLibranza), FilterFactura, FilterDesde,
            FilterHasta, FilterIdsProyecto, getAreas, Order, ColumnOrder);
        }

        [HttpPost("[action]")]
        public IActionResult CambiarEstado()
        {
            using (var mem = new MemoryStream())
            using (var reader = new StreamReader(mem))
            {
                Request.Body.CopyTo(mem);

                var body = reader.ReadToEnd();

                mem.Seek(0, SeekOrigin.Begin);

                VMLibranzaDetalleWorkflow data = (JsonConvert.DeserializeObject<VMLibranzaDetalleWorkflow>(reader.ReadToEnd()));
                var con = configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities");
                BLLibranza bl = new BLLibranza(con, userId);
                VMLibranzaDetalleWorkflow response = bl.CambiarEstado(data);
                return Ok(response);
            }
        }
        [HttpGet("[action]")]
        public string GetNroLibranza(int tipoId)
        {
            var bl = new BLLibranza(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            var pattern = configuration.GetValue<string>("MyConfig:NroLibranzaCeros");
            if (string.IsNullOrEmpty(pattern))
                pattern = "3";
            return bl.GetNroLibranza(tipoId, "D" + pattern);
        }
        [HttpGet("[action]")]
        public JsonResult GetLibranzaById(int Id)
        {
            BLLibranza proyecto = new BLLibranza(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            return Json(proyecto.GetLibranzaById(Id));
        }
        [HttpDelete("[action]")]
        public JsonResult Delete(int id)
        {
            BLLibranza lib = new BLLibranza(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            return Json(lib.Delete(id));
        }
        [HttpGet("[action]")]
        public JsonResult GetEstados()
        {
            BLLibranzaEstado bl = new BLLibranzaEstado(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            return Json(bl.GetAll());
        }
        [HttpGet("[action]")]
        public JsonResult GetLibranzaTipo()
        {
            BLLibranza bl = new BLLibranza(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            return Json(bl.LibranzaTipos());
        }
        [HttpGet("[action]")]
        public JsonResult GetMontoDisponibleProyectoByIdLibranza(int idLibranza, int idProyecto)
        {
            BLLibranza bl = new BLLibranza(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            return Json(bl.GetMontoDisponibleProyectoByIdLibranza(idLibranza, idProyecto));
        }
        [HttpGet("[action]")]
        public JsonResult GetMontoAPagarByIdLibranza(int idLibranza, decimal tasaDeCambio)
        {
            BLLibranza bl = new BLLibranza(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            return Json(bl.GetMontoAPagarByIdLibranza(idLibranza, tasaDeCambio));
        }

        [HttpGet("[action]")]
        //[AllowAnonymous]
        public async Task<IActionResult> GetXLSFilter(string page, string FilterAeropuerto, string FilterEstado, string FilterBeneficiario,
            string FilterProyecto, string FilterTipoLibranza, string FilterFactura, string FilterDesde,
            string FilterHasta, string FilterIdsProyecto, string Order, string ColumnOrder)
        {
            if (string.IsNullOrEmpty(FilterProyecto))
                FilterProyecto = "0";
            if (string.IsNullOrEmpty(FilterTipoLibranza))
                FilterTipoLibranza = "0";
            BLSeguridad BLSeguridad = new BLSeguridad(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);

            List<int> getAreas = BLSeguridad.GetAreasDelUsuario(null, BasicAuthenticationHandler.getUserNameAndPasswordFromHeaders(Request.Headers["Authorization"]).Item1);

            BLLibranza lib = new BLLibranza(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            ICollection<VMLibranza> data = lib.GetAll(0, FilterAeropuerto, FilterEstado, FilterBeneficiario,
            int.Parse(FilterProyecto), int.Parse(FilterTipoLibranza), FilterFactura, FilterDesde,
            FilterHasta, FilterIdsProyecto,getAreas, Order, ColumnOrder, int.MaxValue);

            string sWebRootFolder = configuration.GetValue<string>("MyConfig:UploadFolder"); // _hostingEnvironment.WebRootPath;
            string sFileName = @"Libro Registro de Libranzas-" + DateTime.Today.Year.ToString() + ".xlsx";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            var memory = new MemoryStream();
            using (var fs = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Create, FileAccess.Write))
            {

                var i = 0;
                IWorkbook workbook;
                workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("Libranzas");
                IRow row = excelSheet.CreateRow(i);
                row.CreateCell(0).SetCellValue("Registro de Libranzas del Fideicomiso de Fortalecimiento del Sistema Nacional de Aeropuertos");

                i = i + 2;
                row = excelSheet.CreateRow(i);
                row.CreateCell(0).SetCellValue("ID Proyecto");
                row.CreateCell(1).SetCellValue("Nro. de Libranza");
                row.CreateCell(2).SetCellValue("Fecha");
                row.CreateCell(3).SetCellValue("Nro. de Cuenta");
                row.CreateCell(4).SetCellValue("Nro. de Expediente de Pago");
                row.CreateCell(5).SetCellValue("Obra");
                row.CreateCell(6).SetCellValue("Beneficiario");
                row.CreateCell(7).SetCellValue("Doc. de Pago");
                row.CreateCell(8).SetCellValue("Monto Neto");
                row.CreateCell(9).SetCellValue("IVA");
                row.CreateCell(10).SetCellValue("IIBB");
                row.CreateCell(11).SetCellValue("Monto Total");
                row.CreateCell(12).SetCellValue("Deducciones");
                row.CreateCell(13).SetCellValue("Monto a pagar");
                row.CreateCell(14).SetCellValue("Retenciones");
                row.CreateCell(15).SetCellValue("Monto a beneficiario");
                row.CreateCell(16).SetCellValue("Descripción");
                row.CreateCell(17).SetCellValue("Estado");
                row.CreateCell(18).SetCellValue("Fecha de Estado");
                row.CreateCell(19).SetCellValue("NOTA ORSNA");
                row.CreateCell(20).SetCellValue("Fecha de Pago");
                row.CreateCell(21).SetCellValue("Extracto Bancario");
                row.CreateCell(22).SetCellValue("Aeropuerto");
                row.CreateCell(23).SetCellValue("Grupo aeropuerto");
                row.CreateCell(24).SetCellValue("Provincia");
                i++;
                decimal total = 0;
                foreach (var l in data)
                {
                    row = excelSheet.CreateRow(i);
                    row.CreateCell(0).SetCellValue(l.Proyecto.IdProyecto);
                    row.CreateCell(1).SetCellValue(l.NroLibranza);
                    row.CreateCell(2).SetCellValue(l.Fecha.Day + "/" + l.Fecha.Month + "/" + l.Fecha.Year);
                    row.CreateCell(3).SetCellValue(l.Proyecto.Cuenta.NroCuenta);
                    row.CreateCell(4).SetCellValue(l.Proyecto.NroPago);
                    row.CreateCell(5).SetCellValue(l.Proyecto.CodObra);

                    var beneficiarioNombre = String.Join(',', l.Beneficiario.Select(x => x.RazonSocial).ToList());
                    row.CreateCell(6).SetCellValue(beneficiarioNombre);

                    row.CreateCell(7).SetCellValue(String.Join(" / ", l.Factura.Select(x => x.Nro)));

                    if (l.LibranzasEstado.Id != BD.Utilities.LibranzaEstadosEnum.Anulada)
                    {
                        var monto = l.Factura.Where(x => x.Monto.HasValue && !x.Tipo.ToUpper().Contains("NOTA DE CRÉDITO") && !x.Tipo.ToUpper().Contains("CREDIT NOTE")).Sum(x => x.Monto.Value);
                        monto -= l.Factura.Where(x => x.Monto.HasValue && (x.Tipo.ToUpper().Contains("NOTA DE CRÉDITO") || x.Tipo.ToUpper().Contains("CREDIT NOTE"))).Sum(x => x.Monto.Value);
                        monto = (monto * l.TasaDeCambio);

                        row.CreateCell(8).SetCellValue(double.Parse(monto.ToString()));

                        decimal iva = 0;
                        decimal Ibb = 0;

                        iva = l.Factura.Where(x => x.Iva.HasValue && !x.Tipo.ToUpper().Contains("NOTA DE CRÉDITO")).Sum(x => x.Iva.Value);
                        iva -= l.Factura.Where(x => x.Iva.HasValue && x.Tipo.ToUpper().Contains("NOTA DE CRÉDITO")).Sum(x => x.Iva.Value);

                        Ibb = l.Factura.Where(x => x.Ibb.HasValue && !x.Tipo.ToUpper().Contains("NOTA DE CRÉDITO")).Sum(x => x.Ibb.Value);
                        Ibb -= l.Factura.Where(x => x.Ibb.HasValue && x.Tipo.ToUpper().Contains("NOTA DE CRÉDITO")).Sum(x => x.Ibb.Value);

                        row.CreateCell(9).SetCellValue(double.Parse(iva.ToString()));
                        row.CreateCell(10).SetCellValue(double.Parse(Ibb.ToString()));


                        var montoMasIvaMasIbb = monto + iva + Ibb;
                        row.CreateCell(11).SetCellValue(double.Parse(montoMasIvaMasIbb.ToString()));

                        var mora = l.Mora ?? 0;
                        var multa = l.Multa ?? 0;
                        var montofondoreparo = l.MontoFondoReparo ?? 0;

                        var deducciones = (mora + multa + montofondoreparo) * l.TasaDeCambio;
                        row.CreateCell(12).SetCellValue(double.Parse(deducciones.ToString()));

                        var montoTotal = monto + iva + Ibb;
                        if (l.Libranzatipo.Nombre.Equals("B")) // Si es Tipo B no se usa iva e ibb
                            montoTotal = monto;
                        var pagar = montoTotal - deducciones;
                        if (l.Libranzatipo.Nombre.Equals("B")) // Si es Tipo B no se usa iva e ibb
                            pagar = monto - deducciones;
                        total += pagar;
                        row.CreateCell(13).SetCellValue(double.Parse(pagar.ToString()));

                        var retencion = (l.Retencion ?? decimal.Zero);
                        row.CreateCell(14).SetCellValue(double.Parse(retencion.ToString()));

                        var beneficiario = (pagar - retencion);
                        row.CreateCell(15).SetCellValue(double.Parse(beneficiario.ToString()));
                    }
                    else
                    {
                        row.CreateCell(8).SetCellValue(0);
                        row.CreateCell(9).SetCellValue(0);
                        row.CreateCell(10).SetCellValue(0);
                        row.CreateCell(11).SetCellValue(0);
                        row.CreateCell(12).SetCellValue(0);
                        row.CreateCell(13).SetCellValue(0);
                        row.CreateCell(14).SetCellValue(0);
                        row.CreateCell(15).SetCellValue(0);

                    }
                    row.CreateCell(16).SetCellValue(l.ObjetoDatosGenerales);
                    
                    row.CreateCell(17).SetCellValue(l.LibranzasEstado.Nombre);

                    var ultimaFecha = lib.GetFechaEstado(l.Id);
                    row.CreateCell(18).SetCellValue((ultimaFecha != null) ? ultimaFecha.Value.Day + "/" + ultimaFecha.Value.Month + "/" + ultimaFecha.Value.Year : String.Empty);

                    row.CreateCell(19).SetCellValue(string.Empty);
                    

                    row.CreateCell(20).SetCellValue(String.Empty);

                    if (l.LibranzasEstado.Nombre.Equals("Pagada"))
                    {
                        var ultimaFechaPago = lib.GetFechaPago(l.Id);
                        row.CreateCell(20).SetCellValue((ultimaFechaPago != null) ? ultimaFechaPago.Value.Day + "/" + ultimaFechaPago.Value.Month + "/" + ultimaFechaPago.Value.Year : String.Empty);
                    }

                    row.CreateCell(21).SetCellValue(l.Proyecto.Cuenta.Nombre);
                    var row20 = string.Empty;
                    var row21 = string.Empty;

                    if (l.Proyecto.Cuenta.AeropuertosGrupo.Nombre.Equals("SNA"))
                    {
                        row20 = "SNA";
                        row21 = "SNA";
                    }
                    else
                    {
                        row20 = String.Join(',', l.Proyecto.Aeropuertos.Select(x => x.NombreCorto).ToList());
                        row21 = l.Proyecto.Cuenta.AeropuertosGrupo.Nombre;
                    }

                    row.CreateCell(22).SetCellValue(row20);
                    row.CreateCell(23).SetCellValue(row21);
                    row.CreateCell(24).SetCellValue(l.Provincia.Nombre);
                    i++;
                }
                i++;
                row = excelSheet.CreateRow(i);
                row.CreateCell(4).SetCellValue("Total Ejecutado Libranzas al ");
                row.CreateCell(6).SetCellValue(DateTime.Today.Day + "/" + DateTime.Today.Month + "/" + DateTime.Today.Year);
                row.CreateCell(7).SetCellValue(total.ToString());
                workbook.Write(fs);
            }
            using (var stream = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);
        }


        [HttpGet("[action]")]
        [AllowAnonymous]
        public FileResult DownloadPDF(int Id)
        {
            BLLibranza lib = new BLLibranza(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            byte[] rst = lib.GetPDF(Id);
            Stream stream = new MemoryStream(rst);
            var mimeType = string.Empty;
            var fileName = "libranza_" + Id + ".pdf";
            new FileExtensionContentTypeProvider().TryGetContentType(fileName, out mimeType);

            // Response...
            System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
            {
                FileName = fileName,
                Inline = true  // false = prompt the user for downloading;  true = browser to try to show the file inline
            };
            Response.Headers.Add("Content-Disposition", cd.ToString());
            Response.Headers.Add("X-Content-Type-Options", "nosniff");

            return File(stream, mimeType);

            //return Convert.ToBase64String(rst);
        }


    }
}