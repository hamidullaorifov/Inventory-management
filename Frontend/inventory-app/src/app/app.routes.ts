import { Routes } from '@angular/router';
import { LoginComponent } from './features/auth/login/login.component';
import { RegisterComponent } from './features/auth/register/register.component';
import { AuthGuard } from './core/guards/auth.guard';
import { InventoriesListComponent } from './features/inventory/inventories-list/inventories-list.component';
import { InventoryCreateComponent } from './features/inventory/inventory-create/inventory-create.component';
import { InventoryDetailsComponent } from './features/inventory/inventory-details/inventory-details.component';
import { AddItemComponent } from './features/inventory/add-item/add-item.component';

export const routes: Routes = [
  { path: 'inventory/:id', component: InventoryDetailsComponent },
  { path: 'inventory/:id/add-item', component: AddItemComponent },
  { path: '', component: InventoriesListComponent },
  { path: 'inventory/create', component: InventoryCreateComponent, canActivate: [AuthGuard]},
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: '**', redirectTo: '' }
];