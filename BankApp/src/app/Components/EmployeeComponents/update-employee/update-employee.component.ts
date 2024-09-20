import { Component } from '@angular/core';
import { NavbarComponent } from "../navbar/navbar.component";
import { Employee } from '../../../Modules/Employee';
import { UserRole } from '../../../Modules/UserRole';
import { EmployeeServiceService } from '../../../Services/EmployeeService/employee-service.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Role } from '../../../Modules/Role';

@Component({
  selector: 'app-update-employee',
  standalone: true,
  imports: [NavbarComponent, CommonModule,  FormsModule],

  templateUrl: './update-employee.component.html',
  styleUrl: './update-employee.component.css'
})
export class UpdateEmployeeComponent {

  employee: Employee = {
    employeeId: '',
    employeeFirstName: '',
    employeeLastName: '',
    emailAddress: '',
    passwordHash: '',
    isActive: false,
    userRoles: []
  };

  availableRoles: Role[] = [];   // List of roles from the API
  selectedRoleId: number | null = null;  // Role selected in the form
  existingUserRoleId: number | null = null;  // Track the existing userRoleId

  constructor(
    private employeeService: EmployeeServiceService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    const employeeId = this.route.snapshot.params['id'];
  
    // Fetch employee details by ID
    this.employeeService.getEmployeeById(employeeId).subscribe(
      (data: Employee) => {
        this.employee = data;
        
        // Check if the employee has an existing user role
        if (this.employee.userRoles && this.employee.userRoles.length > 0) {
          this.selectedRoleId = this.employee.userRoles[0].roleId || null;
          this.existingUserRoleId = this.employee.userRoles[0].userRoleId || null;
        }
      },
      (error) => {
        console.error('Error fetching employee', error);
      }
    );
  
    // Fetch available roles
    this.employeeService.getRoles().subscribe(
      (userRoles: Role[]) => {
        this.availableRoles = userRoles;
      },
      (error) => {
        console.error('Error fetching roles', error);
      }
    );
  }

  // Method to update the employee
  updateEmployee(): void {
    // Prepare UserRole for update or create, including userRoleId if it exists
    const userRole: UserRole = {
      userRoleId: this.existingUserRoleId,  // Existing userRoleId or null
      employeeId: this.employee.employeeId,
      roleId: this.selectedRoleId
    };

    console.log(userRole);

    // Call the update employee API
    this.employeeService.updateEmployee(this.employee).subscribe(
      (response) => {
        // Handle successful response
        alert('Employee updated successfully.');
        
        // Update or create user role after the employee has been updated
        this.updateUserRole(userRole);
        
        this.router.navigate(['/admindashboard/employees']); // Redirect to employees list
      },
      (error) => {
        console.error('Error updating employee', error);
        alert('Error updating employee. Please try again.');
      }
    );
  }

  // Method to update or create UserRole
  updateUserRole(userRole: UserRole): void {
    console.log(userRole); // Debugging the data sent

    if (userRole.userRoleId) {
      // If userRoleId exists, perform an update (PUT)
      this.employeeService.updateUserRole(userRole).subscribe(
        (response) => {
          console.log('UserRole updated successfully');
        },
        (error) => {
          console.error('Error updating UserRole', error);
        }
      );
    } else {
      // If no userRoleId, create a new UserRole (POST)
      this.employeeService.createUserRole(userRole).subscribe(
        (response) => {
          console.log('UserRole created successfully');
        },
        (error) => {
          console.error('Error creating UserRole', error);
        }
      );
    }
  }
}
