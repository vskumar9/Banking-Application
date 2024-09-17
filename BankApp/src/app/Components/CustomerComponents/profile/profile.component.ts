import { Component } from '@angular/core';
import { HomeComponent } from "../home/home.component";
import { NavbarComponent } from "../../navbar/navbar.component";

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [HomeComponent, NavbarComponent],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent {
    
}
