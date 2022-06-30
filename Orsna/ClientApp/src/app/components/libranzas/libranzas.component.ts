import { Component, OnInit, AfterViewInit, Input, ViewChild, ElementRef } from '@angular/core';
import { Router } from '@angular/router';
import { Request } from '../../services/request';
import { Validators, FormBuilder, FormGroup, FormControl } from '@angular/forms';
import { DataService } from '../../services/data.service';
import { NgbModal, NgbModalConfig } from '@ng-bootstrap/ng-bootstrap';
import { Constants } from '../../services/constants';
import { SwalPartialTargets } from '@toverux/ngx-sweetalert2';
import createNumberMask from 'text-mask-addons/dist/createNumberMask';

import swal from 'sweetalert2';

@Component({
  selector: 'app-libranzas',
  templateUrl: './libranzas.component.html',
  styleUrls: ['./libranzas.component.css']
})
export class LibranzasComponent implements OnInit {
  @ViewChild('uploader') uploader: any;
  newLibranza: FormGroup;
  titulo: string = "Nueva";
  newFacturaLibranza: FormGroup;
  validation_messages = {
    'IdLibranzaTipo': [
      { type: 'required', message: 'requerido' }
    ],
    'NormaLegal': [
      { type: 'required', message: 'requerido' }
    ],
    'NroLibranza': [
      { type: 'required', message: 'requerido' }
    ],
    'IdProvincia': [
      { type: 'required', message: 'requerido' }
    ],
    'IdProyecto': [
      { type: 'required', message: 'requerido' }
    ],
    'IdLibranzaEstados': [
      { type: 'required', message: 'requerido' }
    ],
    'TipoFondoReparo': [
      { type: 'required', message: 'requerido' },
      { type: 'minlength', message: 'debe tener minimo 3 caracteres' },
      { type: 'maxlength', message: 'debe tener maximo 9 caracteres' },
      { type: 'pattern', message: 'solo debe contener numeros' }
    ],
    'MontoFondoReparo': [
      { type: 'required', message: 'requerido' },
      { type: 'pattern', message: 'solo debe contener numeros' }
    ],
    'Objeto': [
      { type: 'required', message: 'requerido' }
    ],
    'Multa': [
      { type: 'required', message: 'requerido' },
      { type: 'pattern', message: 'solo debe contener numeros' }
    ],
    'Mora': [
      { type: 'required', message: 'requerido' },
      { type: 'pattern', message: 'solo debe contener numeros' }
    ],
    'NroEmbargo': [
      { type: 'required', message: 'requerido' },
      { type: 'minlength', message: 'debe tener minimo 3 caracteres' },
      { type: 'maxlength', message: 'debe tener maximo 9 caracteres' },
      { type: 'pattern', message: 'solo debe contener numeros' }
    ],
    'ResponsableEmbargo': [
      { type: 'required', message: 'requerido' },
      { type: 'minlength', message: 'debe tener minimo 3 caracteres' },
      { type: 'maxlength', message: 'debe tener maximo 9 caracteres' },
      { type: 'pattern', message: 'solo debe contener numeros' }
    ],
    'BeneficiarioEmbargo': [
      { type: 'required', message: 'requerido' },
      { type: 'minlength', message: 'debe tener minimo 3 caracteres' },
      { type: 'maxlength', message: 'debe tener maximo 9 caracteres' },
      { type: 'pattern', message: 'solo debe contener numeros' }
    ],
    'IdentificacionTributaria': [
      { type: 'required', message: 'requerido' },
      { type: 'minlength', message: 'debe tener minimo 3 caracteres' },
      { type: 'maxlength', message: 'debe tener maximo 9 caracteres' },
      { type: 'pattern', message: 'solo debe contener numeros' }
    ],
    'MontoEmbargo': [
      { type: 'required', message: 'requerido' },
      { type: 'minlength', message: 'debe tener minimo 3 caracteres' },
      { type: 'maxlength', message: 'debe tener maximo 9 caracteres' },
      { type: 'pattern', message: 'solo debe contener numeros' }
    ],
    'SaldoEmbargo': [
      { type: 'required', message: 'requerido' },
      { type: 'minlength', message: 'debe tener minimo 3 caracteres' },
      { type: 'maxlength', message: 'debe tener maximo 9 caracteres' },
      { type: 'pattern', message: 'solo debe contener numeros' }
    ],
    'NroEscritura': [
      { type: 'required', message: 'requerido' },
      { type: 'minlength', message: 'debe tener minimo 3 caracteres' },
      { type: 'maxlength', message: 'debe tener maximo 9 caracteres' },
      { type: 'pattern', message: 'solo debe contener numeros' }
    ],
    'MontoRestante': [
      { type: 'required', message: 'requerido' },
      { type: 'minlength', message: 'debe tener minimo 3 caracteres' },
      { type: 'maxlength', message: 'debe tener maximo 9 caracteres' },
      { type: 'pattern', message: 'solo debe contener numeros' }
    ],
    'Tipo': [
      { type: 'required', message: 'requerido' },
    ],
    'Nro': [
      { type: 'required', message: 'requerido' },
    ],
    'Fecha': [
      { type: 'required', message: 'requerido' },
    ],
    'Monto': [
      { type: 'required', message: 'requerido' },
      { type: 'pattern', message: 'solo debe contener numeros' }
    ],
    'Iva': [
      { type: 'required', message: 'requerido' },
      { type: 'pattern', message: 'solo debe contener numeros' }
    ],
    'Ibb': [
      { type: 'required', message: 'requerido' },
      { type: 'pattern', message: 'solo debe contener numeros' }
    ],
    'CesionTipoCuenta': [
      { type: 'required', message: 'requerido' }
    ],
    'CesionNroCuenta': [
      { type: 'required', message: 'requerido' },
      { type: 'minlength', message: 'debe tener 20 caracteres' },
      { type: 'maxlength', message: ' debe tener 20 caracteres' },
      { type: 'pattern', message: 'solo debe contener numeros' }
    ],
    'CesionNombreBanco': [
      { type: 'required', message: 'requerido' }
    ],
    'CesionSucursal': [
      { type: 'required', message: 'requerido' }
    ],
    'CesionCbu': [
      { type: 'required', message: 'requerido' },
      { type: 'pattern', message: 'solo debe contener numeros' },
      { type: 'minlength', message: 'debe tener 22 caracteres' },
      { type: 'maxlength', message: ' debe tener 22 caracteres' }
    ],
    'CesionCuit': [
      { type: 'required', message: 'requerido' },
      { type: 'pattern', message: 'Ejemplo: 12-12345678-1' }
    ],
    'CesionTitular': [
      { type: 'required', message: 'requerido' }
    ],
    'CesionFechaVigencia': [
      { type: 'required', message: 'requerido' }
    ],
    'MonedaId': [
      { type: 'required', message: 'requerido' }
    ],
    'TasaDeCambio': [
      { type: 'required', message: 'requerido' },
      { type: 'pattern', message: 'solo debe contener numeros' },
    ]
  };

  currencyMask = createNumberMask({
    prefix: '',
    suffix: '',
    includeThousandsSeparator: true,
    thousandsSeparatorSymbol: '.',
    allowDecimal: true,
    decimalSymbol: ',',
    decimalLimit: 2,
    integerLimit: null,
    requireDecimal: false,
    allowNegative: false,
    allowLeadingZeroes: false
  });

  
  currencyMask10 = createNumberMask({
    prefix: '',
    suffix: '',
    includeThousandsSeparator: true,
    thousandsSeparatorSymbol: '.',
    allowDecimal: true,
    decimalSymbol: ',',
    decimalLimit: 10,
    integerLimit: null,
    requireDecimal: false,
    allowNegative: false,
    allowLeadingZeroes: false
  });


  ArraySelect: any = [];

  ArraySelectCesion: any = [];
  IdLibranza: string = '0';
  idNewLibranza: string = '0';
  montoDisponible: number = 0;
  provincias: any = [];
  beneficiarios: any = [];
  beneficiariosCesion: any = [];
  proyectos: any = [];
  misBeneficiarios: Array<any> = [];
  misBeneficiariosCesion: Array<any> = [];
  misBeneficiariosCesionEscritura: Array<any> = [];
  misBeneficiariosCesionImporte: Array<any> = [];
  loading: boolean = false;
  infoMessage: string;
  bancosBeneficiario: Array<any> = [];
  bancosBeneficiarioCesion: Array<any> = [];
  CuitBeneficiario: string = "";
  RazonSocialBeneficiario: string = "";
  ListFacturas: any = [];
  idFacturaToEdit: number;
  FacturaToEdit: any;
  idFacturaToDelete: number;
  LibranzaTipo: any;
  IdLibranzaTipo: number;
  IdProvincia: string;
  IdProyecto: string;
  IdLibranzaEstado: string;
  isDisabled: boolean;
  nroLibranza: string = "";
  idPush: number = -1;
  respInscripto: boolean = false;
  NroLibranzaOriginal: string = '0';
  TipoLibranzaSeleccionadoNombre: string = "";
  IdLibranzaTipoOriginal: number = 0;
  CreateByIdProyecto: string = '0';
  VieneDesdeProyectos: boolean = false;
  debeCargarCuentaParaCesion: boolean = false;
  dropdownSettingsSingleProyecto: any;
  selectedItems: any;

  listAdjuntos: any = [];
  nameFile: string = '';
  formFiles: FormGroup;

  monedas: any = [];
  monedaSeleccionadoNombre: string = "AR $";

  EsNacional: boolean = null;
  NotaBeneficiarios: string = 'Ninguno seleccionado';

  constructor(private request: Request
    , private routes: Router
    , private formBuilder: FormBuilder
    , private data: DataService
    , private modalService: NgbModal
    , private config: NgbModalConfig
    , public readonly swalTargets: SwalPartialTargets) { }


  onItemSelectProyecto(item: any) {
    this.IdProyecto = item.id;
    this.changeProyecto(item.id);
    this.newLibranza.controls['IdProyecto'].setValue(item.id);
  }
  onItemDeSelectProyecto(item: any) {
    this.IdProyecto = '';
    this.newLibranza.controls['IdProyecto'].setValue('');
  }

  //onKey(event: any) { // without type info
  //  if (event.target.value == '') {
  //    (<any>$)(event.target).val('0,00');
  //  }
  //}

  /*transformAmount(element) {
    (<any>$)(element.target).number(true, 2, ',', '.');
  }*/

  ngOnInit() {
    this.dropdownSettingsSingleProyecto = {
      singleSelection: true,
      idField: 'id',
      textField: 'nombre',
      itemsShowLimit: 3,
      allowSearchFilter: true,
      enableCheckAll: false,
      searchPlaceholderText: 'Buscar',
      closeDropDownOnSelection: true
    };
    this.loading = true;
    this.data.currentMessage.subscribe(message => this.IdLibranza = message);
    this.data.currentMessage2.subscribe(message => this.CreateByIdProyecto = message);
    this.data.currentMessage3.subscribe(message => this.VieneDesdeProyectos = message);
    if (this.CreateByIdProyecto != '0' && this.VieneDesdeProyectos) {
      this.IdLibranza = '';
    } else {
      this.IdProyecto = '';
      this.CreateByIdProyecto = '';
    }

    this.montoDisponible = 0;
    this.resetForm();
    this.ResetFormFiles();
    this.request.get(Constants.API_PROVINCIA).subscribe(resp => {
      this.provincias = resp.data;
    });
    this.request.get(Constants.API_PROYECTO + Constants.API_PROYECTO_GETALLREDUCIDO).subscribe(resp => {
      this.proyectos = resp.data;
      if (this.IdLibranza == '0' || this.IdLibranza == '') {
        this.loading = false;
        if (this.CreateByIdProyecto != '0') {
          var nombre = ''
          var idProyectoToFind = this.CreateByIdProyecto;
          for (var i = 0, len = this.proyectos.length; i < len; i++) {
            if (this.proyectos[i].id == idProyectoToFind) { // strict equality test
              nombre = this.proyectos[i].nombre;
              break;
            }
          }
          this.selectedItems = [{ 'id': this.CreateByIdProyecto, 'nombre': nombre }];
          this.onItemSelectProyecto(this.selectedItems[0]);
        }
      }
    });

    this.getBeneficiarios();
    this.getBeneficiariosCesion();

    this.request.get(Constants.API_LIBRANZA + Constants.API_GET_LIBRANZA_TIPO).subscribe(res => {
      this.LibranzaTipo = res;
    });

    if (this.IdLibranza != '0' && this.IdLibranza != '') {
      this.titulo = "Editar";
      this.loading = true;
      this.request.get(Constants.API_LIBRANZA + Constants.API_GET_LIBRANZA_BY_ID + "?Id=" + this.IdLibranza).subscribe(lib => {

        let fechaVigencia = this.formatDate(lib.cesionFechaVigencia);
        this.request.get(Constants.API_PROVINCIA).subscribe(provincias => {
          this.provincias = provincias.data;
          if (lib.beneficiario != null) {
            this.misBeneficiarios = lib.beneficiario;
            
            
            this.EsNacional = (lib.beneficiario[0].nacionalExtranjero == "true") ? true : false;
            this.NotaBeneficiarios = (this.EsNacional) ? "Nacional" : "Extranjero";
          }

          if (lib.beneficiarioCesiones != null) {
            this.misBeneficiariosCesion = lib.beneficiarioCesiones;


            if (lib.nroEscrituraCesiones != null) {
              this.misBeneficiariosCesionEscritura = lib.nroEscrituraCesiones;
            }

            if (lib.montosCesiones != null) {

              for (let i = 0; i < lib.montosCesiones.length; i++) {
                var str = lib.montosCesiones[i];
                if (str.toString() == "") {
                  lib.montosCesiones[i] = "0";
                } else {
                  lib.montosCesiones[i] = str.toString().replace(".", ",");
                }
              }

              this.misBeneficiariosCesionImporte = lib.montosCesiones;

            
            }
      
          }

          this.request.get(Constants.API_PROYECTO + Constants.API_PROYECTO_GETALLREDUCIDO).subscribe(proyectos => {
            this.proyectos = proyectos.data;

            this.request.get(Constants.API_LIBRANZA + Constants.API_GET_LIBRANZA_TIPO).subscribe(lt => {
              this.LibranzaTipo = lt;
              this.getBeneficiarios();
              this.ListFacturas = lib.factura;
              if (lib.libranzatipo != null)
                this.IdLibranzaTipo = lib.libranzatipo.id;
              this.IdProvincia = (lib.provincia.id).toString();
              this.IdProyecto = (lib.proyecto.id).toString();
              if (lib.libranzasEstado != null)
                this.IdLibranzaEstado = (lib.libranzasEstado.id).toString();

              this.isDisabled = lib.sustituido;
              this.TipoLibranzaSeleccionadoNombre = lib.proyecto.cuenta.libranzaTipo.nombre;
              this.listAdjuntos = lib.adjuntos;
              this.selectedItems = [{ 'id': lib.proyecto.id, 'nombre': lib.proyecto.nombre }];
              this.monedaSeleccionadoNombre = lib.monedaNombreCorto;

              this.newLibranza = this.formBuilder.group({
                Id: [lib.id, Validators.required],
                IdLibranzaTipo: [this.IdLibranzaTipo],
                NroLibranza: [lib.nroLibranza, Validators.required],
                IdProvincia: [this.IdProvincia, Validators.required],
                NormaLegal: [lib.normaLegal, Validators.required],
                IdProyecto: [this.IdProyecto, [Validators.required]],
                IdProyectoView: [lib.proyecto.idProyecto, []],
                IdLibranzaEstados: [this.IdLibranzaEstado],
                Sustituido: [lib.sustituido, []],
                NroEmbargo: [(lib.sustituido) ? '' : lib.nroEmbargo, []],
                ResponsableEmbargo: [lib.responsableEmbargo, []],
                MontoEmbargo: [(<any>$).number(lib.montoEmbargo, 2, ',', '.'), []],
                Objeto: [lib.objeto],
                Observaciones: [lib.observaciones],
                SaldoEmbargo: [(<any>$).number(lib.saldoEmbargo, 2, ',', '.')],
                RegistraCesion: [lib.registraCesion, []],
                NroEscritura: [lib.nroEscritura, []],
                BeneficiarioEmbargo: [lib.beneficiarioEmbargo, []],
                IdentificacionTributaria: [lib.identificacionTributaria, []],
                MontoFondoReparo: [(<any>$).number(lib.montoFondoReparo, 2, ',', '.')],
                Multa: [(<any>$).number(lib.multa, 2, ',', '.')],
                Mora: [(<any>$).number(lib.mora, 2, ',', '.')],
                MontoRestante: [lib.montoRestante],
                Fecha: [lib.fecha],
                Beneficiario: [],
                Factura: [],
                CesionTipoCuenta: [(lib.cesionTipoCuenta == null) ? '' : lib.cesionTipoCuenta],
                CesionNroCuenta: [lib.cesionNroCuenta],
                CesionNombreBanco: [lib.cesionNombreBanco],
                CesionSucursal: [lib.cesionSucursal],
                CesionCbu: [lib.cesionCbu],
                CesionCuit: [(lib.registraCesion) ? lib.cesionCuit : lib.identificacionTributaria],
                CesionTitular: [(lib.registraCesion) ? lib.cesionTitular : lib.beneficiarioEmbargo],
                CesionFechaVigencia: [fechaVigencia],
                ObjetoDatosGenerales: [lib.objetoDatosGenerales],
                MonedaId: [lib.monedaId],
                TasaDeCambio: [(<any>$).number(lib.tasaDeCambio, 10, ',', '.'), [Validators.required]],
              });
              this.setMontoDisponible(Number(this.IdLibranza), Number(this.IdProyecto), this.parseCurrencyNumber(this.newLibranza.value.TasaDeCambio));
              this.changeRegistraCesion(lib.registraCesion);
              this.loading = false;
            });
          });
        });
      });
    }

    this.request.get(Constants.API_LIBRANZA + Constants.API_GET_MONEDAS).subscribe(resp => {
      this.monedas = resp.data;
    });
  }

  //////////////////////////////VALIDACIONES DE FORMULARIO/////////////////////////////
  resetForm() {
    this.selectedItems = [];

    this.newLibranza = this.formBuilder.group({
      Id: [0, Validators.required],
      NroLibranza: [this.nroLibranza, [Validators.required]],
      IdProvincia: ['', Validators.required],
      IdProyecto: ['', [Validators.required]],
      IdProyectoView: ['', []],
      Sustituido: [false, []],
      NroEmbargo: ['', []],
      ResponsableEmbargo: ['', []],
      MontoEmbargo: ['', []],
      SaldoEmbargo: ['', []],
      RegistraCesion: [false, []],
      NroEscritura: ['', []],
      BeneficiarioEmbargo: ['', []],
      IdentificacionTributaria: ['', []],
      MontoFondoReparo: [''],
      Objeto: [''],
      Observaciones: [''],
      Multa: [''],
      Mora: [''],
      MontoRestante: [''],
      Fecha: [new Date()],
      Beneficiario: [],
      BeneficiarioCesiones: [],
      Factura: [],
      NormaLegal: [''],
      CesionTipoCuenta: [''],
      CesionNroCuenta: [''],
      CesionNombreBanco: [''],
      CesionSucursal: [''],
      CesionCbu: [''],
      CesionCuit: [''],
      CesionTitular: [''],
      CesionFechaVigencia: [''],
      ObjetoDatosGenerales: [''],
      IdLibranzaEstados: [''],
      IdLibranzaTipo: [''],
      MonedaId: [Constants.MONEDA_ARGENTINA],
      TasaDeCambio: ['1,00', [Validators.required]]
    });
    if (this.CreateByIdProyecto != '0' && this.CreateByIdProyecto != '') {
      this.newLibranza.controls['IdProyecto'].setValue(this.CreateByIdProyecto);
      this.changeProyecto(this.CreateByIdProyecto);
    }
  }

  SaveLibranza(data) {
    this.loading = true;
    this.misBeneficiariosCesionEscritura=[];
    this.misBeneficiariosCesionImporte=[];

    for (let i = 1; i < this.misBeneficiariosCesion.length + 1; i++) {
      if (eval(document.getElementById('ImporteCesion_' + i).id + '.value') == "") { } else {}
      console.log(eval(document.getElementById('ImporteCesion_' + i).id + '.value'));
      console.log(eval(document.getElementById('NroEscritura_' + i).id + '.value'));

      if (eval(document.getElementById('ImporteCesion_' + i).id + '.value') == "")
      {
          this.misBeneficiariosCesionImporte.push("0");
      } else { 
        this.misBeneficiariosCesionImporte.push(eval(document.getElementById('ImporteCesion_' + i).id + '.value'));
      }
        this.misBeneficiariosCesionEscritura.push(eval(document.getElementById('NroEscritura_' + i).id + '.value'));
    }
  
 
    data.Provincia = { id: data.IdProvincia };
    data.Proyecto = { id: data.IdProyecto };
    data.Beneficiario = this.misBeneficiarios;
    data.BeneficiarioCesiones = this.misBeneficiariosCesion;
    data.MontosCesiones = this.misBeneficiariosCesionImporte;
    data.NroEscrituraCesiones = this.misBeneficiariosCesionEscritura;

    data.Factura = this.ListFacturas;
    var idUsuario = Number(JSON.parse(sessionStorage.getItem('currentUser')).id);
    this.request.post("/libranza/save?IdUsuario=" + idUsuario, data).subscribe(resp => {
      if (resp.code == 200) {
        this.idNewLibranza = resp.result;
        if (Number(this.idNewLibranza) > 0) {
          if (this.nameFile != "") {
            this.IdLibranza = this.idNewLibranza;
            this.SaveFile();
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
        }
      } else {
        this.infoMessage = resp.error;
        this.loading = false;
      }
    });
  }
  returnList() {
    this.data.changeMessage("0");
    this.routes.navigate(['../dashboard/libranzas']);
    this.modalService.dismissAll();
  }

  changeRegistraCesion(value) {
    this.debeCargarCuentaParaCesion = (value == 'true' || value == true) ? true : false;

    //if (this.debeCargarCuentaParaCesion) {
    //  this.newLibranza.get('CesionTipoCuenta').setValidators([Validators.required]);
    //  this.newLibranza.get('CesionNroCuenta').setValidators([Validators.required]);
    //  this.newLibranza.get('CesionNombreBanco').setValidators([Validators.required]);
    //  this.newLibranza.get('CesionSucursal').setValidators([Validators.required]);
    //  this.newLibranza.get('CesionCbu').setValidators([Validators.minLength(22), Validators.maxLength(22), Validators.pattern('[0-9]+'), Validators.required]);
    //  this.newLibranza.get('CesionCuit').setValidators([Validators.required]);
    //  this.newLibranza.get('CesionTitular').setValidators([Validators.required]);
    //  this.newLibranza.get('CesionFechaVigencia').setValidators([Validators.required]);

    //} else {
    //  this.newLibranza.get('CesionTipoCuenta').clearValidators();
    //  this.newLibranza.get('CesionNroCuenta').clearValidators();
    //  this.newLibranza.get('CesionNombreBanco').clearValidators();
    //  this.newLibranza.get('CesionSucursal').clearValidators();
    //  this.newLibranza.get('CesionCbu').clearValidators();
    //  this.newLibranza.get('CesionCuit').clearValidators();
    //  this.newLibranza.get('CesionTitular').clearValidators();
    //  this.newLibranza.get('CesionFechaVigencia').clearValidators();
    //}
    //this.newLibranza.get('CesionTipoCuenta').updateValueAndValidity();
    //this.newLibranza.get('CesionNroCuenta').updateValueAndValidity();
    //this.newLibranza.get('CesionNombreBanco').updateValueAndValidity();
    //this.newLibranza.get('CesionSucursal').updateValueAndValidity();
    //this.newLibranza.get('CesionCbu').updateValueAndValidity();
    //this.newLibranza.get('CesionCuit').updateValueAndValidity();
    //this.newLibranza.get('CesionTitular').updateValueAndValidity();
    //this.newLibranza.get('CesionFechaVigencia').updateValueAndValidity();
  }
  changeProyecto(id) {
    var tipoLibranzaSeleccionado;
    var nombre;
    var idproyecto;
    if (id != "") {

      this.proyectos.forEach(function (item) {
        if (item.id == id) {
          tipoLibranzaSeleccionado = item.cuenta.libranzaTipo.id;
          nombre = item.cuenta.libranzaTipo.nombre;
          idproyecto = item.idProyecto;
        }
      });
      this.TipoLibranzaSeleccionadoNombre = nombre;
      this.newLibranza.controls['IdProyectoView'].setValue(idproyecto);
      this.IdProyecto = id;

      if (this.IdLibranzaTipo != tipoLibranzaSeleccionado) {
        if (this.IdLibranza != '0' && this.NroLibranzaOriginal == '0') {
          this.NroLibranzaOriginal = this.newLibranza.controls['NroLibranza'].value;
          this.IdLibranzaTipoOriginal = this.IdLibranzaTipo;
        }
        this.IdLibranzaTipo = tipoLibranzaSeleccionado;
        if (this.IdLibranzaTipoOriginal != this.IdLibranzaTipo) {
          this.request.get(Constants.API_LIBRANZA + Constants.API_GET_NROLIBRANZA + "?tipoId=" + this.IdLibranzaTipo).subscribe(nro => {
            this.newLibranza.controls['NroLibranza'].setValue(nro);
          });
        } else {
          this.newLibranza.controls['NroLibranza'].setValue(this.NroLibranzaOriginal);
        }
      }
      this.setMontoDisponible(Number(this.IdLibranza), id, this.parseCurrencyNumber(this.newLibranza.value.TasaDeCambio));

    }
  }

  //////////////////////////FIN VALIDACIONES//////////////////////////////////////////////


  ////////////////////////////////  MODAL BENEFICIARIOS  -  BENEFICIARIOS CESION///////////////////////////////////////////////////////
  SelectBeneficiario(i: number,event) {
    let ben = this.beneficiarios[i];
    let bancos = this.bancosBeneficiario[i];
    let idBanco = this.ArraySelect[ben.id];
    let nacionalExtranjero = (ben.nacionalExtranjero == "true") ? true : false;

    if (this.misBeneficiarios.length == 0) {
      this.EsNacional = nacionalExtranjero;
      this.NotaBeneficiarios = (this.EsNacional) ? "Nacional" : "Extranjero";
    }
    if (idBanco == "") {
      if (this.misBeneficiarios.length == 1) {
        this.EsNacional = null;
        this.NotaBeneficiarios = "Ninguno seleccionado";
        this.misBeneficiarios = [];
      } else {
        this.misBeneficiarios.forEach((item, index) => {
          if (item.id === ben.id) {
            this.misBeneficiarios.splice(index, 1);
          }
        });
      }

    } else {
      this.misBeneficiarios.forEach((item, index) => {
        if (item.id === ben.id) {
          this.misBeneficiarios.splice(index, 1);
        }
      });
      if (nacionalExtranjero == this.EsNacional) {
          bancos.forEach((item, index) => {
            if (item.id == idBanco) {
              ben.bancos = '';
              ben.bancos = [item];
              this.misBeneficiarios.push(ben);
            }
          });
      } else {
        this.ArraySelect[ben.id] = "";
        event.target.selectedIndex = "0";
        swal({
          position: 'center',
          type: 'warning',
          title: "El beneficiario no es del origen correcto.",
          showConfirmButton: true
        });
      }
    }
  }


  SelectBeneficiarioCesion(i: number, event) {
    let ben = this.beneficiariosCesion[i];
    let bancosCesion = this.bancosBeneficiarioCesion[i];
    let idBancoCesion = this.ArraySelectCesion[ben.id];
    let nacionalExtranjero = (ben.nacionalExtranjero == "true") ? true : false;

    if (this.misBeneficiariosCesion.length == 0) {
      this.EsNacional = nacionalExtranjero;
      this.NotaBeneficiarios = (this.EsNacional) ? "Nacional" : "Extranjero";
    }
    if (idBancoCesion == "") {
      if (this.misBeneficiariosCesion.length == 1) {
        this.EsNacional = null;
        this.NotaBeneficiarios = "Ninguno seleccionado";
        this.misBeneficiariosCesion = [];
      } else {
        this.misBeneficiariosCesion.forEach((item, index) => {
          if (item.id === ben.id) {
            this.misBeneficiariosCesion.splice(index, 1);
          }
        });
      }

    } else {
      this.misBeneficiariosCesion.forEach((item, index) => {
        if (item.id === ben.id) {
          this.misBeneficiariosCesion.splice(index, 1);
        }
      });
      if (nacionalExtranjero == this.EsNacional) {
        bancosCesion.forEach((item, index) => {
          if (item.id == idBancoCesion) {
            ben.bancos = '';
            ben.bancos = [item];
            this.misBeneficiariosCesion.push(ben);
          }
        });
      } else {
        this.ArraySelectCesion[ben.id] = "";
        event.target.selectedIndex = "0";
        swal({
          position: 'center',
          type: 'warning',
          title: "El beneficiario no es del origen correcto.",
          showConfirmButton: true
        });
      }
    }
  }


  GetBancos() {
    this.beneficiarios.forEach((item, index) => {
      this.bancosBeneficiario.push(item.bancos);
      this.ArraySelect[item.id] = "";
    });
  }

  GetBancosCesion() {
    try {
      this.beneficiariosCesion.forEach((item, index) => {
        this.bancosBeneficiarioCesion.push(item.bancos);
        this.ArraySelect[item.id] = "";
      });
    } catch (e) {

    }
  }


  getBeneficiarios() {
    this.beneficiarios = [];
    this.bancosBeneficiario = [];
    this.request.get(Constants.API_PROVEEDOR + Constants.API_GET_FILTER + "?FilterRazonSocial=" + this.RazonSocialBeneficiario + "&FilterCuit=" + this.CuitBeneficiario).subscribe(resp => {
      this.beneficiarios = resp.data;
      this.GetBancos();
      this.CheckMBList();
    });
  }

  getBeneficiariosCesion() {
    this.beneficiariosCesion = [];
    this.bancosBeneficiarioCesion = [];
    this.request.get(Constants.API_PROVEEDOR + Constants.API_GET_FILTER + "?FilterRazonSocial=" + this.RazonSocialBeneficiario + "&FilterCuit=" + this.CuitBeneficiario).subscribe(resp => {
      this.beneficiariosCesion = resp.data;
      this.GetBancosCesion();
      this.CheckMBListCesion();
    });
  }
  DeleteBeneficiario(mb: any) {
    this.misBeneficiarios.forEach((item, index) => {
      if (item.id == mb.id) {
        this.misBeneficiarios.splice(index, 1);
        this.ArraySelect[mb.id] = "";
      }
    });
  }
  DeleteBeneficiarioCesion(mb: any) {
    this.misBeneficiariosCesion.forEach((item, index) => {
      if (item.id == mb.id) {
        this.misBeneficiariosCesion.splice(index, 1);
        this.ArraySelect[mb.id] = "";
      }
    });
  }
  CheckMBList() {
    this.beneficiarios.forEach((lb, ilb) => {
      this.misBeneficiarios.forEach((mb, imb) => {
        if (lb.id == mb.id) {
          this.ArraySelect[mb.id] = mb.bancos[0].id;
        }
      });
    });
  }

  CheckMBListCesion() {
    this.beneficiariosCesion.forEach((lb, ilb) => {
      this.misBeneficiariosCesion.forEach((mb, imb) => {
        if (lb.id == mb.id) {
          this.ArraySelect[mb.id] = mb.bancos[0].id;
        }
      });
    });
  }
  ///////////////////////////////////////FIN MODAL BENEFICIARIOS ///////////////////////////////////////////////////////

  ///////////////////////////////////////MODAL DOCUMENTO DE PAGO //////////////////////////////////////////////////////
  OpenModalDocumentoPago(nro: string, ModalDocumentoPago: string) {
    this.ResetDocumentoPago();
    this.modalService.open(ModalDocumentoPago, { size: 'lg', backdropClass: 'light-blue-backdrop' });
    this.ResetDocumentoPago();
    if (nro != '0')
      this.ListFacturas.forEach((item, index) => {
        if (item.id == nro) {

          let resp = this.ListFacturas[index];
          let fecha = this.formatDate(resp.fecha);
          this.newFacturaLibranza = this.formBuilder.group({
            id: [resp.id],
            idLibranza: [resp.idLibranza],
            tipo: [resp.tipo, [
              Validators.required
            ]],
            fecha: [fecha, [Validators.required]],
            nro: [resp.nro, [
              Validators.required
            ]],
            monto: [(<any>$).number(resp.monto, 2, ',', '.'), [
              Validators.required
            ]],
            iva: [(<any>$).number(resp.iva, 2, ',', '.'), [
            ]],
            ibb: [(<any>$).number(resp.ibb, 2, ',', '.'), [
            ]]
          });

          if (resp.tipo.indexOf(" A") > 0 || resp.tipo.includes("Tipo Factura") || resp.tipo.includes("Recibo"))
            this.respInscripto = true;
          else
            this.respInscripto = false;

        }
      });
  }
  ResetDocumentoPago() {
    this.newFacturaLibranza = this.formBuilder.group({
      id: ['0'],
      idLibranza: [this.IdLibranza],
      tipo: ['', Validators.required],
      fecha: ['', [
        Validators.required
      ]],
      nro: ['', [
        Validators.required
      ]],
      monto: ['', [
        Validators.required
      ]],
      iva: ['', [
      ]],
      ibb: ['', [
      ]]
    });
  }

  validarCuentaExistente(data) {
    for (let bank of this.ListFacturas) {
      if (bank.id != data.id && bank.tipo == data.tipo && bank.nro == data.nro) {
        return true;
      }
    }
    return false;
  }



  SaveDocumentoPago(data) {
    let cuentaExistente = this.validarCuentaExistente(data);

    if (!cuentaExistente) {
      this.DeleteDocumentoPago(data.id);
      data.id = this.idPush;

      data.monto = this.parseCurrencyNumber(data.monto);

      if (data.iva == '')
        data.iva = '0,00';

      if (data.ibb == '')
        data.ibb = '0,00';

      data.iva = this.parseCurrencyNumber(data.iva);
      data.ibb = this.parseCurrencyNumber(data.ibb);

      this.ListFacturas.push(data);
      this.idPush--;
    } else {
      swal({
        position: 'center',
        type: 'warning',
        title: "Ya existe un documento de ese tipo con ese número para la libranza.",
        showConfirmButton: true
      });
    }
    this.modalService.dismissAll();
    this.setMontoDisponible(Number(this.IdLibranza), Number(this.IdProyecto), this.parseCurrencyNumber(this.newLibranza.value.TasaDeCambio));
  }
  DeleteDocumentoPago(id: Int32Array) {
    this.ListFacturas.forEach((item, index) => {
      if (item.id == id) {
        this.ListFacturas.splice(index, 1);
      }
    });
    this.setMontoDisponible(Number(this.IdLibranza), Number(this.IdProyecto), this.parseCurrencyNumber(this.newLibranza.value.TasaDeCambio));
  }
  //////////////////////////////////////////FIN MODAL DOCUMENTO DE PAGO /////////////////////////////////////////////////
  ///////////////////////////////FILES LIBRANZAS //////////////////////////////////////////////////////////////////
  SaveFile() {
    this.loading = true;
    this.prepareSaveFile();
    this.request
      .createDataFile(Constants.API_LIBRANZA_ADJUNTOS, this.prepareSaveFile())
      .subscribe(resp => {
        if (resp != null) {
          this.infoMessage = "El archivo se guardó con éxito";
          this.ResetFormFiles();
          this.request.get(Constants.API_LIBRANZA_ADJUNTOS + Constants.API_GET_ADJUNTOS_LIBRANZA + "?id=" + this.IdLibranza).subscribe(resp => {
            this.listAdjuntos = resp;
            this.loading = false;
          });
        }
        else {
          this.infoMessage = "El archivo no se ha podido guardar";
          this.loading = false;
        }
      });
    /*}
    else
      this.request
        .updateDataFile('/ProveedorAdjuntos', this.prepareSaveFile())
        .subscribe(resp => console.log(resp));*/
  }
  ResetFormFiles() {
    this.nameFile = "";
    this.formFiles = new FormGroup(
      {
        id: new FormControl(0),
        idEntidad: new FormControl(this.IdLibranza),
        archivo: new FormControl(null)
      },
      { updateOn: 'submit' }
    );
    ///////////////////////////////////this.uploader.nativeElement.value = '';
  }
  prepareSaveFile(): FormData {
    const formModel = this.formFiles.value;

    let formData = new FormData();

    formData.append('id', formModel.id);
    formData.append('idEntidad', this.IdLibranza);
    formData.append('archivo', formModel.archivo);

    return formData;
  }
  fileChange(files: FileList) {
    this.nameFile = files[0].name;
    if (files && files[0].size > 0) {
      this.formFiles.patchValue({
        archivo: files[0]
      });
      if (this.IdLibranza != '0' && this.IdLibranza != '')
        this.SaveFile();
    }
  }
  GetAdjuntosByLibranza() {
    this.request.get(Constants.API_LIBRANZA_ADJUNTOS + Constants.API_GET_ADJUNTOS_LIBRANZA + "?id=" + this.IdLibranza).subscribe(resp => {
      this.listAdjuntos = resp;
    });
  }
  deleteAdjunto(idAdjunto: number) {
    this.request.delete(Constants.API_LIBRANZA_ADJUNTOS + Constants.API_DELETE + "?id=" + idAdjunto).subscribe(resp => {
      this.infoMessage = resp.result;
      this.request.get(Constants.API_LIBRANZA_ADJUNTOS + Constants.API_GET_ADJUNTOS_LIBRANZA + "?id=" + this.IdLibranza).subscribe(resp => {
        this.listAdjuntos = resp;
      });
    });
  }
  getFileById(idAdjunto: string) {
    window.open(Constants.URL_BASE_WEB_API + Constants.API_FILE + Constants.API_GET_FILE + '?id=' + idAdjunto);
  }
  ////////////////FIN FILES PROVEEDOR ///////////////////////////////////////////////////////////////////////////////////

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

  focusOutTasaDeCambio(event) {
    this.setMontoDisponible(Number(this.IdLibranza), Number(this.IdProyecto), this.parseCurrencyNumber(this.newLibranza.value.TasaDeCambio));
  }
  changeMoneda(event) {
    this.monedaSeleccionadoNombre = this.monedas[event.target.value-1].nombreCorto;
  }

  setMontoDisponible(idLibranza: number, idProyecto: number, tasaDeCambio: number) {
    this.montoDisponible = 0;
    this.request.get(Constants.API_LIBRANZA + Constants.API_GET_MONTODISPONIBLE_PROYECTO_LIBRANZA + "?idLibranza=" + idLibranza + '&idProyecto=' + idProyecto).subscribe(resp => {
      this.montoDisponible = resp;
      var deducciones = 0;
      var iva = 0;
      var ibb = 0;
      var sumFactura = 0;

      if (this.IdLibranzaEstado != Constants.LIBRANZA_ESTADO_ANULADA) {

        if (this.newLibranza.value.MontoFondoReparo)
          deducciones += Number(this.parseCurrencyNumber(this.newLibranza.value.MontoFondoReparo)) * tasaDeCambio;

        if (this.newLibranza.value.Multa)
          deducciones += Number(this.parseCurrencyNumber(this.newLibranza.value.Multa)) * tasaDeCambio;

        if (this.newLibranza.value.Mora)
          deducciones += Number(this.parseCurrencyNumber(this.newLibranza.value.Mora)) * tasaDeCambio;

        this.ListFacturas.forEach((item, index) => {
          if (!item.tipo.toUpperCase().includes("NOTA DE CRÉDITO") && !item.tipo.toUpperCase().includes("CREDIT NOTE")) {
            sumFactura += Number(this.parseCurrencyNumber(item.monto.toString())) * tasaDeCambio;
            if (this.TipoLibranzaSeleccionadoNombre == 'A') {
              iva += Number(this.parseCurrencyNumber(item.iva.toString()));
              ibb += Number(this.parseCurrencyNumber(item.ibb.toString()));
            }
          }
          else {
            sumFactura -= Number(this.parseCurrencyNumber(item.monto.toString())) * tasaDeCambio;
            if (this.TipoLibranzaSeleccionadoNombre == 'A') {
              iva -= Number(this.parseCurrencyNumber(item.iva.toString()));
              ibb -= Number(this.parseCurrencyNumber(item.ibb.toString()));
            }
          }
        });
        var sumaDocumentoDePago = sumFactura;
        var neto = sumaDocumentoDePago - deducciones + iva + ibb; /*Neto =Total Factura - Retenciones*/

        this.montoDisponible -= neto;

        if (this.montoDisponible < 0)
          swal({
            position: 'center',
            type: 'warning',
            title: "Monto Disponible Agotado",
            showConfirmButton: true
          });
      }
    });
  }



  registraSustitucion(event) {
    let ctrl = this.newLibranza.get('MontoFondoReparo');

    if (event.target.checked) {
      this.isDisabled = true;
      ctrl.setValue('');

    } else {
      this.isDisabled = false;
    }
  }

  private formatDate(fecha: string) {
    if (fecha == undefined || fecha == "") return "";
    var part = fecha.split('T');
    return part[0];
  }


  onChangeEstadoLibranza(event): void {  // event will give you full breif of action

    const newVal:string = event.target.value;

    if (newVal.indexOf(" A") > 0 || newVal.includes("Tipo Factura") || newVal.includes("Recibo"))
      this.respInscripto = true;
    else
      this.respInscripto = false;
  }

}
