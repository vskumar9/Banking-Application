import { Component } from '@angular/core';
import { HomeComponent } from "../home/home.component";
import { NavbarComponent } from "../../navbar/navbar.component";

@Component({
  selector: 'app-complaints',
  standalone: true,
  imports: [HomeComponent, NavbarComponent],
  templateUrl: './complaints.component.html',
  styleUrl: './complaints.component.css'
})
export class ComplaintsComponent {

}
