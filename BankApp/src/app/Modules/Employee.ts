import { AuditLog } from "./AuditLog";
import { Complaint } from "./Complaint";
import { ComplaintResolution } from "./ComplaintResolution";
import { Configuration } from "./Configuration";
import { LoanApplication } from "./LoanApplication";
import { LoanRepaymentLog } from "./LoanRepaymentLog";
import { TransactionLog } from "./TransactionLog";
import { UserRole } from "./UserRole";

export interface Employee {
    employeeId: string;
    emailAddress?: string | null;
    passwordHash?: string | null;
    employeeFirstName?: string | null;
    employeeLastName?: string | null;
    createdDate?: Date | null;
    isActive: boolean;
    lastLoginDate?: Date | null;
    auditLogs?: AuditLog[];
    complaintResolutions?: ComplaintResolution[];
    complaints?: Complaint[];
    configurations?: Configuration[];
    loanApplications?: LoanApplication[];
    loanRepaymentLogs?: LoanRepaymentLog[];
    transactionLogs?: TransactionLog[];
    userRoles?: UserRole[];
}