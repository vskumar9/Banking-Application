import { Component } from '@angular/core';
import { HomeComponent } from "../home/home.component";
import { NavbarComponent } from "../../navbar/navbar.component";
import { LoanApplication } from '../../../Modules/LoanApplication';
import { CustomerService } from '../../../Services/CustomerService/customer.service';
import { Router } from '@angular/router';
import { Customer } from '../../../Modules/Customer';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-loan',
  standalone: true,
  imports: [HomeComponent, NavbarComponent, CommonModule, FormsModule],
  templateUrl: './loan.component.html',
  styleUrl: './loan.component.css'
})
export class LoanComponent {

  data: any;
  successMessage: string | null = null;
  errorMessage: string | null = null;
  customer: Customer | null = null;
  loans: LoanApplication[] = [];
  filteredLoans: LoanApplication[] = [];
  filterText: string = ''; 
  paginatedLoans: LoanApplication[] = [];
  currentPage: number = 1;
  itemsPerPage: number = 5;

  constructor(private customerService: CustomerService, private router: Router) {}

  ngOnInit(): void {
    this.loadLoans();
  }

  loadLoans(): void {
    this.customerService.getCustomer().subscribe(
      (customer: Customer) => {
        this.customer = customer;
        this.loans = customer.loanApplications.map(loan => ({
          ...loan,
          loanType: loan.loanType
        }));
        this.filteredLoans = this.loans;
        this.updatePagination();
      },
      (error: any) => {
        this.errorMessage = 'Error loading customer loans';
        console.error('Error loading customer loans:', error);
      }
    );
  }

  viewLoanDetails(loanId: number): void {
    this.router.navigate([`/Home/loan/loanDetails/${loanId}`]);
  }

  applyLoan(): void {
    this.router.navigate(['/Home/loan/applyLoan']); 
  }
  
  filterLoans(): void {
    const filterTextLower = this.filterText.toLowerCase();
    this.filteredLoans = this.loans.filter(loan =>
      loan.loanType?.loanTypeName?.toLowerCase().includes(filterTextLower) ||
      loan.loanStatus!.toLowerCase().includes(filterTextLower) ||
      loan.loanAmount!.toString().includes(filterTextLower)
    );
    this.updatePagination(); // Update pagination after filtering
  }

  updatePagination(): void {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    const endIndex = startIndex + this.itemsPerPage;
    this.paginatedLoans = this.filteredLoans.slice(startIndex, endIndex);
  }

  previousPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.updatePagination();
    }
  }

  nextPage(): void {
    if (this.currentPage < this.totalPages()) {
      this.currentPage++;
      this.updatePagination();
    }
  }

  totalPages(): number {
    return Math.ceil(this.filteredLoans.length / this.itemsPerPage);
  }
  
}
