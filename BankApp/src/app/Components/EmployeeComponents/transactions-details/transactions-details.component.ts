import { Component } from '@angular/core';
import { TransactionLog } from '../../../Modules/TransactionLog';
import { EmployeeServiceService } from '../../../Services/EmployeeService/employee-service.service';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { NavbarComponent } from "../navbar/navbar.component";
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-transactions-details',
  standalone: true,
  imports: [NavbarComponent, CommonModule, RouterLink],
  templateUrl: './transactions-details.component.html',
  styleUrl: './transactions-details.component.css'
})
export class TransactionsDetailsComponent {

  transactionId: number | null = null;  // To hold the current transaction ID
  transaction: TransactionLog | null = null;  // To store the transaction details

  constructor(
    private route: ActivatedRoute,
    private transactionService: EmployeeServiceService
  ) {}

  ngOnInit(): void {
    // Get the transaction ID from the route parameters
    this.transactionId = +this.route.snapshot.params['id'];  // Convert string to number

    if (this.transactionId) {
      // Fetch the transaction details based on the ID
      this.transactionService.getTransactionById(this.transactionId).subscribe(
        (data: TransactionLog) => {
          this.transaction = data;
        },
        (error) => {
          console.error('Error fetching transaction details', error);
        }
      );
    }
  }

}
