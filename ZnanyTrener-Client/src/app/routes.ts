import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { FindCoachComponent } from './find-coach/find-coach.component';
import { UserDetailResolver } from './_resolvers/user-detail.resolver';
import { UserDetailComponent } from './user-detail/user-detail.component';
import { MyProfileResolver } from './_resolvers/my-profile.resolver';
import { AddCertificateComponent } from './add-certificate/add-certificate.component';
import { UserEditComponent } from './user-edit/user-edit.component';
import { AddVisitComponent } from './add-visit/add-visit.component';
import { PaymentFailComponent } from './payment-fail/payment-fail.component';
import { PaymentSuccessComponent } from './payment-success/payment-success.component';
import { UserScheduleComponent } from './user-schedule/user-schedule.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';

export const appRoutes: Routes = [
  { path: 'find-coach', component: FindCoachComponent },
  { path: 'login', component: LoginComponent },
  {
    path: 'me',
    component: UserDetailComponent,
    runGuardsAndResolvers: 'always',
    resolve: { user: MyProfileResolver },
  },
  {
    path: 'admin',
    component: AdminDashboardComponent
  },
  {
    path: 'edit',
    component: UserEditComponent,
    runGuardsAndResolvers: 'always',
    resolve: { user: MyProfileResolver },
  },
  {
    path: 'payment-fail',
    component: PaymentFailComponent
  },
  {
    path: 'payment-success',
    component: PaymentSuccessComponent
  },
  {
    path: 'visit/:id',
    component: AddVisitComponent,
    runGuardsAndResolvers: 'always',
    resolve: { user: UserDetailResolver }
  },
  {
    path: 'my-schedule',
    component: UserScheduleComponent
  },
  {
    path: 'add-cert',
    component: AddCertificateComponent
  },
  {
    path: 'user/:id',
    runGuardsAndResolvers: 'always',
    resolve: { user: UserDetailResolver },
    component: UserDetailComponent,
  },
  { path: 'register', component: RegisterComponent },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];
