"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var forms_1 = require("@angular/forms");
var ConsultaCuentasComponent = /** @class */ (function () {
    function ConsultaCuentasComponent(request, fb, routes, data) {
        this.request = request;
        this.fb = fb;
        this.routes = routes;
        this.data = data;
        this.page = 1;
        this.FilterNroCuenta = "";
        this.FilterNombre = "";
        this.FilterTipoCuenta = "";
        this.Order = "desc";
        this.ColumnOrder = "FechaCreacion";
        this.CountPages = 0;
        this.message = "";
        this.form = fb.group({
            Identificador: ['', forms_1.Validators.required],
            Nombre: ['', forms_1.Validators.required],
            Descripcion: ['', forms_1.Validators.required],
            FechaVigencia: ['', forms_1.Validators.required]
        });
    }
    ConsultaCuentasComponent.prototype.ngOnInit = function () {
        this.GetAll(this.Order, this.ColumnOrder);
    };
    ConsultaCuentasComponent.prototype.GetCountPages = function (order, columnOrder) {
        var _this = this;
        if (columnOrder != undefined && columnOrder != null)
            this.ColumnOrder = columnOrder;
        if (order != undefined && order != null)
            this.Order = order;
        this.request.get("/cuenta/GetCountPages?page=" + this.page + "&FilterNroCuenta=" + this.FilterNroCuenta + "&FilterNombre=" + this.FilterNombre + "&FilterTipoCuenta=" + this.FilterTipoCuenta + "&Order=" + this.Order + "&ColumnOrder=" + this.ColumnOrder).subscribe(function (resp) {
            _this.CountPages = resp * 10;
        });
    };
    ConsultaCuentasComponent.prototype.GetAll = function (order, columnOrder) {
        var _this = this;
        if (columnOrder != undefined && columnOrder != null)
            this.ColumnOrder = columnOrder;
        if (order != undefined && order != null)
            this.Order = order;
        this.request.get("/cuenta/getall?page=" + this.page + "&FilterNroCuenta=" + this.FilterNroCuenta + "&FilterNombre=" + this.FilterNombre + "&FilterTipoCuenta=" + this.FilterTipoCuenta + "&Order=" + this.Order + "&ColumnOrder=" + this.ColumnOrder).subscribe(function (resp) {
            _this.cuentas = resp;
        });
        this.GetCountPages(this.Order, this.ColumnOrder);
    };
    ConsultaCuentasComponent.prototype.EditCuenta = function (id) {
        this.data.changeMessage(id + "");
        this.routes.navigate(['../dashboard/cuenta']);
    };
    ConsultaCuentasComponent.prototype.NewCuenta = function () {
        this.data.changeMessage("0");
        this.routes.navigate(['../dashboard/cuenta']);
    };
    return ConsultaCuentasComponent;
}());
exports.ConsultaCuentasComponent = ConsultaCuentasComponent;
//# sourceMappingURL=consulta-cuentas.component.js.map