import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Product } from 'src/app/models/product.model';
import { Warehouse } from 'src/app/models/warehouse.model';
import { ProductsService } from 'src/app/services/products.service';
import { WarehouseService } from 'src/app/services/warehouse.service';

@Component({
  selector: 'app-warehouse',
  templateUrl: './warehouse.component.html',
  styleUrls: ['./warehouse.component.css']
})
export class WarehouseComponent implements OnInit {
  products: Product[] = [];
  warehouses: Warehouse[] = [];

  constructor(private productsService: ProductsService, private warehouseService: WarehouseService, private router: Router) { }

  ngOnInit(): void {
    this.productsService.getAllProducts()
      .subscribe({
        next: (products) => {
          this.products = products;
        },
        error: (response) => {
          console.log(response);
        }
      });
    this.warehouseService.getAllWarehouses()
      .subscribe({
        next: (warehouse) => {
          this.warehouses = warehouse;
        },
        error: (response) => {
          console.log(response);
        }
      })
  }
    
  }


