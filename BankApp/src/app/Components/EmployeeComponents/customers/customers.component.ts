import { Component, OnInit } from '@angular/core';
import { EmployeeServiceService } from '../../../Services/EmployeeService/employee-service.service';
import { Customer } from '../../../Modules/Customer';
import { HttpClient } from '@angular/common/http';
import { NavbarComponent } from "../navbar/navbar.component";
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../Services/ApiServices/auth.service';

@Component({
  selector: 'app-customers',
  standalone: true,
  imports: [NavbarComponent, CommonModule, RouterLink],
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.css']
})
export class CustomersComponent implements OnInit {
  customers: Customer[] = [];
  paginatedCustomers: Customer[] = [];
  currentPage: number = 1;
  itemsPerPage: number = 5;
  totalPages: number = 0;

  isAdmin:boolean = false;
  isStaff:boolean = false;

  constructor(private employeeService: EmployeeServiceService, private router: Router, private authService: AuthService) {}

  ngOnInit(): void {
    this.loadCustomers();
    this.isAdmin = this.authService.isAdmin();
    this.isStaff = this.authService.isStaff();
  }

  loadCustomers(): void {
    this.employeeService.getCustomer().subscribe(response => {
      this.customers = response;
      this.totalPages = Math.ceil(this.customers.length / this.itemsPerPage);
      this.setPage(this.currentPage);  // Initialize pagination
    });
  }

  setPage(page: number): void {
    if (page < 1 || page > this.totalPages) {
      return;  // Ensure the page is within valid range
    }
    this.currentPage = page;
    const start = (this.currentPage - 1) * this.itemsPerPage;
    const end = start + this.itemsPerPage;
    this.paginatedCustomers = this.customers.slice(start, end);  // Slice the customer list for pagination
  }

  addCustomer(): void {
    console.log('Add customer');
  }

  updateCustomer(customerId: string): void {
    console.log('Update customer', customerId);
  }

  deleteCustomer(customerId: string): void {
    console.log('Delete customer with ID:', customerId);
    this.employeeService.deleteCustomer(customerId).subscribe(() => {
      this.loadCustomers(); 
    });
  }

  viewCustomerDetails(customerId: string): void {
    console.log('View customer details', customerId);
    this.router.navigate(['admindashboard/customers/details', customerId]);
  }
  viewCustomerDetailsStaff(customerId: string): void {
    console.log('View customer details', customerId);
    this.router.navigate(['staffdashboard/customers/details', customerId]);
  }

}
