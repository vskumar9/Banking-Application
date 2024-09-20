import { Component } from '@angular/core';
import { AuthService } from '../../../Services/ApiServices/auth.service';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {

  isAdmin = false;
  isStaff = false;
  isSupport = false;
  isCashier = false;

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit():void{
    this.isAdmin = this.authService.isAdmin();
    this.isStaff = this.authService.isStaff();
    this.isSupport = this.authService.isSupport();
    this.isCashier = this.authService.isCashier();
  }


  
  logout(): void{
    this.authService.customerLogout();
    console.log('logout Current user.');
  }

}
