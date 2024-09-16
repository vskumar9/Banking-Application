import { Customer } from "./Customer";
import { Employee } from "./Employee";
import { LoanPaymentSchedule } from "./LoanPaymentSchedule";
import { LoanRepaymentLog } from "./LoanRepaymentLog";
import { LoanType } from "./LoanType";

export interface LoanApplication {
    loanId?: number | null;
    customerId?: string | null;
    loanTypeId?: number | null;
    loanAmount?: number | null;
    applicationDate?: Date | null;
    files?: string | null;
    loanStatus?: string | null;
    employeeId?: string | null;
    approvalDate?: Date | null;
    comments?: string | null;
    customer?: Customer | null;
    employee?: Employee | null;
    loanPaymentSchedules?: LoanPaymentSchedule[];
    loanRepaymentLogs?: LoanRepaymentLog[];
    loanType?: LoanType | null;
}