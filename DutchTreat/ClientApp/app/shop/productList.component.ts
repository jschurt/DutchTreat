//Tornando um componente angular para que possamos usar em nossas paginas
import { Component, OnInit } from "@angular/core";
import { DataService } from '../shared/dataService';
import { Product } from '../shared/product';


@Component({
    selector: "product-list",
    templateUrl: "productList.component.html",
    styleUrls: ["productList.component.css"]
})

export class ProductList implements OnInit {

    public products: Product[] = [];

    //injetando DataService
    constructor(private data: DataService) {
    }

    ngOnInit(): void {
        this.data.loadProducts()
            .subscribe(success => {
                if (success) {
                    this.products = this.data.products;
                }
            });
    }

    addProduct(product: Product) {
        this.data.AddToOrder(product);
    }


}