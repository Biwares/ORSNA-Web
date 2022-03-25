import { Component, OnInit, Input } from '@angular/core';
import { DOCUMENT, formatNumber, formatCurrency } from '@angular/common';
import { Router } from '@angular/router';
import { Request } from '../../services/request';
import { Subject } from 'rxjs';
import { Validators, FormBuilder, FormGroup, FormControl } from '@angular/forms';
import { DataService } from '../../services/data.service';
import { NgbModal, NgbModalConfig } from '@ng-bootstrap/ng-bootstrap';
import { Constants } from '../../services/constants';
import { SwalPartialTargets } from '@toverux/ngx-sweetalert2';

import { forEach } from '@angular/router/src/utils/collection';

import { DataFile } from '../../Models/DataFile';
import swal from 'sweetalert2';

@Component({
  selector: 'app-proyectos',
  templateUrl: './proyectos.component.html',
  styleUrls: ['./proyectos.component.css']
})
export class ProyectosComponent implements OnInit {
  //////////////////////ALERTAS////////////////////
  @Input() DataFile: DataFile;
  private _info = new Subject<string>();
  staticAlertClosed = false;
  infoMessage: string;
  ProvinciasSelect: any;
  newProyecto: FormGroup;
  formFiles: FormGroup;
  provincias: any;
  areas: any;
  aeropuertos: any = [];
  cuentas: any;
  estados: any;
  provinciasSelect: any;
  dropdownSettings: any;
  idsProvinciaSelect: string = "";
  proyectos: any;
  IdProyecto: string = '0';
  idNewProyecto: string = '0';
  misAeros: any = [];
  misAdjuntos: any;
  tipoAnexo: string = "";
  modificarProyecto: boolean = true;
  nameAdj2: string = '';
  nameAdj1: string = '';
  nameAdj3: string = '';
  nameAdjOtro: string = '';
  loading: boolean = false;
  idArea: string;
  idCuenta: string;
  enableAdj2: boolean = true;
  enableAdj1: boolean = true;
  nombreModulo: string = "";
  validation_messages = {
    'Nombre': [
      { type: 'required', message: 'requerido' }
    ],
    'Descripcion': [
      { type: 'required', message: 'requerido' }
    ],
    'IdCuenta': [
      { type: 'required', message: 'requerido' }
    ],
    'IdProyecto': [
      { type: 'required', message: 'requerido' },
      { type: 'minlength', message: 'debe tener minimo 3 caracteres' },
      { type: 'maxlength', message: 'debe tener maximo 9 caracteres' },
      { type: 'pattern', message: 'solo debe contener numeros' }
    ],
    'IdArea': [
      { type: 'required', message: 'requerido' }
    ],
    'NroContratacion': [
      { type: 'required', message: 'requerido' },
      { type: 'minlength', message: 'debe tener minimo 3 caracteres' },
      { type: 'maxlength', message: 'debe tener maximo 9 caracteres' },
      { type: 'pattern', message: 'solo debe contener numeros' }
    ],
    'NroObra': [
      { type: 'required', message: 'requerido' }
    ],
    'CodObra': [
      { type: 'required', message: 'requerido' }
    ],
    'NroPago': [
      { type: 'required', message: 'requerido' }
    ],
    'NormaLegal': [
      { type: 'required', message: 'requerido' }
    ],
    'Objeto': [
      { type: 'required', message: 'requerido' }
    ],
    'MontoTotal': [
      { type: 'required', message: 'requerido' },
      { type: 'min', message: 'verique monto' }
    ],
    'IdEstadoProyecto': [
      { type: 'required', message: 'requerido' }
    ],
    'TipoEstado': [
      { type: 'required', message: 'requerido' }
    ],
    'Provincias': [
      { type: 'required', message: 'requerido' }
    ],
    'Adjunto2': [
      { type: 'required', message: 'requerido' }
    ]
  };
  constructor(private request: Request
    , private routes: Router
    , private formBuilder: FormBuilder
    , private data: DataService
    , private modalService: NgbModal
    , private config: NgbModalConfig
    , public readonly swalTargets: SwalPartialTargets) {
    // customize default values of modals used by this component tree
    config.backdrop = 'static';
    config.keyboard = false;

  }

  ngOnInit() {
    if (this.IdProyecto == '0' || this.IdProyecto == '') {
      this.request.get("/Provincia").subscribe(resp => {
        this.provincias = resp.data;
      });
      this.request.get("/area").subscribe(resp => {
        this.areas = resp.data;
      });
      this.request.get("/cuenta").subscribe(resp => {
        this.cuentas = resp.data;
      });
      this.request.get("/proyecto/getEstados").subscribe(resp => {
        this.estados = resp;
      });
      //this.request.get("/aeropuerto").subscribe(resp => {
      //  (resp.data).forEach((aeroTodos, indexM) => {
      //    this.aeropuertos.push({
      //      'id': aeroTodos.id, 'nombre': aeroTodos.nombre, 'fechainicio': aeroTodos.fechainicio, 'fechafin': aeroTodos.fechafin, 'nombreCorto': aeroTodos.nombreCorto, 'codIata': aeroTodos.codIata, 'check': false
      //    });
      //  });
      //});
    }

    this.dropdownSettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'nombre',
      itemsShowLimit: 3,
      allowSearchFilter: true,
      enableCheckAll: false,
      searchPlaceholderText: 'Buscar'
    };

    this.data.currentMessage.subscribe(message => {
      this.IdProyecto = message;
      if (this.IdProyecto != '0' && this.IdProyecto != '')
        this.nombreModulo = "Editar";
      else
        this.nombreModulo = "Alta de";
    });

    this.resetForm();
    this.ResetFormFiles();

    if (this.IdProyecto != '0' && this.IdProyecto != '')
      this.request.get("/proyecto/GetProyectoById?idProyecto=" + this.IdProyecto).subscribe(pro => {
        this.enableAdj2 = false;

        this.misAdjuntos = pro.adjuntos;
        this.idArea = (pro.area.id).toString();
        this.idCuenta = pro.cuenta.id;
        this.request.get("/area").subscribe(areas => {
          this.areas = areas.data;
          this.misAeros = pro.aeropuertos;
          this.request.get("/cuenta").subscribe(cue => {
            this.cuentas = cue.data;
            this.request.get("/proyecto/getEstados").subscribe(est => {
              this.estados = est;

              this.request.get('/cuenta/GetAerosToCuenta?idCuenta=' + this.idCuenta).subscribe(aer => {
                this.aeropuertos = [];
                let aeroTodos = aer;
                //let mAeros = [];
                (aeroTodos).forEach((itemT, indexT) => {
                  let check = false;
                  (pro.aeropuertos).forEach((itemA, indexT) => {
                    if (itemT.id == itemA.id)
                      check = true;
                  });
                  this.aeropuertos.push({
                    'id': itemT.id, 'nombre': itemT.nombre, 'fechainicio': itemT.fechainicio, 'fechafin': itemT.fechafin, 'nombreCorto': itemT.nombreCorto, 'codIata': itemT.codIata, 'check': check
                  });
                });
              });
            });
            ///validaciones para adjuntos///////////////////////////
            (pro.adjuntos).forEach((item, index) => {
              if (item.tipoAnexo == "2")
                this.nameAdj2 = (item.nombreArchivo).toString();
              if (item.tipoAnexo == "1") {
                this.nameAdj1 = item.nombreArchivo.toString();
                this.enableAdj1 = false;
              }
            });
            //////////fin de validaciones/////////////////////////
            this.newProyecto = this.formBuilder.group({
              Id: [pro.id, Validators.required],
              Nombre: [pro.nombre, Validators.required],
              Descripcion: [pro.descripcion, Validators.required],
              IdCuenta: [this.idCuenta, Validators.required],
              IdProyecto: [pro.idProyecto, [
                /*Validators.maxLength(9),
                Validators.minLength(3),
                Validators.pattern('[0-9]+'),*/
                Validators.required
              ]],
              IdArea: [this.idArea, Validators.required],
              NroContratacion: [pro.nroContratacion, [
                /*Validators.maxLength(9),
                Validators.minLength(3),
                Validators.pattern('[0-9]+'),*/
                Validators.required
              ]],
              NroObra: [pro.nroObra, Validators.required],
              CodObra: [pro.codObra, Validators.required],
              NroPago: [pro.nroPago, Validators.required],
              NormaLegal: [pro.normaLegal, Validators.required],
              Objeto: [pro.objeto/*, Validators.required*/],
              MontoTotal: [pro.montoTotal, [
                Validators.min(1),
                Validators.required
              ]],
              IdEstadoProyecto: [pro.idEstadoProyecto, Validators.required],
              Aeropuertos: [],
              TipoEstado: [],
              Provincias: [],
              Adjunto2: [''],
              Adjunto1: [''],
              Adjuntos: []
            });
          });
        });
      });
  }


  ////////////////////////////MODAL ADDAERO//////////////////////////////////////

  OpenModalAddAero(ModalAddAero: string) {

    this.modalService.open(ModalAddAero, { size: 'lg' });

  }
  SelectAero(idAero: number, nombre: string) {
    let idd = false;
    this.misAeros.forEach((item, index) => {
      if (item.id === idAero) {
        this.misAeros.splice(index, 1);
        idd = true;
      }
    });
    if (idd == false)
      this.misAeros.push({ 'id': idAero, 'nombre': nombre });
  }

  ////////////////////////////////////////FIN MODALADDAERO/////////////////////////////////

  ///////////////////////////////////// select dropdow ////////////////////////////////////
  onItemSelectProvincia(item: any) {
    this.idsProvinciaSelect += "," + item.id;
  }
  onItemDeSelectProvincia(item: any) {
    let array = this.idsProvinciaSelect.split(',');
    this.idsProvinciaSelect = "";
    for (var i = 0; i < array.length; i++) {
      if (!(array[i] == item.id) && array[i] != "") {
        this.idsProvinciaSelect = this.idsProvinciaSelect + "," + array[i];
      }
    }
  }
  ///////////////////////////////// FIN SELECT DROPDOWN/////////////////////////////////
  //////////////////////////////VALIDACIONES DE FORMULARIO/////////////////////////////
  resetForm() {
    // proyecto details form validations
    this.newProyecto = this.formBuilder.group({
      Id: [0, Validators.required],
      Nombre: ['', Validators.required],
      Descripcion: ['', Validators.required],
      IdCuenta: ['', Validators.required],
      IdProyecto: ['', [
        /*Validators.maxLength(9),
        Validators.minLength(3),
        Validators.pattern('[0-9]+'),*/
        Validators.required
      ]],
      IdArea: ['', Validators.required],
      NroContratacion: ['', [/*
        Validators.maxLength(9),
        Validators.minLength(3),
        Validators.pattern('[0-9]+'),*/
        Validators.required
      ]],
      NroObra: ['', Validators.required],
      CodObra: ['', Validators.required],
      NroPago: ['', Validators.required],
      NormaLegal: ['', Validators.required],
      Objeto: [''/*, Validators.required*/],
      MontoTotal: [0, [
        Validators.min(1),
        Validators.required,
      ]],
      IdEstadoProyecto: ['', Validators.required],
      Aeropuertos: [],
      TipoEstado: [],
      Provincias: [],
      Adjunto2: [''],
      Adjunto1: [''],
      Adjuntos: []
    });
  }

  //////////////////////////FIN VALIDACIONES//////////////////////////////////////////////
  changeCuenta(idCuenta) {
    this.aeropuertos = [];
    this.misAeros = [];
    this.request.get('/cuenta/GetAerosToCuenta?idCuenta=' + idCuenta).subscribe(res => {
      (res).forEach((aeroTodos, indexM) => {
        this.aeropuertos.push({
          'id': aeroTodos.id, 'nombre': aeroTodos.nombre, 'fechainicio': aeroTodos.fechainicio, 'fechafin': aeroTodos.fechafin, 'nombreCorto': aeroTodos.nombreCorto, 'codIata': aeroTodos.codIata, 'check': false
        });
      });
    });
  }
  SaveProyecto(data) {
    this.loading = true;
    data.Cuenta = { id: data.IdCuenta };
    data.Area = { id: data.IdArea };
    data.aeropuertos = this.misAeros;
    this.request.post("/proyecto/save", data).subscribe(resp => {
      if (resp.code == 200) {
        this.idNewProyecto = resp.result;
        if (this.modificarProyecto == true) {
          this.SaveFile();
        } else {
          this.infoMessage = "La información se guardó con éxito";
          swal({
            position: 'center',
            type: 'success',
            title: this.infoMessage,
            showConfirmButton: true,
            preConfirm: () => [this.returnList()],
            allowOutsideClick: false,
          });
          this.loading = false;
        }
      } else {
        this.infoMessage = resp.error;
        this.loading = false;
      }
    });
  }
  returnList() {
    this.data.changeMessage("0");
    this.routes.navigate(['../dashboard/proyectos']);
    this.modalService.dismissAll();
  }
  /////////////////////////////////////FILES ////////////////////////////////////////////
  validarAnexo() {
    if (this.modificarProyecto == true) {
      this.nameAdj1 = " ";
      this.modificarProyecto = false;
    }
    else {
      this.modificarProyecto = true;
      this.nameAdj1 = "";
    }
    let anexo1Existe = false;
    let anexo2Existe = false;
    this.misAdjuntos.forEach((item, index) => {
      if (item.tipoAnexo == "2")
        anexo1Existe = true;
      if (item.tipoAnexo == "1")
        anexo2Existe = true;
    });
    if (anexo1Existe)
      this.nameAdj2 = " ";
    if (anexo2Existe)
      this.nameAdj1 = " ";

  }
  SaveFile() {
    this.loading = true;
    this.request
      .createDataFile('/ProyectoAdjuntos', this.prepareSaveFile())
      .subscribe(resp => {
        if (resp != null) {
          this.infoMessage = "La información se guardó con éxito";
          swal({
            position: 'center',
            type: 'success',
            title: this.infoMessage,
            showConfirmButton: true,
            preConfirm: () => [this.returnList()],
            allowOutsideClick: false,
          });
          this.loading = false;
        }
        else {
          this.infoMessage = "se perdieron datos al guardar: Anexos";
          this.loading = false;
        }
      });
  }
  SaveFileOtro() {
    this.request
      .createDataFile('/ProyectoAdjuntos', this.prepareSaveFile())
      .subscribe(resp => {
        if (resp != null) {
          this.infoMessage = "Se agrego un Anexo al proyecto";
          this.loading = false;
          this.misAdjuntos.push({ "id": resp.id, "hash": resp.hash, "modulo": resp.modulo, "fechaAlta": resp.fechaAlta, "nombreArchivo": resp.nombreArchivo, "tipoAnexo": resp.tipoAnexo });
        }
        else {
          this.infoMessage = "se perdieron datos al guardar: Anexos";
          this.loading = false;
        }
      });
  }
  prepareSaveFile(): FormData {
    const formModel = this.formFiles.value;

    let formData = new FormData();
    formData.append('id', formModel.id);
    formData.append('idEntidad', this.idNewProyecto);
    formData.append('archivo', formModel.archivo);
    formData.append('tipoAnexo', this.tipoAnexo);

    return formData;
  }
  ResetFormFiles() {
    this.nameAdj1 = "";
    this.nameAdj2 = "";
    this.nameAdjOtro = "";
    this.formFiles = new FormGroup(
      {
        id: new FormControl(0),
        idEntidad: new FormControl(this.IdProyecto),
        archivo: new FormControl(null)
      },
      { updateOn: 'submit' }
    );
  }
  deleteAdjunto(idAdjunto: number) {
    this.loading = true;
    this.request.delete("/ProyectoAdjuntos?id=" + idAdjunto).subscribe(resp => {

      this.request.get("/ProyectoAdjuntos/GetAdjuntosByProyecto?id=" + this.IdProyecto).subscribe(resp => {
        this.misAdjuntos = resp;
        this.loading = false;
      });
    });
  }
  getFileById(idAdjunto: string) {
    window.open(Constants.URL_BASE_WEB_API + '/File/GetFile?id=' + idAdjunto);
  }
  fileChangeAdjunto2(files: FileList) {
    this.tipoAnexo = "2";
    this.nameAdj1 = files[0].name;
    if (this.IdProyecto == "0")
      this.nameAdj2 = files[0].name;
    if (files && files[0].size > 0) {
      this.formFiles.patchValue({
        archivo: files[0]
      });
      this.newProyecto.patchValue({
        Adjunto2: files[0]
      });
    }
  }
  fileChangeAdjunto1(files: FileList) {
    this.tipoAnexo = "1";
    this.nameAdj1 = files[0].name;
    if (files && files[0].size > 0) {
      this.formFiles.patchValue({
        archivo: files[0]
      });
      this.newProyecto.patchValue({
        Adjunto1: files[0]
      });
    }
  }
  fileChangeAdjunto3(files: FileList) {
    this.loading = true;
    this.tipoAnexo = "3";
    this.nameAdj3 = files[0].name;
    if (files && files[0].size > 0) {
      this.formFiles.patchValue({
        archivo: files[0]
      });
      this.newProyecto.patchValue({
        Adjunto1: files[0]
      });
      this.idNewProyecto = this.IdProyecto;
      this.SaveFileOtro();
      this.nameAdj3 = "";
      this.ResetFormFiles();
      this.validarAnexo();
    }
  }
}

