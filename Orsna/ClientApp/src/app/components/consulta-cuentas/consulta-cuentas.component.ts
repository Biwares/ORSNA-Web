import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Request } from '../../services/request';

import { FormBuilder, Validators } from '@angular/forms';
import { DataService } from '../../services/data.service';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import swal from 'sweetalert2';
import { Constants } from '../../services/constants';

@Component({
  selector: 'app-consulta-cuentas',
  templateUrl: './consulta-cuentas.component.html',
  styleUrls: ['./consulta-cuentas.component.css']
})


export class ConsultaCuentasComponent implements OnInit {
  form;
  page: number = 1;
  FilterNroCuenta: string = "";
  FilterNombre: string = "";
  FilterTipoLibranza: string = "";
  FilterAeropuertoGrupo: string = "";
  Order: string = "desc";
  ColumnOrder: string = "FechaCreacion";
  CountPages: number = 0;
  CountElements: number = 0;
  CountFilterElements: number = 0;
  message: string = "";
  idToDelete: number;
  nroCuentaToDelete: string;
  closeResult: string;
  LibranzaTipos: any;
  AeropuertosGrupos: any;
  loading: boolean = false;
  constructor(
    private request: Request
    , private fb: FormBuilder
    , private routes: Router
    , private data: DataService
    , private modalService: NgbModal) {
    this.form = fb.group({
      Identificador: ['', Validators.required],
      Nombre: ['', Validators.required],
      Descripcion: ['', Validators.required],
      FechaVigencia: ['', Validators.required]
    });
  }
  cuentas: any;
  puedeEditar: boolean = false;
  puedeEliminar: boolean = false;

  refresh(): void {
    this.ngOnInit();
  }

  ngOnInit() {
    this.request.get(Constants.API_SEGURIDAD + Constants.API_GET_PERMISOS + "?modulo=" + 'Cuentas').subscribe(resp => {
      this.puedeEditar = resp.editar;
      this.puedeEliminar = resp.eliminar;
    });

    this.request.get(Constants.API_LIBRANZA + Constants.API_GET_LIBRANZA_TIPO).subscribe(lib => {
      this.LibranzaTipos = lib;
    });
    this.request.get(Constants.API_AEROPUERTO + Constants.API_GET_AEROPUERTOS_GRUPO).subscribe(grup => {
      this.AeropuertosGrupos = grup;
    });

    this.FilterNroCuenta = "";
    this.FilterNombre = "";
    this.FilterTipoLibranza = "";
    this.FilterAeropuertoGrupo = "";
    this.Order = "desc";
    this.ColumnOrder = "FechaCreacion";
    this.CountPages = 0;
    this.CountElements = 0;
    this.CountFilterElements = 0;

    this.GetAll(this.Order, this.ColumnOrder,1);
  }
  GetAllWithoutFilters() {
    this.request.get(Constants.API_CUENTA + Constants.API_GET_COUNT_FILTER_ELEMENTS + "?page=0").subscribe(resp => {
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
    this.request.get(Constants.API_CUENTA + Constants.API_GET_COUNT_FILTER_ELEMENTS + "?page=" + this.page + "&FilterNroCuenta=" + this.FilterNroCuenta + "&FilterNombre="
      + this.FilterNombre + "&FilterTipoLibranza=" + this.FilterTipoLibranza + "&FilterGrupoAeropuerto=" + this.FilterAeropuertoGrupo
      + "&Order=" + this.Order + "&ColumnOrder=" + this.ColumnOrder).subscribe(resp => {
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
    this.request.get(Constants.API_CUENTA + Constants.API_GET_COUNT_PAGES + "?page=" + this.page + "&FilterNroCuenta=" + this.FilterNroCuenta + "&FilterNombre="
      + this.FilterNombre + "&FilterTipoLibranza=" + this.FilterTipoLibranza + "&FilterGrupoAeropuerto=" + this.FilterAeropuertoGrupo
      + "&Order=" + this.Order + "&ColumnOrder=" + this.ColumnOrder).subscribe(resp => {
      this.CountPages = resp*10;
    });
  }
  GetAll(order: string, columnOrder: string, event: number) {
    this.page = event;
    this.loading = true;
    this.GetAllWithoutFilters();
    this.GetCountFilterElements(this.Order, this.ColumnOrder);
    if (columnOrder != undefined && columnOrder != null && columnOrder != '' && columnOrder != "") {
      this.ColumnOrder = columnOrder;
    }
    if (order != undefined && order != null && order != '' && order != "") {
      this.Order = order;
    }
    this.request.get(Constants.API_CUENTA + Constants.API_GET_ALL + "?page=" + this.page + "&FilterNroCuenta=" + this.FilterNroCuenta + "&FilterNombre="
      + this.FilterNombre + "&FilterTipoLibranza=" + this.FilterTipoLibranza + "&FilterGrupoAeropuerto=" + this.FilterAeropuertoGrupo
      + "&Order=" + this.Order + "&ColumnOrder=" + this.ColumnOrder).subscribe(resp => {
        this.cuentas = resp.data;
        this.loading = false;
      });

    
  }
  EditCuenta(id: number) {
    this.data.changeMessage(id+"");
    this.routes.navigate(['../dashboard/cuenta']);
  }

  OpenModalDeleteCuenta(id: number, NroCuenta: string, ModalDeleteCuenta: string) {
    this.idToDelete = id;
    this.nroCuentaToDelete = NroCuenta;
    swal({
      title: 'Eliminar',
      text: "¿Estás seguro que deseas Eliminar la cuenta: " + this.nroCuentaToDelete + "?",
      type: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      preConfirm: () => [this.DeleteCuenta()],
      confirmButtonText: 'Si, Eliminar!',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
    });
  }
  DeleteCuenta() {
    this.modalService.dismissAll();
    this.request.delete(Constants.API_CUENTA + Constants.API_DELETE + "?id=" + this.idToDelete).subscribe(resp => {
      if (resp.code == 501) {
        swal(
          '',
          resp.error,
          'warning'
        )
      } else if (resp.code == 200) {
        this.GetAll("desc", "FechaCreacion", 1);
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

  NewCuenta() {
    this.data.changeMessage("0");
    this.routes.navigate(['../dashboard/cuenta']);
  }
}
