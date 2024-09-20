import { Component } from '@angular/core';
import { NavbarComponent } from "../../navbar/navbar.component";
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CustomerService } from '../../../Services/CustomerService/customer.service';
import { Router } from '@angular/router';
import { TransferRequest } from '../../../Modules/TransferRequest';
import { TransactionType } from '../../../Modules/TransactionType';
import { Account } from '../../../Modules/Account';

@Component({
  selector: 'app-transfer',
  standalone: true,
  imports: [NavbarComponent, FormsModule, CommonModule],
  templateUrl: './transfer.component.html',
  styleUrl: './transfer.component.css'
})
export class TransferComponent {

  transferRequest: TransferRequest = {
    fromAccountId: 0,
    toAccountId: 0,
    amount: 0,
  };

  successMessage: string | null = null;
  errorMessage: string | null = null;
  isLoading: boolean = false;
  accounts: Account[] = [];
  transactionTypeId: number = 3;
  transactionTypeId2: number = 3;
  transactionTypes: TransactionType[] = []; // Store available transaction types

  constructor(private customerService: CustomerService, private router: Router) {}

  ngOnInit(): void {
    this.loadTransactionTypes(); 
    this.loadCustomerAccounts();
  }

  loadCustomerAccounts(): void {
    this.customerService.getCustomer().subscribe(
      (customer: any) => {
        this.accounts = customer.accounts; // Assuming customer has accounts property
      },
      (error: any) => {
        this.errorMessage = 'Error loading accounts.';
        console.error('Error:', error);
      }
    );
  }

  loadTransactionTypes(): void {
    this.customerService.getTransactionTypes().subscribe({
      next: (types: TransactionType[]) => {
        this.transactionTypes = types;
        if (this.transactionTypes.length > 0) {
          // Set the first type as default (or you can customize)
          this.transactionTypeId2 = this.transactionTypes[0].transactionTypeId!;
        }
      },
      error: (error) => {
        console.error('Error loading transaction types:', error);
        this.errorMessage = 'Failed to load transaction types.';
      }
    });
  }

  // Method to transfer funds
  transferFunds(): void {
    this.successMessage = null;
    this.errorMessage = null;

    if (this.transferRequest.amount <= 0 || this.transferRequest.fromAccountId <= 0 || this.transferRequest.toAccountId <= 0 || this.transactionTypeId <= 0) {
      this.errorMessage = 'Invalid transfer details. Please check your input.';
      return;
    }

    this.isLoading = true;

    this.customerService.transferAmount(this.transferRequest, this.transactionTypeId)
      .subscribe({
        next: () => {
          this.successMessage = 'Transfer successful!';
          this.isLoading = false;
          this.resetForm();
        },
        error: (error) => {
          this.errorMessage = error.error?.message || 'Failed to complete the transfer.';
          this.isLoading = false;
        }
      });
  }

  // Reset form fields
  resetForm(): void {
    this.transferRequest = {
      fromAccountId: 0,
      toAccountId: 0,
      amount: 0,
    };
  }
}
