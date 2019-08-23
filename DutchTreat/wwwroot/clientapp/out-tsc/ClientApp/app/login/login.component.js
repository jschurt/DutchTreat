import * as tslib_1 from "tslib";
import { Component } from '@angular/core';
var Login = /** @class */ (function () {
    function Login(data, router) {
        this.data = data;
        this.router = router;
        this.creds = {
            username: "",
            password: ""
        };
    }
    Login.prototype.onLogin = function () {
        //Call the login service
        alert(this.creds.username);
    };
    Login = tslib_1.__decorate([
        Component({
            selector: "login",
            templateUrl: 'login.component.html'
        })
    ], Login);
    return Login;
}());
export { Login };
//# sourceMappingURL=login.component.js.map