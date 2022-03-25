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
  FilterIdProyecto: string = "";
  FilterArea: string = "";
  FilterEstado: string = "";
  FilterFecha: string = "";
  Order: string = "desc";
  ColumnOrder: string = "FechaCreacion";
  CountPages: number = 0;
  message: string = "";
  idToDelete: number;
  proyectoToDelete: string;
  closeResult: string;
  Aeropuertos: any;
  AeropuertosSelect: any;
  Areas: any;
  AreasSelect: any;
  dropdownSettings: any;
  IdsAeroSelect: string = "";
  IdsAreaSelect: string = "";
  Estados: any;
  proyectos: any;
  ProyectoToDelete: string= "";
  misAeros: Array<string> = [];
  NombreModalAnexos: string = '';
  DetalleProyecto: any = null;
  AnexosProyecto: any = [];
  constructor(private request: Request
    , private fb: FormBuilder
    , private routes: Router
    , private data: DataService
    , private modalService: NgbModal) {
  }

  ngOnInit() {
    this.GetAll(this.Order, this.ColumnOrder, 1);

    this.request.get("/area").subscribe(resp => {

      this.Areas = resp.data;
    });
    this.request.get("/Aeropuerto").subscribe(resp => {
      
      this.Aeropuertos = resp.data;
    });

    this.request.get("/proyecto/GetEstados").subscribe(resp => {
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
  }

  NewProyecto() {
    this.data.changeMessage("0");
    this.routes.navigate(['../dashboard/proyecto']);
  }
  GetCountPages(order: string, columnOrder: string) {
    if (columnOrder != undefined && columnOrder != null)
      this.ColumnOrder = columnOrder;
    if (order != undefined && order != null)
      this.Order = order;
    this.request.get("/proyecto/GetCountPages?page=" + this.page + "&FilterAeropuerto=" + this.IdsAeroSelect + "&FilterIdProyecto=" +
      this.FilterIdProyecto + "&FilterArea=" + this.IdsAreaSelect + "&FilterEstado=" + this.FilterEstado + "&FilterFecha=" +
      this.FilterFecha + "&Order=" + this.Order + "&ColumnOrder=" + this.ColumnOrder).subscribe(resp => {
      this.CountPages = resp * 10;
    });
  }
  GetAll(order: string, columnOrder: string, event: number) {
    this.page = event;
    if (columnOrder != undefined || columnOrder != null || columnOrder != '')
      this.ColumnOrder = columnOrder;
    if (order != undefined || order != null || order != '')
      this.Order = order;
    this.request.get("/proyecto/getall?page=" + event + "&FilterAeropuerto=" + this.IdsAeroSelect + "&FilterIdProyecto=" +
      this.FilterIdProyecto + "&FilterArea=" + this.IdsAreaSelect + "&FilterEstado=" + this.FilterEstado + "&FilterFecha=" +
      this.FilterFecha + "&Order=" + this.Order + "&ColumnOrder=" + this.ColumnOrder).subscribe(resp => {
        for (let proy of resp.data) {
          let aeros = '';
          for (let aer of proy.aeropuertos) {
            aeros += aer.nombre + ' ';
          }
          this.misAeros.push(aeros);
        }
        this.proyectos = resp.data;
    });
    this.GetCountPages(this.Order, this.ColumnOrder);
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
      if (result.value) {
        swal(
          'Eliminado!',
          'Se Eliminó con éxito.',
          'success'
        )
      }
    });
    //this.modalService.open(ModalDeleteProyecto);
  }
  DeleteProyecto() {
    this.modalService.dismissAll();
    this.request.delete("/proyecto/Delete?id=" + this.idToDelete).subscribe(resp => {
      this.GetAll("desc", "Id", 1);
      this.page = 1;
      this.modalService.dismissAll();
    });
  }
  /////////////////////////////////MODAL LISTA DE ANEXOS//////////////////////////////////////////////////////////////////
  getAdjuntosProyecto(id: number, nombreProyecto: string) {
    this.NombreModalAnexos = nombreProyecto;
    this.AnexosProyecto = [];
    this.request.get("/ProyectoAdjuntos/GetAdjuntosByProyecto?id=" + id).subscribe(resp => {
      this.AnexosProyecto = resp;
    });
  }
  getFileById(idAdjunto: string) {
    window.open(Constants.URL_BASE_WEB_API + '/File/GetFile?id=' + idAdjunto);
  }
  ///////////////////////////////////FIN MODAL ANEXOS////////////////////////////////////////////////////////////////////

  /////////////////////////////////MODAL DETALLE PROYECTO//////////////////////////////////////////////////////////////////

  GetProyectoById(id: number) {
    this.DetalleProyecto = null;
    this.request.get("/proyecto/GetProyectoById?idProyecto=" + id).subscribe(pro => {
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
  /////////////////////////////////

}
