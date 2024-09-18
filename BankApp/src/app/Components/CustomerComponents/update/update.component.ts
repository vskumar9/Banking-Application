import { Component } from '@angular/core';
import { NavbarComponent } from "../../navbar/navbar.component";
import { FormBuilder, FormGroup, FormsModule } from '@angular/forms';
import { Customer } from '../../../Modules/Customer';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomerService } from '../../../Services/CustomerService/customer.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-update',
  standalone: true,
  imports: [NavbarComponent, FormsModule, CommonModule],
  templateUrl: './update.component.html',
  styleUrl: './update.component.css'
})
export class UpdateComponent {

  data:any;
  successMessage: string | null = null;
  errorMessage: string | null = null;
  customer: Customer | null = null;

  constructor(
    private fb: FormBuilder, private customerService: CustomerService, private router: Router, private route: ActivatedRoute,
  ) { }

  ngOnInit(): void {
    this.loadCustomer();
  }

  loadCustomer(): void {
    const id = +this.route.snapshot.paramMap.get('id')!;
    this.customerService.getCustomer().subscribe(
      (customer) => this.customer = customer,
      (error: any) => console.error('Error loading customer', error)
    );
  }

  onUpdate(): void {
    const id = this.customer?.customerId;
    this.customerService.update(this.customer!).subscribe(
      (response) => {
        this.successMessage = 'Customer updated successfully!';
        this.errorMessage = null; // Clear any previous error message
        console.log(response);
        this.router.navigate(['Home/profile']);
      },
      (error) => {
        // Check if the error has a response with a message or status code
        if (error.status === 400) {
          // Example: handling validation or bad request errors
          this.errorMessage = 'Bad Request: Please check the input values.';
        } else if (error.status === 500) {
          // Example: handling server errors
          this.errorMessage = 'Internal Server Error: Please try again later.';
        } else if (error.error && error.error.message) {
          // Use the specific error message returned from the backend if available
          this.errorMessage = error.error.message;
        } else {
          // Fallback message for other error types
          this.errorMessage = 'An unexpected error occurred. Please try again.';
        }
  
        this.successMessage = null; // Clear success message if there's an error
        console.error('Error during update:', error); // Log the error for debugging
      }
    );
  }
  

  profilePage()
  {
    this.router.navigate(['Home/profile/']);
  }

}
