using System;
using BD.Models;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using BL.Libranza;
using BD.ViewModels;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Newtonsoft.Json;
using Orsna.Helpers;
using Microsoft.Extensions.Configuration;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Orsna.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LibranzaController : Controller
    {
        private IConfiguration configuration;
        public LibranzaController(IConfiguration iConfig)
        {
            configuration = iConfig;
        }

        [HttpGet]
        public IActionResult Get()
        {
            BLLibranza BL = new BLLibranza(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            ICollection<VMLibranza> getAll = BL.GetAll();
            return Json(new ResultDto<VMLibranza>("success", getAll));
        }
        [HttpGet("[action]")]
        public JsonResult GetAll(string page, string FilterAeropuerto, string FilterEstado, string FilterBeneficiario,
            string FilterProyecto, string FilterTipoLibranza, string FilterFactura, string FilterDesde,
            string FilterHasta, string Order, string ColumnOrder)
        {
            if (string.IsNullOrEmpty(FilterProyecto))
                FilterProyecto = "0";
            if (string.IsNullOrEmpty(FilterTipoLibranza))
                FilterTipoLibranza = "0";
            BLLibranza lib = new BLLibranza(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            ICollection<VMLibranza> data = lib.GetAll(int.Parse(page), FilterAeropuerto, FilterEstado, FilterBeneficiario,
            int.Parse(FilterProyecto), int.Parse(FilterTipoLibranza), FilterFactura, FilterDesde,
            FilterHasta, Order, ColumnOrder);

            return Json(new ResultDto<VMLibranza>("success", data));
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

                    VMLibranza data = (JsonConvert.DeserializeObject<VMLibranza>(reader.ReadToEnd()));

                    BLLibranza bl = new BLLibranza(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
                    GenericResponse<int> response = bl.Save(data);
                    return Json(response);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        [HttpGet("[action]")]
        public int GetCountPages(int page, string FilterAeropuerto, string FilterEstado, string FilterBeneficiario,
            string FilterProyecto, string FilterTipoLibranza, string FilterFactura, string FilterDesde,
            string FilterHasta, string Order, string ColumnOrder)
        {
            if (string.IsNullOrEmpty(FilterProyecto))
                FilterProyecto = "0";
            if (string.IsNullOrEmpty(FilterTipoLibranza))
                FilterTipoLibranza = "0";
            BLLibranza bl = new BLLibranza(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            return bl.GetCountPages(FilterAeropuerto, FilterEstado, FilterBeneficiario,
            int.Parse(FilterProyecto), int.Parse(FilterTipoLibranza), FilterFactura, FilterDesde,
            FilterHasta, Order, ColumnOrder);
        }
        [HttpGet("[action]")]
        public string GetNroLibranza()
        {
            var bl = new BLLibranza(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            return bl.GetNroLibranza();
        }
        [HttpGet("[action]")]
        public JsonResult GetLibranzaById(int Id)
        {
            BLLibranza proyecto = new BLLibranza(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            return Json(proyecto.GetLibranzaById(Id));
        }
        [HttpDelete("[action]")]
        public JsonResult Delete(int id)
        {
            BLLibranza lib = new BLLibranza(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            return Json(lib.Delete(id));
        }
        [HttpGet("[action]")]
        public JsonResult GetEstados()
        {
            BLLibranzaEstado bl = new BLLibranzaEstado(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            return Json(bl.GetAll());
        }
        [HttpGet("[action]")]
        public JsonResult GetLibranzaTipo()
        {
            BLLibranza bl = new BLLibranza(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            return Json(bl.LibranzaTipos());
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetXLSFilter(string page, string FilterAeropuerto, string FilterEstado, string FilterBeneficiario,
            string FilterProyecto, string FilterTipoLibranza, string FilterFactura, string FilterDesde,
            string FilterHasta, string Order, string ColumnOrder)
        {
            if (string.IsNullOrEmpty(FilterProyecto))
                FilterProyecto = "0";
            if (string.IsNullOrEmpty(FilterTipoLibranza))
                FilterTipoLibranza = "0";
            BLLibranza lib = new BLLibranza(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            ICollection<VMLibranza> data = lib.GetAll(int.Parse(page), FilterAeropuerto, FilterEstado, FilterBeneficiario,
            int.Parse(FilterProyecto), int.Parse(FilterTipoLibranza), FilterFactura, FilterDesde,
            FilterHasta, Order, ColumnOrder);

            string sWebRootFolder = configuration.GetValue<string>("MyConfig:UploadFolder"); // _hostingEnvironment.WebRootPath;
            string sFileName = @"demo.xlsx";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            var memory = new MemoryStream();
            using (var fs = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Create, FileAccess.Write))
            {

                var i = 0;
                IWorkbook workbook;
                workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("Demo");
                IRow row = excelSheet.CreateRow(i);
                row.CreateCell(0).SetCellValue("Registro de Libranzas del Fideicomiso de Fortalecimiento del Sistema Nacional de Aeropuertos");

                i = i + 2;
                row = excelSheet.CreateRow(i);
                row.CreateCell(0).SetCellValue("Nro. de Libranza");
                row.CreateCell(1).SetCellValue("Fecha");
                row.CreateCell(2).SetCellValue("Nro. de Cuenta");
                row.CreateCell(3).SetCellValue("Nro. de Expediente de Pago");
                row.CreateCell(4).SetCellValue("Obra");
                row.CreateCell(5).SetCellValue("Beneficiario");
                row.CreateCell(6).SetCellValue("Factura");
                row.CreateCell(7).SetCellValue("Monto Neto");
                row.CreateCell(8).SetCellValue("Descripción");
                row.CreateCell(9).SetCellValue("Estado");
                row.CreateCell(10).SetCellValue("NOTA ORSNA");
                row.CreateCell(11).SetCellValue("Fecha de Pago");
                row.CreateCell(12).SetCellValue("Retenciones");
                row.CreateCell(13).SetCellValue("Neto");
                row.CreateCell(14).SetCellValue("Extracto Bancario");
                row.CreateCell(15).SetCellValue("Observaciones");
                foreach(var l in data)
                {
                    foreach (var lf in l.Factura)
                    {
                        i++;
                        row = excelSheet.CreateRow(i);
                        row.CreateCell(0).SetCellValue(l.NroLibranza);
                        row.CreateCell(1).SetCellValue(l.Fecha.Day + "/" + l.Fecha.Month + "/" + l.Fecha.Year);
                        row.CreateCell(2).SetCellValue(l.Proyecto.Cuenta.NroCuenta);
                        row.CreateCell(3).SetCellValue(l.NroEscritura);
                        row.CreateCell(4).SetCellValue(l.Proyecto.NroObra);
                        row.CreateCell(5).SetCellValue(l.BeneficiarioEmbargo);
                        row.CreateCell(6).SetCellValue(lf.Nro);
                        row.CreateCell(7).SetCellValue(lf.Monto.Value.ToString());
                        row.CreateCell(8).SetCellValue(l.Proyecto.Descripcion);
                        row.CreateCell(9).SetCellValue("sin valor");
                        row.CreateCell(10).SetCellValue(lf.Fecha.Day + "/" + lf.Fecha.Month + "/" + lf.Fecha.Year);
                        row.CreateCell(11).SetCellValue("sin valor");
                        row.CreateCell(12).SetCellValue("sin valor");
                        row.CreateCell(13).SetCellValue(l.Proyecto.Cuenta.Nombre);
                        row.CreateCell(14).SetCellValue("sin valor");
                    }
                }
                
                workbook.Write(fs);
            }
            using (var stream = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);
        }


    }
}