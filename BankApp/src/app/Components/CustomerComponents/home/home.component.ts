import { Component } from '@angular/core';
import { NavbarComponent } from "../../navbar/navbar.component";
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { RouterOutlet } from '@angular/router';
import { CustomerService } from '../../../Services/CustomerService/customer.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [NavbarComponent,RouterOutlet, CommonModule, RouterLink,RouterLinkActive],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  
  data:any;
  customerId: string | null = null;

  constructor(private router: Router,private customer: CustomerService){}



  ngOnInit():void{
    this.customerId = localStorage.getItem('customerId');
    this.customer.getCustomer().subscribe(data => {this.data = data});
  }
}
