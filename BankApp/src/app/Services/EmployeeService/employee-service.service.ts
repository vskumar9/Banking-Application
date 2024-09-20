import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable, PLATFORM_ID } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';
import { AuthService } from '../ApiServices/auth.service';
import { isPlatformBrowser } from '@angular/common';
import { observableToBeFn } from 'rxjs/internal/testing/TestScheduler';
import { Customer } from '../../Modules/Customer';
import { LoanApplication } from '../../Modules/LoanApplication';
import { Employee } from '../../Modules/Employee';
import { UserRole } from '../../Modules/UserRole';
import { TransactionLog } from '../../Modules/TransactionLog';

@Injectable({
  providedIn: 'root'
})
export class EmployeeServiceService {


  private mainUrl = 'https://localhost:7203/api';

  private tokenKey = 'jwtToken'; // Use the correct key for localStorage
  private userRoleSubject = new BehaviorSubject<string>('');
  userRole$ = this.userRoleSubject.asObservable();

  constructor(private http: HttpClient, private router: Router, private authService: AuthService, @Inject(PLATFORM_ID) private platformId: Object) 
  { 
  }

  private getHeaders(): HttpHeaders {
    const token = this.authService.getToken(); // Retrieve token from AuthService
  
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`  // Attach the token here
    });
  }

  getEmployee(): Observable<any> {
    const headers = this.getHeaders(); // Get headers with token
    return this.http.get<any>(`${this.mainUrl}/Employee/employee`, { headers }); // Pass headers in the request
  }
  getEmployees(): Observable<any> {
    const headers = this.getHeaders(); // Get headers with token
    return this.http.get<any>(`${this.mainUrl}/Employee`, { headers }); // Pass headers in the request
  }

  addNewEmployee(employee: Employee): Observable<any>{
    const headers = this.getHeaders(); // Get headers with token
    return this.http.post<any>(`${this.mainUrl}/Employee`, employee, { headers }); 
  }

  deleteEmployee(id:string):Observable<any>{
    const headers = this.getHeaders(); // Get headers with token
    return this.http.delete<any>(`${this.mainUrl}/Employee/${id}`, { headers }); 
  }

  getEmployeeById(employeeId: string): Observable<Employee> {
    const headers = this.getHeaders();
    return this.http.get<Employee>(`${this.mainUrl}/Employee/${employeeId}`, { headers });
  }

  updateEmployee(employee: Employee): Observable<Employee> {
    const headers = this.getHeaders();
    return this.http.put<Employee>(`${this.mainUrl}/Employee`, employee, { headers });
  }

  // getCustomer(page: number = 1, pageSize: number = 10): Observable<any> {
  //   const headers = this.getHeaders();
  //   return this.http.get<any>(`${this.mainUrl}/Customer?page=${page}&pageSize=${pageSize}`, { headers });
  // }

  // Create a new customer
  createCustomer(customer: Customer): Observable<any> {
    const headers = this.getHeaders();
    return this.http.post<any>(`${this.mainUrl}/Customer`, customer, { headers });
  }

  getCustomerById(id:string):Observable<any>{
    const headers = this.getHeaders();
    return this.http.get<any>(`${this.mainUrl}/Customer/${id}`, { headers });
  }

  // Update existing customer
  updateCustomer(customerId: string, customer: Customer): Observable<any> {
    const headers = this.getHeaders();
    return this.http.put<any>(`${this.mainUrl}/Customer/${customerId}`, customer, { headers });
  }

  // Delete customer
  deleteCustomer(customerId: string): Observable<any> {
    const headers = this.getHeaders();
    return this.http.delete<any>(`${this.mainUrl}/Customer/${customerId}`, { headers });
  }
  
  getLoans(): Observable<any>{
    const headers = this.getHeaders(); // Get headers with token
    return  this.http.get<any>(`${this.mainUrl}/LoanApplication`, { headers }); // Pass headers
  }

  updateLoan(loan: LoanApplication): Observable<any>{
    const headers = this.getHeaders(); // Get headers with token
    return this.http.put<any>(`${this.mainUrl}/LoanApplication/${loan.loanId}`, loan, { headers });
  }

  getLoanById(id: number):Observable<any>{
    const headers = this.getHeaders(); // Get headers with token
    return this.http.get<any>(`${this.mainUrl}/LoanApplication/${id}`, { headers });
  }

  getAudit(): Observable<any>{
    const headers = this.getHeaders(); // Get headers with token
    return  this.http.get<any>(`${this.mainUrl}/AuditLog`, { headers }); // Pass headers
  }

  getComplaints(): Observable<any>{//admin, support, staff
    const headers = this.getHeaders(); // Get headers with token
    return  this.http.get<any>(`${this.mainUrl}/LoanApplication`, { headers }); // Pass headers
  }

  getRoles(): Observable<any>{
    const headers = this.getHeaders(); // Get headers with token
    return  this.http.get<any>(`${this.mainUrl}/Role`, { headers }); // Pass headers 
  }
  getUserRole(): Observable<any>{
    const headers = this.getHeaders(); // Get headers with token
    return  this.http.get<any>(`${this.mainUrl}/UserRole`, { headers }); 
  }

  createUserRole(userRole: UserRole) {
    const headers = this.getHeaders();
    return this.http.post(`${this.mainUrl}/UserRole`, userRole, { headers });
  }

  // Service to update UserRole
  updateUserRole(userRole: UserRole) {
    const headers = this.getHeaders();
    return this.http.put(`${this.mainUrl}/UserRole/${userRole.userRoleId}`, userRole, { headers }); // Adjust endpoint as needed
  }


  addUserRole(userRoleData: any) {
    const headers = this.getHeaders(); // Get headers with token
    return this.http.post(`${this.mainUrl}/UserRole`, userRoleData, { headers });  
  }

  getConfiguration():  Observable<any>{
    const headers = this.getHeaders(); // Get headers with token
    return  this.http.get<any>(`${this.mainUrl}/Configuration`, { headers }); // Pass headers 
  }

  getCustomer():  Observable<any>{
    const headers = this.getHeaders(); // Get headers with token
    return  this.http.get<any>(`${this.mainUrl}/Customer`, { headers }); // Pass headers 
  
  }
  getPermission():  Observable<any>{
    const headers = this.getHeaders(); // Get headers with token
    return  this.http.get<any>(`${this.mainUrl}/Permission`, { headers }); // Pass headers 
  }
  getRolePermission():  Observable<any>{
    const headers = this.getHeaders(); // Get headers with token
    return  this.http.get<any>(`${this.mainUrl}/RolePermission`, { headers }); // Pass headers 
  }
  getTransactionLog():  Observable<any>{
    const headers = this.getHeaders(); // Get headers with token
    return  this.http.get<any>(`${this.mainUrl}/TransactionLog`, { headers }); // Pass headers 
  }

  getTransactionById(transactionId: number): Observable<TransactionLog> {
    const headers = this.getHeaders(); // Get headers with token
    return this.http.get<TransactionLog>(`${this.mainUrl}/TransactionLog/${transactionId}`, { headers });
  }
  
  getTransactionType():  Observable<any>{
    const headers = this.getHeaders(); // Get headers with token
    return  this.http.get<any>(`${this.mainUrl}/TransactionType`, { headers }); // Pass headers 
  }

  getAccountStatusType():  Observable<any>{
    const headers = this.getHeaders(); // Get headers with token
    return  this.http.get<any>(`${this.mainUrl}/AccountStatusType`, { headers }); // Pass headers 
  }
  getComplaintFeedback():  Observable<any>{
    const headers = this.getHeaders(); // Get headers with token
    return  this.http.get<any>(`${this.mainUrl}/ComplaintFeedback`, { headers }); // Pass headers 
  }



}
