import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './Components/login/login.component';
import { RegisterComponent } from './Components/register/register.component';
import { MainPageAdminComponent } from './Components/main-page-admin/main-page-admin.component';
import { ClaimsGestComponent } from './Components/Claims/claims-gest/claims-gest.component';
import { SelectConsortiumComponent } from './Components/Consortium/select-consortium/select-consortium.component';
import { RegisterConsortiumComponent } from './Components/Consortium/register-consortium/register-consortium.component';
import { ConfigGridComponent } from './Components/Consortium/Modals/config-grid/config-grid.component';

const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'main-page-admin',
    children : [
      {path: '',component: MainPageAdminComponent},
      {path: 'claims-gest', component: ClaimsGestComponent}
    ]
  },
  { path: 'consortium', component: SelectConsortiumComponent},
  { path: 'register-consortium', component: RegisterConsortiumComponent},
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
