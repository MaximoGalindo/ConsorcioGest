import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './Components/login/login.component';
import { httpInterceptorProviders } from './Helpers/HttpRequestInterceptor';
import { RegisterComponent } from './Components/register/register.component';
import { MainPageAdminComponent } from './Components/main-page-admin/main-page-admin.component';
import { ClaimsGestComponent } from './Components/Claims/claims-gest/claims-gest.component';
import { UsersGestComponent } from './Components/Users/users-gest/users-gest.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialogModule } from '@angular/material/dialog';
import { EditUserModalComponent } from './Components/Users/Modals/edit-user-modal/edit-user-modal.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SelectConsortiumComponent } from './Components/Consortium/select-consortium/select-consortium.component';
import { CommonModule } from '@angular/common';
import { RegisterConsortiumComponent } from './Components/Consortium/register-consortium/register-consortium.component';
import { ConfigTowerModalComponent } from './Components/Consortium/Modals/config-tower-modal/config-tower-modal.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,    
    MainPageAdminComponent,
    ClaimsGestComponent,
    UsersGestComponent,
    EditUserModalComponent,
    SelectConsortiumComponent,
    RegisterConsortiumComponent,
    ConfigTowerModalComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    NgbModule,
    CommonModule
  ],
  providers: [httpInterceptorProviders],
  bootstrap: [AppComponent]
})
export class AppModule { }
