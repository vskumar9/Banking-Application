import { Component } from '@angular/core';
import { HomeComponent } from "../home/home.component";
import { NavbarComponent } from "../../navbar/navbar.component";
import { CustomerService } from '../../../Services/CustomerService/customer.service';
import { Customer } from '../../../Modules/Customer';
import { ActivatedRoute, Router, RouterLink, RouterLinkActive } from '@angular/router';
import { Complaint } from '../../../Modules/Complaint';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-complaints',
  standalone: true,
  imports: [HomeComponent, NavbarComponent, CommonModule,RouterLink, RouterLinkActive, FormsModule],
  templateUrl: './complaints.component.html',
  styleUrl: './complaints.component.css'
})
export class ComplaintsComponent {

  data: any;
  successMessage: string | null = null;
  errorMessage: string | null = null;
  customer: Customer | null = null;
  complaints: Complaint[] = [];
  filteredComplaints: Complaint[] = [];
  paginatedComplaints: Complaint[] = [];
  searchTerm: string = '';
  currentPage: number = 1;
  itemsPerPage: number = 5;

  constructor(
    private customerService: CustomerService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.loadCustomerComplaints();
  }

  loadCustomerComplaints(): void {
    this.customerService.getCustomer().subscribe(
      (customer: Customer) => {
        this.customer = customer;
        this.complaints = customer.complaints;
        this.filterComplaints(); // Initial filter
      },
      (error: any) => {
        this.errorMessage = 'Error loading customer complaints';
        console.error('Error loading customer complaints:', error);
      }
    );
  }

  filterComplaints(): void {
    if (this.searchTerm) {
      this.filteredComplaints = this.complaints.filter(complaint =>
        complaint.complaintDescription!.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        complaint.complaintStatus!.toLowerCase().includes(this.searchTerm.toLowerCase())
      );
    } else {
      this.filteredComplaints = [...this.complaints];
    }
    this.updatePagination();
  }

  updatePagination(): void {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    const endIndex = startIndex + this.itemsPerPage;
    this.paginatedComplaints = this.filteredComplaints.slice(startIndex, endIndex);
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
    return Math.ceil(this.filteredComplaints.length / this.itemsPerPage);
  }

  viewComplaint(complaintId: number): void {
    this.router.navigate(['Home/complaints/details', complaintId]);
  }
}
