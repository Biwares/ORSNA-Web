import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Request } from '../../services/request';

import { FormBuilder, Validators } from '@angular/forms';
import { DataService } from '../../services/data.service';
import { NgbModal, NgbModalConfig } from '@ng-bootstrap/ng-bootstrap';
import swal from 'sweetalert2';
import { Constants } from '../../services/constants';

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
  CountElements: number = 0;
  CountFilterElements: number = 0;
  message: string = "";
  idToDelete: number;
  NombreAreaToDelete: string;
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
      Codigo: ['', Validators.required],
      Nombre: ['', Validators.required]
    });
  }
  areas: any;
  puedeEditar: boolean = false;
  puedeEliminar: boolean = false;

  ngOnInit() {
    this.request.get(Constants.API_SEGURIDAD + Constants.API_GET_PERMISOS + "?modulo=" + 'Áreas').subscribe(resp => {
      this.puedeEditar = resp.editar;
      this.puedeEliminar = resp.eliminar;
    });

    this.FilterCodArea = "";
    this.FilterNombre = "";
    this.CountPages= 0;
    this.CountElements = 0;
    this.CountFilterElements = 0;

    this.GetAll(this.Order, this.ColumnOrder, 1);
  }

  refresh(): void {
    this.ngOnInit();
  }

  GetCountPages(order: string, columnOrder: string) {
    if (columnOrder != undefined && columnOrder != null && columnOrder != '' && columnOrder != "") {
      this.ColumnOrder = columnOrder;
    }
    if (order != undefined && order != null && order != '' && order != "") {
      this.Order = order;
    }
    this.request.get(Constants.API_AREA + Constants.API_GET_COUNT_PAGES + "?page=" + this.page + "&FilterCodArea=" + this.FilterCodArea + "&FilterNombre=" + this.FilterNombre + "&Order=" + this.Order + "&ColumnOrder=" + this.ColumnOrder).subscribe(resp => {
      this.CountPages = resp * 10;
    });
  }
  GetAllWithoutFilters() {
    this.request.get(Constants.API_AREA + Constants.API_GET_COUNT_FILTER_ELEMENTS + "?page=0").subscribe(resp => {
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
    this.request.get(Constants.API_AREA + Constants.API_GET_COUNT_FILTER_ELEMENTS + "?page=" + this.page + "&FilterCodArea=" + this.FilterCodArea + "&FilterNombre=" + this.FilterNombre + "&Order=" + this.Order + "&ColumnOrder=" + this.ColumnOrder).subscribe(resp => {
      this.CountFilterElements = resp;
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
    this.request.get(Constants.API_AREA + Constants.API_GET_ALL + "?page=" + event + "&FilterCodArea=" + this.FilterCodArea + "&FilterNombre=" + this.FilterNombre + "&Order=" + this.Order + "&ColumnOrder=" + this.ColumnOrder).subscribe(resp => {
      this.areas = resp;
      this.loading = false;
    });
  }
  EditArea(id: number) {
    this.data.changeMessage(id + "");
    this.routes.navigate(['../dashboard/area']);
  }
  OpenModalDeleteArea(id: number, nombre: string, ModalDeleteArea: string) {
    this.idToDelete = id;
    this.NombreAreaToDelete = nombre;
    let notaGap = '';
    if (id == Constants.AREA_GAP) {
      notaGap = 'Si elimina  esta área  GAP no podrá ingresar o modificar Monto Total de los Proyectos';
    }
    swal({
      title: 'Eliminar',
      text: "¿Estás seguro que deseas Eliminar el área: " + this.NombreAreaToDelete + "? " + notaGap,
      type: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      preConfirm: () => [this.DeleteArea()],
      confirmButtonText: 'Si, Eliminar!',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
    });
    //this.modalService.open(ModalDeleteArea);
  }
  DeleteArea() {
    this.modalService.dismissAll();
    this.request.delete(Constants.API_AREA + Constants.API_DELETE + "?id=" + this.idToDelete).subscribe(resp => {
      if (resp.code == 501) {
        swal(
          '',
          resp.error,
          'warning'
        )
      } else if (resp.code == 200) {
        this.GetAll("desc", "idArea", 1);
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

  NewArea() {
    this.data.changeMessage("0");
    this.routes.navigate(['../dashboard/area']);
  }
}
