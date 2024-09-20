import { Component } from '@angular/core';
import { NavbarComponent } from "../navbar/navbar.component";
import { Employee } from '../../../Modules/Employee';
import { AuthService } from '../../../Services/ApiServices/auth.service';
import { Router } from '@angular/router';
import { EmployeeServiceService } from '../../../Services/EmployeeService/employee-service.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-support-dash-board',
  standalone: true,
  imports: [NavbarComponent, CommonModule],
  templateUrl: './support-dash-board.component.html',
  styleUrl: './support-dash-board.component.css'
})
export class SupportDashBoardComponent {

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
      this.employee = data;
    });
  }

}
