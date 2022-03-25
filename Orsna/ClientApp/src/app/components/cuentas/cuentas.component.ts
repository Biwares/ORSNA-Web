import { Component, OnInit, ViewEncapsulation, Inject, Input } from '@angular/core';
import { DOCUMENT } from '@angular/common';
import { Router } from '@angular/router';
import { Request } from '../../services/request';
import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { NgbModal, NgbModalConfig } from '@ng-bootstrap/ng-bootstrap';

import { Validators, FormBuilder, FormGroup, FormControl } from '@angular/forms';
import { DataService } from '../../services/data.service';
import swal from 'sweetalert2';
import { Constants } from '../../services/constants';

@Component({
  selector: 'app-cuentas',
  templateUrl: './cuentas.component.html',
  styleUrls: ['./cuentas.component.css']
})
export class CuentasComponent implements OnInit {
  //////////////////////ALERTAS////////////////////
  private _info = new Subject<string>();
  staticAlertClosed = false;
  infoMessage: string;
  newCuenta: FormGroup;
  AeropuertosGrupos: any;
  LibranzaTipos: any;
  idCuenta: string = '0';
  cuenta: any;
  nombreModulo: string = "";

  validation_messages = {
    'NroCuenta': [
      { type: 'required', message: 'requerido' },
      { type: 'minlength', message: 'debe tener 20 caracteres' },
      { type: 'maxlength', message: 'debe tener 20 caracteres' },
      { type: 'pattern', message: 'solo debe contener números' }
    ],
    'Nombre': [
      { type: 'required', message: 'requerido' }
    ],
    'Descripcion': [
      { type: 'required', message: 'requerido' }
    ],
    'IdLibranzaTipo': [
      { type: 'required', message: 'requerido' }
    ],
    'IdAeropuertosGrupo': [
      { type: 'required', message: 'requerido' }
    ],
    'FechaVigencia': [
      { type: 'required', message: 'requerido' }
    ]
  };

  TipoCuenta = [
    "Ahorro",
    "Corriente",
    "Otro"
  ];

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
    this.data.currentMessage.subscribe(message => {
      this.idCuenta = message;
      if (this.idCuenta != '0' && this.idCuenta != '')
        this.nombreModulo = "Editar";
      else
        this.nombreModulo = "Alta de";
    });
    if (this.idCuenta != '0' && this.idCuenta != '') {
      this.request.get(Constants.API_LIBRANZA + Constants.API_GET_LIBRANZA_TIPO ).subscribe(lib => {
        this.LibranzaTipos = lib;
        this.request.get(Constants.API_AEROPUERTO + Constants.API_GET_AEROPUERTOS_GRUPO).subscribe(grup => {
          this.AeropuertosGrupos = grup;
          this.request.get(Constants.API_CUENTA + Constants.API_GET_CUENTA_BY_ID + "?idCuenta=" + this.idCuenta).subscribe(resp => {
            let fechaVigencia = this.formatDate(resp.fechaVigencia);
            this.newCuenta = this.formBuilder.group({
              Id: resp.id,
              NroCuenta: [resp.nroCuenta, [
                /*Validators.maxLength(20),
                Validators.minLength(20),
                Validators.pattern('[0-9]+'),*/
                Validators.required
              ]],
              Nombre: [resp.nombre, Validators.required],
              Descripcion: [resp.descripcion, Validators.required],
              IdLibranzaTipo: [resp.idLibranzaTipo, Validators.required],
              IdAeropuertosGrupo: [resp.idAeropuertosGrupo, Validators.required],
              FechaVigencia: [fechaVigencia]
            });
          });
        })
      });
    }
      this.resetForm();
  }
  SaveCuenta(data, ModalConfirmSave: string) {
    this.request.post(Constants.API_CUENTA + Constants.API_SAVE, data).subscribe(resp => {
      
      if (resp.code == 200) {
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

  private resetForm() {
    this.request.get(Constants.API_LIBRANZA + Constants.API_GET_LIBRANZA_TIPO).subscribe(lib => {
      this.LibranzaTipos = lib;
    });
    this.request.get(Constants.API_AEROPUERTO + Constants.API_GET_AEROPUERTOS_GRUPO).subscribe(grup => {
      this.AeropuertosGrupos = grup;
    });
    // cuenta details form validations
    this.newCuenta = this.formBuilder.group({
      Id: this.idCuenta,
      NroCuenta: ['', [
        /*Validators.maxLength(20),
        Validators.minLength(20),
        Validators.pattern('[0-9]+'),*/
        Validators.required
      ]],
      Nombre: ['', Validators.required],
      Descripcion: ['', Validators.required],
      IdLibranzaTipo: ['', Validators.required],
      IdAeropuertosGrupo: ['', Validators.required],
      FechaVigencia: ['']
    });
  }
  private returnList() {
    this.data.changeMessage("0");
    this.routes.navigate(['../dashboard/cuentas']);
    this.modalService.dismissAll();
  }
  private formatDate(fecha: string) {
    if (fecha == undefined || fecha == "") return "";
    var part = fecha.split('T');
    return part[0];
  }
}
