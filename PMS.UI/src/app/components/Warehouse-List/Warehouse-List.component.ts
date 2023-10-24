import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Warehouse } from 'src/app/models/warehouse.model';
import { WarehouseService } from 'src/app/services/warehouse.service';

@Component({
  selector: 'app-Warehouse-List',
  templateUrl: './Warehouse-List.component.html',
  styleUrls: ['./Warehouse-List.component.css']
})
export class WarehouseListComponent implements OnInit {
  warehouses: Warehouse[] = [];

  constructor(private warehouseService: WarehouseService, private router: Router) { }

  ngOnInit(): void {
    this.warehouseService.getAllWarehouses()
      .subscribe({
        next: (warehouses) => {
          this.warehouses = warehouses;
        },
        error: (response) => {
          console.log(response);
        }
      });
  }

  deleteWarehouse(id: string){
    if(confirm("This will delete the warehouse and all the products inside it. Are you sure?")){
      this.warehouseService.deleteWarehouse(id)
        .subscribe({
          next: (response) => {
            location.reload();
          }
        });
    }
  }

}
