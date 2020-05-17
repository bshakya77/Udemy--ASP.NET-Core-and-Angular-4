"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var AppErrorHandler = /** @class */ (function () {
    function AppErrorHandler(ngZone, toastyService) {
        this.ngZone = ngZone;
        this.toastyService = toastyService;
    }
    AppErrorHandler.prototype.handleError = function (error) {
        var _this = this;
        this.ngZone.run(function () {
            _this.toastyService.error({
                title: 'Error',
                msg: 'An unexpected error occured.',
                theme: 'bootstrap',
                showClose: true,
                timeout: 5000
            });
        });
    };
    return AppErrorHandler;
}());
exports.AppErrorHandler = AppErrorHandler;
//# sourceMappingURL=app.error-handler.js.map