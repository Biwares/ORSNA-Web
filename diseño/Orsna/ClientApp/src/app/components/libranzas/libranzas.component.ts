import { Component, OnInit, Input } from '@angular/core';
import { DOCUMENT } from '@angular/common';
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
import { DISABLED } from '@angular/forms/src/model';

@Component({
  selector: 'app-libranzas',
  templateUrl: './libranzas.component.html',
  styleUrls: ['./libranzas.component.css']
})
export class LibranzasComponent implements OnInit {
  newLibranza: FormGroup;
  newFacturaLibranza: FormGroup;
  validation_messages = {
    'IdLibranzaTipo': [
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
    ]
  };
  ArraySelect: any = [];
  IdLibranza: string = '0';
  idNewLibranza: string = '0';
  provincias: any = [];
  beneficiarios: any = [];
  proyectos: any = [];
  misBeneficiarios: Array<any> = [];
  loading: boolean = false;
  infoMessage: string;
  bancosBeneficiario: Array<any> = [];
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
  nroLibranza: string = "";
  constructor(private request: Request
    , private routes: Router
    , private formBuilder: FormBuilder
    , private data: DataService
    , private modalService: NgbModal
    , private config: NgbModalConfig
    , public readonly swalTargets: SwalPartialTargets) { }

  ngOnInit() {

    this.loading = true;
    this.data.currentMessage.subscribe(message => this.IdLibranza = message);

    this.resetForm();
    this.request.get("/Provincia").subscribe(resp => {
      this.provincias = resp.data;
    });
    this.request.get("/proyecto").subscribe(resp => {
      this.proyectos = resp.data;
      if (this.IdLibranza == '0' || this.IdLibranza == '') {
        this.request.get("/libranza/GetNroLibranza").subscribe(nro => {
          this.nroLibranza = nro;
          if (this.IdLibranza == '0' || this.IdLibranza == '') {
            this.loading = false;
            this.resetForm();
          }
        });
      }
    });
    this.getBeneficiarios();
    this.request.get("/libranza/GetLibranzaTipo").subscribe(res => {
      this.LibranzaTipo = res;
    });

    if (this.IdLibranza != '0' && this.IdLibranza != '') {
      this.loading = true;
      this.request.get("/libranza/GetLibranzaById?Id=" + this.IdLibranza).subscribe(lib => {
        this.request.get("/Provincia").subscribe(provincias => {
          this.provincias = provincias.data;
          if (lib.beneficiario != null)
            this.misBeneficiarios = lib.beneficiario;
          this.request.get("/proyecto").subscribe(proyectos => {
            this.proyectos = proyectos.data;
            this.request.get("/libranza/GetLibranzaTipo").subscribe(lt => {
              this.LibranzaTipo = lt;
              this.getBeneficiarios();
              this.ListFacturas = lib.factura;
              if (lib.libranzatipo != null)
                this.IdLibranzaTipo = lib.libranzatipo.id;
              this.IdProvincia = (lib.provincia.id).toString();
              this.IdProyecto = (lib.proyecto.id).toString();
              if (lib.libranzaEstado != null)
                this.IdLibranzaEstado = (lib.libranzaEstado.id).toString();
              this.newLibranza = this.formBuilder.group({
                Id: [lib.id, Validators.required],
                IdLibranzaTipo: [this.IdLibranzaTipo],
                NroLibranza: [lib.nroLibranza, Validators.required],
                IdProvincia: [this.IdProvincia, Validators.required],
                IdProyecto: [this.IdProyecto, [
                  /*Validators.maxLength(9),
                  Validators.minLength(3),
                  Validators.pattern('[0-9]+'),*/
                  Validators.required
                ]],
                IdLibranzaEstados: [this.IdLibranzaEstado],
                Sustituido: [lib.sustituido, []],
                NroEmbargo: [lib.nroEmbargo, []],
                ResponsableEmbargo: [lib.responsableEmbargo, []],
                MontoEmbargo: [lib.montoEmbargo, []],
                SaldoEmbargo: [lib.saldoEmbargo, []],
                RegistraCesion: [lib.registraCesion, []],
                NroEscritura: [lib.nroEscritura, []],
                BeneficiarioEmbargo: [lib.beneficiarioEmbargo, []],
                IdentificacionTributaria: [lib.identificacionTributaria, []],
                MontoFondoReparo: [lib.montoFondoReparo],
                Multa: [lib.multa],
                Mora: [lib.mora],
                MontoRestante: [lib.montoRestante],
                Fecha: [lib.fecha],
                Beneficiario: [],
                Factura: []
              });
              this.loading = false;
            });
          });
        });
      });
    }
  }
  //////////////////////////////VALIDACIONES DE FORMULARIO/////////////////////////////
  resetForm() {
    this.newLibranza = this.formBuilder.group({
      Id: [0, Validators.required],
      /*IdLibranzaTipo: [''],*/
      NroLibranza: [this.nroLibranza, [
        Validators.required
      ]],
      IdProvincia: ['', Validators.required],
      IdProyecto: ['', [
        /*Validators.maxLength(9),
        Validators.minLength(3),
        Validators.pattern('[0-9]+'),*/
        Validators.required
      ]],
      /*IdLibranzaEstados: [''],*/
      Sustituido: [false, []],
      NroEmbargo: ['', []],
      ResponsableEmbargo: ['', []],
      MontoEmbargo: ['', []],
      SaldoEmbargo: ['', []],
      RegistraCesion: ['', []],
      NroEscritura: ['', []],
      BeneficiarioEmbargo: ['', []],
      IdentificacionTributaria: ['', []],
      MontoFondoReparo: [''],
      Multa: [''],
      Mora: [''],
      MontoRestante: [''],
      Fecha: [new Date()],
      Beneficiario: [],
      Factura: []
    });
  }
  SaveLibranza(data) {
    this.loading = true;
    /* data.LibranzaTipo = { id: data.IdLibranzaTipo };*/
    data.Provincia = { id: data.IdProvincia };
    /*data.LibranzaEstado = { id: data.IdLibranzaEstado };*/
    data.Proyecto = { id: data.IdProyecto };
    data.Beneficiario = this.misBeneficiarios;
    data.Factura = this.ListFacturas;
    this.request.post("/libranza/save", data).subscribe(resp => {
      if (resp.code == 200) {
        this.idNewLibranza = resp.result;
        if (Number(this.idNewLibranza) > 0) {
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
  changeProyecto(id) {
    this.request.get('/proyecto/GetProyectoById?idProyecto=' + id).subscribe(res => {
      this.IdLibranzaTipo = res.cuenta.libranzaTipo.id;
    });
  }
  //////////////////////////FIN VALIDACIONES//////////////////////////////////////////////
  ////////////////////////////////  MODAL BENEFICIARIOS ///////////////////////////////////////////////////////
  SelectBeneficiario(i: number) {
    let ben = this.beneficiarios[i];
    let bancos = this.bancosBeneficiario[i];
    let idBanco = this.ArraySelect[ben.id];
    if (idBanco == "") {
      this.misBeneficiarios.forEach((item, index) => {
        if (item.id === ben.id) {
          this.misBeneficiarios.splice(index, 1);
        }
      });
    }
    else {
      this.misBeneficiarios.forEach((item, index) => {
        if (item.id === ben.id) {
          this.misBeneficiarios.splice(index, 1);
        }
      });
      bancos.forEach((item, index) => {
        if (item.id == idBanco) {
          ben.bancos = '';
          ben.bancos = [item];
          this.misBeneficiarios.push(ben);
        }
      });
    }
  }
  GetBancos() {
    this.beneficiarios.forEach((item, index) => {
      this.bancosBeneficiario.push(item.bancos);
      this.ArraySelect[item.id] = "";
    });
  }
  getBeneficiarios() {
    this.request.get("/Proveedor/getFilter?FilterRazonSocial=" + this.RazonSocialBeneficiario + "&FilterCuit=" + this.CuitBeneficiario).subscribe(resp => {
      this.beneficiarios = resp.data;
      this.GetBancos();
      this.CheckMBList();
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
  /*FilterBeneficiarios() {
    this.listBeneficiarios = this.beneficiarios;
    this.listBancosBeneficiarios = [];
    this.listBeneficiarios = [];
    this.beneficiarios.forEach( (item, index) => {
      if ((item.cuit).includes(this.CuitBeneficiario) && (item.razonSocial).includes(this.RazonSocialBeneficiario)) {
        this.listBeneficiarios.push(item);
        this.listBancosBeneficiarios.push(item.bancos);
        this.ArraySelect[item.id] = "";
      }
    });
    this.CheckMBList();
  }*/
  CheckMBList() {
    this.beneficiarios.forEach((lb, ilb) => {
      this.misBeneficiarios.forEach((mb, imb) => {
        if (lb.id == mb.id) {
          this.ArraySelect[mb.id] = mb.bancos.id;
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
        if (item.nro == nro) {
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
            monto: [resp.monto, [
              Validators.required
            ]],
            iva: [resp.iva, [
            ]],
            ibb: [resp.ibb, [
            ]]
          });
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
  SaveDocumentoPago(data) {
    this.DeleteDocumentoPago(data.nro);
    this.ListFacturas.push(data);
    this.modalService.dismissAll();
  }
  DeleteDocumentoPago(nro: string) {
    this.ListFacturas.forEach((item, index) => {
      if (item.nro == nro) {
        this.ListFacturas.splice(index, 1);
      }
    });
  }
  //////////////////////////////////////////FIN MODAL DOCUMENTO DE PAGO /////////////////////////////////////////////////
  registraSustitucion() {
    let obj = (<HTMLInputElement>document.getElementById('MontoFondoReparo'));
    obj.value = "";
    if (obj.disabled) {
      obj.disabled = false;
    }
    else
      obj.disabled = true;
  }
  private formatDate(fecha: string) {
    if (fecha == undefined || fecha == "") return "";
    var part = fecha.split('T');
    return part[0];
  }
}
