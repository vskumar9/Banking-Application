import { ResolveFn, Routes } from '@angular/router';
import { LoginComponent } from './Components/login/login.component';
import { roleGuard } from './Services/role.guard';
import { DashBoardComponent } from './Components/dash-board/dash-board.component';
import { PageNotFoundComponent } from './Components/page-not-found/page-not-found.component';
import { ProfileComponent } from './Components/CustomerComponents/profile/profile.component';
import { TransferComponent } from './Components/CustomerComponents/transfer/transfer.component';
import { ComplaintsComponent } from './Components/CustomerComponents/complaints/complaints.component';
import { LoanComponent } from './Components/CustomerComponents/loan/loan.component';
import { HomeComponent } from './Components/CustomerComponents/home/home.component';
import { RegisteredComponent } from './Components/CustomerComponents/registered/registered.component';
import { UpdateComponent } from './Components/CustomerComponents/update/update.component';
import { ComplaintDetailsComponent } from './Components/CustomerComponents/complaint-details/complaint-details.component';
import { NewComplaintComponent } from './Components/CustomerComponents/new-complaint/new-complaint.component';
import { LoanDetailsComponent } from './Components/CustomerComponents/loan-details/loan-details.component';

export const routes: Routes = [
  {
    path: 'Home',
    component: HomeComponent,
    title: 'First component',
    canActivate: [roleGuard],
    data: { requiredRole: 'customer' },
  },
  {
    path : 'Home',
    canActivate: [roleGuard],
    data: { requiredRole: 'customer' },
    children: [
      { path: 'profile', title: 'Profile', component: ProfileComponent },
      {path:'profile/update',component:UpdateComponent},
      { path: 'transfer', title: 'Money Transfer', component: TransferComponent },
      { path: 'complaints', title: 'Customer Complaints', component: ComplaintsComponent },
      { path: 'complaints/newComplaint', title: 'Customer Complaints', component: NewComplaintComponent },
      { path: 'complaints/details/:id', title: 'Customer Complaints', component: ComplaintDetailsComponent },
      { path: 'loan', title: 'Loan', component: LoanComponent },
      { path: 'loan/loanDetails/:id', title: 'Loan Details', component: LoanDetailsComponent }
    ]
  },
  {
    path: 'adminDash',
    component: DashBoardComponent,
    canActivate: [roleGuard], // Apply RoleGuard here
    data: { requiredRole: 'admin' }, // Pass the required role to RoleGuard
  },
  {
    path: 'staffDash',
    component: DashBoardComponent,
    canActivate: [roleGuard], // Apply RoleGuard here
    data: { requiredRole: 'staff' }, // Pass the required role to RoleGuard
  },
  {
    path: 'supportDash',
    component: DashBoardComponent,
    canActivate: [roleGuard], // Apply RoleGuard here
    data: { requiredRole: 'support' }, // Pass the required role to RoleGuard
  },
  {
    path: 'cashierDash',
    component: DashBoardComponent,
    canActivate: [roleGuard], // Apply RoleGuard here
    data: { requiredRole: 'cashier' }, // Pass the required role to RoleGuard
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'registered', component: RegisteredComponent },
  { path: '**', component: PageNotFoundComponent },
];
const resolvedChildATitle: ResolveFn<string> = () => Promise.resolve('child a');
