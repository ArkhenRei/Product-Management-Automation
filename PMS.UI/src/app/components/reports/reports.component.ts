import { Component, OnInit } from '@angular/core';
import { ProductWarehouse } from 'src/app/models/productWarehouse.model';
import { WarehouseService } from 'src/app/services/warehouse.service';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.css']
})
export class ReportsComponent implements OnInit {
  reports: ProductWarehouse[] = [];

  constructor(private warehouseService: WarehouseService) { }

  ngOnInit(): void {
    this.warehouseService.getReports()
      .subscribe({
        next: (reports) => {
          this.reports = reports;
        },
        error: (response) => {
          console.log(response);
        }
      });
  }

}
