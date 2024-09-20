import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { LoanType } from '../../../Modules/LoanType';
import { LoanApplication } from '../../../Modules/LoanApplication';
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeeServiceService } from '../../../Services/EmployeeService/employee-service.service';
import { CustomerService } from '../../../Services/CustomerService/customer.service';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from '../navbar/navbar.component';

@Component({
  selector: 'app-loans-update',
  standalone: true,
  imports: [CommonModule, NavbarComponent, FormsModule,ReactiveFormsModule],
  templateUrl: './loans-update.component.html',
  styleUrl: './loans-update.component.css'
})
export class LoansUpdateComponent {

  loanId: number | null = null;
  loanForm: FormGroup;
  loanTypes: LoanType | any;
  loanDetails: LoanApplication | any;

  constructor(
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private employeeService: EmployeeServiceService,
    private customerService: CustomerService,
    private router: Router
  ) {
    // Initialize the form with default values
    this.loanForm = this.fb.group({
      loanAmount: ['', Validators.required],
      loanTypeId: ['', Validators.required],
      loanStatus: ['', Validators.required],
      employeeId: ['', Validators.required],
      comments: ['']
    });
  }

  ngOnInit(): void {
    // Get loan ID from the route parameters
    this.loanId = Number(this.route.snapshot.paramMap.get('id'));
    if (this.loanId) {
      this.getLoanDetails(this.loanId);
      this.getLoanTypes();
    }
  }

  // Fetch loan details and populate the form
  getLoanDetails(loanId: number): void {
    this.employeeService.getLoanById(loanId).subscribe(
      (data: LoanApplication) => {
        console.log(data);
        this.loanDetails = data;
        // Prefill the form with loan details
        this.loanForm.patchValue({
          loanAmount: data.loanAmount,
          loanTypeId: data.loanTypeId,
          loanStatus: data.loanStatus,
          employeeId: data.employeeId,
          comments: data.comments
        });
      },
      (error) => {
        console.error('Error fetching loan details', error);
      }
    );
  }

  // Fetch all loan types for the dropdown
  getLoanTypes(): void {
    this.customerService.getLoanTypes().subscribe(
      (data: LoanType[]) => {
        this.loanTypes = data;
      },
      (error) => {
        console.error('Error fetching loan types', error);
      }
    );
  }

  // Submit the updated loan details
  onSubmit(): void {
    if (this.loanForm.valid && this.loanId) {
      const updatedLoan = {
        ...this.loanDetails,
        ...this.loanForm.value
      };
      
      this.employeeService.updateLoan(updatedLoan).subscribe(
        (response) => {
          console.log('Loan updated successfully', response);
          this.router.navigate(['/staffdashboard/loan']); // Redirect to loan list or details page
        },
        (error) => {
          console.error('Error updating loan', error);
        }
      );
    }
  }

}
