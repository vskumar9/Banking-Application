import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable, PLATFORM_ID } from '@angular/core';
import { Router } from '@angular/router';
import { Customer } from '../../Modules/Customer';
import { BehaviorSubject, catchError, Observable, throwError } from 'rxjs';
import { AuthService } from '../ApiServices/auth.service';
import { isPlatformBrowser } from '@angular/common';
import { ComplaintType } from '../../Modules/ComplaintType';
import { TransferRequest } from '../../Modules/TransferRequest';
import { Account } from '../../Modules/Account';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  private mainUrl = 'https://localhost:7203/api';
  private apiUrl = 'https://localhost:7203/api/Customer'; 
  private complaintAPI = 'https://localhost:7203/api/Complaint';
  private tokenKey = 'jwtToken'; // Use the correct key for localStorage
  private userRoleSubject = new BehaviorSubject<string>('');
  userRole$ = this.userRoleSubject.asObservable();

  constructor(private http: HttpClient, private router: Router, private authService: AuthService, @Inject(PLATFORM_ID) private platformId: Object) 
  { 
  }

  private getHeaders(): HttpHeaders {
    const token = this.authService.getToken(); // Retrieve token from AuthService
    let headers = new HttpHeaders();
    if (token && isPlatformBrowser(this.platformId)) {
      headers = headers.set('Authorization', `Bearer ${token}`);
    }
    return headers;
  }

  registerCustomer(customer: Customer): Observable<any> {
    const headers = this.getHeaders();
    return this.http.post<any>(`${this.apiUrl}`, customer);
  }

  getCustomer(): Observable<any> {
    const headers = this.getHeaders(); // Get headers with token
    return this.http.get<any>(`${this.apiUrl}/customer`, { headers }); // Pass headers in the request
  }

  update(customer: Customer){
    const headers = this.getHeaders(); // Get headers with token
    return this.http.put(`${this.apiUrl}/customer/Update`, customer, { headers });
  }

  complaintDetails(complaintId: number): Observable<any>{
    const headers = this.getHeaders(); // Get headers with token
    return this.http.get<any>(`${this.complaintAPI}/${complaintId}`, { headers });
  }

  complaintType(): Observable<ComplaintType>{
    const headers = this.getHeaders();
    return this.http.get(`${this.mainUrl}/ComplaintType`, { headers });
  }
  registerComplaint(complaint: FormData){
    const headers = this.getHeaders();
    return this.http.post<any>(`${this.mainUrl}/Complaint`, complaint, { headers });
  }

  getFileDownloadUrl(complaintId: number): Observable<Blob> {
    const headers = this.getHeaders();
    return this.http.get(`${this.complaintAPI}/download/${complaintId}`, { responseType: 'blob', headers });

  }

  getLoans(customerId:string): Observable<any>{
    const headers = this.getHeaders();
    return this.http.get(`${this.mainUrl}/LoanApplication/`);
  }

  getLoanById(id: number): Observable<any>{
    const headers = this.getHeaders();
    return  this.http.get(`${this.mainUrl}/LoanApplication/` + id, { headers });
  }

  getLoanTypes(): Observable<any>{
    const headers = this.getHeaders();
    return  this.http.get(`${this.mainUrl}/LoanType`, { headers });
  }

  applyForLoan(formData : FormData): Observable<any>{
    const headers = this.getHeaders();
    return  this.http.post<any>(`${this.mainUrl}/LoanApplication`, formData, { headers });
  }
  
  getLoanFileDownloadUrl(loanId: number): Observable<Blob> {
    const headers = this.getHeaders();
    return this.http.get(`${this.mainUrl}/LoanApplication/download/${loanId}`, { responseType: 'blob', headers });

  }

  getTransactionTypes():Observable<any>{
    const headers = this.getHeaders();
    return this.http.get(`${this.mainUrl}/TransactionType`, { headers });
  }

  transferAmount(request: TransferRequest, transactionTypeId: number): Observable<any> {
    const headers = this.getHeaders();
  const url = `${this.mainUrl}/TransactionLog/transfer?TransactionTypeId=${transactionTypeId}`;
  return this.http.post<any>(url, request, { headers }).pipe(
    catchError((error) => {
      console.error('Transfer error:', error); 
      return throwError(() => new Error(error?.error?.message || 'Transfer failed. Invalid response.'));
    })
  );
  }

  getAccountTypes(): Observable<any>{
    const headers = this.getHeaders();
    return this.http.get(`${this.mainUrl}/AccountStatusType`, { headers });
  }

  createAccount(account: Partial<Account>): Observable<Account> {
    return this.http.post<Account>(`${this.mainUrl}/AccountStatusType`, account,  { headers: this.getHeaders() });

  }


  
}
