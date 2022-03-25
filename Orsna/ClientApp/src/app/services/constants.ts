import { Component, OnInit, ViewEncapsulation, Inject, Input } from '@angular/core';
import { DOCUMENT } from '@angular/common';
declare function require(url: string);

export class Constants {

  public static mypath: any = require("../../../angular.json");

  private static baseHref: string = Constants.mypath.projects.Orsna.architect.build.options.baseHref;

  public static base: string = Constants.spl(Constants.baseHref);

  public static URL_BASE: string = Constants.base;

  public static URL_BASE_WEB_API: string = Constants.URL_BASE + "/api";

  static readonly DATE_FMT = 'dd/MM/yyyy';

  static readonly DATE_TIME_FMT = `${Constants.DATE_FMT} hh:mm:ss`;

  static readonly API_PROYECTO = "/Proyecto";
  static readonly API_PROVINCIA = "/Provincia";
  static readonly API_PROYECTO_ADJUNTOS = "/ProyectoAdjuntos";
  static readonly API_LIBRANZA_ADJUNTOS = "/LibranzaAdjuntos";

  static readonly API_PROYECTO_GETALLREDUCIDO = "/GetAllReducido";

  static readonly API_AEROPUERTO = "/Aeropuerto";
  static readonly API_PROVEEDOR = "/Proveedor";
  static readonly API_SEGURIDAD = "/Seguridad";
  static readonly API_ROL = "/Rol";
  static readonly API_LIBRANZA = "/Libranza";
  static readonly API_AREA = "/Area";
  static readonly API_CUENTA = "/Cuenta";
  static readonly API_USUARIO = "/Usuario";
  static readonly API_FILE = "/File";

  static readonly API_GET_PERMISOS = "/GetPermisos";
  static readonly API_GET_MODULOS = "/GetModulos";

  static readonly API_GET_PERMISOS_VER = "/GetPermisosVer";
  static readonly API_GET_ESTADOS = "/GetEstados";
  static readonly API_GET_LIBRANZA_TIPO = "/GetLibranzaTipo";
  static readonly API_GET_LIBRANZA_BY_ID = "/GetLibranzaById";
  static readonly API_GET_CUENTA_BY_ID = "/GetCuentaById";
  static readonly API_GET_USUARIO_BY_ID = "/GetUsuarioById";


  static readonly API_GET_NROLIBRANZA = "/GetNroLibranza";

  static readonly API_GET_IDTENTATIVO = "/GetIdTentativo";

  static readonly API_GET_DESTINOS = "/GetDestinos";
  static readonly API_GET_ANIOS = "/GetAnios";
  static readonly API_GET_PROYECTOS_IDS = "/GetProyectosIds";
  static readonly API_GET_AEROPUERTOS_GRUPO = "/GetAeropuertosGrupo";

  static readonly API_GET_PROYECTO_ID = "/GetProyectoById";
  static readonly API_GET_AREA_BY_ID = "/GetAreaById";
  static readonly API_GET_ROL_BY_ID = "/GetRolById";

  static readonly API_GET_ADJUNTOS_PROYECTOS = "/GetAdjuntosByProyecto";
  static readonly API_GET_ADJUNTOS_LIBRANZA = "/GetAdjuntosByLibranza";
  static readonly API_GET_MONTODISPONIBLE_PROYECTO_LIBRANZA = "/GetMontoDisponibleProyectoByIdLibranza";


  static readonly API_GET_AEROS_TO_CUENTA = "/GetAerosToCuenta";



  static readonly API_GET_FILTRADAS_USUARIO = "/GetFiltradasPorUsuario";
  static readonly API_GET_COUNT_FILTER_ELEMENTS = "/GetCountFilterElements";
  static readonly API_GET_COUNT_PAGES = "/GetCountPages";
  static readonly API_GET_ALL = "/GetAll";
  static readonly API_GET_FILE = "/GetFile";
  static readonly API_GET_FILTER = "/GetFilter";
  static readonly API_GET_AREAS = "/GetAreas";
  static readonly API_GET_ROLES = "/GetRoles";



  static readonly API_DELETE = "/Delete";
  static readonly API_SAVE = "/Save";
  static readonly API_DOWNLOAD_PDF = "/DownloadPDF";
  static readonly API_GETXLSFILTER = "/GetXLSFilter";
  static readonly API_CAMBIAR_ESTADO = "/CambiarEstado";
  static readonly API_GET_SIGUIENTE_ESTADO = "/GetSiguientesEstado";
  static readonly API_GET_MONTO_A_PAGAR_BY_IDLIBRANZA = "/GetMontoAPagarByIdLibranza";

  static readonly API_GET_MONEDAS = "/GetAllMoneda";
  static readonly LIBRANZA_ESTADO_ANULADA = "8";
  static readonly LIBRANZA_ESTADO_PAGADA = "7";
  static readonly MONEDA_ARGENTINA = 1;
  static readonly AREA_GAP = 8;
  static readonly API_PUEDEEDITARMONTO = "/PuedeEditarMonto";
  
  
  static spl(url: string): string {
    let sp = url.replace(/\//g, "");
    if (sp.length > 0)
      return '/' + sp;
    else
      return "";
  }
}
