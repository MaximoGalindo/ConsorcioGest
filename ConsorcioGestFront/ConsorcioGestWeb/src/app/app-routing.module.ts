import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './Components/login/login.component';
import { RegisterComponent } from './Components/register/register.component';
import { MainPageAdminComponent } from './Components/main-page-admin/main-page-admin.component';
import { ClaimsGestComponent } from './Components/Claims/claims-gest/claims-gest.component';
import { SelectConsortiumComponent } from './Components/Consortium/select-consortium/select-consortium.component';
import { RegisterConsortiumComponent } from './Components/Consortium/register-consortium/register-consortium.component';
import { ConfigGridComponent } from './Components/Consortium/Modals/config-grid/config-grid.component';
import { SaveConsortiumComponent } from './Components/Consortium/save-consortium/save-consortium.component';
import { MainPageUserComponent } from './Components/main-page-user/main-page-user.component';

import { MenuUserComponent } from './Components/menu-user/menu-user.component';
import { UsersGestComponent } from './Components/Users/users-gest/users-gest.component';
import { UserRegisterClaimComponent } from './Components/Users/user-register-claim/user-register-claim.component';
import { ClaimTrackingComponent } from './Components/Users/claim-tracking/claim-tracking.component';

const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'main-page-admin',component: MainPageAdminComponent,
    children : [
      {path: 'claims-gest', component: ClaimsGestComponent},
      {path: 'user-gest',component:UsersGestComponent}
    ]
  },
  { path: 'main-page-user', component: MainPageUserComponent,
    children: [
      {path: '', component: MenuUserComponent}, 
      {path: 'claim-user', component: UserRegisterClaimComponent},
      {path: 'claim-traking', component: ClaimTrackingComponent}
    ]},
  { path: 'consortium', component: SelectConsortiumComponent},
  { path: 'register-consortium', 
    children: [
      {path:'' , component: RegisterConsortiumComponent},
      {path: 'confirm', component: SaveConsortiumComponent}
    ]  
  },
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
