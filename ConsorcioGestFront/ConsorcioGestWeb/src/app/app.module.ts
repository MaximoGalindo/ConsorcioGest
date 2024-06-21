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
import { SelectConsortiumComponent } from './Components/Consortium/select-consortium/select-consortium.component';
import { CommonModule } from '@angular/common';
import { RegisterConsortiumComponent } from './Components/Consortium/register-consortium/register-consortium.component';
import { ConfigTowerModalComponent } from './Components/Consortium/Modals/config-tower-modal/config-tower-modal.component';
import { ConfigGridComponent } from './Components/Consortium/Modals/config-grid/config-grid.component';
import { SaveConsortiumComponent } from './Components/Consortium/save-consortium/save-consortium.component';
import { ShowConfigTowerComponent } from './Components/Consortium/Modals/show-config-tower/show-config-tower.component';
import { UserRegisterClaimComponent } from './Components/Users/user-register-claim/user-register-claim.component';
import { MainPageUserComponent } from './Components/main-page-user/main-page-user.component';
import { NavbarUserComponent } from './Components/navbar-user/navbar-user.component';
import { FooterUserComponent } from './Components/footer-user/footer-user.component';
import { MenuUserComponent } from './Components/menu-user/menu-user.component';
import { ClaimsInfoComponent } from './Components/Claims/claims-info/claims-info.component';
import { SeeMoreClaimComponent } from './Components/Claims/Modals/see-more-claim/see-more-claim.component';
import { CarruselImagesModalComponent } from './Components/Claims/Modals/carrusel-images-modal/carrusel-images-modal.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ClaimTrackingComponent } from './Components/Users/claim-tracking/claim-tracking.component';
import { NewReservationComponent } from './Components/Reservations/new-reservation/new-reservation.component';
import { ReservationCommonSpacesComponent } from './Components/Reservations/reservation-common-spaces/reservation-common-spaces.component';
import { ReservationsGestComponent } from './Components/Reservations/reservations-gest/reservations-gest.component';
import { CommonSpaceComponent } from './Components/Reservations/common-space/common-space.component';
import { NewsurveyComponent } from './Components/Surveys/newsurvey/newsurvey.component';
import { SurveysGestComponent } from './Components/Surveys/surveys-gest/surveys-gest.component';
import { SeeMoreSurveyComponent } from './Components/Surveys/Modals/see-more-survey/see-more-survey.component';
import { MyReservationsComponent } from './Components/Reservations/my-reservations/my-reservations.component';
import { StatsComponent } from './Components/Stats/stats/stats.component';
import { PieChartComponent } from './Components/Stats/pie-chart/pie-chart.component';
import { BarChartComponent } from './Components/Stats/bar-chart/bar-chart.component';
import { LineChartComponent } from './Components/Stats/line-chart/line-chart.component';
import { NumberClaimsChartComponent } from './Components/Stats/number-claims-chart/number-claims-chart.component';
import { TermsAndConditionsComponent } from './Components/Info/terms-and-conditions/terms-and-conditions.component';
import { FAQSComponent } from './Components/Info/faqs/faqs.component';
import { CommonSpaceConfigComponent } from './Components/Consortium/Modals/common-space-config/common-space-config.component';
import { CancelReservationComponent } from './Components/Reservations/Modals/cancel-reservation/cancel-reservation.component';


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
    ConfigGridComponent,
    SaveConsortiumComponent,
    ShowConfigTowerComponent,
    UserRegisterClaimComponent,
    MainPageUserComponent,
    NavbarUserComponent,
    FooterUserComponent,
    MenuUserComponent,
    ClaimsInfoComponent,
    SeeMoreClaimComponent,
    CarruselImagesModalComponent,
    ClaimTrackingComponent,
    NewReservationComponent,
    ReservationCommonSpacesComponent,
    ReservationsGestComponent,
    CommonSpaceComponent,
    NewsurveyComponent,
    SurveysGestComponent,
    SeeMoreSurveyComponent,
    MyReservationsComponent,
    StatsComponent,
    PieChartComponent,
    BarChartComponent,
    LineChartComponent,
    NumberClaimsChartComponent,
    TermsAndConditionsComponent,
    FAQSComponent,
    CommonSpaceConfigComponent,
    CancelReservationComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    CommonModule,
    NgbModule,

  ],
  providers: [httpInterceptorProviders],
  bootstrap: [AppComponent]
})
export class AppModule { }
