import { Component } from '@angular/core';
import { HomeComponent } from "../home/home.component";
import { NavbarComponent } from "../../navbar/navbar.component";

@Component({
  selector: 'app-loan',
  standalone: true,
  imports: [HomeComponent, NavbarComponent],
  templateUrl: './loan.component.html',
  styleUrl: './loan.component.css'
})
export class LoanComponent {

}
