import { Component, OnInit, ViewEncapsulation, Inject, Input, ViewChild } from '@angular/core';
import { DOCUMENT } from '@angular/common';
import { Router } from '@angular/router';
import { Request } from '../../services/request';
import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { NgbModal, NgbModalConfig } from '@ng-bootstrap/ng-bootstrap';
import { SwalPartialTargets } from '@toverux/ngx-sweetalert2';
import { DataFile } from '../../Models/DataFile';

import { Validators, FormBuilder, FormGroup, FormControl } from '@angular/forms';
import { DataService } from '../../services/data.service';
import { Constants } from '../../services/constants';
import { Jsonp } from '@angular/http';
import swal from 'sweetalert2';
import { forEach } from '@angular/router/src/utils/collection';

@Component({
  selector: 'app-beneficarios',
  templateUrl: './beneficiarios.component.html',
  styleUrls: ['./beneficiarios.component.css']
})
export class BeneficiariosComponent implements OnInit {
  @Input() DataFile: DataFile;
  @ViewChild('uploader') uploader: any;

  private _info = new Subject<string>();
  staticAlertClosed = false;
  infoMessage: string;
  nombreModulo: string = "";
  newProveedor: FormGroup;
  newBancoBeneficiario: FormGroup;
  formFiles: FormGroup;
  cbuToDelete: string;
  idBeneficiario: string = '0';
  area: any;
  esNacional: string = "Nacional";
  listBanks: any = [];
  listAdjuntos: any = [];
  idBankToEdit: number;
  BankToEdit: any;
  idBankToDelete: number;
  nameFile: string = '';
  loading: boolean = false;
  idPush: number = -1;
  validation_messages = {
    'Codigo': [
      { type: 'required', message: 'Codigo Requerido' },
      { type: 'minlength', message: 'debe tener 20 caracteres' },
      { type: 'maxlength', message: ' debe tener 20 caracteres' },
      { type: 'pattern', message: 'solo debe contener numeros' }
    ],
    'Nombre': [
      { type: 'required', message: 'requerido' }
    ],
    'Titular': [
      { type: 'required', message: 'requerido' }
    ],
    'TipoCuenta': [
      { type: 'required', message: 'requerido' }
    ],
    'Sucursal': [
      { type: 'required', message: 'requerido' }
    ],
    'RazonSocial': [
      { type: 'required', message: 'requerido' }],
    'Descripcion': [
      { type: 'required', message: 'requerido' }],
    'Cuit': [
      { type: 'required', message: 'requerido' },
      { type: 'pattern', message: 'Ejemplo: 12-12345678-1' }],
    'FechaAlta': [
      { type: 'required', message: 'requerido' }],
    'NacionalExtranjero': [
      { type: 'required', message: 'requerido' }],
    'Email': [
      { type: 'required', message: 'requerido' }],
    'NroCuenta': [
      { type: 'required', message: 'requerido' },
      { type: 'minlength', message: 'debe tener 20 caracteres' },
      { type: 'maxlength', message: ' debe tener 20 caracteres' },
      { type: 'pattern', message: 'solo debe contener numeros' }
    ],
    'FechaVigencia': [
      { type: 'required', message: 'requerido' }
    ],
    'NombreBanco': [
      { type: 'required', message: 'requerido' }
    ],
    'Telefono': [
      { type: 'required', message: 'requerido' }
    ],
    'Cbu': [
      { type: 'pattern', message: 'solo debe contener numeros' },
      { type: 'required', message: 'requerido' },
      { type: 'minlength', message: 'debe tener 22 caracteres' },
      { type: 'maxlength', message: ' debe tener 22 caracteres' },
    ],
    'BicSwift': [
      { type: 'pattern', message: 'solo debe contener numeros' },
      { type: 'required', message: 'requerido' }
    ]
  };
  nombreModal: string = "Nueva";
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
    this.data.currentMessage.subscribe(message => {
      this.idBeneficiario = message;
      if (this.idBeneficiario != '0' && this.idBeneficiario != '')
        this.nombreModulo = "Editar"
      else
        this.nombreModulo = "Alta de";
    });
    if (this.idBeneficiario != '0' && this.idBeneficiario != '')
      this.request.get("/Proveedor/GetProveedorById?IdProveedor=" + this.idBeneficiario).subscribe(res => {
        let resp = JSON.parse(res);

        if (resp.NacionalExtranjero == "true") {
          resp.NacionalExtranjero = "true";
          this.esNacional = "Nacional";
        } else {
          resp.NacionalExtranjero = "false";
          this.esNacional = "Extranjero";
        }
        let fecha = this.formatDate(resp.FechaAlta);
        this.listBanks = resp.Bancos;
        this.listAdjuntos = resp.Adjuntos;
        this.newProveedor = this.formBuilder.group({
          Id: resp.Id,
          RazonSocial: [resp.RazonSocial, [Validators.required]],
          Descripcion: [resp.Descripcion],
          Cuit: [resp.Cuit, [Validators.required]],
          FechaAlta: [fecha],
          NacionalExtranjero: [resp.NacionalExtranjero, Validators.required],
          Email: [resp.Email],
          Telefono: [resp.Telefono]
        });
      });
    this.resetForm();
    this.ResetFormFiles();
    this.GetAdjuntosByBeneficiario();
  }
  SaveProveedor(data) {
    data.bancos = this.listBanks;
    var esNacionalInt = 0;
    if (this.esNacional == 'Nacional') {
      data.NacionalExtranjero = 'true';
      esNacionalInt = 1;
    } else
      data.NacionalExtranjero = 'false';

    var cuentasErroneas = false;
    for (let bank of this.listBanks) {
      if (bank.EsNacional != esNacionalInt) {
        cuentasErroneas = true;
      }
    }
    if (cuentasErroneas) {
      //this.infoMessage = ;
      swal({
        position: 'center',
        type: 'warning',
        title: "Falta información obligatoria en las cuentas bancarias.",
        showConfirmButton: true
      });
    } else {
      this.loading = true;

      this.request.post("/Proveedor/save", data).subscribe(resp => {
        if (resp.code == 200) {
          this.idBeneficiario = resp.result;
          if (this.nameFile != "")
            this.SaveFile();
          this.resetForm();
          this.infoMessage = "Los datos se guardaron con éxito";
          this.loading = false;
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
          this.loading = false;
        }
      });
    }
  }

  private resetForm() {
    // cuenta details form validations
    this.newProveedor = this.formBuilder.group({
      Id: this.idBeneficiario,
      RazonSocial: ['', [Validators.required]],
      Descripcion: [''],
      Cuit: ['', [Validators.required]],
      FechaAlta: [''],
      NacionalExtranjero: ['true', Validators.required],
      Email: [''],
      Telefono: ['']
    });
  }

  returnList() {
    this.data.changeMessage("0");
    this.routes.navigate(['../dashboard/beneficiarios']);
    this.modalService.dismissAll();
  }
  NacionalExtranjeroChange() {
    if (this.esNacional == "Nacional") {
      this.esNacional = "Extranjero";
    } else {
      this.esNacional = "Nacional";
    }
  }
  /////////////////////////////////////// CUENTA BANCARIA PROVEEDOR//////////////

  OpenModalCuentaBancariaBeneficiario(nroCuenta: string, ModalCuentaBancariaBeneficiario: string, nombreModal: string) {
    this.nombreModal = nombreModal;
    this.ResetCuentaBancariaBeneficiario();
    this.modalService.open(ModalCuentaBancariaBeneficiario, { size: 'lg', backdropClass: 'light-blue-backdrop' });
    this.ResetCuentaBancariaBeneficiario();
    if (nroCuenta != '0')
      this.listBanks.forEach((item, index) => {
        if (item.Id == nroCuenta) {
          let resp = this.listBanks[index];

          let fechaVigencia = this.formatDate(resp.FechaVigencia);
          if ((<HTMLInputElement>document.getElementById("NacionalExtranjero")).value == "true")
            this.newBancoBeneficiario = this.formBuilder.group({
              Id: [resp.Id],
              IdBeneficiario: [resp.IdBeneficiario],
              TipoCuenta: [resp.TipoCuenta, Validators.required],
              NroCuenta: [resp.NroCuenta, [
                Validators.required
              ]],
              NombreBanco: [resp.NombreBanco, Validators.required],
              Sucursal: [resp.Sucursal, Validators.required],
              Cbu: [resp.Cbu, [
                Validators.minLength(22),
                Validators.maxLength(22),
                Validators.pattern('[0-9]+'),
                Validators.required
              ]],
              Titular: [resp.Titular, Validators.required],
              FechaVigencia: [fechaVigencia, Validators.required],
              Direccion: [resp.Direccion],
              EsNacional: [1],
              BicSwift: [resp.BicSwift],
              Cuit: [resp.Cuit]
            });
          else
            this.newBancoBeneficiario = this.formBuilder.group({
              Id: [resp.Id],
              IdBeneficiario: [resp.IdBeneficiario],
              TipoCuenta: [resp.TipoCuenta, Validators.required],
              NroCuenta: [resp.NroCuenta, [Validators.required]],
              NombreBanco: [resp.NombreBanco, Validators.required],
              Sucursal: [resp.Sucursal, Validators.required],
              Cbu: [resp.Cbu, [Validators.required]],
              Titular: [resp.Titular, Validators.required],
              FechaVigencia: [fechaVigencia, Validators.required],
              Direccion: [resp.Direccion],
              EsNacional: [0],
              BicSwift: [resp.BicSwift, Validators.required],
              Cuit: [resp.Cuit, Validators.required]
            });
        }
      });
  }
  SaveBancoBeneficiario(data) {
    this.DeleteBancoBeneficiario(data.Id);
    data.Id = this.idPush;
    if (data.TipoCuenta == null)
      data.TipoCuenta = "";

    let cuentaExistente = this.validarCuentaExistente(data);

    if (!cuentaExistente) {
      if ((<HTMLInputElement>document.getElementById("NacionalExtranjero")).value == "true") {
        data.Direccion = '';
        data.BicSwift = '';
        data.esNacional = 1;
        data.Cuit = '';
      } else {
        data.esNacional = 0;
      }
      this.listBanks.push(data);
    }
    this.idPush--;
    this.modalService.dismissAll();

    if (cuentaExistente) {
      //this.infoMessage = ;
      swal({
        position: 'center',
        type: 'warning',
        title: "Ya existe la cuenta bancaria para éste beneficiario.",
        showConfirmButton: true
      });
    }
  }
  DeleteBancoBeneficiario(nroCuenta: string) {
    this.listBanks.forEach((item, index) => {
      if (item.Id == nroCuenta) {
        this.listBanks.splice(index, 1);
      }
    });
  }
  ResetCuentaBancariaBeneficiario() {
    var valor = (<HTMLInputElement>document.getElementById("NacionalExtranjero")).value;
    var f = this.esNacional;
    if (valor == "true")
      this.newBancoBeneficiario = this.formBuilder.group({
        Id: [0, []],
        IdBeneficiario: [this.idBeneficiario],
        TipoCuenta: ['', [Validators.required]],
        NroCuenta: ['', [
          Validators.required
        ]],
        NombreBanco: ['', Validators.required],
        Sucursal: ['', [Validators.required]],
        Cbu: ['', [
          Validators.minLength(22),
          Validators.maxLength(22),
          Validators.required,
          Validators.pattern('[0-9]+'),

        ]],
        Titular: ['', [Validators.required]],
        FechaVigencia: ['', Validators.required],
        Direccion: ['', []],
        EsNacional: [1],
        BicSwift: ['', []],
        Cuit: ['']
      });
    else
      this.newBancoBeneficiario = this.formBuilder.group({
        Id: [0, []],
        IdBeneficiario: [this.idBeneficiario],
        TipoCuenta: ['', Validators.required],
        NroCuenta: ['', [Validators.required]],
        NombreBanco: ['', Validators.required],
        Sucursal: ['', Validators.required],
        Cbu: ['', [Validators.required]],
        Titular: ['', Validators.required],
        FechaVigencia: ['', Validators.required],
        Direccion: [''],
        EsNacional: [0],
        BicSwift: ['', Validators.required],
        Cuit: ['', Validators.required]
      });
  }
  validarCuentaExistente(data) {
    for (let bank of this.listBanks) {
      if ((bank.NroCuenta == data.NroCuenta && this.newProveedor.controls["NacionalExtranjero"].value == false) || (bank.Cbu == data.Cbu && this.newProveedor.controls["NacionalExtranjero"].value == true)) {
        return true;
      }
    }
    return false;
  }
  //////////////////////////////////////FIN MODAL BANCOS/////////////////////////////////////////////////////////
  OpenModalDeleteCuentaProveedor(id: number, cbu: string, ModalDeleteCuentaBancariaProveedor: string) {
    this.cbuToDelete = cbu;
    this.idBankToDelete = id;
    swal({
      title: 'Eliminar',
      text: "¿Estás seguro que deseas Eliminar la cuenta: " + this.cbuToDelete,
      type: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      preConfirm: () => [this.DeleteBancoProveedor()],
      confirmButtonText: 'Si, Eliminar!',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.value) {
        swal(
          'Eliminado!',
          'Se eliminó con éxito.',
          'success'
        )
      }
    });
    //this.modalService.open(ModalDeleteCuentaBancariaProveedor, { backdropClass: 'light-blue-backdrop' });
  }
  DeleteBancoProveedor() {
    this.request.delete("/ProveedorBancos/Delete?id=" + this.idBankToDelete).subscribe(resp => {
      this.GetBancosToProveedor();
      this.modalService.dismissAll();
    });
  }
  GetBancosToProveedor() {
    this.request.get("/ProveedorBancos/GetBancosToProveedor?idProveedor=" + this.idBeneficiario).subscribe(resp => {
      this.listBanks = resp;
    });
  }
  ///////////////////FIN DE BANCOS PROVEEDOR//////////////////////////////////////////////////////////////////////////////

  ///////////////////////////////FILES PROVEEDOR //////////////////////////////////////////////////////////////////
  SaveFile() {
    this.loading = true;
    this.prepareSaveFile();
    this.request
      .createDataFile('/ProveedorAdjuntos', this.prepareSaveFile())
      .subscribe(resp => {
        if (resp != null) {
          this.infoMessage = "El archivo se guardó con éxito";
          this.ResetFormFiles();
          this.request.get("/ProveedorAdjuntos/GetAdjuntosByBeneficiario?id=" + this.idBeneficiario).subscribe(resp => {
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
        idEntidad: new FormControl(this.idBeneficiario),
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
    formData.append('idEntidad', this.idBeneficiario);
    formData.append('archivo', formModel.archivo);

    return formData;
  }
  fileChange(files: FileList) {
    this.nameFile = files[0].name;
    if (files && files[0].size > 0) {
      this.formFiles.patchValue({
        archivo: files[0]
      });
      if (this.idBeneficiario != '0' && this.idBeneficiario != '')
        this.SaveFile();
    }
  }
  GetAdjuntosByBeneficiario() {
    this.request.get("/ProveedorAdjuntos/GetAdjuntosByBeneficiario?id=" + this.idBeneficiario).subscribe(resp => {
      this.listAdjuntos = resp;
    });
  }
  deleteAdjunto(idAdjunto: number) {
    this.request.delete("/ProveedorAdjuntos/delete?id=" + idAdjunto).subscribe(resp => {
      this.infoMessage = resp;
      this.request.get("/ProveedorAdjuntos/GetAdjuntosByBeneficiario?id=" + this.idBeneficiario).subscribe(resp => {
        this.listAdjuntos = resp;
      });
    });
  }
  getFileById(idAdjunto: string) {
    window.open(Constants.URL_BASE_WEB_API + '/File/GetFile?id=' + idAdjunto);
  }
  ////////////////FIN FILES PROVEEDOR ///////////////////////////////////////////////////////////////////////////////////
  private formatDate(fecha: string) {

    if (fecha == undefined || fecha == "") return "";

    var part = fecha.split('T');
    return part[0];
  }

}
