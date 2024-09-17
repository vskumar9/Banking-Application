import { Account } from "./Account";
import { Complaint } from "./Complaint";
import { ComplaintFeedback } from "./ComplaintFeedback";
import { LoanApplication } from "./LoanApplication";
import { TransactionLog } from "./TransactionLog";

export interface Customer {
    customerId: string ;
    passwordHash: string;
    customerFirstName: string;
    customerLastName: string ;
    customerAddress1: string ;
    customerAddress2: string;
    city: string ;
    state: string;
    zipCode: string ;
    emailAddress: string;
    cellPhone: string ;
    homePhone: string ;
    workPhone: string ;
    isActive: boolean;
    lastLoginDate?: Date ;
    accounts: Account[];
    complaintFeedbacks?: ComplaintFeedback[];
    complaints: Complaint[];
    loanApplications: LoanApplication[];
    transactionLogs: TransactionLog[];
}