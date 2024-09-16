import { Routes } from '@angular/router';
import { LoginComponent } from './Components/login/login.component';
import { roleGuard } from './Services/role.guard';
import { authGuard } from './Services/auth.guard';
import { DashBoardComponent } from './Components/dash-board/dash-board.component';
import { ForbiddenComponent } from './Components/forbidden/forbidden.component';

export const routes: Routes = [
    // {
    //     path: '',
    //     component: CompanyListComponent,
    //     canActivate: [authGuard] // Apply AuthGuard here
    //   },
      {
        path: 'Home',
        component: DashBoardComponent,
        canActivate: [roleGuard], // Apply RoleGuard here
        data: { requiredRole: 'customer' } // Pass the required role to RoleGuard
      },
      {
        path: 'adminDash',
        component: DashBoardComponent,
        canActivate: [roleGuard], // Apply RoleGuard here
        data: { requiredRole: 'admin' } // Pass the required role to RoleGuard
      },
      {
        path: 'login',
        component: LoginComponent
      },
      {
        path: 'forbidden',
        component: ForbiddenComponent
      },
];
