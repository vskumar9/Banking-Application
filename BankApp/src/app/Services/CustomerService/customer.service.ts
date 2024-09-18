import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable, PLATFORM_ID } from '@angular/core';
import { Router } from '@angular/router';
import { Customer } from '../../Modules/Customer';
import { BehaviorSubject, Observable } from 'rxjs';
import { AuthService } from '../ApiServices/auth.service';
import { isPlatformBrowser } from '@angular/common';
import { Complaint } from '../../Modules/Complaint';
import { ComplaintType } from '../../Modules/ComplaintType';

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

  


  
}
