import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './Components/login/login.component';
import { RegisterComponent } from './Components/register/register.component';
import { MainPageAdminComponent } from './Components/main-page-admin/main-page-admin.component';
import { ClaimsGestComponent } from './Components/Claims/claims-gest/claims-gest.component';
import { SelectConsortiumComponent } from './Components/Consortium/select-consortium/select-consortium.component';
import { RegisterConsortiumComponent } from './Components/Consortium/register-consortium/register-consortium.component';
import { ConfigGridComponent } from './Components/Consortium/Modals/config-grid/config-grid.component';
import { MainPageUserComponent } from './Components/main-page-user/main-page-user.component';

import { MenuUserComponent } from './Components/menu-user/menu-user.component';
import { UsersGestComponent } from './Components/Users/users-gest/users-gest.component';
import { UserRegisterClaimComponent } from './Components/Users/user-register-claim/user-register-claim.component';
import { ClaimTrackingComponent } from './Components/Users/claim-tracking/claim-tracking.component';
import { NewReservationComponent } from './Components/Reservations/new-reservation/new-reservation.component';
import { ReservationCommonSpacesComponent } from './Components/Reservations/reservation-common-spaces/reservation-common-spaces.component';
import { ReservationsGestComponent } from './Components/Reservations/reservations-gest/reservations-gest.component';
import { NewsurveyComponent } from './Components/Surveys/newsurvey/newsurvey.component';
import { SurveysGestComponent } from './Components/Surveys/surveys-gest/surveys-gest.component';
import { MyReservationsComponent } from './Components/Reservations/my-reservations/my-reservations.component';
import { StatsComponent } from './Components/Stats/stats/stats.component';
import { TermsAndConditionsComponent } from './Components/Info/terms-and-conditions/terms-and-conditions.component';
import { FAQSComponent } from './Components/Info/faqs/faqs.component';

const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'main-page-admin',component: MainPageAdminComponent,
    children : [
      {path: 'claims-gest', component: ClaimsGestComponent},
      {path: 'user-gest',component:UsersGestComponent},
      {path: 'reservation-gest', component:ReservationsGestComponent},
      {path: 'survey-gest',component:SurveysGestComponent},
      {path: 'stats',component:StatsComponent},
      {path: 'terms-and-conditions',component:TermsAndConditionsComponent,data: { isAdmin: true }},
      {path: 'FAQS',component:FAQSComponent,data: { isAdmin: true } }
    ]
  },
  {path: 'user-admin-gest',component:UsersGestComponent},
  { path: 'main-page-user', component: MainPageUserComponent,
    children: [
      {path: '', component: MenuUserComponent}, 
      {path: 'claim-user', component: UserRegisterClaimComponent},
      {path: 'claim-traking', component: ClaimTrackingComponent},
      {path: 'reservation-common-spaces', component: ReservationCommonSpacesComponent},
      {path: 'reservation-user/:commonSpaceID',component: NewReservationComponent},
      {path: 'my-reservation',component:MyReservationsComponent},
      {path: 'terms-and-conditions',component:TermsAndConditionsComponent,data: { isAdmin: false }},
      {path: 'FAQS',component:FAQSComponent, data: { isAdmin: false } }
    ]},
  { path: 'consortium', component: SelectConsortiumComponent},
  { path: 'register-consortium', 
    children: [
      {path:'' , component: RegisterConsortiumComponent},
    ]  
  },
  {path:'claim-survey/:surveyID',component: NewsurveyComponent}
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
