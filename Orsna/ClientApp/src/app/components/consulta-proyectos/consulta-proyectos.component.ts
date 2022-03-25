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
  selector: 'app-consulta-proyectos',
  templateUrl: './consulta-proyectos.component.html',
  styleUrls: ['./consulta-proyectos.component.css']
})
export class
  ConsultaProyectosComponent implements OnInit {
  form;
  page: number = 1;
  FilterAeropuerto: string = "";
  FilterObra: string = "";
  FilterArea: string = "";
  FilterDestino: number = 0;
  FilterEstado: string = "";
  FilterFecha: number = 0;
  Order: string = "desc";
  ColumnOrder: string = "FechaCreacion";
  CountPages: number = 0;
  CountElements: number = 0;
  CountFilterElements: number = 0;
  message: string = "";
  idToDelete: number;
  proyectoToDelete: string;
  closeResult: string;

  Aeropuertos: any;
  AeropuertosSelect: any;
  IdsAeroSelect: string = "";
  dropdownSettings: any;

  Areas: any;
  AreasSelect: any;
  dropdownSettingsArea: any;
  IdsAreaSelect: string = "";

  Cuentas: any;
  CuentasSelect: any;
  dropdownSettingsCuentas: any;
  IdsCuentasSelect: string = "";

  ProyectosId: any;
  FilterIdProyecto: string = "";

  Destinos: any;

  Estados: any;
  proyectos: any;
  ProyectosSelect: any;
  ProyectoToDelete: string= "";
  misAeros: Array<string> = [];
  NombreModalAnexos: string = '';
  DetalleProyecto: any = null;
  AnexosProyecto: any = [];
  puedeEditar: boolean = false;
  puedeEditarLibranzas: boolean = false;
  puedeEliminar: boolean = false;
  Anios: any = [];
  loading: boolean = false;

  constructor(private request: Request
    , private fb: FormBuilder
    , private routes: Router
    , private data: DataService
    , private modalService: NgbModal) {
  }

  refresh(): void {
    this.ngOnInit();
  }

  resetFields() {
    this.FilterAeropuerto = "";
    this.FilterEstado = "";
    this.FilterObra = "";
    this.FilterFecha = 0;
    this.FilterArea = "";
    this.FilterDestino = 0;
    this.FilterEstado = "";

    this.Order = "desc";
    this.ColumnOrder = "Fecha";
    this.CountPages = 0;
    this.CountElements = 0;
    this.CountFilterElements = 0;
    this.message = "";
    this.IdsAeroSelect = "";
    this.AeropuertosSelect = [];
    this.CuentasSelect= [];
    this.IdsCuentasSelect = "";
    this.AreasSelect = [];
    this.IdsAreaSelect = "";
    this.ProyectosSelect = [];
    this.ProyectosId = "";
    this.FilterIdProyecto= "";

  }


  ngOnInit() {
    this.resetFields();
    this.request.get(Constants.API_SEGURIDAD + Constants.API_GET_PERMISOS + "?modulo=" + 'Proyectos').subscribe(resp => {
      this.puedeEditar = resp.editar;
      this.puedeEliminar = resp.eliminar;
    });

    this.request.get(Constants.API_SEGURIDAD + Constants.API_GET_PERMISOS +"?modulo=" + 'Libranzas de Pago').subscribe(resp => {
      this.puedeEditarLibranzas = resp.editar;
    });

    this.GetAll(this.Order, this.ColumnOrder, 1);

    this.request.get(Constants.API_AREA + Constants.API_GET_FILTRADAS_USUARIO).subscribe(resp => {

      this.Areas = resp.data;
    });
    this.request.get(Constants.API_CUENTA).subscribe(resp => {
      this.Cuentas = resp.data;
    });
    this.request.get(Constants.API_PROYECTO + Constants.API_GET_DESTINOS).subscribe(resp => {
      this.Destinos = resp;
    });
    this.request.get(Constants.API_AEROPUERTO).subscribe(resp => {
      this.Aeropuertos = resp.data;
    });
    this.request.get(Constants.API_PROYECTO + Constants.API_GET_PROYECTOS_IDS).subscribe(resp => {
      this.ProyectosId = resp;
    });
    this.request.get(Constants.API_PROYECTO + Constants.API_GET_ANIOS).subscribe(resp => {
      this.Anios = resp;
    });
    this.request.get(Constants.API_PROYECTO + Constants.API_GET_ESTADOS).subscribe(resp => {
      this.Estados = resp;
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
    this.dropdownSettingsArea = {
      singleSelection: false,
      idField: 'id',
      textField: 'nombre',
      itemsShowLimit: 3,
      allowSearchFilter: true,
      enableCheckAll: false,
      searchPlaceholderText: 'Buscar'
    };
    this.dropdownSettingsCuentas = {
      singleSelection: false,
      idField: 'id',
      textField: 'descripcion',
      itemsShowLimit: 3,
      allowSearchFilter: true,
      enableCheckAll: false,
      searchPlaceholderText: 'Buscar'
    };
  }

  NewProyecto() {
    this.data.changeMessage("0");
    this.routes.navigate(['../dashboard/proyecto']);
  }
  NewLibranza(id: number) {
    this.data.create(id + "");
    this.routes.navigate(['../dashboard/libranza']);
  }
  GetAllWithoutFilters() {
    this.request.get(Constants.API_PROYECTO + Constants.API_GET_COUNT_FILTER_ELEMENTS + "?page=0").subscribe(resp => {
      this.CountElements = resp;
    });
  }
  GetCountFilterElements(order: string, columnOrder: string) {
    if (columnOrder != undefined && columnOrder != null && columnOrder != '' && columnOrder != "") {
      this.ColumnOrder = columnOrder;
    }
    if (order != undefined && order != null && order != '' && order != "") {
      this.Order = order;
    }
    this.request.get(Constants.API_PROYECTO + Constants.API_GET_COUNT_FILTER_ELEMENTS + "?page=" + this.page + "&FilterAeropuerto=" + this.IdsAeroSelect + "&FilterIdProyecto=" +
      this.FilterIdProyecto + "&FilterArea=" + this.IdsAreaSelect + "&FilterEstado=" + this.FilterEstado + "&FilterFechaCreacion=" +
      this.FilterFecha + "&FilterObra=" + this.FilterObra + "&Order=" + this.Order + "&ColumnOrder=" + this.ColumnOrder + "&FilterCuentas=" + this.IdsCuentasSelect + "&FilterDestino=" + this.FilterDestino).subscribe(resp => {
        this.CountFilterElements = resp;
      });
  }
  GetCountPages(order: string, columnOrder: string) {
    if (columnOrder != undefined && columnOrder != null && columnOrder != '' && columnOrder != "") {
      this.ColumnOrder = columnOrder;
    }
    if (order != undefined && order != null && order != '' && order != "") {
      this.Order = order;
    }
    this.request.get(Constants.API_PROYECTO + Constants.API_GET_COUNT_PAGES +"?page=" + this.page + "&FilterAeropuerto=" + this.IdsAeroSelect + "&FilterIdProyecto=" +
      this.FilterIdProyecto + "&FilterArea=" + this.IdsAreaSelect + "&FilterEstado=" + this.FilterEstado + "&FilterFecha=" +
      this.FilterFecha + "&FilterObra=" + this.FilterObra + "&Order=" + this.Order + "&ColumnOrder=" + this.ColumnOrder + "&FilterCuentas=" + this.IdsCuentasSelect + "&FilterDestino=" + this.FilterDestino).subscribe(resp => {
      this.CountPages = resp * 10;
    });
  }
  GetAll(order: string, columnOrder: string, event: number) {
    this.loading = true;
    this.GetAllWithoutFilters();
    this.GetCountFilterElements(this.Order, this.ColumnOrder);
    this.page = event;
    if (columnOrder != undefined && columnOrder != null && columnOrder != '' && columnOrder != "") {
      this.ColumnOrder = columnOrder;
    }
    if (order != undefined && order != null && order != '' && order != "") {
      this.Order = order;
    }
    this.request.get(Constants.API_PROYECTO + Constants.API_GET_ALL + "?page=" + event + "&FilterAeropuerto=" + this.IdsAeroSelect + "&FilterIdProyecto=" +
      this.FilterIdProyecto + "&FilterArea=" + this.IdsAreaSelect + "&FilterEstado=" + this.FilterEstado + "&FilterFecha=" +
      this.FilterFecha + "&FilterObra=" + this.FilterObra + "&Order=" + this.Order + "&ColumnOrder=" + this.ColumnOrder + "&FilterCuentas=" + this.IdsCuentasSelect + "&FilterDestino=" + this.FilterDestino).subscribe(resp => {
        for (let proy of resp.data) {
          let aeros = '';
          for (let aer of proy.aeropuertos) {
            aeros += aer.nombre + ' ';
          }
          this.misAeros.push(aeros);
        }
        this.proyectos = resp.data;
        this.loading = false;
      });
    
  }

  EditProyecto(id: number) {
    this.data.changeMessage(id + "");
    this.routes.navigate(['../dashboard/proyecto']);
  }
  OpenModalDeleteProyecto(id: number, nombre: string) {
    this.idToDelete = id;
    this.ProyectoToDelete = nombre;
    swal({
      title: 'Eliminar',
      text: "¿Estás seguro de Eliminar el Proyecto: " + nombre + "?",
      type: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      preConfirm: () => [this.DeleteProyecto()],
      confirmButtonText: 'Sí, Eliminar!',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
    });
    //this.modalService.open(ModalDeleteProyecto);
  }
  DeleteProyecto() {
    this.modalService.dismissAll();
    this.request.delete(Constants.API_PROYECTO + Constants.API_DELETE + "?id=" + this.idToDelete).subscribe(resp => {
      if (resp.code == 501) {
        swal(
          '',
          resp.error,
          'warning'
        )
      } else if (resp.code == 200) {
        this.GetAll("desc", "Id", 1);
        this.page = 1;
        this.modalService.dismissAll();
        swal(
          'Eliminado!',
          'Se eliminó con éxito.',
          'success'
        )
      }
    });
  }
  /////////////////////////////////MODAL LISTA DE ANEXOS//////////////////////////////////////////////////////////////////
  getAdjuntosProyecto(id: number, nombreProyecto: string) {
    this.NombreModalAnexos = nombreProyecto;
    this.AnexosProyecto = [];
    this.request.get(Constants.API_PROYECTO_ADJUNTOS + Constants.API_GET_ADJUNTOS_PROYECTOS +  "?id=" + id).subscribe(resp => {
      this.AnexosProyecto = resp;
    });
  }
  getFileById(idAdjunto: string) {
    window.open(Constants.URL_BASE_WEB_API + Constants.API_FILE + Constants.API_GET_FILE + '?id=' + idAdjunto);
  }
  ///////////////////////////////////FIN MODAL ANEXOS////////////////////////////////////////////////////////////////////

  /////////////////////////////////MODAL DETALLE PROYECTO//////////////////////////////////////////////////////////////////

  GetProyectoById(id: number) {
    this.DetalleProyecto = null;
    this.request.get(Constants.API_PROYECTO + Constants.API_GET_PROYECTO_ID + "?idProyecto=" + id).subscribe(pro => {
      this.DetalleProyecto = pro;
    });
  }

  /////////////////////////////////FIN MODAL DETALLE PROYECTO//////////////////////////////////////////////////////////////////
  ///////////////////////////////////// select dropdow //////////////////////////////////
  onItemSelectAero(item: any) {
    this.IdsAeroSelect += ","+item.id;
  }
  onItemDeSelectAero(item: any) {
    let array = this.IdsAeroSelect.split(',');
    this.IdsAeroSelect = "";
    for (var i = 0; i < array.length; i++) {
      if (!(array[i] == item.id) && array[i]!="") {
        this.IdsAeroSelect = this.IdsAeroSelect + "," + array[i];
      }
    }
  }
  onItemSelectProyectosId(item: any) {
    this.FilterIdProyecto += "," + item.id;
  }
  onItemDeSelectProyectosId(item: any) {
    let array = this.FilterIdProyecto.split(',');
    this.FilterIdProyecto = "";
    for (var i = 0; i < array.length; i++) {
      if (!(array[i] == item.id) && array[i] != "") {
        this.FilterIdProyecto = this.FilterIdProyecto + "," + array[i];
      }
    }
  }
  onItemSelectArea(item: any) {
    console.log(item);
    this.IdsAreaSelect += "," + item.id;
  }
  onItemDeSelectArea(item: any) {
    let array = this.IdsAreaSelect.split(',');
    this.IdsAreaSelect = "";
    for (var i = 0; i < array.length; i++) {
      if (!(array[i] == item.id) && array[i] != "") {
        this.IdsAreaSelect = this.IdsAreaSelect + "," + array[i];
      }
    }
  }

  onItemSelectCuentas(item: any) {
    console.log(item);
    this.IdsCuentasSelect += "," + item.id;
  }
  onItemDeSelectCuentas(item: any) {
    let array = this.IdsCuentasSelect.split(',');
    this.IdsCuentasSelect = "";
    for (var i = 0; i < array.length; i++) {
      if (!(array[i] == item.id) && array[i] != "") {
        this.IdsCuentasSelect = this.IdsCuentasSelect + "," + array[i];
      }
    }
  }
  /////////////////////////////////

}
