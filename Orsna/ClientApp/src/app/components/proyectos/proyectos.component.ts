import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Request } from '../../services/request';
import { Subject } from 'rxjs';
import { Validators, FormBuilder, FormGroup, FormControl } from '@angular/forms';
import { DataService } from '../../services/data.service';
import { NgbModal, NgbModalConfig } from '@ng-bootstrap/ng-bootstrap';
import { Constants } from '../../services/constants';
import { SwalPartialTargets } from '@toverux/ngx-sweetalert2';
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
  cuentaSeleccionada: string = "cuenta";
  areas: any = [];
  destinos: any = [];
  aeropuertos: any = [];
  selectedAll: any;
  cuentas: any = [];
  estados: any;
  provinciasSelect: any;
  dropdownSettings: any;
  idsProvinciaSelect: string = "";
  proyectos: any;
  IdProyecto: string = '0';
  IdTentativo: string = '0';
  idNewProyecto: string = '0';
  misAeros: any = [];
  misAerosEnGrillaDeAlta: any = [];
  misAdjuntos: any;
  //tipoAnexo: string = "";
  modificarProyecto: boolean = true;
  modificarProyectoAnexo2: boolean = false;
  nameAdj2: string = '';
  nameAdj1: string = '';
  nameAdj3: string = '';
  nameAdjOtro: string = '';
  loading: boolean = false;
  idArea: string;
  idCuenta: string;
  nombreModulo: string = "";
  requiereAeropuerto: boolean = false;
  aeropuertoGrupoAnterior: number = 0;
  puedeEditarMonto: boolean = false;

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
    'IdArea': [
      { type: 'required', message: 'requerido' }
    ],
    'DestinosId': [
      { type: 'required', message: 'requerido' }
    ],
    'NroContratacion': [
      { type: 'minlength', message: 'debe tener minimo 3 caracteres' },
      { type: 'maxlength', message: 'debe tener maximo 9 caracteres' },
      { type: 'pattern', message: 'solo debe contener numeros' }
    ],
    'Objeto': [
      { type: 'required', message: 'requerido' }
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
    ],
    'PagosImpuestosIncluidos': [
      { type: 'required', message: 'requerido' }
    ],
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

  transformAmount(element) {
    (<any>$)(element.target).number(true, 2, ',', '.');
  }

  ngOnInit() {
    this.request.get(Constants.API_PROYECTO + Constants.API_PUEDEEDITARMONTO).subscribe(est => {
      this.puedeEditarMonto = est;
    });

    if (this.IdProyecto == '0' || this.IdProyecto == '') {
      this.request.get(Constants.API_PROVINCIA).subscribe(resp => {
        this.provincias = resp.data;
      });
      this.request.get(Constants.API_AREA + Constants.API_GET_FILTRADAS_USUARIO).subscribe(resp => {
        this.areas = resp.data;
      });
      this.request.get(Constants.API_CUENTA).subscribe(resp => {
        this.cuentas = resp.data;
      });
      this.request.get(Constants.API_PROYECTO + Constants.API_GET_ESTADOS).subscribe(resp => {
        this.estados = resp;
      });

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

    if (this.IdProyecto != '0' && this.IdProyecto != '') {
      this.request.get(Constants.API_PROYECTO + Constants.API_GET_PROYECTO_ID + "?idProyecto=" + this.IdProyecto).subscribe(pro => {

        this.IdTentativo = pro.codigo;
        var cuentaSeleccionada = "";

        this.aeropuertoGrupoAnterior = pro.cuenta.aeropuertosGrupo.id;
        this.misAdjuntos = pro.adjuntos;
        this.idArea = (pro.area.idArea).toString();
        this.idCuenta = pro.cuenta.id;
        this.request.get(Constants.API_AREA + Constants.API_GET_FILTRADAS_USUARIO).subscribe(areas => {
          this.areas = areas.data;
          this.misAeros = pro.aeropuertos;
          this.request.get(Constants.API_CUENTA).subscribe(cue => {
            this.cuentas = cue.data;
            this.cuentas.forEach(function (element) {
              if (pro.cuenta.id == element.id)
                cuentaSeleccionada = element.descripcion;
            });

            this.cuentaSeleccionada = cuentaSeleccionada;
            this.requiereAeropuerto = pro.destinos.requiereAeropuerto;

            this.request.get(Constants.API_PROYECTO + Constants.API_GET_ESTADOS).subscribe(est => {
              this.estados = est;

              this.request.get(Constants.API_CUENTA + Constants.API_GET_AEROS_TO_CUENTA + '?idCuenta=' + this.idCuenta).subscribe(aer => {
                this.aeropuertos = [];
                let aeroTodos = aer;
                //let mAeros = [];
                (aeroTodos).forEach((itemT, indexT) => {
                  let check = false;
                  (pro.aeropuertos).forEach((itemA, indexT) => {
                    if (itemT.id == itemA.id) {
                      check = true;
                      this.misAerosEnGrillaDeAlta.push({
                        'id': itemT.id, 'nombre': itemT.nombre, 'fechainicio': itemT.fechainicio, 'fechafin': itemT.fechafin, 'nombreCorto': itemT.nombreCorto, 'codIata': itemT.codIata, 'selected': check
                      });
                    }
                  });
                  this.aeropuertos.push({
                    'id': itemT.id, 'nombre': itemT.nombre, 'fechainicio': itemT.fechainicio, 'fechafin': itemT.fechafin, 'nombreCorto': itemT.nombreCorto, 'codIata': itemT.codIata, 'selected': check
                  });
                  this.checkIfAllSelected();

                  this.request.get(Constants.API_PROYECTO + Constants.API_GET_DESTINOS + "?idCuenta=" + pro.cuenta.id).subscribe(resp => {
                    this.destinos = resp;
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
              }
            });
            //////////fin de validaciones/////////////////////////
            this.newProyecto = this.formBuilder.group({
              Id: [pro.id, Validators.required],
              Nombre: [pro.nombre, Validators.required],
              Descripcion: [pro.descripcion, Validators.required],
              IdCuenta: [this.idCuenta, Validators.required],
              IdProyecto: [pro.idProyecto,],
              IdArea: [this.idArea, Validators.required],
              DestinosId: [(pro.destinosId == 0) ? '' : pro.destinosId, Validators.required],
              NroContratacion: [pro.nroContratacion, [
                /*Validators.maxLength(9),
                Validators.minLength(3),
                Validators.pattern('[0-9]+'),*/
              ]],
              NroObra: [pro.nroObra,],
              CodObra: [pro.codObra],
              NroPago: [pro.nroPago,],
              Objeto: [pro.objeto/*, Validators.required*/],
              MontoTotal: [(<any>$).number(pro.montoTotal, 2, ',', '.')],
              MontoPagadoAniosAnteriores: [(<any>$).number(pro.montoPagadoAniosAnteriores, 2, ',', '.')],
              IdEstadoProyecto: [pro.idEstadoProyecto, Validators.required],
              Aeropuertos: [],
              TipoEstado: [],
              Provincias: [],
              Adjunto3: [''],
              Adjunto2: [''],
              Adjunto1: [''],
              Adjuntos: [],
              PagosImpuestosIncluidos: [pro.pagosImpuestosIncluidos,],
            });
          });
        });
      });
    } else {
      this.request.get(Constants.API_PROYECTO + Constants.API_GET_IDTENTATIVO).subscribe(resp => {
        this.IdTentativo = resp;
      });
    }
  }


  ////////////////////////////MODAL ADDAERO//////////////////////////////////////

  OpenModalAddAero(ModalAddAero: string) {

    this.modalService.open(ModalAddAero, { size: 'lg' });

  }
  SelectAeroTodos() {
    for (var i = 0; i < this.aeropuertos.length; i++) {
      let idd = -1;
      this.aeropuertos[i].selected = this.selectedAll;
      this.misAeros.forEach((item, index) => {
        if (item.id === this.aeropuertos[i].id) {
          idd = index;
          if (!this.selectedAll)
            this.misAeros.splice(idd, 1);
        }
      });
      if (this.selectedAll && idd < 0)
        this.misAeros.push({ 'id': this.aeropuertos[i].id, 'nombre': this.aeropuertos[i].nombre });
    }
  }
  checkIfAllSelected() {
    this.selectedAll = this.aeropuertos.length == this.misAeros.length && this.aeropuertos.length > 0;
  }
  SelectAero(itemT: any) {
    let idd = false;
    this.misAeros.forEach((item, index) => {
      if (item.id === itemT.id) {
        this.misAeros.splice(index, 1);
        idd = true;
      }
    });
    if (idd == false)
      this.misAeros.push({ 'id': itemT.id, 'nombre': itemT.nombre, 'fechainicio': itemT.fechainicio, 'fechafin': itemT.fechafin, 'nombreCorto': itemT.nombreCorto, 'codIata': itemT.codIata, 'selected': true });

    this.checkIfAllSelected();
  }
  SeleccionarAeropuertos() {
    this.misAeros = [];

    (this.aeropuertos).forEach((itemT, indexT) => {
      var check = false;
      (this.misAerosEnGrillaDeAlta).forEach((itemA, indexA) => {
        if (itemT.id == itemA.id) {
          check = true;
          this.misAeros.push({ 'id': itemT.id, 'nombre': itemT.nombre, 'fechainicio': itemT.fechainicio, 'fechafin': itemT.fechafin, 'nombreCorto': itemT.nombreCorto, 'codIata': itemT.codIata, 'selected': true });
        }
      });
      this.aeropuertos.splice(indexT, 1, { 'id': itemT.id, 'nombre': itemT.nombre, 'fechainicio': itemT.fechainicio, 'fechafin': itemT.fechafin, 'nombreCorto': itemT.nombreCorto, 'codIata': itemT.codIata, 'selected': check });
    });
  }
  GuardarAeropuertos() {
    this.misAerosEnGrillaDeAlta = [];
    this.misAeros.forEach((itemT, index) => {
      this.misAerosEnGrillaDeAlta.push(itemT);
    });
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
      IdProyecto: ['',],
      IdArea: ['', Validators.required],
      DestinosId: ['', Validators.required],
      NroContratacion: ['', [/*
        Validators.maxLength(9),
        Validators.minLength(3),
        Validators.pattern('[0-9]+'),*/
      ]],
      NroObra: ['',],
      CodObra: [''],
      NroPago: ['',],
      Objeto: [''/*, Validators.required*/],
      MontoTotal: ['0'],
      MontoPagadoAniosAnteriores: ['0'],
      IdEstadoProyecto: ['', Validators.required],
      Aeropuertos: [],
      TipoEstado: [],
      Provincias: [],
      Adjunto3: [''],
      Adjunto2: [''],
      Adjunto1: [''],
      Adjuntos: [],
      PagosImpuestosIncluidos: [false],
    });
  }

  //////////////////////////FIN VALIDACIONES//////////////////////////////////////////////
  changeCuenta(args) {
    var idCuenta = args.target.value;
    var idCuentaNumber = +idCuenta;
    var cuentaObjeto = this.cuentas.find(x => x.id === idCuentaNumber);
    this.cuentaSeleccionada = cuentaObjeto.descripcion;

    if (cuentaObjeto.aeropuertosGrupo.id != this.aeropuertoGrupoAnterior) {
      if (cuentaObjeto.aeropuertosGrupo.nombre != "SNA") {
        this.misAeros = [];
        this.misAerosEnGrillaDeAlta = [];
      }
      this.aeropuertos = [];
      this.request.get(Constants.API_CUENTA + Constants.API_GET_AEROS_TO_CUENTA + '?idCuenta=' + idCuenta).subscribe(res => {

        (res).forEach((aeroTodos, indexM) => {
          let check = false;
          (this.misAeros).forEach((itemA, indexT) => {
            if (aeroTodos.id == itemA.id)
              check = true;
          });
          this.aeropuertos.push({
            'id': aeroTodos.id, 'nombre': aeroTodos.nombre, 'fechainicio': aeroTodos.fechainicio, 'fechafin': aeroTodos.fechafin, 'nombreCorto': aeroTodos.nombreCorto, 'codIata': aeroTodos.codIata, 'selected': check
          });
        });
        this.checkIfAllSelected();

      });
    }
    this.aeropuertoGrupoAnterior = cuentaObjeto.aeropuertosGrupo.id;
    this.request.get(Constants.API_PROYECTO + Constants.API_GET_DESTINOS + "?idCuenta=" + idCuenta).subscribe(resp => {
      this.destinos = resp;
      if (this.destinos.length == 1) {
        this.newProyecto.controls['DestinosId'].setValue(this.destinos[0].id);
        this.requiereAeropuerto = this.destinos[0].requiereAeropuerto;
      }
    });
  }
  changeDestino(args) {
    var idDestino = args.target.value;
    if (idDestino == 0) {
      return;
    }
    var requiere = this.requiereAeropuerto;
    this.destinos.forEach(function (element) {
      if (element.id == idDestino) {
        requiere = element.requiereAeropuerto;
      }
    });
    this.requiereAeropuerto = requiere;
  }
  SaveProyecto(data) {
    this.loading = true;
    let error = false;
    let montoTotal = this.parseCurrencyNumber(data.MontoTotal);
    let montoPagadoAniosAnteriores = this.parseCurrencyNumber(data.MontoPagadoAniosAnteriores);

    if (montoTotal == 0 && montoPagadoAniosAnteriores != 0) {
      error = true;
    } else if (montoTotal != 0 && montoPagadoAniosAnteriores != 0 && montoPagadoAniosAnteriores > montoTotal) {
      error = true;
    }
    if (error) {
      swal({
        position: 'center',
        type: 'error',
        title: "El monto pagado años anteriores debe ser menor al monto total del proyecto",
        showConfirmButton: true, 
        allowOutsideClick: false,
      });
      this.loading = false;
    } else {
      data.Cuenta = { id: data.IdCuenta, descripcion: this.cuentaSeleccionada };
      data.Area = { IdArea: data.IdArea };
      data.aeropuertos = this.misAeros;
      this.request.post(Constants.API_PROYECTO + Constants.API_SAVE, data).subscribe(resp => {
        if (resp.code == 200) {
          this.idNewProyecto = resp.result;
          if (this.modificarProyecto == true || this.modificarProyectoAnexo2 == true ) {
            const formModel = this.formFiles.value;
            if (formModel.tipoAnexo1 == true) {
              this.SaveFile(1);
            }
            if (formModel.tipoAnexo2 == true) {
              this.SaveFile(2);
            }
            if (formModel.tipoAnexo3 == true) {
              this.SaveFile(3);
            }
          }  
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

        } else {
          this.infoMessage = resp.error;
          this.loading = false;
        }
      });
    }
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

  validarAnexo2() {
    if (this.modificarProyectoAnexo2 == true) {
      this.nameAdj2 = " ";
      this.modificarProyectoAnexo2 = false;
    }
    else {
      this.modificarProyectoAnexo2 = true;
      this.nameAdj2 = "";
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

  SaveFile(tipo: number) {
    this.loading = true;
    this.request
      .createDataFile('/ProyectoAdjuntos', this.prepareSaveFile(tipo))
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
      .createDataFile('/ProyectoAdjuntos', this.prepareSaveFile(3))
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
  prepareSaveFile(tipo: number): FormData {
    const formModel = this.formFiles.value;

    let formData = new FormData();
    formData.append('id', formModel.id);
    formData.append('idEntidad', this.idNewProyecto);
    formData.append('archivo', formModel['archivo' + tipo.toString()]);
    formData.append('tipoAnexo', tipo.toString());

    return formData;
  }
  ResetFormFiles() {
    this.nameAdj1 = "";
    this.nameAdj2 = "";
    this.nameAdj3 = "";
    this.nameAdjOtro = "";
    this.formFiles = new FormGroup(
      {
        id: new FormControl(0),
        idEntidad: new FormControl(this.IdProyecto),
        archivo1: new FormControl(null),
        archivo2: new FormControl(null),
        archivo3: new FormControl(null),
        tipoAnexo1: new FormControl(false),
        tipoAnexo2: new FormControl(false),
        tipoAnexo3: new FormControl(false),
        nameAdj1: new FormControl(""),
        nameAdj2: new FormControl(""),
        nameAdj3: new FormControl(""),
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
    
    var fileURL = Constants.URL_BASE_WEB_API + '/File/GetFile?id=' + idAdjunto;
    window.open(fileURL);
  }

  fileChangeAdjunto1(files: FileList) {
    
    this.nameAdj1 = files[0].name;

    if (files && files[0].size > 0) {
      this.formFiles.patchValue({
        archivo1: files[0],
        tipoAnexo1: true,
        nameAdj1: files[0].name
      });
      this.newProyecto.patchValue({
        Adjunto1: files[0]
      });
    }
  }

  fileChangeAdjunto2(files: FileList) {

    this.nameAdj2 = files[0].name;

    if (files && files[0].size > 0) {
      this.formFiles.patchValue({
        archivo2: files[0],
        tipoAnexo2: true,
        nameAdj2: files[0].name
      });
      this.newProyecto.patchValue({
        Adjunto2: files[0]
      });
    }
  }

  fileChangeAdjunto3(files: FileList) {
    this.loading = true;
    this.nameAdj3 = files[0].name;

    if (files && files[0].size > 0) {
      this.formFiles.patchValue({
        archivo3: files[0],
        tipoAnexo3: true,
        nameAdj3: files[0].name  
      });
      this.newProyecto.patchValue({
        Adjunto3: files[0]
      });
      this.idNewProyecto = this.IdProyecto;
      this.SaveFileOtro();
      this.nameAdj3 = "";
      this.ResetFormFiles();
      this.validarAnexo();
    }
  }

  parseCurrencyNumber(strg: string): number {
    strg = strg || "";
    var decimal = '.';
    strg = strg.replace(/[^0-9$.,]/g, '');
    if (strg.indexOf(',') > strg.indexOf('.')) decimal = ',';
    if ((strg.match(new RegExp("\\" + decimal, "g")) || []).length > 1) decimal = "";
    if (decimal !== "" && (strg.length - strg.indexOf(decimal) - 1 == 3) && strg.indexOf("0" + decimal) !== 0) decimal = "";
    strg = strg.replace(new RegExp("[^0-9$" + decimal + "]", "g"), "");
    strg = strg.replace(',', '.');
    return parseFloat(strg);
  }

}

