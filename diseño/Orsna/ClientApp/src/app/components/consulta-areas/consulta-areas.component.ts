import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Request } from '../../services/request';

import { FormBuilder, Validators } from '@angular/forms';
import { DataService } from '../../services/data.service';
import { NgbModal, NgbModalConfig } from '@ng-bootstrap/ng-bootstrap';
import swal from 'sweetalert2';

@Component({
  selector: 'app-consulta-areas',
  templateUrl: './consulta-areas.component.html',
  styleUrls: ['./consulta-areas.component.css']
})


export class ConsultaAreasComponent implements OnInit {
  form;
  page: number = 1;
  FilterCodArea: string = "";
  FilterNombre: string = "";
  Order: string = "desc";
  ColumnOrder: string = "idArea";
  CountPages: number = 0;
  message: string = "";
  idToDelete: number;
  NombreAreaToDelete: string;
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
      Codigo: ['', Validators.required],
      Nombre: ['', Validators.required]
    });
  }
  areas: any;
  ngOnInit() {

    this.GetAll(this.Order, this.ColumnOrder, 1);
  }
  GetCountPages(order: string, columnOrder: string) {
    if (columnOrder != undefined && columnOrder != null)
      this.ColumnOrder = columnOrder;
    if (order != undefined && order != null)
      this.Order = order;
    this.request.get("/area/GetCountPages?page=" + this.page + "&FilterCodArea=" + this.FilterCodArea + "&FilterNombre=" + this.FilterNombre + "&Order=" + this.Order + "&ColumnOrder=" + this.ColumnOrder).subscribe(resp => {
      this.CountPages = resp * 10;
    });
  }
  GetAll(order: string, columnOrder: string, event: number) {
    this.page = event;
    if (columnOrder != undefined || columnOrder != null || columnOrder != '')
      this.ColumnOrder = columnOrder;
    if (order != undefined || order != null || order != '')
      this.Order = order;
    this.request.get("/area/getall?page=" + event + "&FilterCodArea=" + this.FilterCodArea + "&FilterNombre=" + this.FilterNombre + "&Order=" + this.Order + "&ColumnOrder=" + this.ColumnOrder).subscribe(resp => {
      this.areas = resp;
    });
    this.GetCountPages(this.Order, this.ColumnOrder);
  }
  EditArea(id: number) {
    this.data.changeMessage(id + "");
    this.routes.navigate(['../dashboard/area']);
  }
  OpenModalDeleteArea(id: number, nombre: string, ModalDeleteArea: string) {
    this.idToDelete = id;
    this.NombreAreaToDelete = nombre;
    swal({
      title: 'estas seguro?',
      text: "desea eliminar el área: " + this.NombreAreaToDelete,
      type: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      preConfirm: () => [this.DeleteArea()],
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
    //this.modalService.open(ModalDeleteArea);
  }
  DeleteArea() {
    this.modalService.dismissAll();
    this.request.delete("/area/Delete?id=" + this.idToDelete).subscribe(resp => {
      this.GetAll("desc", "idArea", 1);
      this.page = 1;
      this.modalService.dismissAll();
    });
  }

  NewArea() {
    this.data.changeMessage("0");
    this.routes.navigate(['../dashboard/area']);
  }
}
