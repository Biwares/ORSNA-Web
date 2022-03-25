import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Request } from '../../services/request';

import { FormBuilder, Validators } from '@angular/forms';
import { DataService } from '../../services/data.service';
import { NgbModal, NgbModalConfig } from '@ng-bootstrap/ng-bootstrap';
import swal from 'sweetalert2';

@Component({
  selector: 'app-consulta-rolmodulo',
  templateUrl: './consulta-rolmodulo.component.html',
  styleUrls: ['./consulta-rolmodulo.component.css']
})


export class ConsultaRolModuloComponent implements OnInit {
  form;
  page: number = 1;
  FilterNombre: string = "";
  Order: string = "desc";
  ColumnOrder: string = "idRolModulo";
  CountPages: number = 0;
  message: string = "";
  idToDelete: number;
  NombreRolModuloToDelete: string;
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
      idrol: ['', Validators.required],
      idmodulo: ['', Validators.required]
    });
  }
  rolesmodulo: any;
  ngOnInit() {

    this.GetAll(this.Order, this.ColumnOrder, 1);
  }
  GetCountPages(order: string, columnOrder: string) {
    if (columnOrder != undefined && columnOrder != null)
      this.ColumnOrder = columnOrder;
    if (order != undefined && order != null)
      this.Order = order;
    this.request.get("/rolmodulo/GetCountPages?page=" + this.page + "&FilterNombre=" + this.FilterNombre + "&Order=" + this.Order + "&ColumnOrder=" + this.ColumnOrder).subscribe(resp => {
      this.CountPages = resp * 10;
    });
  }
  GetAll(order: string, columnOrder: string, event: number) {
    this.page = event;
    if (columnOrder != undefined || columnOrder != null || columnOrder != '')
      this.ColumnOrder = columnOrder;
    if (order != undefined || order != null || order != '')
      this.Order = order;
    this.request.get("/rolmodulo/getall?page=" + event + "&FilterNombre=" + this.FilterNombre + "&Order=" + this.Order + "&ColumnOrder=" + this.ColumnOrder).subscribe(resp => {
      this.rolesmodulo = resp;
    });
    this.GetCountPages(this.Order, this.ColumnOrder);
  }
  EditRolModulo(id: number) {
    this.data.changeMessage(id + "");
    this.routes.navigate(['../dashboard/rolesmodulo']);
  }
  OpenModalDeleteRolModulo(id: number, nombre: string, ModalDeleteRolModulo: string) {
    this.idToDelete = id;
    this.NombreRolModuloToDelete = nombre;
    swal({
      title: 'estas seguro?',
      text: "desea eliminar la relación de rol/modulo: " + this.NombreRolModuloToDelete,
      type: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      preConfirm: () => [this.DeleteRolModulo()],
      confirmButtonText: 'Si, Eliminar!',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.value) {
        swal(
          'Eliminado!',
          'Se eliminó con éxito.',
          'success'
        )
      }
    });
    //this.modalService.open(ModalDeleteRolModulo);
  }
  DeleteRolModulo() {
    this.modalService.dismissAll();
    this.request.delete("/rolmodulo/Delete?id=" + this.idToDelete).subscribe(resp => {
      this.GetAll("desc", "idRolModulo", 1);
      this.page = 1;
      this.modalService.dismissAll();
    });
  }

  NewRolModulo() {
    this.data.changeMessage("0");
    this.routes.navigate(['../dashboard/rolmodulo']);
  }
}
