import { Component } from '@angular/core';
import { AuthService } from '../../Services/ApiServices/auth.service';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  
  constructor(private authService: AuthService, private router: Router) { }

  
  logout(): void{
    this.authService.customerLogout();
    console.log('logout Current user.');
  }
}
