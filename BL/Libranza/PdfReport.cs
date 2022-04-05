using BD.ViewModels;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;

namespace BL.Libranza
{

    public class PdfReport
    {
        // DATOS DE LIBRANZA //
        static string libranzaLetra = "";
        static string libranzaNro = "";

        // PAGINA Y CONTROL DEL LINEAS //
        static int page_line = 0;
        static int page_num = 0;
        static int page_size = 65;

        // DEFINO FUENTES //
        static Font backFont = new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD | Font.BOLD, BaseColor.DARK_GRAY);
        static Font boldFont = new Font(Font.FontFamily.HELVETICA, 8, Font.NORMAL | Font.BOLD, BaseColor.DARK_GRAY);
        static Font largeFont = new Font(Font.FontFamily.HELVETICA, 10, Font.ITALIC, BaseColor.DARK_GRAY);
        static Font normalFont = new Font(Font.FontFamily.HELVETICA, 8, Font.NORMAL | Font.NORMAL, BaseColor.DARK_GRAY);
        static Font smallFont = new Font(Font.FontFamily.HELVETICA, 8, Font.NORMAL | Font.NORMAL, BaseColor.DARK_GRAY);
        static Font titleFont = new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL | Font.BOLD, BaseColor.DARK_GRAY);
        static Font underlineFont = new Font(Font.FontFamily.HELVETICA, 8, Font.NORMAL | Font.UNDERLINE, BaseColor.DARK_GRAY);
        static Font underlineBoldFont = new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD | Font.UNDERLINE, BaseColor.DARK_GRAY);
        static Font underlineBoldFontNormal = new Font(Font.FontFamily.HELVETICA, 8, Font.BOLD | Font.UNDERLINE, BaseColor.DARK_GRAY);

        public static byte[] Create(VMLibranza libranza)
        {
            DateTime libranzaFecha = libranza.Fecha;
            string[] strPagos;
            decimal totalLibranzaCesion = 0;

            VMBeneficiario beneficiario = null;
            foreach (VMBeneficiario i in libranza.Beneficiario)
                beneficiario = i;

            var EsNacional = beneficiario.NacionalExtranjero == "true" ? true : false;

            VMBeneficiarioBancos banco = null;
            foreach (VMBeneficiarioBancos i in beneficiario.Bancos)
                banco = i;

            VMLibranzaTipo libranzaTipo = libranza.Libranzatipo;
            VMProyecto proyecto = libranza.Proyecto;

            VMCuenta cuenta = proyecto.Cuenta;

            VMBeneficiarioBancos cuentaDepositaria = null;
            foreach (VMBeneficiarioBancos i in libranza.CuentaDepositaria)
                cuentaDepositaria = i;

            libranzaLetra = libranzaTipo.Id == 1 ? "'A'" : "'B'";
            libranzaNro = Convert.ToString(libranza.NroLibranza);

            // CALCULO DE LIBRANZA // 

            decimal totalDeducciones = (decimal)(libranza.MontoFondoReparo == null ? 0 : libranza.MontoFondoReparo) +
                                       (decimal)(libranza.Multa == null ? 0 : libranza.Multa) +
                                       (decimal)(libranza.Mora == null ? 0 : libranza.Mora);

            decimal totalLibranza = 0;
            decimal subTotal = 0;

            // CALCULA DOCUMENTOS //
            foreach (VMFactura item in libranza.Factura)
            {
                if (libranzaLetra == "'A'" || proyecto.PagosImpuestosIncluidos == true)
                    subTotal = (decimal)(item.Monto == null ? 0 : item.Monto) +
                                (decimal)(item.Iva == null ? 0 : item.Iva) +
                                (decimal)(item.Ibb == null ? 0 : item.Ibb);
                else
                    subTotal = (decimal)(item.Monto == null ? 0 : item.Monto);

                if (item.Tipo.ToUpper().Contains("CRÉDITO") || item.Tipo.ToUpper().Contains("CREDIT NOTE"))
                    subTotal = subTotal * -1;

                totalLibranza += subTotal;
            }
            totalLibranza = totalLibranza - totalDeducciones;

            // GENERO PDF //
            page_num = 0;

            using (MemoryStream myMemoryStream = new MemoryStream())
            {
                Document document = new Document();
                document.SetPageSize(PageSize.A4);
                document.SetMargins(0, 0, 20, 20);

                PdfWriter myPDFWriter = PdfWriter.GetInstance(document, myMemoryStream);
                document.Open();

                PdfPTable table = new PdfPTable(14);
                PdfPCell cell = new PdfPCell();

                // CABECERA //
                page_line = page_size;
                ControlPage(ref table, ref cell, libranzaFecha);

                // LINEA
                ControlPage(ref table, ref cell, libranzaFecha);
                cell = new PdfPCell(new Phrase("Páguese por el fideicomiso de fortalecimiento del Sistema Nacional de Aeropuertos a:", underlineBoldFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 14;
                cell.Border = 0;
                table.AddCell(cell);

                // LINEA
                ControlPage(ref table, ref cell, libranzaFecha);
                //cell = new PdfPCell(new Phrase((libranza.RegistraCesion) ? libranza.BeneficiarioCesion  + ", CUIT " + libranza.CesionCuit.ToString() : beneficiario.RazonSocial.ToUpper() + " CUIT: " + beneficiario.Cuit, boldFont));
                cell = new PdfPCell(new Phrase(beneficiario.RazonSocial.ToUpper() + ", CUIT " + beneficiario.Cuit, boldFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 14;
                cell.Border = 0;
                table.AddCell(cell);

                // LINEA  
                ControlPage(ref table, ref cell, libranzaFecha);
                cell = new PdfPCell(new Phrase(" ", normalFont));
                cell.Colspan = 14;
                cell.Border = 0;
                table.AddCell(cell);

                // LINEA
                ControlPage(ref table, ref cell, libranzaFecha);
                cell = new PdfPCell(new Phrase("Cuenta Pagadora:", boldFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 4;
                cell.Border = 0;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(IsEmpty(cuenta.Nombre), normalFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 10;
                cell.Border = 0;
                table.AddCell(cell);

                // LINEA
                ControlPage(ref table, ref cell, libranzaFecha);
                cell = new PdfPCell(new Phrase("N° Cuenta Fiducidiaria:", boldFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 4;
                cell.Border = 0;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(IsEmpty(cuenta.NroCuenta), normalFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 10;
                cell.Border = 0;
                table.AddCell(cell);

                // LINEA
                ControlPage(ref table, ref cell, libranzaFecha);
                cell = new PdfPCell(new Phrase("En concepto de:", boldFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 4;
                cell.Border = 0;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(IsEmpty(proyecto.CodObra) + " " + IsEmpty(proyecto.Nombre), normalFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 10;
                cell.Border = 0;
                table.AddCell(cell);

                // LINEA
                ControlPage(ref table, ref cell, libranzaFecha);
                cell = new PdfPCell(new Phrase("Importe neto a transferir:", boldFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 4;
                cell.Border = 0;
                table.AddCell(cell);


                cell = new PdfPCell(new Phrase(Utils.numberToText(totalLibranza, libranza.MonedaNombreCorto), boldFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 10;
                cell.Border = 0;
                table.AddCell(cell);

                // LINEA
                ControlPage(ref table, ref cell, libranzaFecha);
                cell = new PdfPCell(new Phrase("Datos de la cuenta depositaria:", underlineBoldFontNormal));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 14;
                cell.Border = 0;
                table.AddCell(cell);

                //if (libranza.RegistraCesion)
                //{

                //    //// LINEA
                //    //ControlPage(ref table, ref cell, libranzaFecha);
                //    //cell = new PdfPCell(new Phrase("Tipo:", boldFont));
                //    //cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //    //cell.Colspan = 4;
                //    //cell.Border = 0;
                //    //table.AddCell(cell);

                //    //cell = new PdfPCell(new Phrase(IsEmpty(libranza.CesionTipoCuenta), normalFont));
                //    //cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //    //cell.Colspan = 10;
                //    //cell.Border = 0;
                //    //table.AddCell(cell);

                //    //// LINEA
                //    //ControlPage(ref table, ref cell, libranzaFecha);
                //    //cell = new PdfPCell(new Phrase("N°Cuenta:", boldFont));
                //    //cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //    //cell.Colspan = 4;
                //    //cell.Border = 0;
                //    //table.AddCell(cell);

                //    //cell = new PdfPCell(new Phrase(IsEmpty(libranza.CesionNroCuenta), normalFont));
                //    //cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //    //cell.Colspan = 10;
                //    //cell.Border = 0;
                //    //table.AddCell(cell);

                //    //// LINEA
                //    //ControlPage(ref table, ref cell, libranzaFecha);
                //    //cell = new PdfPCell(new Phrase("Banco:", boldFont));
                //    //cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //    //cell.Colspan = 4;
                //    //cell.Border = 0;
                //    //table.AddCell(cell);

                //    //cell = new PdfPCell(new Phrase(IsEmpty(libranza.CesionNombreBanco), normalFont));
                //    //cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //    //cell.Colspan = 10;
                //    //cell.Border = 0;
                //    //table.AddCell(cell);

                //    //// LINEA
                //    //ControlPage(ref table, ref cell, libranzaFecha);
                //    //cell = new PdfPCell(new Phrase("Surcusal:", boldFont));
                //    //cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //    //cell.Colspan = 4;
                //    //cell.Border = 0;
                //    //table.AddCell(cell);

                //    //cell = new PdfPCell(new Phrase(IsEmpty(libranza.CesionSucursal), normalFont));
                //    //cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //    //cell.Colspan = 10;
                //    //cell.Border = 0;
                //    //table.AddCell(cell);

                //    //// LINEA
                //    //ControlPage(ref table, ref cell, libranzaFecha);
                //    //cell = new PdfPCell(new Phrase("CBU:", boldFont));
                //    //cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //    //cell.Colspan = 4;
                //    //cell.Border = 0;
                //    //table.AddCell(cell);

                //    //cell = new PdfPCell(new Phrase(IsEmpty(libranza.CesionCbu), normalFont));
                //    //cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //    //cell.Colspan = 10;
                //    //cell.Border = 0;
                //    //table.AddCell(cell);

                //    //// LINEA
                //    //ControlPage(ref table, ref cell, libranzaFecha);
                //    //cell = new PdfPCell(new Phrase("CUIT:", boldFont));
                //    //cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //    //cell.Colspan = 4;
                //    //cell.Border = 0;
                //    //table.AddCell(cell);

                //    //cell = new PdfPCell(new Phrase(IsEmpty(libranza.CesionCuit), normalFont));
                //    //cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //    //cell.Colspan = 10;
                //    //cell.Border = 0;
                //    //table.AddCell(cell);

                //    //// LINEA
                //    //ControlPage(ref table, ref cell, libranzaFecha);
                //    //cell = new PdfPCell(new Phrase("Titular de la cuenta:", boldFont));
                //    //cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //    //cell.Colspan = 4;
                //    //cell.Border = 0;
                //    //table.AddCell(cell);

                //    //cell = new PdfPCell(new Phrase(IsEmpty(libranza.CesionTitular), normalFont));
                //    //cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //    //cell.Colspan = 10;
                //    //cell.Border = 0;
                //    //table.AddCell(cell);

                //    foreach (VMBeneficiario item in libranza.BeneficiarioCesiones)
                //    {

                //        var EsNacionalCesion = item.NacionalExtranjero == "true" ? true : false;

                //        VMBeneficiarioBancos bancoCesion = null;
                //        foreach (VMBeneficiarioBancos i in item.Bancos)
                //            bancoCesion = i;

                       

                //        libranzaLetra = libranzaTipo.Id == 1 ? "'A'" : "'B'";
                //        libranzaNro = Convert.ToString(libranza.NroLibranza);


                //        // LINEA
                //        ControlPage(ref table, ref cell, libranzaFecha);
                //        cell = new PdfPCell(new Phrase("Tipo:", boldFont));
                //        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //        cell.Colspan = 4;
                //        cell.Border = 0;
                //        table.AddCell(cell);

                //        cell = new PdfPCell(new Phrase(IsEmpty(bancoCesion.TipoCuenta), normalFont));
                //        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //        cell.Colspan = 10;
                //        cell.Border = 0;
                //        table.AddCell(cell);

                //        // LINEA
                //        ControlPage(ref table, ref cell, libranzaFecha);
                //        cell = new PdfPCell(new Phrase("N°Cuenta:", boldFont));
                //        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //        cell.Colspan = 4;
                //        cell.Border = 0;
                //        table.AddCell(cell);

                //        cell = new PdfPCell(new Phrase(IsEmpty(bancoCesion.NroCuenta), normalFont));
                //        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //        cell.Colspan = 10;
                //        cell.Border = 0;
                //        table.AddCell(cell);

                //        // LINEA
                //        ControlPage(ref table, ref cell, libranzaFecha);
                //        cell = new PdfPCell(new Phrase("Banco:", boldFont));
                //        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //        cell.Colspan = 4;
                //        cell.Border = 0;
                //        table.AddCell(cell);

                //        cell = new PdfPCell(new Phrase(IsEmpty(bancoCesion.NombreBanco), normalFont));
                //        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //        cell.Colspan = 10;
                //        cell.Border = 0;
                //        table.AddCell(cell);

                //        // LINEA
                //        ControlPage(ref table, ref cell, libranzaFecha);
                //        cell = new PdfPCell(new Phrase("Surcusal:", boldFont));
                //        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //        cell.Colspan = 4;
                //        cell.Border = 0;
                //        table.AddCell(cell);

                //        cell = new PdfPCell(new Phrase(IsEmpty(bancoCesion.Sucursal), normalFont));
                //        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //        cell.Colspan = 10;
                //        cell.Border = 0;
                //        table.AddCell(cell);

                //        // LINEA
                //        ControlPage(ref table, ref cell, libranzaFecha);
                //        cell = new PdfPCell(new Phrase("CBU:", boldFont));
                //        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //        cell.Colspan = 4;
                //        cell.Border = 0;
                //        table.AddCell(cell);

                //        cell = new PdfPCell(new Phrase(IsEmpty(bancoCesion.Cbu), normalFont));
                //        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //        cell.Colspan = 10;
                //        cell.Border = 0;
                //        table.AddCell(cell);

                //        // LINEA
                //        ControlPage(ref table, ref cell, libranzaFecha);
                //        cell = new PdfPCell(new Phrase("CUIT:", boldFont));
                //        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //        cell.Colspan = 4;
                //        cell.Border = 0;
                //        table.AddCell(cell);

                //        cell = new PdfPCell(new Phrase(IsEmpty(item.Cuit), normalFont));
                //        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //        cell.Colspan = 10;
                //        cell.Border = 0;
                //        table.AddCell(cell);

                //        // LINEA
                //        ControlPage(ref table, ref cell, libranzaFecha);
                //        cell = new PdfPCell(new Phrase("Titular de la cuenta:", boldFont));
                //        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //        cell.Colspan = 4;
                //        cell.Border = 0;
                //        table.AddCell(cell);

                //        cell = new PdfPCell(new Phrase(IsEmpty(item.RazonSocial), normalFont));
                //        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //        cell.Colspan = 10;
                //        cell.Border = 0;
                //        table.AddCell(cell);

                //    }
                //}
                //else
                //{
                    // LINEA
                    ControlPage(ref table, ref cell, libranzaFecha);
                    cell = new PdfPCell(new Phrase("Tipo:", boldFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Colspan = 4;
                    cell.Border = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(IsEmpty(cuentaDepositaria.TipoCuenta), normalFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Colspan = 10;
                    cell.Border = 0;
                    table.AddCell(cell);

                    // LINEA
                    ControlPage(ref table, ref cell, libranzaFecha);
                    cell = new PdfPCell(new Phrase("N°Cuenta:", boldFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Colspan = 4;
                    cell.Border = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(IsEmpty(cuentaDepositaria.NroCuenta), normalFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Colspan = 10;
                    cell.Border = 0;
                    table.AddCell(cell);

                    // LINEA
                    ControlPage(ref table, ref cell, libranzaFecha);
                    cell = new PdfPCell(new Phrase("Banco:", boldFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Colspan = 4;
                    cell.Border = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(IsEmpty(cuentaDepositaria.NombreBanco), normalFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Colspan = 10;
                    cell.Border = 0;
                    table.AddCell(cell);

                    // LINEA
                    ControlPage(ref table, ref cell, libranzaFecha);
                    cell = new PdfPCell(new Phrase("Surcusal:", boldFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Colspan = 4;
                    cell.Border = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(IsEmpty(cuentaDepositaria.Sucursal), normalFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Colspan = 10;
                    cell.Border = 0;
                    table.AddCell(cell);

                    if (beneficiario.NacionalExtranjero.ToUpper().Trim() == "TRUE")
                    {
                        // LINEA
                        ControlPage(ref table, ref cell, libranzaFecha);
                        cell = new PdfPCell(new Phrase("CBU:", boldFont));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 4;
                        cell.Border = 0;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(IsEmpty(cuentaDepositaria.Cbu), normalFont));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 10;
                        cell.Border = 0;
                        table.AddCell(cell);

                        // LINEA
                        ControlPage(ref table, ref cell, libranzaFecha);
                        cell = new PdfPCell(new Phrase("CUIT:", boldFont));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 4;
                        cell.Border = 0;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(IsEmpty(beneficiario.Cuit), normalFont));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 10;
                        cell.Border = 0;
                        table.AddCell(cell);

                        // LINEA
                        ControlPage(ref table, ref cell, libranzaFecha);
                        cell = new PdfPCell(new Phrase("Titular de la cuenta:", boldFont));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 4;
                        cell.Border = 0;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(IsEmpty(beneficiario.RazonSocial), normalFont));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 10;
                        cell.Border = 0;
                        table.AddCell(cell);

                    }
                    else
                    {
                        // LINEA
                        ControlPage(ref table, ref cell, libranzaFecha);
                        cell = new PdfPCell(new Phrase("CBU / IBAN:", boldFont));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 4;
                        cell.Border = 0;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(IsEmpty(cuentaDepositaria.Cbu), normalFont));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 10;
                        cell.Border = 0;
                        table.AddCell(cell);

                        // LINEA
                        ControlPage(ref table, ref cell, libranzaFecha);
                        cell = new PdfPCell(new Phrase("BIC / SWIFT:", boldFont));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 4;
                        cell.Border = 0;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(IsEmpty(cuentaDepositaria.BicSwift), normalFont));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 10;
                        cell.Border = 0;
                        table.AddCell(cell);

                        // LINEA
                        ControlPage(ref table, ref cell, libranzaFecha);
                        cell = new PdfPCell(new Phrase("Identificacion Tributaria:", boldFont));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 4;
                        cell.Border = 0;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(IsEmpty(beneficiario.Cuit), normalFont));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 10;
                        cell.Border = 0;
                        table.AddCell(cell);

                        // LINEA
                        ControlPage(ref table, ref cell, libranzaFecha);
                        cell = new PdfPCell(new Phrase("Titular de la cuenta:", boldFont));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 4;
                        cell.Border = 0;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(IsEmpty(cuentaDepositaria.Titular), normalFont));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 10;
                        cell.Border = 0;
                        table.AddCell(cell);
                    }
               // }

                // LINEA
                ControlPage(ref table, ref cell, libranzaFecha);
                cell = new PdfPCell(new Phrase("Deducciones a considerar en concepto de:", underlineBoldFontNormal));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 14;
                cell.Border = 0;
                table.AddCell(cell);

                // LINEA
                ControlPage(ref table, ref cell, libranzaFecha);
                cell = new PdfPCell(new Phrase("Objeto:", boldFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 4;
                cell.Border = 0;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(IsEmpty(libranza.Objeto), normalFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 10;
                cell.Border = 0;
                table.AddCell(cell);

                // LINEA
                ControlPage(ref table, ref cell, libranzaFecha);
                cell = new PdfPCell(new Phrase("Fondo de Reparo:", boldFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 4;
                cell.Border = 0;
                table.AddCell(cell);

                if (libranza.Sustituido == true)
                {
                    cell = new PdfPCell(new Phrase(IsEmpty(""), normalFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Colspan = 2;
                    cell.Border = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("(Sustituido por póliza de caución)", boldFont));
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.Colspan = 8;
                    cell.Border = 0;
                    table.AddCell(cell);
                }
                else
                {
                    cell = new PdfPCell(new Phrase(IsEmpty(string.Format("{0:N2}", (libranza.MontoFondoReparo == null ? 0 : libranza.MontoFondoReparo))), normalFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Colspan = 10;
                    cell.Border = 0;
                    table.AddCell(cell);
                }

                // LINEA
                ControlPage(ref table, ref cell, libranzaFecha);
                cell = new PdfPCell(new Phrase("Multa:", boldFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 4;
                cell.Border = 0;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(IsEmpty(string.Format("{0:N2}", (libranza.Multa == null ? 0 : libranza.Multa))), normalFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 10;
                cell.Border = 0;
                table.AddCell(cell);

                // LINEA
                ControlPage(ref table, ref cell, libranzaFecha);
                cell = new PdfPCell(new Phrase("Mora:", boldFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 4;
                cell.Border = 0;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(IsEmpty(string.Format("{0:N2}", (libranza.Mora == null ? 0 : libranza.Mora))), normalFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 10;
                cell.Border = 0;
                table.AddCell(cell);

                // LINEA
                ControlPage(ref table, ref cell, libranzaFecha);
                cell = new PdfPCell(new Phrase("Total Deducciones:", boldFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 4;
                cell.Border = 0;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(libranza.MonedaNombreCorto + " " + IsEmpty(string.Format("{0:N2}", totalDeducciones)), normalFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 8;
                cell.Border = 0;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(" ", normalFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 8;
                cell.Border = 0;
                table.AddCell(cell);

                // LINEA
                ControlPage(ref table, ref cell, libranzaFecha);
                cell = new PdfPCell(new Phrase("Retenciones a considerar en concepto de Embargo/Cesiones:", underlineBoldFontNormal));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 14;
                cell.Border = 0;
                table.AddCell(cell);

                // LINEA  
                ControlPage(ref table, ref cell, libranzaFecha);
                cell = new PdfPCell(new Phrase("Datos del Embargo:", underlineBoldFontNormal));
                cell.Colspan = 14;
                cell.Border = 0;
                table.AddCell(cell);

                // LINEA
                ControlPage(ref table, ref cell, libranzaFecha);
                cell = new PdfPCell(new Phrase("Número:", boldFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 4;
                cell.Border = 0;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(IsEmpty(libranza.NroEmbargo), normalFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 10;
                cell.Border = 0;
                table.AddCell(cell);

                // LINEA
                ControlPage(ref table, ref cell, libranzaFecha);
                cell = new PdfPCell(new Phrase("Responsable:", boldFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 4;
                cell.Border = 0;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(IsEmpty(libranza.ResponsableEmbargo), normalFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 10;
                cell.Border = 0;
                table.AddCell(cell);

                // LINEA
                ControlPage(ref table, ref cell, libranzaFecha);
                cell = new PdfPCell(new Phrase("Monto:", boldFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 4;
                cell.Border = 0;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(libranza.MonedaNombreCorto + " " + IsEmpty(string.Format("{0:N2}", libranza.MontoEmbargo)), normalFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 10;
                cell.Border = 0;
                table.AddCell(cell);

                // LINEA
                ControlPage(ref table, ref cell, libranzaFecha);
                cell = new PdfPCell(new Phrase("Saldo:", boldFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 4;
                cell.Border = 0;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(libranza.MonedaNombreCorto + " " + IsEmpty(string.Format("{0:N2}", libranza.SaldoEmbargo)), normalFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 10;
                cell.Border = 0;
                table.AddCell(cell);

                // LINEA
                ControlPage(ref table, ref cell, libranzaFecha);
                cell = new PdfPCell(new Phrase("Datos de las cesiones:", underlineBoldFontNormal));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 14;
                cell.Border = 0;
                table.AddCell(cell);


                ControlPage(ref table, ref cell, libranzaFecha);
                cell = new PdfPCell(new Phrase("Registra:", boldFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 4;
                cell.Border = 0;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase((libranza.RegistraCesion ? "SI" : "NO"), normalFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 10;
                cell.Border = 0;
                table.AddCell(cell);

                 strPagos = new string[libranza.BeneficiarioCesiones.Count];

                if (libranza.RegistraCesion)
                {
                    var indice = 0;
                    foreach (VMBeneficiario item in libranza.BeneficiarioCesiones)
                    {

                        var EsNacionalCesion = item.NacionalExtranjero == "true" ? true : false;

                        VMBeneficiarioBancos bancoCesion = null;
                        foreach (VMBeneficiarioBancos i in item.Bancos)
                            bancoCesion = i;



                        libranzaLetra = libranzaTipo.Id == 1 ? "'A'" : "'B'";
                        libranzaNro = Convert.ToString(libranza.NroLibranza);


                        

                        // LINEA
                        ControlPage(ref table, ref cell, libranzaFecha);
                        cell = new PdfPCell(new Phrase("N°Escritura:", boldFont));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 4;
                        cell.Border = 0;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(IsEmpty(libranza.NroEscrituraCesiones[0]), normalFont));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 10;
                        cell.Border = 0;
                        table.AddCell(cell);

                        // LINEA
                        ControlPage(ref table, ref cell, libranzaFecha);
                        cell = new PdfPCell(new Phrase("Beneficiario:", boldFont));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 4;
                        cell.Border = 0;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(IsEmpty(item.RazonSocial), normalFont));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 10;
                        cell.Border = 0;
                        table.AddCell(cell);

                        // LINEA
                        ControlPage(ref table, ref cell, libranzaFecha);
                        cell = new PdfPCell(new Phrase("CUIT:", boldFont));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 4;
                        cell.Border = 0;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(IsEmpty(item.Cuit), normalFont));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 10;
                        cell.Border = 0;
                        table.AddCell(cell);

                        // LINEA
                        ControlPage(ref table, ref cell, libranzaFecha);
                        cell = new PdfPCell(new Phrase("Datos de la cuenta depositaria:", underlineBoldFontNormal));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 14;
                        cell.Border = 0;
                        table.AddCell(cell);

                        // LINEA
                        ControlPage(ref table, ref cell, libranzaFecha);
                        cell = new PdfPCell(new Phrase("Tipo:", boldFont));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 4;
                        cell.Border = 0;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(IsEmpty(bancoCesion.TipoCuenta), normalFont));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 10;
                        cell.Border = 0;
                        table.AddCell(cell);

                        // LINEA
                        ControlPage(ref table, ref cell, libranzaFecha);
                        cell = new PdfPCell(new Phrase("N°Cuenta:", boldFont));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 4;
                        cell.Border = 0;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(IsEmpty(bancoCesion.NroCuenta), normalFont));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 10;
                        cell.Border = 0;
                        table.AddCell(cell);

                        // LINEA
                        ControlPage(ref table, ref cell, libranzaFecha);
                        cell = new PdfPCell(new Phrase("Banco:", boldFont));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 4;
                        cell.Border = 0;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(IsEmpty(bancoCesion.NombreBanco), normalFont));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 10;
                        cell.Border = 0;
                        table.AddCell(cell);

                        // LINEA
                        ControlPage(ref table, ref cell, libranzaFecha);
                        cell = new PdfPCell(new Phrase("Surcusal:", boldFont));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 4;
                        cell.Border = 0;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(IsEmpty(bancoCesion.Sucursal), normalFont));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 10;
                        cell.Border = 0;
                        table.AddCell(cell);

                        // LINEA
                        ControlPage(ref table, ref cell, libranzaFecha);
                        cell = new PdfPCell(new Phrase("CBU:", boldFont));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 4;
                        cell.Border = 0;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(IsEmpty(bancoCesion.Cbu), normalFont));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 10;
                        cell.Border = 0;
                        table.AddCell(cell);

                        // LINEA
                        ControlPage(ref table, ref cell, libranzaFecha);
                        cell = new PdfPCell(new Phrase("CUIT:", boldFont));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 4;
                        cell.Border = 0;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(IsEmpty(item.Cuit), normalFont));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 10;
                        cell.Border = 0;
                        table.AddCell(cell);

                        // LINEA
                        ControlPage(ref table, ref cell, libranzaFecha);
                        cell = new PdfPCell(new Phrase("Titular de la cuenta:", boldFont));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 4;
                        cell.Border = 0;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(IsEmpty(item.RazonSocial), normalFont));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 10;
                        cell.Border = 0;
                        table.AddCell(cell);

                        totalLibranzaCesion = totalLibranzaCesion + libranza.MontosCesiones[indice].Value;
                        strPagos[indice] = IsEmpty(item.RazonSocial) + ' ' + libranza.MonedaNombreCorto + "  " + string.Format("{0:N2}", (libranza.MontosCesiones[indice].Value));
                        indice++;
                    }


                    

                }
                else{
   
                        // LINEA
                        ControlPage(ref table, ref cell, libranzaFecha);
                        cell = new PdfPCell(new Phrase("N°Escritura:", boldFont));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 4;
                        cell.Border = 0;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(IsEmpty(libranza.NroEscritura), normalFont));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 10;
                        cell.Border = 0;
                        table.AddCell(cell);

                        // LINEA
                        ControlPage(ref table, ref cell, libranzaFecha);
                        cell = new PdfPCell(new Phrase("Beneficiario:", boldFont));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 4;
                        cell.Border = 0;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(IsEmpty(libranza.BeneficiarioCesion), normalFont));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 10;
                        cell.Border = 0;
                        table.AddCell(cell);

                        // LINEA
                        ControlPage(ref table, ref cell, libranzaFecha);
                        cell = new PdfPCell(new Phrase("CUIT:", boldFont));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 4;
                        cell.Border = 0;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(IsEmpty(libranza.IdentificacionTributaria), normalFont));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 10;
                        cell.Border = 0;
                        table.AddCell(cell);
                    }




                // LINEA
                ControlPage(ref table, ref cell, libranzaFecha);
                cell = new PdfPCell(new Phrase("Datos generales del trámite:", underlineBoldFontNormal));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 14;
                cell.Border = 0;
                table.AddCell(cell);

                // LINEA
                ControlPage(ref table, ref cell, libranzaFecha);
                cell = new PdfPCell(new Phrase("Expediente Pago N°:", boldFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 4;
                cell.Border = 0;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(IsEmpty(proyecto.NroPago), normalFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 10;
                cell.Border = 0;
                table.AddCell(cell);

                // LINEA
                ControlPage(ref table, ref cell, libranzaFecha);
                cell = new PdfPCell(new Phrase("Norma Legal:", boldFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 4;
                cell.Border = 0;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(IsEmpty(libranza.NormaLegal), normalFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 10;
                cell.Border = 0;
                table.AddCell(cell);

                // LINEA
                ControlPage(ref table, ref cell, libranzaFecha);
                cell = new PdfPCell(new Phrase("Objeto:", boldFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 4;
                cell.Border = 0;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(IsEmpty(libranza.ObjetoDatosGenerales), normalFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 10;
                cell.Border = 0;
                table.AddCell(cell);

                // LINEA
                ControlPage(ref table, ref cell, libranzaFecha);
                cell = new PdfPCell(new Phrase("Observaciones:", boldFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 4;
                cell.Border = 0;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(IsEmpty(libranza.Observaciones), normalFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 10;
                cell.Border = 0;
                table.AddCell(cell);


                // LINEA  
                ControlPage(ref table, ref cell, libranzaFecha);
                cell = new PdfPCell(new Phrase(" ", normalFont));
                cell.Colspan = 14;
                cell.Border = 0;
                table.AddCell(cell);

                // LINEA
                ControlPage(ref table, ref cell, libranzaFecha);
                cell = new PdfPCell(new Phrase("Documentos de pago:", boldFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 14;
                cell.Border = 0;
                table.AddCell(cell);

                // LINEA
                ControlPage(ref table, ref cell, libranzaFecha);
                cell = new PdfPCell(new Phrase("Tipo Doc", boldFont));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Colspan = 2;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Número", boldFont));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Colspan = 2;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Fecha", boldFont));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Colspan = 2;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Monto Neto", boldFont));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;

                cell.Colspan = 2;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("IVA", boldFont));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Colspan = 2;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("IIBB", boldFont));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Colspan = 2;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Monto Total", boldFont));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Colspan = 2;
                table.AddCell(cell);

                foreach (VMFactura item in libranza.Factura)
                {
                    // LINEA
                    ControlPage(ref table, ref cell, libranzaFecha);
                    cell = new PdfPCell(new Phrase(item.Tipo, smallFont));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.Colspan = 2;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(item.Nro, smallFont));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.Colspan = 2;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(string.Format("{0:dd/MM/yyyy}", item.Fecha), smallFont));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.Colspan = 2;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(libranza.MonedaNombreCorto + " " + string.Format("{0:N2}", (item.Monto == null ? 0 : item.Monto)), smallFont));
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    //cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                    if(proyecto.PagosImpuestosIncluidos==false)
                    {
                        cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                    }
                    cell.Colspan = 2;
                    table.AddCell(cell);


                    cell = new PdfPCell(new Phrase(libranza.MonedaNombreCorto + " " + string.Format("{0:N2}", (decimal)(item.Iva == null ? 0 : item.Iva)), smallFont));
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.Colspan = 2;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(libranza.MonedaNombreCorto + " " + string.Format("{0:N2}", (decimal)(item.Ibb == null ? 0 : item.Ibb)), smallFont));
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.Colspan = 2;
                    table.AddCell(cell);

                    subTotal = (decimal)((item.Monto == null ? 0 : item.Monto) + (item.Iva == null ? 0 : item.Iva) + (item.Ibb == null ? 0 : item.Ibb));
                    if (item.Tipo.ToUpper().Contains("CRÉDITO"))
                        subTotal = subTotal * -1;

                    subTotal = Math.Abs(subTotal);

                    cell = new PdfPCell(new Phrase(libranza.MonedaNombreCorto + " " + string.Format("{0:N2}", subTotal), normalFont));
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    if (proyecto.PagosImpuestosIncluidos == true)
                    {
                        cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                    }
                    cell.Colspan = 2;
                    table.AddCell(cell);
                }

                // LINEA
                ControlPage(ref table, ref cell, libranzaFecha);
                cell = new PdfPCell(new Phrase(" ", normalFont));
                cell.Colspan = 14;
                cell.Border = 0;
                table.AddCell(cell);

                // LINEA
                ControlPage(ref table, ref cell, libranzaFecha);

                /*if (libranzaLetra == "'A'")
                    cell = new PdfPCell(new Phrase("Monto Total: ", boldFont));
                else*/
                cell = new PdfPCell(new Phrase("Subtotal: ", boldFont));

                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 4;
                cell.Border = 0;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(libranza.MonedaNombreCorto + " " + string.Format("{0:N2}", totalLibranza + totalDeducciones), normalFont));

                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.Colspan = 4;
                cell.Border = 0;
                table.AddCell(cell);

                // LINEA
                cell = new PdfPCell(new Phrase(" ", normalFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 8;
                cell.Border = 0;
                table.AddCell(cell);

                // LINEA
                ControlPage(ref table, ref cell, libranzaFecha);
                cell = new PdfPCell(new Phrase("Total Deducciones: ", boldFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 4;
                cell.Border = 0;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(libranza.MonedaNombreCorto + " " + string.Format("{0:N2}", (totalDeducciones) * -1), normalFont));
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.Colspan = 4;
                cell.Border = 0;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(" ", normalFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 8;
                cell.Border = 0;
                table.AddCell(cell);

                // LINEA
                ControlPage(ref table, ref cell, libranzaFecha);
                cell = new PdfPCell(new Phrase("Monto a Pagar: ", boldFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 4;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(libranza.MonedaNombreCorto + " " + string.Format("{0:N2}", totalLibranza), normalFont));
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.Colspan = 4;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(" ", normalFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 8;
                cell.Border = 0;
                table.AddCell(cell);

                // LINEA
                ControlPage(ref table, ref cell, libranzaFecha);
                cell = new PdfPCell(new Phrase(" ", normalFont));
                cell.Colspan = 14;
                cell.Border = 0;
                table.AddCell(cell);

                if (strPagos.Length > 0)
                {

                    ControlPage(ref table, ref cell, libranzaFecha);
                    cell = new PdfPCell(new Phrase(" ", boldFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Colspan = 4;
                    cell.Border = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", boldFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Colspan = 10;
                    cell.Border = 0;
                    table.AddCell(cell);
                }

                for (var i = 0; i < strPagos.Length; i++)
                {

                    // LINEA
                    cell = new PdfPCell(new Phrase("Monto a transferir a " + strPagos[i], largeFont));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.Colspan = 14;
                    table.AddCell(cell);


                }


                // LINEA
                cell = new PdfPCell(new Phrase("Monto a transferir a " + (beneficiario.RazonSocial) + ' ' + libranza.MonedaNombreCorto + "  " + string.Format("{0:N2}", (totalLibranza - totalLibranzaCesion)), largeFont));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Colspan = 14;
                table.AddCell(cell);

                // LINEA
                ControlPage(ref table, ref cell, libranzaFecha);
                cell = new PdfPCell(new Phrase(" ", normalFont));
                cell.Colspan = 14;
                cell.Border = 0;
                table.AddCell(cell);

                // LINEA
                ControlPage(ref table, ref cell, libranzaFecha);
                cell = new PdfPCell(new Phrase(" ", normalFont));
                cell.Colspan = 14;
                cell.Border = 0;
                table.AddCell(cell);

                // LINEA
                ControlPage(ref table, ref cell, libranzaFecha);
                cell = new PdfPCell(new Phrase(" ", normalFont));
                cell.Colspan = 14;
                cell.Border = 0;
                table.AddCell(cell);

                // LINEA
                ControlPage(ref table, ref cell, libranzaFecha);
                cell = new PdfPCell(new Phrase(" ", normalFont));
                cell.Colspan = 14;
                cell.Border = 0;
                table.AddCell(cell);

                // LINEA
                ControlPage(ref table, ref cell, libranzaFecha);
                string s = "                                               ";
                cell = new PdfPCell(new Phrase("FIRMA Y SELLO ADMINISTRACION" + s + "ORSNA (Gerente o Responsable)", smallFont));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Colspan = 7;
                cell.Border = 0;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("FIRMA Y SELLO AUTORIZACION DE PAGO" + s + "ORSNA (Directorio)", smallFont));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Colspan = 7;
                cell.Border = 0;
                table.AddCell(cell);

                // FINAL
                document.Add(table);
                document.Close();

                byte[] content = myMemoryStream.ToArray();
                return content;
            }
        }
        public static string IsEmpty(string value)
        {
            return (value == null || value.Trim().Length == 0 ? "N/A" : value);
        }
        public static void ControlPage(ref PdfPTable table, ref PdfPCell cell, DateTime libranzaFecha)
        {
            if (page_line >= page_size)
            {
                page_line = 0;

                // LINEA  
                page_line++;
                cell = new PdfPCell(new Phrase(" ", normalFont));
                cell.Colspan = 14;
                cell.Border = 0;
                table.AddCell(cell);
                page_line++;

                // LINEA   
                page_line++;
                cell = new PdfPCell(new Phrase("Autorización de Pago Libranza ORSNA: " + libranzaLetra, titleFont));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Colspan = 14;
                cell.Border = 0;
                table.AddCell(cell);
                /*
                // LINEA  
                page_line++;
                cell = new PdfPCell(new Phrase(" ", normalFont));
                cell.Colspan = 14;
                cell.Border = 0;
                table.AddCell(cell);*/

                // LINEA          
                page_line++;
                //string base64 = @"iVBORw0KGgoAAAANSUhEUgAAAQUAAAAjCAYAAACdFB8OAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsQAAA7EAZUrDhsAABzMSURBVHhe7Vx3fFXFtv4O6aSRkISEEGrA0IKAiIKCIE0U60VQsIvXcsUCIlgABRHsBRRFbKCigoIFRZqEKjWhJKQnpCek98Z + 35q9d3ISIsj9vfeHvv35O5yzZ09dM + tba81MtGkELFiwYMFAK + PbggULFhQsUrBgwUITWKRgwYKFJrBIwYIFC01gkYIFCxaawCIFCxYsNIFFChYsWGgCixQuAJevTUJsYbXxZMHCPxMWKVwAXB1awcFmM54sWPhnwiKFC8Cf0UFtvYb7tmbC7f0Y9PsqEQdzKlV6SXU9Lv82GQO / TkL4l4m447d0FDPNxIv7c + G34iSCVsZh / h + 5RqqOz08WofuqeNTUnzFSwDYy8MC2TOMJeCwiC8 / syYFcSrXNPYwlh06r9Bm7suH1wUkMXZuMiIxylXYlf89hXhPvHS1A / zWJKKvV + 3MgpwID1yShNcdw95YMjqmxXUHtGQ3D1iXjEmMsEzemIa + yTr0btT5FpV / MsV + 0OgGzjXbKa88wXzrcWae8 + yGpRKVf / 9MpLDuar34LpPw38cU4lFuJQd8kob6FS7bJxTVKVpGnddkKPospwgD2Wfozan0qtqSVGW90yPuglbHw / fAkXjusy0bw6I4s1V + Zqysol02pTctJWyILqXsQ870b1djXhQfz1DsZj7Qr7wXye2dGhfr9d4dFCv8LcH4vWi3qpcMC0dnTCYOWx2B3VjmcHGzYl1iC / n6uuP0iL2xIKkVvLh7BjT + fwrztWZg1wA939fRWSmqP2XtykZBajpXRRUYKEJlXjRVM / zSmUD0fzquiklTBJt6Low1tXBzwZWwR3tibhy / HBMOXz + sTS1XeXVkVWByRjaTiatRTwR / ZlI5ILmKnVuwj3136QSw6su9vXRmIr + OK4UlSaY6dyWXo7u2M23p4YS + Jr + MncSp9K9O7eDnhnp5tcOdF3hga5KbSPSiHiMwKLB0eCD83R6xhvYIdbDeuqEb9FmxNL0daWS2Kqs / gYDMFNTGXpJmfU4XZuxvJM7GkBkeyKzCVbXo7t8Loj + OxNEqX44yd2bibJHZ / Lx / M7O + Hp37NwA0kI8FelimoqlPl2ro5YNyn8cgsr1XvBEJmh1NKMTy4tfpM35SBK9alqHdHT1fjFPs6rbcPprD8xFAvlX4spayBJP / u + K9JoY6WRBbX / yXO / A3 + LOP9Y1yEVWdQ + mBP3MeFsuG6jhjChXjX5iy4OVK8HMKSIQGYPdBfKVwGrWEd5bbhcAEi7gjFrIF + WDykHXKnhal0wW4qUlZ + FZbf2AmPUpFNOFKBfQJccc + PaSipqVckIEqtwC / 5WVHHOjg3GeV1 + P7aELxBohI486U / yen6n9Iw4vsUtG3jLBXChSHRbb9lYEy4r8r / QB8fVD7cC9VFtfjgeCNRqVY4H88O8sOcS / zx4YggVBUaik3yC2fdozt6YGQHd0zo4oVXxTJX1CHn / h64h / LYwrGsGtNBZXclgTX0m3BheRkbu6L61PimEasPnMY3k7pgE0my0PC2bPzPx91RyXDd + BDMHReMRw1P6o3fs / HxzZ2x4PIAPMM + b72jG37Yr3sL0vaIYHc8RULecG1HEQ69lCr1TqAiRP6z4LIAvHZFIGKmXYTdbDeRRObO / gWS4MZwrCM41rtIhAocg4PdmP7OuCBSyC2twb8 + PQrvZ3 + H06Ob4Th9MwLnRWD697FGjqa4fmUkAuZGIOTFXQ2fDi / sRNeXdqt6YnPPtgoH00ow5J0DsD2xBQ6s3 / uZ3zHuwyM4eEq3MiaCWY//8xG47qNII6UppA3f53bgttXHjZSz0f3lPXCcuVWN57/F8fxq+Hg7GU86xnZ2R2KBsciouAO+TqYrmoj7aHGeJjHEi5WsO4Mr27vTYtXD9twh2F48gmf36lbwcbr/42mFxnfyQH1xLfbRsgkqWOZBKu1kKnAnWrcahi1ciw0oo4W7n8T0+NB2eJDK4bQgEg9t15Wkhu9Wjw5GIuvbeaocP0+gMtToIUIKxzCMFtEe3m1dEF3QbFPV2QFjN5xC/6+ScC3d9SlUNoELlXzuvlz0pRc0ZFUCMspqEEvC8PFxUe9foJWX8MZxYSRK6Q3IvoyQgAmhgTN20Upz1XqVYZEr65rY3RsulPViuvAmKIIGXE0lZZzDOaHstTMcE58NDA3i+NjPJIYGHk6tsDquBOEMAfxWxMKtjROJzNPI2QiRpyBMxkFBJ9EzEUI7TmLvyTBp8BcJWHSwMSz5Z1DCBZDCin0ZaDdjC9btz0JJIYUuMWddPXIY4727KRm2R3/DsSzdVTWRwQnIy69EOvOkZ5YiPaMUGZllSOb3ugNZCHv6d3xxqNESbosvwKD5O7E3lhaqgu5cdZ1qa1NkLgaRAF7cpMdvgkzWebq4Cj/vy8TirclGaiOS+b7wdAUyS1o+LYhkHxLiC1FPRSvJrcAn+xtj9QvBbd29UJhVSZdYj90F8/flYSKVWoFyur+XN1o7OcDbw1F5Bd2ERFo7kgRy4OvqAG3hQHQIcVcbmYKDSWXYSHe0I+Nh0L2dsatxLyC3sh5fjdMt7m/xJcrSmxA9k3j8eSqrNr03Pr21C5abZak8Es6IN7BqfAflNtMVU6+uZ18X2C1uca+L6a3c3sMYgwlqoIQOwR4sy8ZWG5a/mor+E0mm/j+9oM0K53tnjO/sgcLsSpJDFeYNDkDp8/0UqUiLwbTuexiyCGQ/pJKue4iHk1pSAhUO2eFlkkIVFdT25nFUsw+vUL4CGa89Kc4mqQaQzPq0dWXs4qxCCBPPkrTEC+nK8EfItbevM+4K80Y+FXzPv7oYuezAeluTAASvH6FsbBpGcY5KSaRXkazrZKxP9sXbhicm+cXj+SfgL5HCtoQCPLD8CKXExUxKv25gID6fdjGW3dEX3UMMhuUMhc/fhUpj40qgrAE/reiezZnQHTOu7YaZ14ZiZN8AfUF6u2DqZ0eN3MC9X8dQCZxgc3XEynvDsXnmZZg2opM+85zkZ0Z1NnISkiYK4eWMOWtiziIkR+O9vUWyx5MbGA+7y3j4wHE99WOC/uICcQWt0a2X+uGqj2LR/fN42F49xmpbYc3YDoyRKYvSOkyihdt1S2cU0yJPYSzvzH6tnBCCRfQcAqn48kmnEg4PdsM9WzLg5+eilFp7vA8O3N4Ve44VcjHWo5KhgYQNgpNTQ2np6xueYVjgJVTutouilCW7e0sm+pO0FOiR5PMzjgt66kVtGP+yHJ+l/LprOqCeymZ77RhCOYYhH5zEfcPaYXBgo/eg+IPKKxb1pwmcEyaMYhiiQMW+eWO6Gofn8pOYyjHe3M0Lo+nRhL0bo/oi6XBtxWm04Z3hgdh5oggBlFmrt08ob+pf7GeJrB2GPW0+PAlv9sFxaTS2p5ehML0c2mOUxxN9UPtIL+XhfBVbrMKAQsb47diu9H1vahkOTeqquhQxuQs2RBWoTU5/tvP6tiysZfghkLF3IDHN6O+HW/q3Rf9P4lW6CRUWs42O9MaclkVjJsO1lTd0UmRVTkL5PbEU7VfGoS29DBmzAotc99Mp+LDvtndO4PtEfVP174i/9P9TcJ+9HRWyiVJZqxR1VA9f442Oad9E46PfTwnF4+o+/tjy0ACVPvitA9hPQnGlQlcuHqHSTEjI8e4WLiouyr3PDcVlnbxhe3yzcqtvGhCI7+6lZTFQzbTjWWUYGGIscELlNS2KTCL5QXtztP5MODEsqGOfR/b1x9YH9f6YKKAX0nbmNlVmRC8/bI/OV17JoXlXYECHxjaaYySV4MMR7REq8XgzRNNlXcfFEkYLNDFUt7ByKvFlXBFu7OpF/nNQdxy2cYE/1FeXn2xMfRpTpDyE+3q3oWVqhW8TitGXlk65rAZkF/0qkk9icTU8aW0HtdM38uTEQDhhaPvWeCwiW1nxy6jIaaW1WBNfrKzvZMPaf0klGtGhNYKECIlCEsJPKaXqvRnfy2apuP2yeRbm29i+QJbJF6xDYukAejmpJbX4La2MXlAbrE8qVRZcFpLsi8iG5XDDdf+DXscmhitimW8x5CKQjb3VJ4sRyLruNOLyLBKCkEArY17F6+nRxkWRq+xXmNhOGYo1k3b25VRy+jUEkdhH0pLbQ9KXHi1QYdbDJCiRr2Az++3B35dLSEGsOFGg5NudbQmEKH9KLlUk4EzjMoFejxC5YD+9H9ngFIjmSE9vo6f1LWUny1BkILKQvZUQ9u/viPOSwu6kQlyxaK8c0tMitsfXd/Y13jSFA5XwjMRgrK7qjVGMM1s1kIKLuzOqljQlhW+jcnDr+/Q+6DP+PvsyDO/m20g+rOOhYR0x/coQhLVrOtEmFCkQfvQUTkv8zlFc0t0HBx6/VKWfixRm/RiPV3+mZ+DiCO3t0Wr/QrRrTHgANv27v5HrbJyLFCxY+KdAp79zYHtikWzXKnfqP1foMWRLeGQo39Gio6oOsbmN8bWguQdfRqs8/Tu677R6Euf1a6+HIE+PZHhQKnsANry/PRU95+2E7cktuOOL48iXPYbmIAlNHRCEu6VfpOmDtPjzNulHfufCq+KhsFOTLtHjwSev6qjK/xaZg1L234KF/884LymUi3+qlFqDF2P9P4Ov7DeIz8GPuGsNoPJV0v2zPbEZtsf0j+ejvyFbNivLanDP8I5o46a7WXPHdMEH0y6GnzctsSintEvFX72bcTbL7UxqPLNXYDOFFXX4ZHJvePu6Km/mxbWxiMutgDvd9Zaw5nC2CoOk7MpJjE+J12/ooRMf0xZuOXvT8q9AdurVHoKBP+jWyseEhA657KtALiRl0X2W8OE0PzlMlz2DKpKq/JZ88smmOy2QtGpzF85Aelktu9soZzlbl7SWUMF3ci5vQtox6zaRbfShOcSVjjpdpcfZhGy0yWUf+7HKJS3JYw81Frs2ZE1kNOufjF/CGHukM/T5s6PuSkM+8pHf9pC6TLnJeznVaQkid/NUQYdGWXAujLLyXubHbEvSZI6aQy5zReZVqdDWhIRO2TReUsZe3vksLzIyIX0tr61XIZnMg9lvaVvSROZmmr2c1ZphmvRH3kn/TEg5mQOZ6+Y4wbWZfwHG7rzhw5dUoimyyehow/wbe2DeGH0jpzn6v/4HIlOKlcXNXjwC7Tyd9fCB4YfaEOSAFNxILLIJyGYXjA/Fc6Nb2PklUgoq8TUt9zs705CZV6HIxd/HFbkvDFPvVfjABTplSAesntob2fQwgmSfgMTlzVhaFmU1BT6yj1+T8KHzwl1IzaOysr5gxpCSz5neSpbEiUJmZ85Ae2eMkbspWgofJG6VW4DVZRQ6y49hfLzpxk7wXB6jYtKSf4epDbU8Od6jbAZ28cC8S/1x/RdJ6gRCER8X8K392+LSdq6YuTlT9U3kIy+1p/qqDTcJs9UmGzHxlzSsPXAa6U/2QTDHKpAbeodOlaFuRt+zzsvlSG/Wr+k4yr709XPFRycKMW1zhtrINCEbdV0CXJF0Z3cjBbhvSwY+Zjtqvtj3t8d1wJWMvQe8eRw/3H8RJnT1xJ2sZ9WhfH0c/Ky4NkQdi352sgh3f5uCbfeEYkQHD2xMKcG1nydCm9sYnjm8G61OQXJZlwk5nt39SE8MMeJ9e6hxbKF8pD+UdXhHd0Td1k29c3svBlWivCI2zunALp44aGw62sO2OAq9QjxwYopeLo+yD3jjmNrsVGWphJ/d3BnJXA/zd9CAiCzZVrC/C5Lu6q72Fhbsz8Pc7Vn6mFnm6SvaYfHQdupEZejn8cr7lTLdg9wQd0d39FqdiP4s/8VY3dMO5Hq4s6e32vDttyJW3ZNQbbP/UQ+FYf7+0/iec6QmnfVc0tkDBzgW26IoPa+cish9FBvNwoxwvHEkHzM4Dwqcp8eGBOCtYUGKiGUjtFCOwPl7bK82+PWGTnq+c4AtnBsTevnpk0BXf/5Gcc2l901xOK0EkXKMyGwh7dwVITSAxOVEK/zrk5di26zB8JKNLhkQMVPcdjuc4YBSxYMgOvu6qXAiY96VGMtYX5rNy2kaltgj0NMFqx+4WB1lFpMMqoUxZdLsEJlZitS0Un2iOflyPJpPryKL38p7kPxMFyL8q3idEyJHcnIUl8zFPH+wv0qXo8b27o7YzYWSRwaX90VP9FFHknK5p2RWX/z7Yl+083JCJRV/zdhgFLOedm1doM3sSzIIR/1MXWnlqKuOFlSuD6dysa5lm+IVmRtyYoGFEJxIMm9GNl7JNaFOYDgHchVX4CYkbUccckmpFetLzq1CjHE3YdnRAny8Lw/bpnZTfX9tTDCuo6KpmaMi+5PcJc+qA3nYcUcotKfD8crV7THtm2SkFNeoExg5bRj5jX5CoS5y2bX5fUKJItQ85t1rHE8qcKzn+vsSV09H1JIMYx+4iIpbyzHpp0aiAK+NbK/LbnY/7J14trF5W2TDOYnOLEem4bXI0hbjkvtwT73ssxerjU85ZQgigUpa8RO9kVFYg7ciC7CTZeduTMOamzsquXxxU0csIXn+klKm7j+IIqeTfLNI4PGnq7H8WAHauDQ9BZOjYdncVUn8aCRy1fbz/RHu56YuoF1JwpW0hAcvwsG4YrWZW8L1cOxukjbXQhS/S6f3xp7MCsz4IRUfjg9R/fmR5PE2CWst5bs6tkidzkh6JvPOHqjfKzkfzksKnrS894+g8oqrQqX1nLMDW+Iab7q9vycdA1/Zp1s9LtjVU3obb0xoynKNDWuLEaG+iHhkoK6AHHjY4r1GHh3jVhxB56e345mNCerGpIkU8yKQsXP+Z5gyMAgTZX9BuUpnL6z5v1IpqBAOnJT5N3TH8zf1wHP0fuR70c1hcJU9Dn5e+O2vhxDXksVFNs7LoqmQBRhIa2tCXEx1aUZI/Z0TeGJXNgb66+/lFMGDH1kYrlQY8Sqkxzn0OEavT8Vl3yRhjnGZqYoL/raL22IJLVToqkTcJ8RD+ZkjnLU7Bzf1boO1XBiz7K4Bm5Bd/PZc4CFezurvF3xIAPbieTwiG9+P74CbqAzPGG0u42K+JtwXI2hVBTMG+Kkz/hqOSSBn+ItpuacO8mu4+PQUF51X+9Z4O6qAvMEGnBwwnPLp+lm8Ikll+QzIBa1lVwVh+iD/Jrc2pV/n4AT1ThSsh4+L6vOxVN1QeFGW77Ldq+nNyd8lRNG1bw65qyCXtiZwnNPt26SXcONPaZRNCq76TicxuVBl9kOFCJyDIBLKEnpFA2nlJ3XXT0xu79EGg8J88Mrh0zoRslywhyMCmVcg0pJ6FAEYMJVOpfEfaVP+BuOGn/Vr2JJsZpdjaKnEnfKWNdNBPEP2Rb49KN+X2W4oyXpaHx+VX4j76vC2WHQwD9fJ2mRjLu9Fq7+tGdJeP7U6H85LCoIVt/ZC71A2SvemjGHA6Nf+gG3aRtju34iHPz+uj4CE8PzknhjWTe+cQC0f/mN/XblfsCceGke3joJOpYfxHAlA8EdqMTZHpCmL9vKPCXB64Bd1KmDjdywtvLT9sGxmNkFjvSa+uSsc/gFcpHb3JQTihWzYJxeUNNw1KAjzxnbFi+O6YcE13dT3nFGd8ZR4LvQw4hILEZPd8h385ujl64rq2eF4gYq6ii6zyxvmDUpad0OBtDn96M4FYmtaOXxfa7xhKXKxH4H8duLky808uULbl16D4AwXxqQeXri3r4/yTD+6OlhMY8NC+zKqkDHjGfxIa6IVV2NHelOPSsQvynyK1mVnYimepELqC1g/MqyixV3LdLk+vF7cVkKO7xruQLQA6avcZCyhd2MPsbBuTjZ1/UNk+bu44vQ+Jv1K95YWUyBW+tSpcuzOrkQCPYVDtGr2ewT2V6CbQ6dOHeW17IWRV4hPLoXJUeDVIe5oKyRkB3Hty2k1NyRznAwZ1sXo+1OOorGUzeBAN3VkO4ykptLZ1cz8atiWRDG8OIHrGRLdEdZGzWlps7i9kgbMPO5EK5L1gijY5h9GbxoIOX6WfRjqcQPM0iqJ/1zJNmW+hxj3QsQzjEgug+3lKPRdfhKPkTzNI1kxNAJzbblw/Opqux3K6mRt2BDQ2gl19BKeJ/F+x/l1eTdGXwzngTGS8+P4rMvwDC2qUjaZB7EE8mEjDmSwX54arJTLHnLKIO58jcR6dnjvljA4eTDEYP9e+ioah9JLMLiTN04tHYtLhHxkwCJkcx+C5ScPD8EyWvMGSJ200GojtBmi2VfVz/Ia9kF//8QPcSp2l8sxz9pfgrKDHIGqa3VcoP9e2/LV7ebYmVGOyZsy1N8DfDKqPVCs91nWqizuMvZv1PepeJAKve4a1q82kfQ8Mkz7xSIbbML+0/v5qs9Q8/IQZZxJD2IlySByclddgVTeVsqFh7MNobTi4lF05eJpYnkJacdUup9v7YJYkpOpd3P25KIjFULWSic5V6cySQiy8LIA7KbiLD2ar8jhHsascmFI7v4LZGPzpcEB+OFwvuqDbFLKX3HWkwBm06uQ9yJHwUHG77EkARPSP38qgiyfAIYizrT64u2YOJZfhRP8CGGJ/OwhdwDkuvGPVO4bNqTSA9XvPsjmoVy2epRye5JelamjJqbvyEZXkoUQiXgZIoC3GIa1FnJkWfnjqP/QM7qfHpcIQ0i0vYRyT/dD+2C3ho3euVSwuIQyPMtQTsYoFvg43fuFl/sr5Qe7m/JYL1Q+dzGOTwlVZUTpVx0vVFesN50qRTrlP4YeWJXyAjRMY5vS9iTjopnM1QgJH2hMOMlnk6RMqIEXOAeZDB3lLz+lP7K/8MeJIrw6tB3lV8m1mY7n2OePZW0y7CiSPp4PstF4oYhILNA+2JOufbI/QzuRVWqkno2EvHItKqNEO9ZCnpKqWi0yvUQ7wk9cbrmRqqO6rl7bGp+vrTqYqW04lqNV1dYZbxoRlVGqyp8qrDRSmiK7pErVnXS6Qj0fyyxVfZHPuXAyp0w7yjz7U4uNlEaM+C5Ziy+sNp507Mgo1zp+Eqth0RENrx7VFuzPVemu70VrXstjtHzSeNiqeA0vR2pYEqU9uC1TvRc8tStb6/JpnPGkqbJ485iG1/l57aiGV6K0ero4LsuitfeP5hu5NK1AfMqFR7RCyrD9ylht0UG9TUFMQZUql1TU2M/XD+dp7T46aTxp2sPbMzWfD2O0crldxX7FFVYZbzTt1UN5msO7Jxp+4w32ZcERrTXHsym1VKOianh6v7Y+QZfjogNGn5nH8/1obVdGmUpffbJIjcPEQhmb8SzfEel6PsHnMYUa3jmufquxv3Vc/37piLblVOPaeefI6Ub5vH1cu2tzuvFG04I+4hyY5Th+kbmJfJHXS5FavJ1MXmJ/PDk/tWcoAykj45TvRZHa0qh81d/Qz/U60kprNCyOUv0UfBVbqNkoIxlzK35/GVuk0vdmUTaso6zm7PV6yZpEVbesgQe2Zai0PVnlertm2+xjBOU3cWOads36FJVHnkUO+5hXkCOsxnaz+W3iu4RirhG9PyKXT07o/ZSyoZ/FqfIiE3Ntng/nPX2w0IhzXV4SSyJ/iSh7AwLzL/l8jKNR0T8hfNlfaYSmSN/cMGwOmRqpT8KM5nnEq5C6pF7ZuLKHeaRn31bzOuRZHsUANr8Krtcpv/R0OeZSFpUQNz2/sl7tEdiXEytlhiQmJK/9pqHeB92BaLlNW8OYBfa/GyBDa5YkaJ6Xj2p8AnG1pbnmMpQ5k78daVLWrn57mUke8VJk/8dEi2M25qUlyEmXjNFeJi2Ol32QwNJMlzpJM6qv5nNLbbTUH4GMU9/YPLtMS7BI4QIgf+n4xZgO6NnsCrAFC/8kWKRwAZALMXK89FcZ14KFvyMsUrBgwUITnB2AWLBg4f81LFKwYMFCE1ikYMGChSawSMGCBQtNYJGCBQsW7AD8Dz+4FqQCljjUAAAAAElFTkSuQmCC";
                string base64 = Utils.imagenLogoBase64();
                byte[] imageBytes = Convert.FromBase64String(base64);
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(imageBytes);
                if (logo.Width > 180)
                {
                    var porc = 180 * 100 / logo.Width;
                    //logo.Width = logo.Width * porc;
                    logo.ScalePercent(porc);
                }
                logo.ScaleAbsoluteWidth(150);
                cell = new PdfPCell(logo);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 7;
                cell.Border = 0;
                table.AddCell(cell);

                string[] mes = { "", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
                cell = new PdfPCell(new Phrase("Buenos Aires, " + libranzaFecha.Day.ToString() + " de " + mes[libranzaFecha.Month] + " de " + libranzaFecha.Year.ToString(), normalFont));
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.Colspan = 7;
                cell.Border = 0;
                table.AddCell(cell);

                // LINEA  
                page_line++;
                page_num++;
                cell = new PdfPCell(new Phrase("Página: " + Convert.ToString(page_num), normalFont));
                cell.Colspan = 9;
                cell.Border = 0;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Libranza ORSNA " + libranzaLetra + " N°: " + libranzaNro, normalFont));
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.Colspan = 5;
                table.AddCell(cell);

                // LINEA  
                page_line++;
                cell = new PdfPCell(new Phrase(" ", normalFont));
                cell.Colspan = 14;
                cell.Border = 0;
                table.AddCell(cell);
            }
            else
            {
                page_line++;
            }
        }

    }
}