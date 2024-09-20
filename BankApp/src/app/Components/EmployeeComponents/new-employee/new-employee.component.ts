import { Component } from '@angular/core';
import { NavbarComponent } from "../navbar/navbar.component";
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { EmployeeServiceService } from '../../../Services/EmployeeService/employee-service.service';
import { Router } from '@angular/router';
import { Role } from '../../../Modules/Role';

@Component({
  selector: 'app-new-employee',
  standalone: true,
  imports: [NavbarComponent, CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './new-employee.component.html',
  styleUrl: './new-employee.component.css'
})
export class NewEmployeeComponent {
  employeeForm: FormGroup;
  availableRoles: Role[] = [];  // Array to store the available roles

  constructor(
    private fb: FormBuilder,
    private employeeService: EmployeeServiceService,
    private router: Router
  ) {
    // Initialize the form with validation
    this.employeeForm = this.fb.group({
      emailAddress: ['', [Validators.required, Validators.email]],
      passwordHash: ['', Validators.required],
      employeeFirstName: ['', Validators.required],
      employeeLastName: ['', Validators.required],
      isActive: [true],
      roleId: [null, Validators.required],  // Form control for the role
      createdDate: [new Date()],
      lastLoginDate: [null],
    });
  }

  ngOnInit(): void {
    this.fetchAvailableRoles();  // Fetch roles when the component is initialized
  }

  // Fetch roles from the service
  fetchAvailableRoles(): void {
    this.employeeService.getRoles().subscribe(
      (roles: Role[]) => {
        this.availableRoles = roles;
      },
      (error) => {
        console.error('Error fetching roles', error);
      }
    );
  }

  onSubmit(): void {
    if (this.employeeForm.valid) {
      const newEmployee = this.employeeForm.value;
  
      // Assuming that roleId is the selected role from the form
      const userRoleData = {
        employeeId: newEmployee.employeeId, // This will be generated in the backend after adding the employee
        roleId: newEmployee.roleId,
      };
  
      // First, add the employee
      this.employeeService.addNewEmployee(newEmployee).subscribe(
        (response: any) => {
          console.log('Employee added successfully', response);
  
          // Now, add the user role linking the employee and role
          const employeeId = response.employeeId; // Get the employeeId from the response
          userRoleData.employeeId = employeeId;
  
          // Now send the userRole data to the backend
          this.employeeService.addUserRole(userRoleData).subscribe(
            (roleResponse) => {
              console.log('UserRole added successfully', roleResponse);
              this.router.navigate(['/admindashboard/employees']);
            },
            (roleError) => {
              console.error('Error adding UserRole', roleError);
            }
          );
        },
        (error) => {
          console.error('Error adding employee', error);
        }
      );
    }
  }
  
}
