import { HttpClient } from '@angular/common/http';
import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { Router } from '@angular/router';
import { isPlatformBrowser } from '@angular/common';
import { jwtDecode } from 'jwt-decode';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Token } from '../../Modules/Token';
import { Login } from '../../Modules/Login';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = 'https://localhost:7203/api/Login/'; 
  private apiLogout = 'https://localhost:7203/api/Logout/';
  private tokenKey = 'jwtToken'; // Use the correct key for localStorage
  private userRoleSubject = new BehaviorSubject<string>('');
  userRole$ = this.userRoleSubject.asObservable();

  constructor(private http: HttpClient, private router: Router, @Inject(PLATFORM_ID) private platformId: Object) 
  { 
    this.setUserRoleFromToken();
  }

  customerAuth(login: Login): Observable<Token> {
    console.log("welcome customer");
    return this.http.post<Token>(this.apiUrl+'customer', login).pipe(
      tap(response => {
        if (isPlatformBrowser(this.platformId)) {
          localStorage.setItem(this.tokenKey, response.token); // Store the token in localStorage
        }
        this.setUserRoleFromToken(); // Update user role
        this.router.navigate(['/Home']); // Navigate to home or dashboard
      })
    );
  }
  employeeAuth(login: Login): Observable<Token> {
    console.log("welcome employee");
    return this.http.post<Token>(this.apiUrl+'employee', login).pipe(
      tap(response => {
        if (isPlatformBrowser(this.platformId)) {
          localStorage.setItem(this.tokenKey, response.token); 
        }
        this.setUserRoleFromToken(); 
        this.router.navigate(['/dashborad']); 
      })
    );
  }

  setUserRoleFromToken(): void {
    const token = this.getToken();
    if (token) {
      try {
        const decodedToken: any = jwtDecode(token);
        this.userRoleSubject.next(decodedToken.role || '');
      } catch (e) {
        console.error('Error decoding token:', e);
        this.userRoleSubject.next('');
      }
    } else {
      this.userRoleSubject.next('');
    }
  }

  getToken(): string | null {
    if (isPlatformBrowser(this.platformId)) {
      return localStorage.getItem(this.tokenKey);
    }
    return null;
  }

  isAuthenticated(): boolean {
    if (isPlatformBrowser(this.platformId)) {
      const token = localStorage.getItem(this.tokenKey);
      return !!token;
    }
    return false;
  }

  hasRole(requiredRole: string): boolean {
    const token = this.getToken();
    if (!token) return false;
    try {
      const decodedToken: any = jwtDecode(token);
      return decodedToken.role === requiredRole;
    } catch (e) {
      console.error('Error decoding token:', e);
      return false;
    }
  }

  getUserRole(): string {
    return this.userRoleSubject.value;
  }

  isAdmin(): boolean {
    return this.getUserRole() === 'admin';
  }
  isStaff(): boolean {
    return this.getUserRole() === 'staff';
  }
  isSupport(): boolean {
    return this.getUserRole() === 'support';
  }
  isCashier(): boolean {
    return this.getUserRole() === 'cashier';
  }

  customerLogout(): void {
    if (isPlatformBrowser(this.platformId)) {
      const token = this.getToken();
      if (token) {
        this.http.post(`${this.apiLogout}customer`, {}, {
          headers: { Authorization: `Bearer ${token}` }
        }).subscribe({
          next: () => {
            localStorage.removeItem(this.tokenKey); // Clear the token from localStorage
            this.userRoleSubject.next(''); // Clear the user role
            this.router.navigate(['/login']); // Redirect to login page
          },
          error: (err) => {
            console.error('Logout error:', err);
            localStorage.removeItem(this.tokenKey); // Clear the token even if the logout request fails
            this.userRoleSubject.next(''); // Clear the user role
            this.router.navigate(['/login']); // Redirect to login page
          }
        });
      } else {
        this.router.navigate(['/login']); // Redirect to login page if no token is present
      }
    } else {
      this.router.navigate(['/login']); // Redirect to login page if not in browser
    }
  }
  employeeLogout(): void {
    if (isPlatformBrowser(this.platformId)) {
      const token = this.getToken();
      if (token) {
        this.http.post(`${this.apiLogout}employee`, {}, {
          headers: { Authorization: `Bearer ${token}` }
        }).subscribe({
          next: () => {
            localStorage.removeItem(this.tokenKey); // Clear the token from localStorage
            this.userRoleSubject.next(''); // Clear the user role
            this.router.navigate(['/login']); // Redirect to login page
          },
          error: (err) => {
            console.error('Logout error:', err);
            localStorage.removeItem(this.tokenKey); // Clear the token even if the logout request fails
            this.userRoleSubject.next(''); // Clear the user role
            this.router.navigate(['/login']); // Redirect to login page
          }
        });
      } else {
        this.router.navigate(['/login']); // Redirect to login page if no token is present
      }
    } else {
      this.router.navigate(['/login']); // Redirect to login page if not in browser
    }
  }
  

  // logout() {
  //   if (isPlatformBrowser(this.platformId)) {
  //     localStorage.removeItem(this.tokenKey);
  //   }
  //   this.router.navigate(['/login']);
  // }
}
