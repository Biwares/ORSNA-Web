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
    return Constants;
}());
exports.Constants = Constants;
//# sourceMappingURL=constants.js.map