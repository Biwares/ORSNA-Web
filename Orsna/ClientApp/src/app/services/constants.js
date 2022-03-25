"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Constants = /** @class */ (function () {
    function Constants() {
    }
    Constants.spl = function (url) {
        var sp = url.replace(/\//g, "");
        if (sp.length > 0)
            return '/' + sp;
        else
            return "";
    };
    Constants.mypath = require("../../../angular.json");
    Constants.baseHref = Constants.mypath.projects.Orsna.architect.build.options.baseHref;
    Constants.base = Constants.spl(Constants.baseHref);
    Constants.URL_BASE = Constants.base;
    Constants.URL_BASE_WEB_API = Constants.URL_BASE + "/api";
    Constants.DATE_FMT = 'dd/MM/yyyy';
    Constants.DATE_TIME_FMT = Constants.DATE_FMT + " hh:mm:ss";
    Constants.API_PROYECTO = "/Proyecto";
    Constants.API_PROVINCIA = "/Provincia";
    Constants.API_PROYECTO_ADJUNTOS = "/ProyectoAdjuntos";
    Constants.API_LIBRANZA_ADJUNTOS = "/LibranzaAdjuntos";
    Constants.API_PROYECTO_GETALLREDUCIDO = "/GetAllReducido";
    Constants.API_AEROPUERTO = "/Aeropuerto";
    Constants.API_PROVEEDOR = "/Proveedor";
    Constants.API_SEGURIDAD = "/Seguridad";
    Constants.API_ROL = "/Rol";
    Constants.API_LIBRANZA = "/Libranza";
    Constants.API_AREA = "/Area";
    Constants.API_CUENTA = "/Cuenta";
    Constants.API_USUARIO = "/Usuario";
    Constants.API_FILE = "/File";
    Constants.API_GET_PERMISOS = "/GetPermisos";
    Constants.API_GET_MODULOS = "/GetModulos";
    Constants.API_GET_PERMISOS_VER = "/GetPermisosVer";
    Constants.API_GET_ESTADOS = "/GetEstados";
    Constants.API_GET_LIBRANZA_TIPO = "/GetLibranzaTipo";
    Constants.API_GET_LIBRANZA_BY_ID = "/GetLibranzaById";
    Constants.API_GET_CUENTA_BY_ID = "/GetCuentaById";
    Constants.API_GET_USUARIO_BY_ID = "/GetUsuarioById";
    Constants.API_GET_NROLIBRANZA = "/GetNroLibranza";
    Constants.API_GET_IDTENTATIVO = "/GetIdTentativo";
    Constants.API_GET_DESTINOS = "/GetDestinos";
    Constants.API_GET_ANIOS = "/GetAnios";
    Constants.API_GET_PROYECTOS_IDS = "/GetProyectosIds";
    Constants.API_GET_AEROPUERTOS_GRUPO = "/GetAeropuertosGrupo";
    Constants.API_GET_PROYECTO_ID = "/GetProyectoById";
    Constants.API_GET_AREA_BY_ID = "/GetAreaById";
    Constants.API_GET_ROL_BY_ID = "/GetRolById";
    Constants.API_GET_ADJUNTOS_PROYECTOS = "/GetAdjuntosByProyecto";
    Constants.API_GET_ADJUNTOS_LIBRANZA = "/GetAdjuntosByLibranza";
    Constants.API_GET_MONTODISPONIBLE_PROYECTO_LIBRANZA = "/GetMontoDisponibleProyectoByIdLibranza";
    Constants.API_GET_AEROS_TO_CUENTA = "/GetAerosToCuenta";
    Constants.API_GET_FILTRADAS_USUARIO = "/GetFiltradasPorUsuario";
    Constants.API_GET_COUNT_FILTER_ELEMENTS = "/GetCountFilterElements";
    Constants.API_GET_COUNT_PAGES = "/GetCountPages";
    Constants.API_GET_ALL = "/GetAll";
    Constants.API_GET_FILE = "/GetFile";
    Constants.API_GET_FILTER = "/GetFilter";
    Constants.API_GET_AREAS = "/GetAreas";
    Constants.API_GET_ROLES = "/GetRoles";
    Constants.API_DELETE = "/Delete";
    Constants.API_SAVE = "/Save";
    Constants.API_DOWNLOAD_PDF = "/DownloadPDF";
    Constants.API_GETXLSFILTER = "/GetXLSFilter";
    Constants.API_CAMBIAR_ESTADO = "/CambiarEstado";
    Constants.API_GET_SIGUIENTE_ESTADO = "/GetSiguientesEstado";
    Constants.API_GET_MONTO_A_PAGAR_BY_IDLIBRANZA = "/GetMontoAPagarByIdLibranza";
    Constants.API_GET_MONEDAS = "/GetAllMoneda";
    Constants.LIBRANZA_ESTADO_ANULADA = "8";
    Constants.LIBRANZA_ESTADO_PAGADA = "7";
    Constants.MONEDA_ARGENTINA = 1;
    Constants.AREA_GAP = 8;
    Constants.API_PUEDEEDITARMONTO = "/PuedeEditarMonto";
    return Constants;
}());
exports.Constants = Constants;
//# sourceMappingURL=constants.js.map