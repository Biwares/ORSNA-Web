import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Request } from '../../services/request';

import { FormBuilder, Validators } from '@angular/forms';
import { DataService } from '../../services/data.service';
import { NgbModal, NgbModalConfig } from '@ng-bootstrap/ng-bootstrap';
import swal from 'sweetalert2';
import { Constants } from '../../services/constants';

@Component({
  selector: 'app-consulta-usuarios',
  templateUrl: './consulta-usuarios.component.html',
  styleUrls: ['./consulta-usuarios.component.css']
})


export class ConsultaUsuariosComponent implements OnInit {
  form;
  page: number = 1;
  FilterEmail: string = "";
  Order: string = "desc";
  ColumnOrder: string = "idUsuario";
  CountPages: number = 0;
  CountElements: number = 0;
  CountFilterElements: number = 0;
  message: string = "";
  idToDelete: number;
  EmailUsuarioToDelete: string;
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
      Email: ['', Validators.required],
      Password: ['', Validators.required]
    });
  }
  usuarios: any;
  puedeEditar: boolean = false;
  puedeEliminar: boolean = false;

  refresh(): void {
    this.ngOnInit();
  }

  ngOnInit() {
    this.request.get(Constants.API_SEGURIDAD + Constants.API_GET_PERMISOS + "?modulo=" + 'Usuarios').subscribe(resp => {
      this.puedeEditar = resp.editar;
      this.puedeEliminar = resp.eliminar;
    });

    this.FilterEmail = "";
    this.Order = "desc";
    this.ColumnOrder = "idUsuario";
    this.CountPages = 0;
    this.CountElements = 0;
    this.CountFilterElements = 0;

    this.GetAll(this.Order, this.ColumnOrder, 1);
  }
  GetCountPages(order: string, columnOrder: string) {
    if (columnOrder != undefined && columnOrder != null && columnOrder != '' && columnOrder != "") {
      this.ColumnOrder = columnOrder;
    }
    if (order != undefined && order != null && order != '' && order != "") {
      this.Order = order;
    }
    this.request.get(Constants.API_USUARIO + Constants.API_GET_COUNT_PAGES + "?page=" + this.page + "&FilterEmail=" + this.FilterEmail + "&Order=" + this.Order + "&ColumnOrder=" + this.ColumnOrder).subscribe(resp => {
      this.CountPages = resp * 10;
    });
  }
  GetCountFilterElements(order: string, columnOrder: string) {
    if (columnOrder != undefined && columnOrder != null && columnOrder != '' && columnOrder != "") {
      this.ColumnOrder = columnOrder;
    }
    if (order != undefined && order != null && order != '' && order != "") {
      this.Order = order;
    }
    this.request.get(Constants.API_USUARIO + Constants.API_GET_COUNT_FILTER_ELEMENTS + "?page=" + this.page + "&FilterEmail=" + this.FilterEmail + "&Order=" + this.Order + "&ColumnOrder=" + this.ColumnOrder).subscribe(resp => {
      this.CountFilterElements = resp;
    });
  }
  GetAllWithoutFilters() {
    this.request.get(Constants.API_USUARIO + Constants.API_GET_COUNT_FILTER_ELEMENTS + "?page=0").subscribe(resp => {
      this.CountElements = resp;
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
    this.request.get(Constants.API_USUARIO + Constants.API_GET_ALL + "?page=" + event + "&FilterEmail=" + this.FilterEmail + "&Order=" + this.Order + "&ColumnOrder=" + this.ColumnOrder).subscribe(resp => {
      this.usuarios = resp;
      this.loading = false;
    });
  }
  EditUsuario(id: number) {
    this.data.changeMessage(id + "");
    this.routes.navigate(['../dashboard/usuario']);
  }
  OpenModalDeleteUsuario(id: number, Email: string, ModalDeleteUsuario: string) {
    this.idToDelete = id;
    this.EmailUsuarioToDelete = Email;
    swal({
      title: 'Cambiar estado',
      text: "¿Estás seguro de cambiar el estado del Usuario: " + this.EmailUsuarioToDelete + "?",
      type: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      preConfirm: () => [this.DeleteUsuario()],
      confirmButtonText: 'Si, Cambiar!',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.value) {
        swal(
          'Cambiado!',
          'Se cambió con éxito.',
          'success'
        )
      }
    });
    //this.modalService.open(ModalDeleteUsuario);
  }
  DeleteUsuario() {
    this.modalService.dismissAll();
    this.request.delete(Constants.API_USUARIO + Constants.API_DELETE + "?id=" + this.idToDelete).subscribe(resp => {
      this.GetAll("desc", "idUsuario", 1);
      this.page = 1;
      this.modalService.dismissAll();
    });
  }

  NewUsuario() {
    this.data.changeMessage("0");
    this.routes.navigate(['../dashboard/usuario']);
  }
}
