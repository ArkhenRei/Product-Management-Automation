import { Injectable } from '@angular/core';
import { Product } from '../models/product.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {
  baseApiUrl: string="https://localhost:44348";

  constructor(private http: HttpClient) { }

  getAllProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(this.baseApiUrl + '/api/Products/get-all-products');
  }

  addProduct(newProduct : Product): Observable<Product>{
    newProduct.id = '00000000-0000-0000-0000-000000000000';
    return this.http.post<Product>(this.baseApiUrl + "/api/products/add-product", newProduct);
  }

  getProduct(id: string): Observable<Product> {
    return this.http.get<Product>(this.baseApiUrl + '/api/products/get-product-by-id?id=' + id);
  }

  updateProduct(id: string, updateProductRequest: Product): Observable<Product> {
    return this.http.put<Product>(this.baseApiUrl + '/api/products/update-product?id=' + id, updateProductRequest);
  }

  deleteProduct(id: string): Observable<Product> {
    return this.http.delete<Product>(this.baseApiUrl + '/api/products?id='+id);
  }
}
