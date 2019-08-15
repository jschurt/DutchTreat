import * as tslib_1 from "tslib";
//Tornando um componente angular para que possamos usar em nossas paginas
import { Component } from "@angular/core";
let ProductList = class ProductList {
    constructor() {
        this.products = [
            {
                title: "First Product",
                price: 19.99
            },
            {
                title: "Second Product",
                price: 9.99
            },
            {
                title: "Third Product",
                price: 14.99
            }
        ];
    }
};
ProductList = tslib_1.__decorate([
    Component({
        selector: "product-list",
        templateUrl: "productList.component.html",
        styleUrls: []
    })
], ProductList);
export { ProductList };
//# sourceMappingURL=productList.component.js.map