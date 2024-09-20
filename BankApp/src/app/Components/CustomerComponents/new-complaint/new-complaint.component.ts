import { Component } from '@angular/core';
import { NavbarComponent } from "../../navbar/navbar.component";
import { ComplaintType } from '../../../Modules/ComplaintType';
import { CustomerService } from '../../../Services/CustomerService/customer.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, FormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Complaint } from '../../../Modules/Complaint';

@Component({
  selector: 'app-new-complaint',
  standalone: true,
  imports: [NavbarComponent,FormsModule, CommonModule],
  templateUrl: './new-complaint.component.html',
  styleUrl: './new-complaint.component.css'
})
export class NewComplaintComponent {

  complaintTypes:ComplaintType|any;
  complaint: Complaint = { // Initialize with an empty complaint object
    complaintTypeId: null,
    complaintDate: new  Date(),
    complaintDescription: '',
    files: null,
    complaintStatus: 'Open',
    employeeId: null
  };

  selectedFile: File | null = null; // For storing the selected file

  constructor(private customerService: CustomerService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    this.loadComplaintTypes();
  }

  loadComplaintTypes() {
    this.customerService.complaintType().subscribe((data) => {
      this.complaintTypes = data;
    });
  }

  onFileSelected(event: any): void {
    const file = event.target.files[0];
    if (file) {
      this.selectedFile = file; // Storing the selected file
    }
  }

  submitComplaint(): void {
    const formData = new FormData();

    // Adding the form data
    formData.append('complaintTypeId', this.complaint.complaintTypeId!.toString());
    formData.append('complaintDate', this.complaint.complaintDate.toISOString());
    formData.append('complaintDescription', this.complaint.complaintDescription!);
    formData.append('complaintStatus', this.complaint.complaintStatus!);
    if (this.complaint.employeeId) {
      formData.append('employeeId', this.complaint.employeeId!.toString());
    }

    // Adding the file to FormData
    if (this.selectedFile) {
      formData.append('file', this.selectedFile); 
    } else {
      console.error('No file selected');
      return;
    }

    this.customerService.registerComplaint(formData).subscribe(
      response => {
        console.log('Complaint registered successfully:', response);
        this.router.navigate(['Home/complaints']);
      },
      error => {
        console.error('Error registering complaint:', error);
      }
    );
  }

}
