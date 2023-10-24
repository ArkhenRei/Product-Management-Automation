import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProductsComponent } from './components/products/products.component';
import { AddProductComponent } from './components/add-product/add-product.component';
import { EditProductComponent } from './components/edit-product/edit-product.component';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { NgToastModule } from 'ng-angular-popup';
import { TokenInterceptor } from './Interceptors/token.interceptor';
import { HeaderComponent } from './components/header/header.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { WarehouseComponent } from './components/warehouse/warehouse.component';
import { WarehouseListComponent } from './components/Warehouse-List/Warehouse-List.component';
import { ReportsComponent } from './components/reports/reports.component';

@NgModule({
  declarations: [
    AppComponent,
    ProductsComponent,
    AddProductComponent,
    EditProductComponent,
    LoginComponent,
    SignupComponent,
    DashboardComponent,
    HeaderComponent,
    SidebarComponent,
    WarehouseComponent,
    WarehouseListComponent,
    ReportsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgToastModule
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: TokenInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
