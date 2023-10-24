import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductsComponent } from './components/products/products.component';
import { AddProductComponent } from './components/add-product/add-product.component';
import { EditProductComponent } from './components/edit-product/edit-product.component';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { AuthGuard } from './guard/auth.guard';
import { ResetPasswordComponent } from './components/reset-password/reset-password.component';
import { WarehouseComponent } from './components/warehouse/warehouse.component';
import { WarehouseListComponent } from './components/Warehouse-List/Warehouse-List.component';
import { ReportsComponent } from './components/reports/reports.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },
  {
    path: 'products',
    component: ProductsComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'products/add',
    component: AddProductComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'products/edit/:id',
    component: EditProductComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'signup',
    component: SignupComponent
  },
  {
    path: 'dashboard',
    component: DashboardComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'reset',
    component: ResetPasswordComponent,
  },
  {
    path: 'warehouse',
    component: WarehouseComponent
  },
  {
    path: 'warehouse-list',
    component: WarehouseListComponent
  },
  {
    path: 'reports',
    component: ReportsComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
