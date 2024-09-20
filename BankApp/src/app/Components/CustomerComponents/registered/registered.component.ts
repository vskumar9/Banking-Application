import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, NgForm, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CustomerService } from '../../../Services/CustomerService/customer.service';
import { Router } from '@angular/router';
import { Customer } from '../../../Modules/Customer';


@Component({
  selector: 'app-registered',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],

  templateUrl: './registered.component.html',
  styleUrl: './registered.component.css'
})
export class RegisteredComponent {

  customer: Customer = {
    customerId: '',
    customerFirstName: '',
    customerLastName: '',
    customerAddress1: '',
    customerAddress2: '',
    city: '',
    state: '',
    zipCode: '',
    emailAddress: '',
    cellPhone: '',
    homePhone: '',
    workPhone: '',
    passwordHash: '', 
    isActive: true,
    accounts: [],
    complaints: [],
    complaintFeedbacks: [],
    loanApplications: [],
    transactionLogs: []
  };

  successMessage: string | null = null;
  errorMessage: string | null = null;
  registeredCustomer: Customer | null = null;

  constructor(
    private customerService: CustomerService,
    private router: Router
  ) {}

  onSubmit(registerForm: NgForm): void {
    if (registerForm.valid) {
      this.customerService.registerCustomer(this.customer).subscribe(
        (response: Customer) => {
          this.successMessage = 'Customer registered successfully!';
          this.registeredCustomer = response; // Store registered customer details
          registerForm.resetForm(); // Reset the form after successful registration
        },
        error => {
          console.error('Error registering customer', error);
          this.errorMessage = 'Error registering customer. Please try again.';
        }
      );
    }
  }

  // Redirect back to login page
  loginPage() {
    this.router.navigate(['/login']);
  }
}
