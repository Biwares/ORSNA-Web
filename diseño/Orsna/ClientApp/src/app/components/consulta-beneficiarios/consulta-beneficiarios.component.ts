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
  message: string = "";
  idToDelete: number;
  razonSocialToDelete: string;
  closeResult: string;
  AnexosBeneficiario: any = [];
  NombreModalAnexos: string="";
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

  ngOnInit() {
    this.GetAll(this.Order, this.ColumnOrder, 1);
  }
  GetCountPages(order: string, columnOrder: string) {
    if (columnOrder != undefined && columnOrder != null)
      this.ColumnOrder = columnOrder;
    if (order != undefined && order != null)
      this.Order = order;
    this.request.get("/proveedor/GetCountPages?page=" + this.page + "&FilterRazonSocial=" + this.FilterRazonSocial + "&FilterCuit=" + this.FilterCuit + "&FilterNacionalExtranjero=" + this.FilterNacionalExtranjero + "&Order=" + this.Order + "&ColumnOrder=" + this.ColumnOrder).subscribe(resp => {
      this.CountPages = resp * 10;
    });
  }
  GetAll(order: string, columnOrder: string, event: number) {
    this.page = event;
    if (columnOrder != undefined || columnOrder != null || columnOrder != '')
      this.ColumnOrder = columnOrder;
    if (order != undefined || order != null || order != '')
      this.Order = order;
    this.request.get("/proveedor/getall?page=" + event + "&FilterRazonSocial=" + this.FilterRazonSocial + "&FilterCuit=" + this.FilterCuit + "&FilterNacionalExtranjero=" + this.FilterNacionalExtranjero + "&Order=" + this.Order + "&ColumnOrder=" + this.ColumnOrder).subscribe(resp => {
      this.proveedores = resp;
    });
    this.GetCountPages(this.Order, this.ColumnOrder);
  }
  EditProveedor(id: number) {
    this.data.changeMessage(id + "");
    this.routes.navigate(['../dashboard/beneficiario']);
  }
  OpenModalDeleteProveedor(id: number, razonSocial: string) {
    this.idToDelete = id;
    swal({
      title: 'Eliminar',
      text: "¿Estás seguro de Eliminar el Beneficiario: " + razonSocial + '?',
      type: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      preConfirm: () => [this.DeleteProveedor()],
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
    })
    this.razonSocialToDelete = razonSocial;
    //this.modalService.open(ModalDeleteProveedor);
  }
  DeleteProveedor() {
    this.modalService.dismissAll();
    this.request.delete("/proveedor/Delete?id=" + this.idToDelete).subscribe(resp => {
      this.GetAll("desc", "Id", 1);
      this.page = 1;
      this.modalService.dismissAll();
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
