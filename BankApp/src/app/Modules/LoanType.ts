import { LoanApplication } from "./LoanApplication";

export interface LoanType {
    loanTypeId?: number | null;
    loanTypeName?: string | null;
    interestRate?: number | null;
    description?: string | null;
    loanApplications?: LoanApplication[];
}