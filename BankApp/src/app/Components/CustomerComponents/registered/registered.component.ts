import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CustomerService } from '../../../Services/CustomerService/customer.service';
import { CommonModule } from '@angular/common';
import { Customer } from '../../../Modules/Customer';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registered',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],

  templateUrl: './registered.component.html',
  styleUrl: './registered.component.css'
})
export class RegisteredComponent {

  registerForm: FormGroup;
  successMessage: string | null = null;
  errorMessage: string | null = null;
  registeredCustomer: Customer | null = null; // To hold customer data

  // customerData: Customer=[{

  //   customerId: '' ,
  //   passwordHash: '',
  //   customerFirstName: '',
  //   customerLastName: '',
  //   customerAddress1: '',
  //   customerAddress2: '',
  //   city: '',
  //   state: '',
  //   zipCode: '',
  //   emailAddress: '',
  //   cellPhone: '',
  //   homePhone: '',
  //   workPhone: '',
  //   isActive: false,
  //   lastLoginDate: Date ,
  //   accounts: [],
  //   complaintFeedbacks: [],
  //   complaints: [],
  //   loanApplications: [],
  //   transactionLogs: []
  // }
  // ];

  constructor(private fb: FormBuilder, private registerService: CustomerService, private router: Router) {
    this.registerForm = this.fb.group({
      customerFirstName: ['', Validators.required],
      customerLastName: ['', Validators.required],
      customerAddress1: ['', Validators.required],
      customerAddress2: [''],
      city: ['', Validators.required],
      state: ['', Validators.required],
      zipCode: ['', Validators.required],
      emailAddress: ['', [Validators.required, Validators.email]],
      cellPhone: ['', Validators.required],
      homePhone: [''],
      workPhone: [''],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  onSubmit(): void {
    if (this.registerForm.valid) {
      const customerData = this.registerForm.value;
      this.registerService.registerCustomer(customerData).subscribe(
        response => {
          this.successMessage = 'Customer registered successfully!';
          this.errorMessage = null; // Clear any previous error message
          this.registeredCustomer = response; // Store the received customer data
          this.registerForm.reset(); // Optionally reset the form
        },
        error => {
          this.errorMessage = 'Error registering customer. Please try again.';
          this.successMessage = null; // Clear any previous success message
          this.registeredCustomer = null; // Clear any previous customer data
        }
      );
    }
  }

  loginPage(){
    this.router.navigate(['/login']);
  }

}
