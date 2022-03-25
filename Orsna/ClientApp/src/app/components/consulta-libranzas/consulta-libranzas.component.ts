import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Request } from '../../services/request';
import { HttpService } from '../../services/http-service.service';

import { FormBuilder, Validators } from '@angular/forms';
import { DataService } from '../../services/data.service';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { forEach } from '@angular/router/src/utils/collection';
import swal from 'sweetalert2';
import { Constants } from '../../services/constants';
import createNumberMask from 'text-mask-addons/dist/createNumberMask';

@Component({
  selector: 'app-consulta-libranzas',
  templateUrl: './consulta-libranzas.component.html',
  styleUrls: ['./consulta-libranzas.component.css']
})
export class ConsultaLibranzasComponent implements OnInit {
  form;
  page: number = 1;
  FilterAeropuerto: string = "";
  FilterEstado: string = "";
  FilterBeneficiario: string = "";
  FilterProyecto: string = '';
  FilterTipoLibranza: string = '';
  FilterFactura: string = "";
  FilterDesde: string = "";
  FilterHasta: string = "";
  Order: string = "desc";
  ColumnOrder: string = "Fecha";
  CountPages: number = 0;
  CountElements: number = 0;
  CountFilterElements: number = 0;
  message: string = "";
  closeResult: string;
  Aeropuertos: any;
  Libranzas: any;
  LibranzaTipos: any;
  AeropuertosSelect: any;
  Beneficiarios: any;
  Estados: any;
  BeneficiariosSelect: any;
  ProyectosSelect: any;
  IdProyectosSelect: any;
  dropdownSettings: any;
  dropdownSettingsSingleProyecto: any;
  dropdownSettingsSingleProyectoId: any;
  dropdownSettingsBeneficiario: any;
  IdsAeroSelect: string = "";
  IdsBenSelect: string = "";
  IdsProyectosIdsSelect: string = "";
  Proyectos: any;
  ProyectoToDelete: string = "";
  misBen: Array<string> = [];
  misFac: Array<string> = [];
  DetalleLibranza: any = null;
  loading: boolean = false;
  motivoCambio: string = "";
  fechaPago: string = "";
  montoLibranza: string = "";
  retencion: string = "";
  retencionObservaciones: string = "";
  MonedaId: number = 1;
  TasaDeCambio: number = 1.00;
  EsNacional: boolean = null;
  nuevoEstadoLibranza: string = "";
  idLibranzaActual: number;
  SiguientesEstado: any = [];
  puedeEditar: boolean = false;
  puedeEditarPagada: boolean = false;
  monedas: any = [];

  NombreModalAdjuntos: string = '';
  AdjuntosLibranzas: any = [];

  currencyMask = createNumberMask({
    prefix: '',
    suffix: '',
    includeThousandsSeparator: true,
    thousandsSeparatorSymbol: '.',
    allowDecimal: true,
    decimalSymbol: ',',
    decimalLimit: 2,
    integerLimit: null,
    requireDecimal: false,
    allowNegative: false,
    allowLeadingZeroes: false
  });

  currencyMask10 = createNumberMask({
    prefix: '',
    suffix: '',
    includeThousandsSeparator: true,
    thousandsSeparatorSymbol: '.',
    allowDecimal: true,
    decimalSymbol: ',',
    decimalLimit: 10,
    integerLimit: null,
    requireDecimal: false,
    allowNegative: false,
    allowLeadingZeroes: false
  });

  resetFields() {
    this.FilterAeropuerto = "";
    this.FilterEstado = "";
    this.FilterBeneficiario = "";
    this.FilterProyecto = '';
    this.FilterTipoLibranza = '';
    this.FilterFactura = "";
    this.FilterDesde = "";
    this.FilterHasta = "";
    this.Order = "desc";
    this.ColumnOrder = "Fecha";
    this.CountPages = 0;
    this.CountElements = 0;
    this.CountFilterElements = 0;
    this.message = "";
    this.IdsAeroSelect = "";
    this.IdsBenSelect = "";
    this.IdsProyectosIdsSelect = "";
    this.AeropuertosSelect = [];
    this.BeneficiariosSelect = [];
    this.ProyectosSelect = [];
    this.IdProyectosSelect = [];
  }

  constructor(
    private request: Request
    , private dataHttpService: HttpService,
    private fb: FormBuilder
    , private routes: Router
    , private data: DataService
    , private modalService: NgbModal) { }

  setPropertiesDropdownSettingsSingleProyecto() {
    this.dropdownSettingsSingleProyecto = {
      singleSelection: true,
      idField: 'id',
      textField: 'nombre',
      itemsShowLimit: 3,
      allowSearchFilter: true,
      enableCheckAll: false,
      searchPlaceholderText: 'Buscar',
      closeDropDownOnSelection: true
    };
  }

  ngOnInit() {
    this.resetFields();
    this.GetAll(this.Order, this.ColumnOrder, 1);

    this.request.get(Constants.API_PROYECTO + Constants.API_PROYECTO_GETALLREDUCIDO).subscribe(resp => {
      this.Proyectos = resp.data;
    });
    this.request.get(Constants.API_AEROPUERTO).subscribe(resp => {
      this.Aeropuertos = resp.data;
    });
    this.request.get(Constants.API_PROVEEDOR).subscribe(resp => {
      this.Beneficiarios = resp.data;
    });

    this.request.get(Constants.API_SEGURIDAD + Constants.API_GET_PERMISOS + "?modulo=" + 'Libranzas de Pago').subscribe(resp => {
      this.puedeEditar = resp.editar;
    });

    this.request.get(Constants.API_SEGURIDAD + Constants.API_GET_PERMISOS + "?modulo=" + 'Editar Libranzas Pagadas').subscribe(resp => {
      this.puedeEditarPagada = resp.editar;
    });

    this.request.get(Constants.API_LIBRANZA + Constants.API_GET_ESTADOS).subscribe(resp => {
      this.Estados = resp;
    });
    this.request.get(Constants.API_LIBRANZA + Constants.API_GET_LIBRANZA_TIPO).subscribe(resp => {
      this.LibranzaTipos = resp;
    });

    this.dropdownSettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'nombre',
      itemsShowLimit: 3,
      allowSearchFilter: true,
      enableCheckAll: false,
      searchPlaceholderText: 'Buscar'
    };
    this.dropdownSettingsSingleProyecto = {
      singleSelection: true,
      idField: 'id',
      textField: 'nombre',
      itemsShowLimit: 3,
      allowSearchFilter: true,
      enableCheckAll: false,
      searchPlaceholderText: 'Buscar',
      closeDropDownOnSelection: true
    };
    this.dropdownSettingsSingleProyectoId = {
      idField: 'id',
      textField: 'idProyecto',
      itemsShowLimit: 3,
      allowSearchFilter: true,
      enableCheckAll: false,
      searchPlaceholderText: 'Buscar',
      closeDropDownOnSelection: true
    };
    this.dropdownSettingsBeneficiario = {
      singleSelection: false,
      idField: 'id',
      textField: 'razonSocial',
      itemsShowLimit: 3,
      allowSearchFilter: true,
      enableCheckAll: false,
      searchPlaceholderText: 'Buscar'
    };
  }
  NewLibranza() {
    this.data.changeMessage("0");
    this.data.create("0");
    this.routes.navigate(['../dashboard/libranza']);
  }
  GetCountFilterElements(order: string, columnOrder: string) {
    if (columnOrder != undefined && columnOrder != null && columnOrder != '' && columnOrder != "") {
      this.ColumnOrder = columnOrder;
    }
    if (order != undefined && order != null && order != '' && order != "") {
      this.Order = order;
    }
    this.request.get(Constants.API_LIBRANZA + Constants.API_GET_COUNT_FILTER_ELEMENTS + "?page=" + this.page + "&FilterAeropuerto=" + this.IdsAeroSelect + "&FilterEstado=" +
      this.FilterEstado + "&FilterBeneficiario=" + this.IdsBenSelect + "&FilterProyecto=" + this.FilterProyecto
      + "&FilterTipoLibranza=" + this.FilterTipoLibranza + "&FilterFactura=" + this.FilterFactura
      + "&FilterDesde=" + this.FilterDesde + "&FilterHasta=" + this.FilterHasta + "&FilterIdsProyecto=" + this.IdsProyectosIdsSelect
      + "&Order=" + this.Order + "&ColumnOrder=" + this.ColumnOrder).subscribe(resp => {
        this.CountFilterElements = resp;
      });
  }
  //transformAmount(element) {
  //  (<any>$)(element.target).number(true, 2, ',', '.');
  //}
  refresh(): void {
    this.ngOnInit();
  }

  GetCountPages(order: string, columnOrder: string) {
    if (columnOrder != undefined && columnOrder != null && columnOrder != '' && columnOrder != "") {
      this.ColumnOrder = columnOrder;
    }
    if (order != undefined && order != null && order != '' && order != "") {
      this.Order = order;
    }
    this.request.get(Constants.API_LIBRANZA + Constants.API_GET_COUNT_PAGES + "?page=" + this.page + "&FilterAeropuerto=" + this.IdsAeroSelect + "&FilterEstado=" +
      this.FilterEstado + "&FilterBeneficiario=" + this.IdsBenSelect + "&FilterProyecto=" + this.FilterProyecto
      + "&FilterTipoLibranza=" + this.FilterTipoLibranza + "&FilterFactura=" + this.FilterFactura
      + "&FilterDesde=" + this.FilterDesde + "&FilterHasta=" + this.FilterHasta + "&FilterIdsProyecto=" + this.IdsProyectosIdsSelect
      + "&Order=" + this.Order + "&ColumnOrder=" + this.ColumnOrder).subscribe(resp => {
        this.CountPages = resp;
      });
  }
  GetAllWithoutFilters() {
    this.request.get(Constants.API_LIBRANZA + Constants.API_GET_COUNT_FILTER_ELEMENTS + "?page=0").subscribe(resp => {
      this.CountElements = resp;
    });
  }
  GetAll(order: string, columnOrder: string, event: number) {
    this.GetAllWithoutFilters();
    this.GetCountFilterElements(this.Order, this.ColumnOrder);
    this.loading = true;
    this.page = event;
    if (columnOrder != undefined && columnOrder != null && columnOrder != '' && columnOrder != "") {
      this.ColumnOrder = columnOrder;
    }
    if (order != undefined && order != null && order != '' && order != "") {
      this.Order = order;
    }
    this.request.get(Constants.API_LIBRANZA + Constants.API_GET_ALL + "?page=" + this.page + "&FilterAeropuerto=" + this.IdsAeroSelect + "&FilterEstado=" +
      this.FilterEstado + "&FilterBeneficiario=" + this.IdsBenSelect + "&FilterProyecto=" + this.FilterProyecto
      + "&FilterTipoLibranza=" + this.FilterTipoLibranza + "&FilterFactura=" + this.FilterFactura
      + "&FilterDesde=" + this.FilterDesde + "&FilterHasta=" + this.FilterHasta + "&FilterIdsProyecto=" + this.IdsProyectosIdsSelect
      + "&Order=" + this.Order + "&ColumnOrder=" + this.ColumnOrder).subscribe(resp => {
        this.misBen = [];
        this.misFac = [];
        this.Libranzas = resp.data;
        if (this.Libranzas) {
          for (let b of this.Libranzas) {
            this.misBen.push(b.beneficiario);
          }
          for (let f of this.Libranzas) {
            this.misFac.push(f.factura);
          }
        }
        this.loading = false;
      });
  }

  EditLibranza(id: number) {
    this.data.changeMessage(id + "");
    this.routes.navigate(['../dashboard/libranza']);
  }
  ///////////////////////////////////// select dropdow //////////////////////////////////
  onItemSelectAero(item: any) {
    this.IdsAeroSelect += "," + item.id;
  }
  onItemDeSelectAero(item: any) {
    let array = this.IdsAeroSelect.split(',');
    this.IdsAeroSelect = "";
    for (var i = 0; i < array.length; i++) {
      if (!(array[i] == item.id) && array[i] != "") {
        this.IdsAeroSelect = this.IdsAeroSelect + "," + array[i];
      }
    }
  }
  onItemSelectProyecto(item: any) {
    this.FilterProyecto = item.id;
  }
  onItemDeSelectProyecto(item: any) {
    this.FilterProyecto = '';
  }
  onItemSelectBen(item: any) {
    this.IdsBenSelect += "," + item.id;
  }
  onItemDeSelectBen(item: any) {
    let array = this.IdsBenSelect.split(',');
    this.IdsBenSelect = "";
    for (var i = 0; i < array.length; i++) {
      if (!(array[i] == item.id) && array[i] != "") {
        this.IdsBenSelect = this.IdsBenSelect + "," + array[i];
      }
    }
  }

  onItemSelectProyetoId(item: any) {
    this.IdsProyectosIdsSelect += "," + item.id;
  }
  onItemDeSelectProyetoId(item: any) {
    let array = this.IdsProyectosIdsSelect.split(',');
    this.IdsProyectosIdsSelect = "";
    for (var i = 0; i < array.length; i++) {
      if (!(array[i] == item.id) && array[i] != "") {
        this.IdsProyectosIdsSelect = this.IdsProyectosIdsSelect + "," + array[i];
      }
    }
    console.log(this.IdsProyectosIdsSelect);
  }
  /////////////////////////////////MODAL LISTA DE ANEXOS//////////////////////////////////////////////////////////////////
  getAdjuntosLibranza(id: number, idintificadorLibranza: string) {
    this.NombreModalAdjuntos = idintificadorLibranza;
    this.AdjuntosLibranzas = [];
    this.request.get(Constants.API_LIBRANZA_ADJUNTOS + Constants.API_GET_ADJUNTOS_LIBRANZA + "?id=" + id).subscribe(resp => {
      this.AdjuntosLibranzas = resp;
    });
  }
  getFileById(idAdjunto: string) {
    window.open(Constants.URL_BASE_WEB_API + Constants.API_FILE + Constants.API_GET_FILE + '?id=' + idAdjunto);
  }
  ///////////////////////////////////FIN MODAL ANEXOS////////////////////////////////////////////////////////////////////

  GetLibranzaById(id: number) {
    this.motivoCambio = "";
    this.fechaPago = "";
    this.retencion = "";
    this.retencionObservaciones = "";
    this.montoLibranza = "";
    this.nuevoEstadoLibranza = "";

    this.idLibranzaActual = id;
    this.DetalleLibranza = null;
    this.request.get(Constants.API_LIBRANZA + Constants.API_GET_LIBRANZA_BY_ID + "?Id=" + id).subscribe(lib => {
      this.DetalleLibranza = lib;
      this.montoLibranza = this.DetalleLibranza.montoNeto;
      if (lib.libranzasEstado.nombre == 'Pagada') {
        this.nuevoEstadoLibranza = lib.libranzasEstado.id
        this.retencion = (<any>$).number(lib.retencion, 2, ',', '.');
        this.retencionObservaciones = lib.retencionObservaciones;
      }
      this.MonedaId = lib.monedaId;
      this.TasaDeCambio = (<any>$).number(lib.tasaDeCambio, 10, ',', '.');
      this.EsNacional = (lib.beneficiario[0].nacionalExtranjero == "true")?true:false;
      this.GetSiguientesEstado();
      this.request.get(Constants.API_LIBRANZA + Constants.API_GET_MONEDAS).subscribe(resp => {
        this.monedas = resp.data;
      });
    });
  }
  DownloadPDF(Id: string, NroLibranza: string, e) {
    var fileURL = Constants.URL_BASE_WEB_API + Constants.API_LIBRANZA + Constants.API_DOWNLOAD_PDF+  '?Id=' + Id;
    window.open(fileURL);
  }

  viewLibranza(Id: number) {
    window.open(Constants.URL_BASE + "#/libranzaview/" + Id);
  }
  GetXLSFilter() {
    var url = Constants.API_LIBRANZA + Constants.API_GETXLSFILTER + "?page=" + this.page + "&FilterAeropuerto=" + this.IdsAeroSelect + "&FilterEstado=" +
      this.FilterEstado + "&FilterBeneficiario=" + this.IdsBenSelect + "&FilterProyecto=" + this.FilterProyecto
      + "&FilterTipoLibranza=" + this.FilterTipoLibranza + "&FilterFactura=" + this.FilterFactura
      + "&FilterDesde=" + this.FilterDesde + "&FilterHasta=" + this.FilterHasta + "&FilterIdsProyecto=" + this.IdsProyectosIdsSelect
      + "&Order=" + this.Order + "&ColumnOrder=" + this.ColumnOrder;

    var year = new Date().getFullYear();
    var filename = "Libro Registro de Libranzas-" + year + ".xlsx";
    this.request.downloadFile(url, filename);
  }

  /////////////////////////////////
  ////////////////////////////////MODAL CAMBIAR ESTADO ////////////////////////////////
  parseCurrencyNumber(strg: string): number {
    strg = strg || "";
    var decimal = '.';
    strg = strg.replace(/[^0-9$.,]/g, '');
    if (strg.indexOf(',') > strg.indexOf('.')) decimal = ',';
    if ((strg.match(new RegExp("\\" + decimal, "g")) || []).length > 1) decimal = "";
    if (decimal !== "" && (strg.length - strg.indexOf(decimal) - 1 == 3) && strg.indexOf("0" + decimal) !== 0) decimal = "";
    strg = strg.replace(new RegExp("[^0-9$" + decimal + "]", "g"), "");
    strg = strg.replace(',', '.');
    return parseFloat(strg);
  }

  CambiarEstado() {
    var ret = this.parseCurrencyNumber(this.retencion);

    this.request.get(Constants.API_LIBRANZA + Constants.API_GET_MONTO_A_PAGAR_BY_IDLIBRANZA + "?idLibranza=" + this.idLibranzaActual + "&tasaDeCambio=" + this.TasaDeCambio).subscribe(resp => {
      if (this.nuevoEstadoLibranza == Constants.LIBRANZA_ESTADO_PAGADA && ret > resp) {//Pagada
        swal(
          'Cambio de Estado',
          'La retención debe ser menor al monto a pagar de la libranza.',
          'error'
        );
        return;
      }

      let payload = {
        "Id": 0,
        "IdLibranza": this.idLibranzaActual,
        "IdUsuario": Number(JSON.parse(sessionStorage.getItem('currentUser')).id),
        "Observaciones": this.motivoCambio,
        "IdEstadoAnterior": Number(this.nuevoEstadoLibranza),
        "IdNuevoEstado": Number(this.nuevoEstadoLibranza),
        "FechaPago": this.fechaPago,
        "Retencion": this.parseCurrencyNumber(this.retencion),
        "RetencionObservaciones": this.retencionObservaciones,
        "MonedaActualId": this.MonedaId,
        "TasaDeCambioActual": this.parseCurrencyNumber(this.TasaDeCambio.toString()),
      };

    this.request.post(Constants.API_LIBRANZA + Constants.API_CAMBIAR_ESTADO, payload).subscribe(resp => {
        this.motivoCambio = "";
        this.fechaPago = "";
        this.retencion = "";
        this.retencionObservaciones = "";
        this.montoLibranza = "";
        this.nuevoEstadoLibranza = "";
        this.TasaDeCambio = 1;
        this.MonedaId = 1;
        this.GetAll(this.Order, this.ColumnOrder, 1);
        swal(
          'Cambiar Estado',
          'Se cambio el estado con éxito.',
          'success'
        );
        (<any>$)("#workflow").modal('hide');
      });
    });
  }
  GetSiguientesEstado() {
    this.SiguientesEstado = [];
    this.request.get(Constants.API_LIBRANZA + Constants.API_GET_SIGUIENTE_ESTADO + "?IdLibranza=" + this.idLibranzaActual).subscribe(resp => {
      this.SiguientesEstado = resp;
    });
  }
  ////////////////////FIN MODAL///////////////////////////////////////////////////////
  onChangeEstadoLibranza(event): void {  // event will give you full breif of action
    const newVal = event.target.value;
    if (newVal == Constants.LIBRANZA_ESTADO_PAGADA) //Pagada
      swal(
        'Cambio de Estado',
        'Luego de colocar en estado pagada la libranza, no podrá revertir, cambiar estado o editar la misma nuevamente.',
        'warning'
      );
  }
}
