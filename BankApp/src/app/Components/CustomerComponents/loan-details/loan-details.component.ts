import { Component } from '@angular/core';
import { NavbarComponent } from "../../navbar/navbar.component";
import { ActivatedRoute } from '@angular/router';
import { CustomerService } from '../../../Services/CustomerService/customer.service';
import { LoanApplication } from '../../../Modules/LoanApplication';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-loan-details',
  standalone: true,
  imports: [NavbarComponent, CommonModule],
  templateUrl: './loan-details.component.html',
  styleUrl: './loan-details.component.css'
})
export class LoanDetailsComponent {

  loanId: number | null = null;
  loan: LoanApplication | null = null;
  errorMessage: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private customerService: CustomerService
  ) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.loanId = +params.get('id')!;
      if (this.loanId) {
        this.loadLoanDetails();
      }
    });
  }

  loadLoanDetails(): void {
    this.customerService.getLoanById(this.loanId!).subscribe(
      (loan: LoanApplication) => {
        this.loan = loan;
      },
      (error: any) => {
        this.errorMessage = 'Error loading loan details';
        console.error('Error loading loan details:', error);
      }
    );
  }
  
}
