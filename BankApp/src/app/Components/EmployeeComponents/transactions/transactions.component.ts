import { Component } from '@angular/core';
import { TransactionLog } from '../../../Modules/TransactionLog';
import { EmployeeServiceService } from '../../../Services/EmployeeService/employee-service.service';
import { Router } from '@angular/router';
import { NavbarComponent } from "../navbar/navbar.component";
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-transactions',
  standalone: true,
  imports: [NavbarComponent, CommonModule],
  templateUrl: './transactions.component.html',
  styleUrl: './transactions.component.css'
})
export class TransactionsComponent {

  transactions: TransactionLog[] = [];

  constructor(private transactionService: EmployeeServiceService, private router: Router) {}

  ngOnInit(): void {
    // Fetch the list of transactions on initialization
    this.transactionService.getTransactionLog().subscribe(
      (data: TransactionLog[]) => {
        this.transactions = data;
      },
      (error) => {
        console.error('Error fetching transactions', error);
      }
    );
  }

  // Navigate to transaction details
  viewTransactionDetails(transactionId: number | null): void {
    if (transactionId) {
      this.router.navigate([`/staffdashboard/transactions/transaction-details/${transactionId}`]);
    }
  }


}
