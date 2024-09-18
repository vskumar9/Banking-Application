import { Component } from '@angular/core';
import { HomeComponent } from "../home/home.component";
import { NavbarComponent } from "../../navbar/navbar.component";
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { CustomerService } from '../../../Services/CustomerService/customer.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [HomeComponent, NavbarComponent, CommonModule, RouterLink,RouterLinkActive],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent {

  data:any | null = null;
  customerId: string | null = null;

  constructor(private router: Router,private customer: CustomerService){}

  ngOnInit():void{
    this.customerId = localStorage.getItem('customerId');
    this.customer.getCustomer().subscribe(data => {this.data = data});
  }

  update(customerId: number) {
    this.router.navigate(['Home/profile/update']);
  }
    
}
