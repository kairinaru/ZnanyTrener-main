import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ModalModule } from 'ngx-bootstrap/modal';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { appRoutes } from './routes';
import { NgxTimeSchedulerModule } from 'ngx-time-scheduler';
import { JwtModule } from '@auth0/angular-jwt';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { NavComponent } from './nav/nav.component';
import { ToastrModule } from 'ngx-toastr';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { FindCoachComponent } from './find-coach/find-coach.component';
import { CoachPreviewComponent } from './coach-preview/coach-preview.component';
import { UserDetailComponent } from './user-detail/user-detail.component';
import { UserDetailResolver } from './_resolvers/user-detail.resolver';
import { MyProfileResolver } from './_resolvers/my-profile.resolver';
import { AddCertificateComponent } from './add-certificate/add-certificate.component';
import { UserEditComponent } from './user-edit/user-edit.component';
import { AddVisitComponent } from './add-visit/add-visit.component';
import { PaymentSuccessComponent } from './payment-success/payment-success.component';
import { PaymentFailComponent } from './payment-fail/payment-fail.component';
import { UserScheduleComponent } from './user-schedule/user-schedule.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { FileUploadModule } from 'ng2-file-upload';

export function tokenGetter() {
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    NavComponent,
    FindCoachComponent,
    CoachPreviewComponent,
    UserDetailComponent,
    AddCertificateComponent,
    UserEditComponent,
    AddVisitComponent,
    PaymentSuccessComponent,
    PaymentFailComponent,
    UserScheduleComponent,
    AdminDashboardComponent,
  ],
  imports: [
    BrowserModule,
    BrowserModule,
    RouterModule.forRoot(appRoutes),
    ReactiveFormsModule,
    FormsModule,
    FileUploadModule,
    ToastrModule.forRoot(),
    BrowserAnimationsModule,
    BsDatepickerModule.forRoot(),
    HttpClientModule,
    CollapseModule.forRoot(),
    ModalModule.forRoot(),
    JwtModule.forRoot({
      config: {
        tokenGetter,
        allowedDomains: ['localhost:5000'],
        disallowedRoutes: ['localhost:5000/api/auth'],
      },
    }),
    NgxTimeSchedulerModule,
  ],
  providers: [UserDetailResolver, MyProfileResolver],
  bootstrap: [AppComponent],
})
export class AppModule {}
