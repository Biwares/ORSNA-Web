import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Request } from '../../services/request';

import { FormBuilder, Validators } from '@angular/forms';
import { DataService } from '../../services/data.service';
import { NgbModal, NgbModalConfig } from '@ng-bootstrap/ng-bootstrap';
import swal from 'sweetalert2';
import { Constants } from '../../services/constants';

@Component({
  selector: 'app-consulta-rol',
  templateUrl: './consulta-rol.component.html',
  styleUrls: ['./consulta-rol.component.css']
})


export class ConsultaRolComponent implements OnInit {
  form;
  page: number = 1;
  FilterNombre: string = "";
  Order: string = "desc";
  ColumnOrder: string = "idRol";
  CountPages: number = 0;
  CountElements: number = 0;
  CountFilterElements: number = 0;
  message: string = "";
  idToDelete: number;
  NombreRolToDelete: string;
  closeResult: string;
  loading: boolean = false;
  constructor(
    private request: Request
    , private fb: FormBuilder
    , private routes: Router
    , private data: DataService
    , private modalService: NgbModal
    , private config: NgbModalConfig) {
    // customize default values of modals used by this component tree
    config.backdrop = 'static';
    config.keyboard = false;
    //end modal customize
    this.form = fb.group({
      Nombre: ['', Validators.required]
    });
  }
  roles: any;
  puedeEditar: boolean = false;
  puedeEliminar: boolean = false;

  refresh(): void {
    this.ngOnInit();
  }

  ngOnInit() {
    this.request.get(Constants.API_SEGURIDAD + Constants.API_GET_PERMISOS +  "?modulo=" + 'Roles').subscribe(resp => {
      this.puedeEditar = resp.editar;
      this.puedeEliminar = resp.eliminar;
    });

    this.FilterNombre = "";
    this.Order = "desc";
    this.ColumnOrder = "idRol";
    this.CountPages= 0;
    this.CountElements = 0;
    this.CountFilterElements = 0;


    this.GetAll(this.Order, this.ColumnOrder, 1);
  }
  GetAllWithoutFilters() {
    this.request.get(Constants.API_ROL + Constants.API_GET_COUNT_FILTER_ELEMENTS + "?page=0").subscribe(resp => {
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
    this.request.get(Constants.API_ROL + Constants.API_GET_COUNT_FILTER_ELEMENTS + "?page=" + this.page + "&FilterNombre=" + this.FilterNombre + "&Order=" + this.Order + "&ColumnOrder=" + this.ColumnOrder).subscribe(resp => {
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
    this.request.get(Constants.API_ROL + Constants.API_GET_COUNT_PAGES + "?page=" + this.page + "&FilterNombre=" + this.FilterNombre + "&Order=" + this.Order + "&ColumnOrder=" + this.ColumnOrder).subscribe(resp => {
      this.CountPages = resp * 10;
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
    this.request.get(Constants.API_ROL + Constants.API_GET_ALL + "?page=" + event + "&FilterNombre=" + this.FilterNombre + "&Order=" + this.Order + "&ColumnOrder=" + this.ColumnOrder).subscribe(resp => {
      this.roles = resp;
      this.loading = false;
    });
  }
  EditRol(id: number) {
    this.data.changeMessage(id + "");
    this.routes.navigate(['../dashboard/rol']);
  }
  OpenModalDeleteRol(id: number, nombre: string, ModalDeleteRol: string) {
    this.idToDelete = id;
    this.NombreRolToDelete = nombre;
    swal({
      title: 'Eliminar',
      text: "¿Estás seguro de Eliminar el Rol: " + nombre + "?",
      type: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      preConfirm: () => [this.DeleteRol()],
      confirmButtonText: 'Si, Eliminar!',
      cancelButtonText: 'Cancelar'
    });
    //this.modalService.open(ModalDeleteRol);
  }
  DeleteRol() {
    this.modalService.dismissAll();
    this.request.delete(Constants.API_ROL + Constants.API_DELETE + "?id=" + this.idToDelete).subscribe(resp => {
      if (resp.code == 501) {
        swal(
          '',
          resp.error,
          'warning'
        )
      } else if (resp.code == 200) {
        this.GetAll("desc", "idRol", 1);
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

  NewRol() {
    this.data.changeMessage("0");
    this.routes.navigate(['../dashboard/rol']);
  }
}
