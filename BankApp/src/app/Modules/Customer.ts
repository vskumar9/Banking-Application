import { Account } from "./Account";
import { Complaint } from "./Complaint";
import { ComplaintFeedback } from "./ComplaintFeedback";
import { LoanApplication } from "./LoanApplication";
import { TransactionLog } from "./TransactionLog";

export interface Customer {
    customerId?: string | null;
    passwordHash?: string | null;
    customerFirstName?: string | null;
    customerLastName?: string | null;
    customerAddress1?: string | null;
    customerAddress2?: string | null;
    city?: string | null;
    state?: string | null;
    zipCode?: string | null;
    emailAddress?: string | null;
    cellPhone?: string | null;
    homePhone?: string | null;
    workPhone?: string | null;
    isActive: boolean;
    lastLoginDate?: Date | null;
    accounts?: Account[];
    complaintFeedbacks?: ComplaintFeedback[];
    complaints?: Complaint[];
    loanApplications?: LoanApplication[];
    transactionLogs?: TransactionLog[];
}