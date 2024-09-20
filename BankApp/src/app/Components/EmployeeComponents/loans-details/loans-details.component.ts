import { Component } from '@angular/core';
import { LoanApplication } from '../../../Modules/LoanApplication';
import { EmployeeServiceService } from '../../../Services/EmployeeService/employee-service.service';
import { ActivatedRoute } from '@angular/router';
import { NavbarComponent } from "../navbar/navbar.component";
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-loans-details',
  standalone: true,
  imports: [NavbarComponent, CommonModule],
  templateUrl: './loans-details.component.html',
  styleUrl: './loans-details.component.css'
})
export class LoansDetailsComponent {

  loanId: number | null = null;
  loanDetails: LoanApplication | null = null;

  constructor(
    private route: ActivatedRoute,
    private employeeService: EmployeeServiceService // Service to fetch loan details
  ) {}

  ngOnInit(): void {
    // Fetch loanId from route parameters
    this.loanId = Number(this.route.snapshot.paramMap.get('id'));
    if (this.loanId) {
      this.getLoanDetails(this.loanId);
    }
  }

  // Fetch loan details by loan ID
  getLoanDetails(loanId: number): void {
    this.employeeService.getLoanById(loanId).subscribe(
      (data: LoanApplication) => {
        this.loanDetails = data;
      },
      (error) => {
        console.error('Error fetching loan details', error);
      }
    );
  }

}
