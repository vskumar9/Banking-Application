import { Component } from '@angular/core';
import { LoanApplication } from '../../../Modules/LoanApplication';
import { EmployeeServiceService } from '../../../Services/EmployeeService/employee-service.service';
import { NavbarComponent } from "../navbar/navbar.component";
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-loans',
  standalone: true,
  imports: [NavbarComponent, CommonModule, FormsModule,ReactiveFormsModule],
  templateUrl: './loans.component.html',
  styleUrl: './loans.component.css'
})
export class LoansComponent {

  loans: LoanApplication[]=[];
  filteredLoans: LoanApplication[]=[];
  searchTerm: string = '';

  constructor(private employeeService: EmployeeServiceService,private router: Router) {}

  ngOnInit(): void {
    this.fetchLoans();
  }

  fetchLoans(): void {
    this.employeeService.getLoans().subscribe(
      (data:LoanApplication[]) => {
        this.loans = data;
        this.filteredLoans = data;  // Initialize the filtered list with all loans
      },
      (error) => {
        console.error('Error fetching loans', error);
      }
    );
  }

  // Filtering logic
  filterLoans(): void {
    const term = this.searchTerm.toLowerCase();
    this.filteredLoans = this.loans.filter((loan: LoanApplication) =>
      loan.customer?.customerFirstName?.toLowerCase().includes(term) ||
      loan.customer?.customerLastName?.toLowerCase().includes(term) ||
      loan.loanStatus?.toLowerCase().includes(term) ||
      loan.loanType?.loanTypeName?.toLowerCase().includes(term) ||
      loan.employee?.employeeFirstName?.toLowerCase().includes(term)
    );
  }

  updateLoan(id: number): void {
    if (id) {
      console.log('Navigating to update loan', id);
      this.router.navigate([`/staffdashboard/loan/update/${id}`])
      
    }
  }

  viewDetails(loanId: number): void {
    if (loanId) {
      this.router.navigate(['staffdashboard/loan/details',loanId])
      console.log('Navigating to loan details', loanId);
    }
  }

}
