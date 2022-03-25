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
  selector: 'app-areas',
  templateUrl: './areas.component.html',
  styleUrls: ['./areas.component.css']
})
export class AreasComponent implements OnInit {
  //////////////////////ALERTAS////////////////////
  private _info = new Subject<string>();
  staticAlertClosed = false;
  infoMessage: string;
  newArea: FormGroup;

  idArea: string = '0';
  area: any;
  titulo: string = "";

  validation_messages = {
    'Codigo': [
      { type: 'required', message: 'requerido' },
      { type: 'minlength', message: 'el codigo debe tener minimo 3 caracteres' },
      { type: 'maxlength', message: 'el codigo debe tener maximo 9 caracteres' },
      { type: 'pattern', message: 'el codigo solo debe contener numeros' }
    ],
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
    this.data.currentMessage.subscribe(message => this.idArea = message);
    if (this.idArea != '0' && this.idArea != '')
      this.request.get(Constants.API_AREA + Constants.API_GET_AREA_BY_ID + "?idArea=" + this.idArea).subscribe(resp => {
        this.newArea = this.formBuilder.group({
          Id: resp.id,
          Codigo: [resp.codigo, [
            /*Validators.maxLength(9),
            Validators.minLength(3),
            Validators.pattern('[0-9]+'),*/
            Validators.required
          ]],
          Nombre: [resp.nombre, Validators.required]
        });
      });

    if (this.idArea == '0' || this.idArea == '')
      this.titulo = "Alta de Área";
    else
      this.titulo = "Editar Área";
    this.resetForm();
  }
  SaveArea(data, ModalConfirmSave: string) {
    this.request.post(Constants.API_AREA + Constants.API_SAVE, data).subscribe(resp => {

      if (resp.code == 200) {
        this.idArea = '0';
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
    this.newArea = this.formBuilder.group({
      Id: [0, Validators.required],
      Codigo: ['', [
        /*Validators.maxLength(9),
        Validators.minLength(3),
        Validators.pattern('[0-9]+'),*/
        Validators.required
      ]],
      Nombre: ['', Validators.required]
    });
  }
  private returnList() {
    this.data.changeMessage("0");
    this.routes.navigate(['../dashboard/areas']);
    this.modalService.dismissAll();
  }
  private formatDate(fecha: string) {

    if (fecha == undefined || fecha == "") return "";

    var part = fecha.split('T');
    return part[0];
  }


}
