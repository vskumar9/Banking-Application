import { Component } from '@angular/core';
import { EmployeeServiceService } from '../../../Services/EmployeeService/employee-service.service';
import { Employee } from '../../../Modules/Employee';
import { NavbarComponent } from "../navbar/navbar.component";
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [NavbarComponent, CommonModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent {

  employee: Employee | null = null;
  errorMessage: string | null = null;

  constructor(private employeeService: EmployeeServiceService) {}

  ngOnInit(): void {
    this.employeeService.getEmployee().subscribe(
      (data: Employee) => {
        this.employee = data;
      },
      (error) => {
        console.error('Error fetching employee details', error);
        this.errorMessage = 'Error fetching employee details. Please try again later.';
      }
    );
  }


}
