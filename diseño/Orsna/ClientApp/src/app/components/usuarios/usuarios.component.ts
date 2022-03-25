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
  selector: 'app-usuarios',
  templateUrl: './usuarios.component.html',
  styleUrls: ['./usuarios.component.css']
})
export class UsuariosComponent implements OnInit {
  //////////////////////ALERTAS////////////////////
  private _info = new Subject<string>();
  staticAlertClosed = false;
  infoMessage: string;
  newUsuario: FormGroup;

  idUsuario: string = '0';
  Usuario: any;
  titulo: string = "";

  validation_messages = {
    'Email': [
      { type: 'required', message: 'requerido' },
      { type: 'minlength', message: 'el Email debe tener minimo 3 caracteres' },
      { type: 'maxlength', message: 'el Email debe tener maximo 9 caracteres' },
      { type: 'pattern', message: 'el Email solo debe contener numeros' }
    ],
    'Password': [
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
    this.data.currentMessage.subscribe(message => this.idUsuario = message);
    if (this.idUsuario != '0' && this.idUsuario != '')
      this.request.get("/usuario/GetUsuarioById?idUsuario=" + this.idUsuario).subscribe(resp => {
        this.newUsuario = this.formBuilder.group({
          Id: resp.id,
          Email: [resp.Email, [
            /*Validators.maxLength(9),
            Validators.minLength(3),
            Validators.pattern('[0-9]+'),*/
            Validators.required
          ]],
          Password: [resp.Password, Validators.required]
        });
      });

    if (this.idUsuario == '0' || this.idUsuario == '')
      this.titulo = "Alta de Usuario";
    else
      this.titulo = "Editar Usuario";
    this.resetForm();
  }
  SaveUsuario(data, ModalConfirmSave: string) {
    this.request.post("/usuario/save", data).subscribe(resp => {

      if (resp.code == 200) {
        this.idUsuario = '0';
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
    this.newUsuario = this.formBuilder.group({
      Id: [0, Validators.required],
      Email: ['', [
        /*Validators.maxLength(9),
        Validators.minLength(3),
        Validators.pattern('[0-9]+'),*/
        Validators.required
      ]],
      Password: ['', Validators.required]
    });
  }
  private returnList() {
    this.data.changeMessage("0");
    this.routes.navigate(['../dashboard/usuarios']);
    this.modalService.dismissAll();
  }
  private formatDate(fecha: string) {

    if (fecha == undefined || fecha == "") return "";

    var part = fecha.split('T');
    return part[0];
  }


}
