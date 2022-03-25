import { Component, OnInit, ViewEncapsulation, Inject, Input } from '@angular/core';
import { DOCUMENT } from '@angular/common';
import { Router } from '@angular/router';
import { Request } from '../../services/request';
import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { NgbModal, NgbModalConfig } from '@ng-bootstrap/ng-bootstrap';
import swal from 'sweetalert2';

import { Validators, FormBuilder, FormGroup, FormControl } from '@angular/forms';
import { DataService } from '../../services/data.service';
import { Constants } from '../../services/constants';


@Component({
  selector: 'app-rol',
  templateUrl: './rol.component.html',
  styleUrls: ['./rol.component.css']
})
export class RolComponent implements OnInit {
  //////////////////////ALERTAS////////////////////
  private _info = new Subject<string>();
  staticAlertClosed = false;
  infoMessage: string;
  newRol: FormGroup;

  idRol: string = '0';
  rol: any;
  titulo: string = "";
  Modulos: any = [];
  ModulosPorRol: any = [];
  
  validation_messages = {
    'Nombre': [
      { type: 'required', message: 'requerido' }
    ]
  };

  constructor(
    private request: Request
    , private routes: Router
    , private formBuilder: FormBuilder
    , private data: DataService
    , private modalService: NgbModal
    , private config: NgbModalConfig) {
    // customize default values of modals used by this component tree
    config.backdrop = 'static';
    config.keyboard = false;
  }

  ngOnInit() {
    this.data.currentMessage.subscribe(message => this.idRol = message);

    if (this.idRol != '0' && this.idRol != '')
      this.request.get(Constants.API_ROL + Constants.API_GET_ROL_BY_ID + "?idRol=" + this.idRol).subscribe(resp => {
        this.newRol = this.formBuilder.group({
          Id: resp.id,
          Nombre: [resp.nombre, Validators.required]
        });
      });

    this.request.get(Constants.API_ROL + Constants.API_GET_MODULOS + "?idRol=0").subscribe(resp => {
      this.Modulos = resp;

      if (this.idRol != '0' && this.idRol != '') {
        this.request.get(Constants.API_ROL + Constants.API_GET_MODULOS + '?idRol=' + this.idRol).subscribe(resp => {
          this.ModulosPorRol = resp;
          (this.ModulosPorRol).forEach((itemPorRol, indexA) => {
            (this.Modulos).forEach((itemMod, indexB) => {
              if (itemMod.idModulo == itemPorRol.idModulo) {
                itemMod.ver = itemPorRol.ver;
                itemMod.editar = itemPorRol.editar;
                itemMod.eliminar = itemPorRol.eliminar;
              }
            });
          });
        });
      }
    });

    if (this.idRol == '0' || this.idRol == '')
      this.titulo = "Alta de Rol";
    else
      this.titulo = "Editar Rol";

    this.resetForm();
  }
  SaveRol(data, ModalConfirmSave: string) {
    data.modulos = this.Modulos;

    this.request.post(Constants.API_ROL + Constants.API_SAVE, data).subscribe(resp => {

      if (resp.code == 200) {
        this.idRol = '0';
        this.resetForm();
        this.infoMessage = "Los datos se guardaron con éxito";
        swal({
          position: 'center',
          type: 'success',
          title: this.infoMessage,
          showConfirmButton: true,
          preConfirm: () => [this.returnList()],
          allowOutsideClick: false,
        });
        //this.modalService.open(ModalConfirmSave);
      } else {
        this.infoMessage = resp.error;
      }
    });
  }
  Check(event) {
    let idd = false;

    this.Modulos.forEach((item, index) => {
      if (item.idModulo == event.target.value) {
        if (event.target.name.includes("ver")) {
          item.ver = event.target.checked;
        }
        if (event.target.name.includes("editar")) {
          item.editar = event.target.checked;
        }
        if (event.target.name.includes("eliminar")) {
          item.eliminar = event.target.checked;
        }
      }
    });
  }
  resetForm() {
    // cuenta details form validations
    this.newRol = this.formBuilder.group({
      Id: [0, Validators.required],
      Nombre: ['', Validators.required]
    });
  }
  private returnList() {
    this.data.changeMessage("0");
    this.routes.navigate(['../dashboard/roles']);
    this.modalService.dismissAll();
  }
  private formatDate(fecha: string) {

    if (fecha == undefined || fecha == "") return "";

    var part = fecha.split('T');
    return part[0];
  }


}
