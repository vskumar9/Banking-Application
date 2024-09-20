import { Component } from '@angular/core';
import { NavbarComponent } from '../navbar/navbar.component';
import { AuthService } from '../../../Services/ApiServices/auth.service';
import { Router } from '@angular/router';
import { EmployeeServiceService } from '../../../Services/EmployeeService/employee-service.service';
import { Employee } from '../../../Modules/Employee';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-staff-dash-board',
  standalone: true,
  imports: [NavbarComponent, CommonModule],
  templateUrl: './staff-dash-board.component.html',
  styleUrl: './staff-dash-board.component.css'
})
export class StaffDashBoardComponent {

  employee: Employee =
  {
    employeeFirstName: '',
    employeeLastName: '',
    emailAddress: '',
    lastLoginDate: new Date(),
    employeeId: '',
    isActive: false
  };

  constructor(
    private authService: AuthService,
    private router: Router,
    private employeeService: EmployeeServiceService
  ) { }

  ngOnInit(): void {
    this.employeeService.getEmployee().subscribe(data => {
      console.log(data)
      this.employee = data;
    });
  }


}
