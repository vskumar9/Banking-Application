import { Component } from '@angular/core';
import { NavbarComponent } from "../../navbar/navbar.component";
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { RouterOutlet } from '@angular/router';
import { CustomerService } from '../../../Services/CustomerService/customer.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [NavbarComponent,RouterOutlet, CommonModule, RouterLink,RouterLinkActive],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  
  data: any;
  customerId: string | null = null;

  // Pagination properties
  currentPage: number = 1;
  itemsPerPage: number = 5;
  totalItems: number = 0;

  constructor(private router: Router, private customerService: CustomerService) {}

  ngOnInit(): void {
    this.customerId = localStorage.getItem('customerId');
    this.customerService.getCustomer().subscribe(data => {
      this.data = data;
      this.totalItems = data.transactionLogs.length;
      this.updatePagedTransactions();
    });
  }

  // Method to get transactions for the current page
  getPagedTransactions(): any[] {
    const start = (this.currentPage - 1) * this.itemsPerPage;
    const end = start + this.itemsPerPage;
    return this.data?.transactionLogs.slice(start, end) || [];
  }

  // Method to update the transactions based on the current page
  updatePagedTransactions(): void {
    this.data.transactionLogs = this.getPagedTransactions();
  }

  // Pagination control methods
  nextPage(): void {
    if (this.currentPage * this.itemsPerPage < this.totalItems) {
      this.currentPage++;
      this.updatePagedTransactions();
    }
  }

  prevPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.updatePagedTransactions();
    }
  }

  totalPages(): number {
    return Math.ceil(this.totalItems / this.itemsPerPage);
  }
}
