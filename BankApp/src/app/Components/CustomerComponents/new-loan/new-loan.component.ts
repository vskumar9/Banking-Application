import { Component } from '@angular/core';
import { NavbarComponent } from "../../navbar/navbar.component";
import { CustomerService } from '../../../Services/CustomerService/customer.service';
import { Router } from '@angular/router';
import { LoanApplication } from '../../../Modules/LoanApplication';
import { LoanType } from '../../../Modules/LoanType';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-new-loan',
  standalone: true,
  imports: [NavbarComponent, FormsModule, CommonModule],
  templateUrl: './new-loan.component.html',
  styleUrl: './new-loan.component.css'
})
export class NewLoanComponent {

  loanApplication: LoanApplication = {
    loanTypeId: 0,
    loanAmount: 0,
    applicationDate: new Date,
    files: null,
    comments: null,
  };
  loanTypes: LoanType[] = [];
  successMessage: string | null = null;
  errorMessage: string | null = null;
  selectedFile: File | null = null;

  constructor(private customerService: CustomerService, private router: Router) {}

  ngOnInit(): void {
    this.loadLoanTypes();
  }

  loadLoanTypes(): void {
    this.customerService.getLoanTypes().subscribe(
      (loanTypes: LoanType[]) => {
        this.loanTypes = loanTypes;
      },
      (error: any) => {
        this.errorMessage = 'Error loading loan types';
        console.error('Error loading loan types:', error);
      }
    );
  }

  handleFileInput(event: any): void {
    const file: File = event.target.files[0];
    if (file) {
      this.selectedFile = file; 
    }
  }

  submitLoanApplication(): void {
    const formData = new FormData();
    formData.append('loanTypeId', this.loanApplication.loanTypeId!.toString());
    formData.append('loanAmount', this.loanApplication.loanAmount!.toString());
    formData.append('applicationDate', this.loanApplication.applicationDate!.toString());
    if (this.loanApplication.comments) {
      formData.append('comments', this.loanApplication.comments);
    }
    if (this.loanApplication.employeeId) {
      formData.append('employeeId', this.loanApplication.employeeId!.toString());
    }

    if (this.selectedFile) {
      formData.append('file', this.selectedFile); 
    } else {
      console.error('No file selected');
      return;
    }

    this.customerService.applyForLoan(formData).subscribe(
      response => {
        console.log('Complaint registered successfully:', response);
        this.router.navigate(['Home/loan']);
      },
      error => {
        console.error('Error registering complaint:', error);
      }
    );
  }

}
