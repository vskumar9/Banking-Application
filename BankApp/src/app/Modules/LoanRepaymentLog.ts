import { Employee } from "./Employee";
import { LoanApplication } from "./LoanApplication";

export interface LoanRepaymentLog {
    repaymentId?: number | null;
    loanId?: number | null;
    repaymentDate?: Date | null;
    repaymentAmount?: number | null;
    employeeId?: string | null;
    repaymentMethod?: string | null;
    employee?: Employee | null;
    loan?: LoanApplication | null;
}