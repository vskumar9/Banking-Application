import { ResolveFn, Routes } from '@angular/router';
import { LoginComponent } from './Components/login/login.component';
import { roleGuard } from './Services/role.guard';
import { DashBoardComponent } from './Components/EmployeeComponents/dash-board/dash-board.component';
import { PageNotFoundComponent } from './Components/page-not-found/page-not-found.component';
import { ProfileComponent } from './Components/CustomerComponents/profile/profile.component';
import { ProfileComponent as profile } from './Components/EmployeeComponents/profile/profile.component';
import { TransferComponent } from './Components/CustomerComponents/transfer/transfer.component';
import { ComplaintsComponent } from './Components/CustomerComponents/complaints/complaints.component';
import { LoanComponent } from './Components/CustomerComponents/loan/loan.component';
import { HomeComponent } from './Components/CustomerComponents/home/home.component';
import { RegisteredComponent } from './Components/CustomerComponents/registered/registered.component';
import { UpdateComponent } from './Components/CustomerComponents/update/update.component';
import { ComplaintDetailsComponent } from './Components/CustomerComponents/complaint-details/complaint-details.component';
import { NewComplaintComponent } from './Components/CustomerComponents/new-complaint/new-complaint.component';
import { LoanDetailsComponent } from './Components/CustomerComponents/loan-details/loan-details.component';
import { NewLoanComponent } from './Components/CustomerComponents/new-loan/new-loan.component';
import { StaffDashBoardComponent } from './Components/EmployeeComponents/staff-dash-board/staff-dash-board.component';
import { SupportDashBoardComponent } from './Components/EmployeeComponents/support-dash-board/support-dash-board.component';
import { CustomersComponent } from './Components/EmployeeComponents/customers/customers.component';
import { NewCustomerComponent } from './Components/EmployeeComponents/new-customer/new-customer.component';
import { CustomerDetailsComponent } from './Components/EmployeeComponents/customer-details/customer-details.component';
import { CongfigurationComponent } from './Components/EmployeeComponents/congfiguration/congfiguration.component';
import { EmployessComponent } from './Components/EmployeeComponents/employess/employess.component';
import { AuditComponent } from './Components/EmployeeComponents/audit/audit.component';
import { LoansComponent } from './Components/EmployeeComponents/loans/loans.component';
import { LoansDetailsComponent } from './Components/EmployeeComponents/loans-details/loans-details.component';
import { LoansUpdateComponent } from './Components/EmployeeComponents/loans-update/loans-update.component';
import { NewEmployeeComponent } from './Components/EmployeeComponents/new-employee/new-employee.component';
import { UpdateEmployeeComponent } from './Components/EmployeeComponents/update-employee/update-employee.component';
import { TransactionsComponent } from './Components/EmployeeComponents/transactions/transactions.component';
import { TransactionsDetailsComponent } from './Components/EmployeeComponents/transactions-details/transactions-details.component';

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
      { path: 'loan/loanDetails/:id', title: 'Loan Details', component: LoanDetailsComponent },
      { path: 'loan/applyLoan', title: 'New Loan', component: NewLoanComponent }
    ]
  },
  {
    path: 'admindashboard',
    component: DashBoardComponent,
    canActivate: [roleGuard], // Apply RoleGuard here
    data: { requiredRole: 'admin' }, // Pass the required role to RoleGuard
  },
  {
    path : 'admindashboard',
    canActivate: [roleGuard],
    data: { requiredRole: 'admin' },
    children: [
      { path: 'customers', title: 'customers', component: CustomersComponent },
      { path: 'customers/new', title: 'customers', component: NewCustomerComponent },
      { path: 'customers/details/:id', title: 'customers', component: CustomerDetailsComponent },
      { path: 'configuration', title: 'Configuration', component: CongfigurationComponent },
      { path: 'employees', title: 'Employees', component: EmployessComponent },
      { path: 'employees/new', title: 'Employees', component: NewEmployeeComponent },
      { path: 'employees/update/:id', title: 'Employees', component: UpdateEmployeeComponent },
      { path: 'audit', title: 'Audit', component: AuditComponent },
      { path: 'profile', title: 'profile', component: profile },
    ]
  },
  {
    path: 'staffdashboard',
    component: StaffDashBoardComponent,
    canActivate: [roleGuard], 
    data: { requiredRole: 'staff' },
  },
  {
    path : 'staffdashboard',
    canActivate: [roleGuard],
    data: { requiredRole: 'staff' },
    children: [
      { path: 'loan', title: 'loans', component: LoansComponent },
      { path: 'loan/details/:id', title: 'loan details', component: LoansDetailsComponent },
      { path: 'loan/update/:id', title: 'loan details', component: LoansUpdateComponent },
      { path: 'customers', title: 'customers', component: CustomersComponent },
      { path: 'customers/details/:id', title: 'customers', component: CustomerDetailsComponent },
      { path: 'customers/new', title: 'customers', component: NewCustomerComponent },
      { path: 'transactions', title: 'transactions', component: TransactionsComponent },
      { path: 'transactions/transaction-details/:id', title: 'transactions', component: TransactionsDetailsComponent },
      { path: 'profile', title: 'profile', component: profile },
      // { path: 'configuration', title: 'Configuration', component: CongfigurationComponent },
      // { path: 'employees', title: 'Employees', component: EmployessComponent },
      // { path: 'audit', title: 'Audit', component: AuditComponent },
    ]
  },

  {
    path: 'supportdashboard',
    component: SupportDashBoardComponent,
    canActivate: [roleGuard], // Apply RoleGuard here
    data: { requiredRole: 'support' }, // Pass the required role to RoleGuard
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
