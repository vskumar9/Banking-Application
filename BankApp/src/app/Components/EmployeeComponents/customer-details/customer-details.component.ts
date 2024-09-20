import { Component } from '@angular/core';
import { NavbarComponent } from "../navbar/navbar.component";
import { Customer } from '../../../Modules/Customer';
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeeServiceService } from '../../../Services/EmployeeService/employee-service.service';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../Services/ApiServices/auth.service';

@Component({
  selector: 'app-customer-details',
  standalone: true,
  imports: [NavbarComponent, CommonModule],
  templateUrl: './customer-details.component.html',
  styleUrl: './customer-details.component.css'
})
export class CustomerDetailsComponent {

  customer: Customer = {
    customerId: '',
    passwordHash: '',
    customerFirstName: '',
    customerLastName: '',
    customerAddress1: '',
    customerAddress2: '',
    city: '',
    state: '',
    zipCode: '',
    emailAddress: '',
    cellPhone: '',
    homePhone: '',
    workPhone: '',
    isActive: false,
    lastLoginDate: undefined,
    accounts: [],
    complaintFeedbacks: [],
    complaints: [],
    loanApplications: [],
    transactionLogs: []
  };

  isAdmin:boolean = false;
  isStaff:boolean= false;
  constructor(private authService: AuthService, private employeeService:EmployeeServiceService, private route: ActivatedRoute, private router:Router) {}

  ngOnInit():void{
    const id:string = this.route.snapshot.params['id'];
    this.employeeService.getCustomerById(id).subscribe((data) => { 
      console.log(data);
      this.customer =  data;
     },
    (error)=> console.error('Error fetching',error) );
    this.isAdmin = this.authService.isAdmin();
    this.isStaff = this.authService.isStaff();
  }

  goBack(){
    if(this.isAdmin) this.router.navigate(['admindashboard/customers']);
    if(this.isStaff) this.router.navigate(['staffdashboard/customers'])
  }


}
