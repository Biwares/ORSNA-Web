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


@Component({
  selector: 'app-rolmodulo',
  templateUrl: './rolmodulo.component.html',
  styleUrls: ['./rolmodulo.component.css']
})
export class RolModuloComponent implements OnInit {
  //////////////////////ALERTAS////////////////////
  private _info = new Subject<string>();
  staticAlertClosed = false;
  infoMessage: string;
  newRolModulo: FormGroup;
  
  titulo: string = "";

  idRolModulo: string = '0';
  idRol: string = '0';
  idModulo: string = '0';

  roles: any;
  modulos: any;

  ver: string = "";
  editar: string = "";
  eliminar: string = "";

  validation_messages = {
    'idrol': [
      { type: 'required', message: 'requerido' }
    ],
    'idmodulo': [
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
    if (this.idRolModulo != '0' && this.idRolModulo != '')
      this.request.get("/rolmodulo/GetRolModuloById?idRolmodulo=" + this.idRolModulo).subscribe(resp => {
        this.newRolModulo = this.formBuilder.group({
          Id: resp.id,
          idrol: [resp.idrol, Validators.required],
          idmodulo: [resp.idmodulo, Validators.required]
        });
      });

    if (this.idRolModulo == '0' || this.idRolModulo == '')
      this.titulo = "Alta de RolModulo";
    else
      this.titulo = "Editar RolModulo";
    this.resetForm();
  }
  SaveRolModulo(data, ModalConfirmSave: string) {
    this.request.post("/rolmodulo/save", data).subscribe(resp => {

      if (resp.code == 200) {
        this.idRolModulo = '0';
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

    resetForm() {
    // cuenta details form validations
    this.newRolModulo = this.formBuilder.group({
      Id: [0, Validators.required],
      idrol: ['', [
         Validators.required
      ]],
      idmodulo: ['', Validators.required]
    });
  }
  private returnList() {
    this.data.changeMessage("0");
    this.routes.navigate(['../dashboard/rolesmodulo']);
    this.modalService.dismissAll();
  }
  private formatDate(fecha: string) {

    if (fecha == undefined || fecha == "") return "";

    var part = fecha.split('T');
    return part[0];
  }


}
