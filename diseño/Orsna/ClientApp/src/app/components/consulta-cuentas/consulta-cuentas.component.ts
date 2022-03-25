import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Request } from '../../services/request';

import { FormBuilder, Validators } from '@angular/forms';
import { DataService } from '../../services/data.service';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import swal from 'sweetalert2';

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
  message: string = "";
  idToDelete: number;
  nroCuentaToDelete: string;
  closeResult: string;
  LibranzaTipos: any;
  AeropuertosGrupos: any;
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
  ngOnInit() {

    this.request.get("/libranza/GetLibranzaTipo").subscribe(lib => {
      this.LibranzaTipos = lib;
    });
    this.request.get("/aeropuerto/GetAeropuertosGrupo").subscribe(grup => {
      this.AeropuertosGrupos = grup;
    });

    this.GetAll(this.Order, this.ColumnOrder,1);
  }
  GetCountPages(order: string, columnOrder: string) {
    if (columnOrder != undefined && columnOrder != null)
      this.ColumnOrder = columnOrder;
    if (order != undefined && order != null)
      this.Order = order;
    this.request.get("/cuenta/GetCountPages?page=" + this.page + "&FilterNroCuenta=" + this.FilterNroCuenta + "&FilterNombre="
      + this.FilterNombre + "&FilterTipoLibranza=" + this.FilterTipoLibranza + "&FilterGrupoAeropuerto=" + this.FilterAeropuertoGrupo
      + "&Order=" + this.Order + "&ColumnOrder=" + this.ColumnOrder).subscribe(resp => {
      this.CountPages = resp*10;
    });
  }
  GetAll(order: string, columnOrder: string, event: number) {
    this.page = event;
    if (columnOrder != undefined || columnOrder != null || columnOrder != '')
      this.ColumnOrder = columnOrder;
    if (order != undefined || order != null || order != '')
      this.Order = order;
    this.request.get("/cuenta/getall?page=" + this.page + "&FilterNroCuenta=" + this.FilterNroCuenta + "&FilterNombre="
      + this.FilterNombre + "&FilterTipoLibranza=" + this.FilterTipoLibranza + "&FilterGrupoAeropuerto=" + this.FilterAeropuertoGrupo
      + "&Order=" + this.Order + "&ColumnOrder=" + this.ColumnOrder).subscribe(resp => {
      this.cuentas = resp;
    });
    this.GetCountPages(this.Order, this.ColumnOrder);
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
      text: "¿Estás seguro de Eliminar la cuenta " + this.nroCuentaToDelete + "?",
      type: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      preConfirm: () => [this.DeleteCuenta()],
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

    //this.modalService.open(ModalDeleteCuenta);
  }
  DeleteCuenta() {
    this.modalService.dismissAll();
    this.request.delete("/cuenta/Delete?id=" + this.idToDelete).subscribe(resp => {
        this.GetAll("desc", "FechaCreacion",1);
        this.page = 1;
        this.modalService.dismissAll();
    });
  }

  NewCuenta() {
    this.data.changeMessage("0");
    this.routes.navigate(['../dashboard/cuenta']);
  }
}
