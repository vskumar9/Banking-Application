import { Component } from '@angular/core';
import { EmployeeServiceService } from '../../../Services/EmployeeService/employee-service.service';
import { Router } from '@angular/router';
import { Configuration } from '../../../Modules/Configuration';
import { NavbarComponent } from "../navbar/navbar.component";
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-congfiguration',
  standalone: true,
  imports: [NavbarComponent, CommonModule],
  templateUrl: './congfiguration.component.html',
  styleUrl: './congfiguration.component.css'
})
export class CongfigurationComponent {

  configurations: Configuration | any ;
  errorMessage: string | null = null;

  constructor(
    private employeeService: EmployeeServiceService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.employeeService.getConfiguration().subscribe(
      data => this.configurations = data,
      error => this.errorMessage = 'Error fetching configurations. Please try again later.'
    );
  }
}
