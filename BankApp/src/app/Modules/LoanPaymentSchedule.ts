import { LoanApplication } from "./LoanApplication";

export interface LoanPaymentSchedule {
    paymentId?: number | null;
    loanId?: number | null;
    paymentDate?: Date | null;
    paymentAmount?: number | null;
    balanceAfterPayment?: number | null;
    paymentStatus?: string | null;
    loan?: LoanApplication | null;
}