import { Component } from '@angular/core';
import { NavbarComponent } from "../navbar/navbar.component";
import { CommonModule } from '@angular/common';
import { Employee } from '../../../Modules/Employee';
import { EmployeeServiceService } from '../../../Services/EmployeeService/employee-service.service';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-employess',
  standalone: true,
  imports: [NavbarComponent, CommonModule, FormsModule],
  templateUrl: './employess.component.html',
  styleUrl: './employess.component.css'
})
export class EmployessComponent {

  employees: Employee[] = [];           // List of all employees
  filteredEmployees: Employee[] = [];   // Filtered list based on search
  searchTerm: string = '';              // Search term for filtering

  constructor(private employeeService: EmployeeServiceService, private router: Router) {}

  ngOnInit(): void {
    // Fetch the employees when the component initializes
    this.employeeService.getEmployees().subscribe(
      (data: Employee[]) => {
        this.employees = data;
        this.filteredEmployees = data; // Initially, no filtering
      },
      (error) => {
        console.error('Error fetching employees', error);
      }
    );
  }

  // Filter employees based on the search term
  filterEmployees(): void {
    const term = this.searchTerm.toLowerCase();
    this.filteredEmployees = this.employees.filter(employee =>
      (employee.employeeFirstName?.toLowerCase().includes(term) || 
       employee.employeeLastName?.toLowerCase().includes(term) ||
       employee.emailAddress?.toLowerCase().includes(term))
    );
  }

  goToAddEmployee(): void {
    this.router.navigate(['admindashboard/employees/new']);
  }

  goToUpdateEmployee(employeeId: string): void {
    this.router.navigate([`admindashboard/employees/update/${employeeId}`]);
  }


  deleteEmployee(employeeId: string): void {
    if (confirm('Are you sure you want to delete this employee?')) {
      this.employeeService.deleteEmployee(employeeId).subscribe(
        () => {
          this.filteredEmployees = this.filteredEmployees.filter(emp => emp.employeeId !== employeeId);
          alert('Employee deleted successfully.');
        },
        error => {
          console.error('Error deleting employee', error);
          alert('Error deleting employee. Please try again.');
        }
      );
    }
  }

}
