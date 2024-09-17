import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Login } from '../../Modules/Login';
import { AuthService } from '../../Services/ApiServices/auth.service';
import { Router } from '@angular/router';
import { CustomerService } from '../../Services/CustomerService/customer.service';
import { Customer } from '../../Modules/Customer';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent implements OnInit {
  alert: boolean = true;
  success: boolean = false;
  submitted = false;
  customerForm: FormGroup;
  employeeForm: FormGroup;
  selectedUserType: 'customer' | 'employee' | null = null;
  roleAdmin: boolean = true;
  roleStaff: boolean = false;
  roleSupport: boolean = false;
  roleCashier: boolean = false;
  errorMessage: string = '';

  loginModule: Login = {
    id: '',
    password: '',
  };
 

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.customerForm = this.fb.group({
      id: ['', [Validators.required]],
      password: ['', [Validators.required]],
    });

    this.employeeForm = this.fb.group({
      id: ['', [Validators.required]],
      password: ['', [Validators.required]],
    });
  }
  ngOnInit(): void {
    this.authService.customerLogout();
    this.authService.employeeLogout();
  }

  selectUserType(userType: 'customer' | 'employee') {
    this.selectedUserType = userType;
  }
  registerPage(){
    this.router.navigate(['/registered']);
  }
  roleBased(value: string) {
    if (value == 'admin') {
      this.roleAdmin = true;
      this.roleCashier = false;
      this.roleStaff = false;
      this.roleSupport = false;
    } else if (value == 'staff') {
      this.roleAdmin = false;
      this.roleCashier = false;
      this.roleStaff = true;
      this.roleSupport = false;
    } else if (value == 'support') {
      this.roleAdmin = false;
      this.roleCashier = false;
      this.roleStaff = false;
      this.roleSupport = true;
    } else if (value == 'cashier') {
      this.roleAdmin = false;
      this.roleCashier = true;
      this.roleStaff = false;
      this.roleSupport = false;
    }
  }

  login(userType: 'customer' | 'employee') {
    if (userType === 'customer' && this.employeeForm.valid) {
      const loginData = this.employeeForm.value;
      this.authService.customerAuth(loginData).subscribe(
        (data) => this.handleAuthResponse(data, '/Home', loginData),
        (error) => this.handleError(error)
      );
    } else if (userType === 'employee' && this.employeeForm.valid) {
      const loginData = this.employeeForm.value;
      if (this.roleAdmin) {
        this.authService.employeeAuth(loginData).subscribe(
          (data) => this.handleAuthResponse(data, '/adminDash', loginData),
          (error) => this.handleError(error)
        );
      } else if (this.roleCashier) {
        this.authService.employeeAuth(loginData).subscribe(
          (data) => this.handleAuthResponse(data, '/cashierDash', loginData),
          (error) => this.handleError(error)
        );
      } else if (this.roleStaff) {
        this.authService.employeeAuth(loginData).subscribe(
          (data) => this.handleAuthResponse(data, '/staffDash', loginData),
          (error) => this.handleError(error)
        );
      } else if (this.roleSupport) {
        this.authService.employeeAuth(loginData).subscribe(
          (data) => this.handleAuthResponse(data, '/supportDash', loginData),
          (error) => this.handleError(error)
        );
      } else {
        alert('Please fill in all required fields.');
      }
    } else {
      alert('Please fill in all required fields.');
    }
  }

  private handleAuthResponse(data: any, redirectPath: string, customerId:any) {
    const token = data?.token;
    if (token) {
      localStorage.setItem('token', token);
      // localStorage.setItem('customerId', customerId);
      this.router.navigate([redirectPath]);
    } else {
      alert('Failed to retrieve token.');
    }
  }

  private handleError(error: any) {
    console.error('Login error:', error);
    alert('Invalid login details or server error');
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
