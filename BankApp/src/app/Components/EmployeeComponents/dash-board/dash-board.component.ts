import { Component, OnInit } from '@angular/core';
import { NavbarComponent } from '../navbar/navbar.component';
import { Employee } from '../../../Modules/Employee';
import { AuthService } from '../../../Services/ApiServices/auth.service';
import { Router } from '@angular/router';
import { EmployeeServiceService } from '../../../Services/EmployeeService/employee-service.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-dash-board',
  standalone: true,
  imports: [NavbarComponent, CommonModule],
  templateUrl: './dash-board.component.html',
  styleUrls: ['./dash-board.component.css']
})
export class DashBoardComponent implements OnInit {

  employee: Employee =
  {
    employeeFirstName: '',
    employeeLastName: '',
    emailAddress: '',
    lastLoginDate: new Date(),
    employeeId: '',
    isActive: false
  };

  isAdmin:boolean = false;
  isStaff:boolean = false;

  constructor(
    private authService: AuthService,
    private router: Router,
    private employeeService: EmployeeServiceService
  ) { }

  ngOnInit(): void {
    this.employeeService.getEmployee().subscribe(data => {
      this.employee = data;
    });
    this.isAdmin =  this.authService.isAdmin();
    this.isStaff = this.authService.isStaff();
  }

}
