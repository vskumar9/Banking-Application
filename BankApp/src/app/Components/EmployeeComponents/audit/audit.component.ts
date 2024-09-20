import { Component } from '@angular/core';
import { AuditLog } from '../../../Modules/AuditLog';
import { EmployeeServiceService } from '../../../Services/EmployeeService/employee-service.service';
import { NavbarComponent } from "../navbar/navbar.component";
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-audit',
  standalone: true,
  imports: [NavbarComponent, FormsModule, CommonModule],
  templateUrl: './audit.component.html',
  styleUrl: './audit.component.css'
})
export class AuditComponent {

  auditLogs: AuditLog[]=[];
  searchTerm: string = '';  // For filtering the audit logs

  constructor(private employeeService: EmployeeServiceService) {}

  ngOnInit(): void {
    this.fetchAuditLogs();
  }

  fetchAuditLogs(): void {
    this.employeeService.getAudit().subscribe(
      (data: AuditLog[]) => {
        this.auditLogs = data;
      },
      (error) => {
        console.error('Error fetching audit logs', error);
      }
    );
  }

  filterAuditLogs(): AuditLog[] {
  const term = this.searchTerm.toLowerCase();
  return this.auditLogs.filter((data: AuditLog) =>
    data.action?.toLowerCase().includes(term) || 
    data.employee?.employeeFirstName?.toLowerCase().includes(term) ||
    data.employee?.employeeLastName?.toLowerCase().includes(term) ||
    data.ipAddress?.toLowerCase().includes(term)
  );
}

  
}
