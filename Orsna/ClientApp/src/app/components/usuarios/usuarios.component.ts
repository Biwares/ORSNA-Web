import { Component, OnInit, Inject, Input } from '@angular/core';
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

export class RegistrationValidator {
  static validate(newUsuario: FormGroup) {
    let Password = newUsuario.controls.Password.value;
    let ConfirmPassword = newUsuario.controls.ConfirmPassword.value;

    if (ConfirmPassword.length <= 0) {
      return null;
    }

    if (ConfirmPassword !== Password) {
      return {
        doesMatchPassword: true
      };
    }

    return null;

  }
}

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
  passwordFormGroup: FormGroup;

  idUsuario: string = '0';
  Usuario: any;
  titulo: string = "";
  isDisabled: boolean = false;

  allAreas: any;
  misAreas: any = [];
  selAreas: any = [];
  areasSeleccionadasEnModal: any = [];

  allRoles: any;
  misRoles: any = [];
  selRoles: any = [];
  rolesSeleccionadosEnModal: any = [];


  validation_messages = {
    'Email': [
      { type: 'required', message: 'requerido' },
      { type: 'pattern', message: 'Formato de correo incorrecto.Ejemplo: usuario@dominio.com' },
    ],
    'Password': [
      { type: 'required', message: 'requerido' },
    ],
    'ConfirmPassword': [
      { type: 'required', message: 'requerido' },
    ],
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

    this.request.get(Constants.API_AREA).subscribe(resp => {
      this.allAreas = resp.data;
      if (this.idUsuario != '0' && this.idUsuario != '') {
        this.request.get(Constants.API_USUARIO + Constants.API_GET_AREAS + '?idUsuario=' + this.idUsuario).subscribe(resp => {

          this.misAreas = [];
          this.selAreas = [];
          this.areasSeleccionadasEnModal = [];

          let usuarioAreas = resp;

          (this.allAreas).forEach((itemT, indexT) => {
            let check = false;
            (usuarioAreas).forEach((itemA, indexT) => {
              if (itemT.idArea == itemA.idArea)
                check = true;
            });
            this.misAreas.push({ 'idArea': itemT.idArea, 'nombre': itemT.nombre, 'codigo': itemT.codigo, 'check': check, 'idusuario': this.idUsuario });

            if (check) {
              this.selAreas.push({ 'idArea': itemT.idArea, 'nombre': itemT.nombre, 'idusuario': this.idUsuario });
              this.areasSeleccionadasEnModal.push({ 'idArea': itemT.idArea, 'nombre': itemT.nombre, 'idusuario': this.idUsuario });
            }
          });
        });
      } else {
        (this.allAreas).forEach((itemT, indexT) => {
          this.misAreas.push({ 'idArea': itemT.idArea, 'nombre': itemT.nombre, 'codigo': itemT.codigo, 'check': false, 'idusuario': this.idUsuario });
        });
      }
    });

    this.request.get(Constants.API_ROL).subscribe(resp => {
      this.allRoles = resp.data;
      if (this.idUsuario != '0' && this.idUsuario != '') {
        this.request.get(Constants.API_USUARIO + Constants.API_GET_ROLES + '?idUsuario=' + this.idUsuario).subscribe(resp => {

          this.misRoles = [];
          this.selRoles = [];
          this.rolesSeleccionadosEnModal = [];
          let usuarioRoles = resp;

          (this.allRoles).forEach((itemT, indexT) => {
            let check = false;
            (usuarioRoles).forEach((itemA, indexT) => {
              if (itemT.idRol == itemA.idRol) {
                check = true;
              }
            });
            this.misRoles.push({ 'idRol': itemT.idRol, 'nombre': itemT.nombre, 'check': check, 'idusuario': this.idUsuario });

            if (check) {
              this.selRoles.push({ 'idRol': itemT.idRol, 'nombre': itemT.nombre, 'idusuario': this.idUsuario });
              this.rolesSeleccionadosEnModal.push({ 'idRol': itemT.idRol, 'nombre': itemT.nombre, 'idusuario': this.idUsuario });
            }
          });
        });
      } else {
        (this.allRoles).forEach((itemT, indexT) => {
          this.misRoles.push({ 'idRol': itemT.idRol, 'nombre': itemT.nombre, 'check': false, 'idusuario': this.idUsuario });
        });
      }
    });

    if (this.idUsuario != '0' && this.idUsuario != '') {

      this.request.get(Constants.API_USUARIO + Constants.API_GET_USUARIO_BY_ID + "?idUsuario=" + this.idUsuario).subscribe(resp => {

        this.passwordFormGroup = this.formBuilder.group({
          Password: [resp.password, Validators.required],
          ConfirmPassword: [resp.password, Validators.required]
        }, {
            validator: RegistrationValidator.validate.bind(this)
          });

        this.newUsuario = this.formBuilder.group({
          Id: resp.id,
          Email: [resp.email, Validators.compose([
            Validators.required,
            Validators.pattern('^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$')])],
          passwordFormGroup: this.passwordFormGroup,
          CheckAD: resp.checkAD,
        });
        

        this.isDisabled = this.newUsuario.controls.CheckAD.value;

        this.checkIsDisabledAD(this.isDisabled);
      });



    }

    if (this.idUsuario == '0' || this.idUsuario == '')
      this.titulo = "Alta de Usuario";
    else
      this.titulo = "Editar Usuario";

    this.resetForm();
  }
  SaveUsuario(data, ModalConfirmSave: string) {

    data.UsuarioRol = this.selRoles;
    data.UsuariosAreas = this.selAreas;
    data.Password = data.passwordFormGroup.Password;

    const invalid = [];
    const controls = this.newUsuario.controls;
    for (const name in controls) {
      if (controls[name].invalid) {
        invalid.push(name);
        console.log(name);
      }
    }

    this.request.post(Constants.API_USUARIO + Constants.API_SAVE, data).subscribe(resp => {

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
    this.passwordFormGroup = this.formBuilder.group({
      Password: ['', Validators.required],
      ConfirmPassword: ['', Validators.required]
    }, {
        validator: RegistrationValidator.validate.bind(this)
      });

    this.newUsuario = this.formBuilder.group({
      Email: ['', Validators.compose([
        Validators.required,
        Validators.pattern('^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$')])],
      passwordFormGroup: this.passwordFormGroup,
      CheckAD: false,
    });
    this.isDisabled = false;
    this.checkIsDisabledAD(this.isDisabled);
  }

  checkIsDisabledAD(parameter) {
    if (parameter) {
      this.newUsuario.get('passwordFormGroup').get('Password').clearValidators();
      this.newUsuario.get('passwordFormGroup').get('ConfirmPassword').clearValidators();
    } else {
      this.newUsuario.get('passwordFormGroup').get('Password').setValidators([Validators.required]);
      this.newUsuario.get('passwordFormGroup').get('ConfirmPassword').setValidators([Validators.required]);
    }

    this.newUsuario.get('passwordFormGroup').get('Password').updateValueAndValidity();
    this.newUsuario.get('passwordFormGroup').get('ConfirmPassword').updateValueAndValidity();
  }

  checkStatus(event) {
    this.isDisabled = event.target.checked;
    this.checkIsDisabledAD(this.isDisabled);
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
  OpenModalAddArea(ModalAddArea: string) {

    this.modalService.open(ModalAddArea, { size: 'lg' });

  }
  SelectArea(idArea: number, nombre: string) {
    let idd = false;
    this.areasSeleccionadasEnModal.forEach((item, index) => {
      if (item.idArea === idArea) {
        this.areasSeleccionadasEnModal.splice(index, 1);
        idd = true;
      }
    });
    if (idd == false)
      this.areasSeleccionadasEnModal.push({ 'idArea': idArea, 'nombre': nombre });
  }
  SeleccionarAreas() {
    this.areasSeleccionadasEnModal = [];

    (this.misAreas).forEach((itemT, indexT) => {
      var check = false;

      (this.selAreas).forEach((itemA, indexA) => {
        if (itemT.idArea == itemA.idArea) {
          check = true;
          this.areasSeleccionadasEnModal.push({ 'idArea': itemT.idArea, 'nombre': itemT.nombre, 'idusuario': this.idUsuario, 'codigo': itemT.codigo, 'check': true });
        }
      });
      this.misAreas.splice(indexT, 1, { 'idArea': itemT.idArea, 'nombre': itemT.nombre, 'idusuario': this.idUsuario, 'codigo': itemT.codigo, 'check': check  });
    });
  }
  GuardarAreas() {
    this.selAreas = [];
    this.areasSeleccionadasEnModal.forEach((itemT, index) => {
      this.selAreas.push({ 'idArea': itemT.idArea, 'nombre': itemT.nombre, 'idusuario': this.idUsuario });
    });
  }
  SelectRol(idRol: number, nombre: string) {
    let idd = false;
    this.rolesSeleccionadosEnModal.forEach((item, index) => {
      if (item.idRol === idRol) {
        this.rolesSeleccionadosEnModal.splice(index, 1);
        idd = true;
      }
    });
    if (idd == false)
      this.rolesSeleccionadosEnModal.push({ 'idRol': idRol, 'nombre': nombre });
  }
  SeleccionarRoles() {
    this.rolesSeleccionadosEnModal = [];

    (this.misRoles).forEach((itemT, indexT) => {
      var check = false;

      (this.selRoles).forEach((itemA, indexA) => {
        if (itemT.idRol == itemA.idRol) {
          check = true;
          this.rolesSeleccionadosEnModal.push({ 'idRol': itemT.idRol, 'nombre': itemT.nombre, 'idusuario': this.idUsuario, 'check': true });
        }
      });
      this.misRoles.splice(indexT, 1, { 'idRol': itemT.idRol, 'nombre': itemT.nombre, 'idusuario': this.idUsuario, 'check': check });
      
    });
  }
  GuardarRoles() {
    this.selRoles = [];
    this.rolesSeleccionadosEnModal.forEach((itemT, index) => {
      this.selRoles.push({ 'idRol': itemT.idRol, 'nombre': itemT.nombre, 'idusuario': this.idUsuario });
    });
  }
}
