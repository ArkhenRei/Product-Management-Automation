import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Warehouse } from '../models/warehouse.model';
import { ProductWarehouse } from '../models/productWarehouse.model';

@Injectable({
  providedIn: 'root'
})
export class WarehouseService {
  baseApiUrl: string="https://localhost:44348"

constructor(private http: HttpClient) { }

getAllWarehouses(): Observable<Warehouse[]> {
  return this.http.get<Warehouse[]>(this.baseApiUrl + '/api/Warehouse/get-all-warehouses');
}

addWarehouse(newWarehouse: Warehouse): Observable<Warehouse>{
  newWarehouse.id = '0';
  return this.http.post<Warehouse>(this.baseApiUrl + '/api/Warehouse/add-warehouse', newWarehouse);
}

getWarehouse(id: string): Observable<Warehouse>{
  return this.http.get<Warehouse>(this.baseApiUrl + '/api/Warehouse/get-warehouse-by-id?id=' + id);
}

updateWarehouse(id: string, updateWarehouseRequest: Warehouse): Observable<Warehouse> {
  return this.http.put<Warehouse>(this.baseApiUrl + '/api/Warehouse/update-warehouse?id='+id,updateWarehouseRequest);
}

deleteWarehouse(id: string): Observable<Warehouse>{
  return this.http.delete<Warehouse>(this.baseApiUrl + '/api/Warehouse?id='+id);
}

getReports(): Observable<ProductWarehouse[]>{
  return this.http.get<ProductWarehouse[]>(this.baseApiUrl + '/api/Warehouse/get-all-importexport');
}

async addProductToWarehouse(productWarehouse: ProductWarehouse): Promise<any> {
  const response = await this.http.post(`/api/warehouse/products`, productWarehouse);
  return response;
}

async removeProductFromWarehouse(warehouseId: number, productId: string, quantity: number): Promise<any> {
  const response = await this.http.post(`/api/warehouse/remove-product/${warehouseId}/${productId}`, { quantity });
  return response;
}
}
