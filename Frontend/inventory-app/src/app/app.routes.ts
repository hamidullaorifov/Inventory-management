import { Routes } from '@angular/router';
import { LoginComponent } from './features/auth/login/login.component';
import { RegisterComponent } from './features/auth/register/register.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { AuthGuard } from './core/guards/auth.guard';
import { InventoriesListComponent } from './features/inventory/inventories-list/inventories-list.component';
import { InventoryCreateComponent } from './features/inventory/inventory-create/inventory-create.component';

export const routes: Routes = [
  { path: '', component: InventoriesListComponent },
  { path: 'inventory/create', component: InventoryCreateComponent, canActivate: [AuthGuard]},
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: '**', redirectTo: '' }
];