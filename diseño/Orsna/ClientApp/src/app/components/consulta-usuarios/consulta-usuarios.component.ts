import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Request } from '../../services/request';

import { FormBuilder, Validators } from '@angular/forms';
import { DataService } from '../../services/data.service';
import { NgbModal, NgbModalConfig } from '@ng-bootstrap/ng-bootstrap';
import swal from 'sweetalert2';

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
  message: string = "";
  idToDelete: number;
  EmailUsuarioToDelete: string;
  closeResult: string;

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
  ngOnInit() {

    this.GetAll(this.Order, this.ColumnOrder, 1);
  }
  GetCountPages(order: string, columnOrder: string) {
    if (columnOrder != undefined && columnOrder != null)
      this.ColumnOrder = columnOrder;
    if (order != undefined && order != null)
      this.Order = order;
    this.request.get("/usuario/GetCountPages?page=" + this.page + "&FilterEmail=" + this.FilterEmail + "&Order=" + this.Order + "&ColumnOrder=" + this.ColumnOrder).subscribe(resp => {
      this.CountPages = resp * 10;
    });
  }
  GetAll(order: string, columnOrder: string, event: number) {
    this.page = event;
    if (columnOrder != undefined || columnOrder != null || columnOrder != '')
      this.ColumnOrder = columnOrder;
    if (order != undefined || order != null || order != '')
      this.Order = order;
    this.request.get("/usuario/getall?page=" + event + "&FilterEmail=" + this.FilterEmail + "&Order=" + this.Order + "&ColumnOrder=" + this.ColumnOrder).subscribe(resp => {
      this.usuarios = resp;
    });
    this.GetCountPages(this.Order, this.ColumnOrder);
  }
  EditUsuario(id: number) {
    this.data.changeMessage(id + "");
    this.routes.navigate(['../dashboard/usuario']);
  }
  OpenModalDeleteUsuario(id: number, Email: string, ModalDeleteUsuario: string) {
    this.idToDelete = id;
    this.EmailUsuarioToDelete = Email;
    swal({
      title: 'estas seguro?',
      text: "desea eliminar el usuario: " + this.EmailUsuarioToDelete,
      type: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      preConfirm: () => [this.DeleteUsuario()],
      confirmButtonText: 'Si, Eliminar!',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.value) {
        swal(
          'Eliminado!',
          'Se elimino con éxito.',
          'success'
        )
      }
    });
    //this.modalService.open(ModalDeleteUsuario);
  }
  DeleteUsuario() {
    this.modalService.dismissAll();
    this.request.delete("/usuario/Delete?id=" + this.idToDelete).subscribe(resp => {
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
