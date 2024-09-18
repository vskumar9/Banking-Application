import { Component } from '@angular/core';
import { Complaint } from '../../../Modules/Complaint';
import { CustomerService } from '../../../Services/CustomerService/customer.service';
import { ActivatedRoute, Router, RouterLink, RouterLinkActive } from '@angular/router';
import { NavbarComponent } from "../../navbar/navbar.component";
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-complaint-details',
  standalone: true,
  imports: [NavbarComponent,CommonModule, RouterLink, RouterLinkActive],
  templateUrl: './complaint-details.component.html',
  styleUrl: './complaint-details.component.css'
})
export class ComplaintDetailsComponent {

  complaint:  Complaint | null = null;
  constructor(private customerService:CustomerService, private route: ActivatedRoute, private router:Router) {}

  ngOnInit():void{
    const id = +this.route.snapshot.params['id'];
    this.customerService.complaintDetails(id).subscribe((data) => { 
      console.log(data);
      this.complaint =  data;
     },
    (error)=> console.error('Error fetching',error) );
  }

  goBack(){
    this.router.navigate(['Home/complaints'])
  }
  downloadFile(complaintId: number | null): void {
    if (!complaintId) {
      console.error('Complaint ID is missing');
      return;
    }

    this.customerService.getFileDownloadUrl(complaintId).subscribe(
      (response: Blob) => {
        const blob = new Blob([response], { type: response.type });
        const url = window.URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url;
        a.download = this.complaint?.files?.name || 'complaint-file';
        a.click();
        window.URL.revokeObjectURL(url); // Clean up after download
      },
      (error) => {
        console.error('Error downloading the file:', error);
      }
    );
  }
}
