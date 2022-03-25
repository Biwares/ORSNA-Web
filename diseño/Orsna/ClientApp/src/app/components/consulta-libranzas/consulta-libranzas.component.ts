import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Request } from '../../services/request';

import { FormBuilder, Validators } from '@angular/forms';
import { DataService } from '../../services/data.service';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { forEach } from '@angular/router/src/utils/collection';
import swal from 'sweetalert2';
import { Constants } from '../../services/constants';

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
  FilterProyecto: string = "";
  FilterTipoLibranza: string = '';
  FilterFactura: string = "";
  FilterDesde: string = "";
  FilterHasta: string = "";
  Order: string = "desc";
  ColumnOrder: string = "Fecha";
  CountPages: number = 0;
  message: string = "";
  closeResult: string;
  Aeropuertos: any;
  Libranzas: any;
  LibranzaTipos: any;
  AeropuertosSelect: any;
  Beneficiarios: any;
  Estados: any;
  BeneficiariosSelect: any;
  dropdownSettings: any;
  dropdownSettingsBeneficiario: any;
  IdsAeroSelect: string = "";
  IdsBenSelect: string = "";
  Proyectos: any;
  ProyectoToDelete: string = "";
  misBen: Array<string> = [];
  misFac: Array<string> = [];
  DetalleLibranza: any = null;
  loading: boolean = false;
  constructor(
    private request: Request
    , private fb: FormBuilder
    , private routes: Router
    , private data: DataService
    , private modalService: NgbModal) { }

  ngOnInit() {
    this.GetAll(this.Order, this.ColumnOrder, 1);

    this.request.get("/proyecto").subscribe(resp => {
      this.Proyectos = resp.data;
    });
    this.request.get("/Aeropuerto").subscribe(resp => {
      this.Aeropuertos = resp.data;
    });
    this.request.get("/Proveedor").subscribe(resp => {
      this.Beneficiarios = resp.data;
    });

    this.request.get("/libranza/GetEstados").subscribe(resp => {
      this.Estados = resp;
    });
    this.request.get("/libranza/GetLibranzaTipo").subscribe(resp => {
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
    this.routes.navigate(['../dashboard/libranza']);
  }
  GetCountPages(order: string, columnOrder: string) {
    if (columnOrder != undefined && columnOrder != null)
      this.ColumnOrder = columnOrder;
    if (order != undefined && order != null)
      this.Order = order;
    this.request.get("/libranza/GetCountPages?page=" + this.page + "&FilterAeropuerto=" + this.IdsAeroSelect + "&FilterEstado=" +
      this.FilterEstado + "&FilterBeneficiario=" + this.IdsBenSelect + "&FilterProyecto=" + this.FilterProyecto
      + "&FilterTipoLibranza=" + this.FilterTipoLibranza + "&FilterFactura=" + this.FilterFactura
      + "&FilterDesde=" + this.FilterDesde + "&FilterHasta=" + this.FilterHasta
      + "&Order=" + this.Order + "&ColumnOrder=" + this.ColumnOrder).subscribe(resp => {
        this.CountPages = resp * 10;
      });
  }
  GetAll(order: string, columnOrder: string, event: number) {
    this.loading = true;
    this.page = event;
    if (columnOrder != undefined || columnOrder != null || columnOrder != '')
      this.ColumnOrder = columnOrder;
    if (order != undefined || order != null || order != '')
      this.Order = order;
    this.request.get("/libranza/getall?page=" + this.page + "&FilterAeropuerto=" + this.IdsAeroSelect + "&FilterEstado=" +
      this.FilterEstado + "&FilterBeneficiario=" + this.IdsBenSelect + "&FilterProyecto=" + this.FilterProyecto
      + "&FilterTipoLibranza=" + this.FilterTipoLibranza + "&FilterFactura=" + this.FilterFactura
      + "&FilterDesde=" + this.FilterDesde + "&FilterHasta=" + this.FilterHasta
      + "&Order=" + this.Order + "&ColumnOrder=" + this.ColumnOrder).subscribe(resp => {
        this.misBen = [];
        this.misFac = [];
        this.Libranzas = resp.data;
        for (let b of this.Libranzas) {
          this.misBen.push(b.beneficiario);
        }
        for (let f of this.Libranzas) {
          this.misFac.push(f.factura);
        }
        this.loading = false;
      });
    this.GetCountPages(this.Order, this.ColumnOrder);
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
  GetLibranzaById(id: number) {
    this.DetalleLibranza = null;
    this.request.get("/libranza/GetLibranzaById?Id=" + id).subscribe(lib => this.DetalleLibranza = lib);
  }
  CreatePdf(Id: number) {
    window.open(Constants.URL_BASE_WEB_API + "/File/CreatePDF?Id=" + Id + "&Tipo=Libranza");
  }
  GetXLSFilter() {
    window.open(Constants.URL_BASE_WEB_API + "/libranza/GetXLSFilter?page=" + this.page + "&FilterAeropuerto=" + this.IdsAeroSelect + "&FilterEstado=" +
      this.FilterEstado + "&FilterBeneficiario=" + this.IdsBenSelect + "&FilterProyecto=" + this.FilterProyecto
      + "&FilterTipoLibranza=" + this.FilterTipoLibranza + "&FilterFactura=" + this.FilterFactura
      + "&FilterDesde=" + this.FilterDesde + "&FilterHasta=" + this.FilterHasta
      + "&Order=" + this.Order + "&ColumnOrder=" + this.ColumnOrder);
  }
  /////////////////////////////////
}
