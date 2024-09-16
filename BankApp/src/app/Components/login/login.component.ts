import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Component } from '@angular/core';
import { Login } from '../../Modules/Login';
import { AuthService } from '../../Services/ApiServices/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  // templateUrl: './login.component.html',
  template: `
  <div>
      <button (click)="selectUserType('customer')">Customer Login</button>
      <button (click)="selectUserType('employee')">Employee Login</button>
    </div>

    <ng-container *ngIf="selectedUserType === 'customer'">
      <form [formGroup]="employeeForm" (ngSubmit)="login('customer')">
        <label for="customerId">Customer ID</label>
        <input type="email" id="customerId" formControlName="id" required>

        <label for="customerPassword">Password</label>
        <input type="password" id="customerPassword" formControlName="password" required>

        <button type="submit">Login</button>
      </form>
    </ng-container>

    <ng-container *ngIf="selectedUserType === 'employee'">
      <form [formGroup]="employeeForm" (ngSubmit)="login('employee')">
        <label for="employeeId">Employee ID</label>
        <input type="email" id="employeeId" formControlName="id" required>

        <label for="employeePassword">Password</label>
        <input type="password" id="employeePassword" formControlName="password" required>

        <button type="submit">Login</button>
      </form>
    </ng-container>
    `,
  styleUrl: './login.component.css'
})
export class LoginComponent {

  customerForm: FormGroup;
  employeeForm: FormGroup;
  selectedUserType: 'customer' | 'employee' | null = null;

  loginModule: Login = {
    id: '',
    password:''
  }

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {
    this.customerForm = this.fb.group({
      id: ['', [Validators.required]],
      password: ['', [Validators.required]]
    });

    this.employeeForm = this.fb.group({
      id: ['', [Validators.required]],
      password: ['', [Validators.required]]
    });
  }

  selectUserType(userType: 'customer' | 'employee') {
    this.selectedUserType = userType;
  }

  login(userType: 'customer' | 'employee') {
    if (userType === 'customer' && this.employeeForm.valid) {
      const loginData = this.employeeForm.value;
      this.authService.customerAuth(loginData).subscribe(
        data => this.handleAuthResponse(data, '/Home'),
        error => this.handleError(error)
      );
    } else if (userType === 'employee' && this.employeeForm.valid) {
      const loginData = this.employeeForm.value;
      this.authService.employeeAuth(loginData).subscribe(
        data => this.handleAuthResponse(data, '/adminDash'),
        error => this.handleError(error)
      );
    } else {
      alert("Please fill in all required fields.");
    }
  }

  private handleAuthResponse(data: any, redirectPath: string) {
    const token = data?.token;
    if (token) {
      localStorage.setItem('token', token);
      this.router.navigate([redirectPath]);
    } else {
      alert("Failed to retrieve token.");
    }
  }

  private handleError(error: any) {
    console.error('Login error:', error);
    alert("Invalid login details or server error");
  }


  // customerLogin() {
  //   console.log(this.login.id, this.login.password);
  //   if (this.login.id && this.login.password) {
  //     this.authService.customerAuth(this.login).subscribe(
  //       data => {
  //         console.log(data)
  //        const token = data?.token;
  //       //  console.log(token) // Directly get the token from API response
  //         if (token) {
  //           localStorage.setItem('token', token); // Store the token directly
  //           console.log('Token stored');
  //           console.log(localStorage.getItem('token'));
  //           this.router.navigate(['/Home']); // Navigate to the products component
  //         } else {
  //           console.log('No token found in API response.');
  //           alert("Failed to retrieve token.");
  //         }        
  //       },
  //       error => {
  //         console.error('Login error:', error);
  //         alert("Invalid login details or server error");
  //       }
  //     );
  //   } else {
  //     alert("Please enter username and password first");
  //   }
  // }
  // employeeLogin() {
  //   console.log(this.login.id, this.login.password);
  //   if (this.login.id && this.login.password) {
  //     this.authService.employeeAuth(this.login).subscribe(
  //       data => {
  //         console.log(data)
  //        const token = data?.token;
  //       //  console.log(token) // Directly get the token from API response
  //         if (token) {
  //           localStorage.setItem('token', token); // Store the token directly
  //           console.log('Token stored');
  //           console.log(localStorage.getItem('token'));
  //           this.router.navigate(['/adminDash']); // Navigate to the products component
  //         } else {
  //           console.log('No token found in API response.');
  //           alert("Failed to retrieve token.");
  //         }        
  //       },
  //       error => {
  //         console.error('Login error:', error);
  //         alert("Invalid login details or server error");
  //       }
  //     );
  //   } else {
  //     alert("Please enter username and password first");
  //   }
  // }

}
