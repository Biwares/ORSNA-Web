import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Request } from '../../services/request';

import { FormBuilder, Validators } from '@angular/forms';
import { DataService } from '../../services/data.service';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import swal from 'sweetalert2';
import { Constants } from '../../services/constants';


@Component({
  selector: 'app-consulta-beneficiarios',
  templateUrl: './consulta-beneficiarios.component.html',
  styleUrls: ['./consulta-beneficiarios.component.css']
})
export class ConsultaBeneficariosComponent implements OnInit {
  form;
  page: number = 1;
  FilterRazonSocial: string = "";
  FilterCuit: string = "";
  FilterNacionalExtranjero: string = "";
  Order: string = "desc";
  ColumnOrder: string = "Id";
  CountPages: number = 0;
  CountElements: number = 0;
  CountFilterElements: number = 0;
  message: string = "";
  idToDelete: number;
  razonSocialToDelete: string;
  closeResult: string;
  AnexosBeneficiario: any = [];
  NombreModalAnexos: string = "";
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
  proveedores: any;
  puedeEditar: boolean = false;
  puedeEliminar: boolean = false;

  ngOnInit() {
    this.request.get(Constants.API_SEGURIDAD + Constants.API_GET_PERMISOS + "?modulo=" + 'Beneficiarios').subscribe(resp => {
      this.puedeEditar = resp.editar;
      this.puedeEliminar = resp.eliminar;
    });

    this.FilterRazonSocial = "";
    this.FilterCuit = "";
    this.FilterNacionalExtranjero = "";
    this.CountPages= 0;
    this.CountElements = 0;
    this.CountFilterElements = 0;


    this.GetAll(this.Order, this.ColumnOrder, 1);
  }

  refresh(): void {
    this.ngOnInit();
  }

  GetAllWithoutFilters() {
    this.request.get(Constants.API_PROVEEDOR + Constants.API_GET_COUNT_FILTER_ELEMENTS + "?page=0").subscribe(resp => {
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
    this.request.get(Constants.API_PROVEEDOR + Constants.API_GET_COUNT_FILTER_ELEMENTS + "?page=" + this.page + "&FilterRazonSocial=" + this.FilterRazonSocial + "&FilterCuit=" + this.FilterCuit + "&FilterNacionalExtranjero=" + this.FilterNacionalExtranjero + "&Order=" + this.Order + "&ColumnOrder=" + this.ColumnOrder).subscribe(resp => {
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
    this.request.get(Constants.API_PROVEEDOR + Constants.API_GET_COUNT_PAGES + "?page=" + this.page + "&FilterRazonSocial=" + this.FilterRazonSocial + "&FilterCuit=" + this.FilterCuit + "&FilterNacionalExtranjero=" + this.FilterNacionalExtranjero + "&Order=" + this.Order + "&ColumnOrder=" + this.ColumnOrder).subscribe(resp => {
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
    this.request.get(Constants.API_PROVEEDOR + Constants.API_GET_ALL + "?page=" + event + "&FilterRazonSocial=" + this.FilterRazonSocial + "&FilterCuit=" + this.FilterCuit + "&FilterNacionalExtranjero=" + this.FilterNacionalExtranjero + "&Order=" + this.Order + "&ColumnOrder=" + this.ColumnOrder).subscribe(resp => {
      this.proveedores = resp.data;
      this.loading = false;
    });
  }
  EditProveedor(id: number) {
    this.data.changeMessage(id + "");
    this.routes.navigate(['../dashboard/beneficiario']);
  }
  OpenModalDeleteProveedor(id: number, razonSocial: string) {
    this.idToDelete = id;
    swal({
      title: 'Eliminar',
      text: "¿Estás seguro de Eliminar el Beneficiario: " + razonSocial + "?",
      type: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      preConfirm: () => [this.DeleteProveedor()],
      confirmButtonText: 'Sí, Eliminar!',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
    });
    this.razonSocialToDelete = razonSocial;
    //this.modalService.open(ModalDeleteProveedor);
  }
  DeleteProveedor() {
    this.modalService.dismissAll();
    this.request.delete(Constants.API_PROVEEDOR + Constants.API_DELETE + "?id=" + this.idToDelete).subscribe(resp => {
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

  NewProveedor() {
    this.data.changeMessage("0");
    this.routes.navigate(['../dashboard/beneficiario']);
  }

  /////////////////////////////////MODAL LISTA DE ANEXOS//////////////////////////////////////////////////////////////////
  getAdjuntosBeneficiario(id: number, nombreBeneficiario: string) {
    this.NombreModalAnexos = nombreBeneficiario;
    this.AnexosBeneficiario = [];
    this.request.get("/ProveedorAdjuntos/GetAdjuntosByBeneficiario?id=" + id.toString()).subscribe(resp => {
      this.AnexosBeneficiario = resp;
    });
  }
  getFileById(idAdjunto: string) {
    window.open(Constants.URL_BASE_WEB_API + '/File/GetFile?id=' + idAdjunto);
  }
  ///////////////////////////////////FIN MODAL ANEXOS////////////////////////////////////////////////////////////////////
}
