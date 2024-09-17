import { Component, ElementRef, OnInit, Renderer2 } from '@angular/core';
import { NavbarComponent } from "../navbar/navbar.component";

@Component({
  selector: 'app-dash-board',
  standalone: true,
  imports: [NavbarComponent],
  templateUrl: './dash-board.component.html',
  styleUrl: './dash-board.component.css'
})
export class DashBoardComponent {

}
