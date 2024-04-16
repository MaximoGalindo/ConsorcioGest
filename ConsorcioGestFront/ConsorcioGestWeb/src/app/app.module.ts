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

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,    
    MainPageAdminComponent,
    ClaimsGestComponent,
    UsersGestComponent,
    EditUserModalComponent  
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    NgbModule, 
  ],
  providers: [httpInterceptorProviders],
  bootstrap: [AppComponent]
})
export class AppModule { }
