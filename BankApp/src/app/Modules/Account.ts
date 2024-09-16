import { AccountStatusType } from "./AccountStatusType";
import { Customer } from "./Customer";
import { SavingsInterestRate } from "./SavingsInterestRate";
import { TransactionLog } from "./TransactionLog";

export interface Account {
    accountId?: number | null;
    currentBalance?: number | null;
    interestSavingsRateId?: number | null;
    accountStatusTypeId?: number | null;
    customerId?: string | null;
    accountStatusType?: AccountStatusType | null;
    customer?: Customer | null;
    interestSavingsRate?: SavingsInterestRate | null;
    transactionLogs?: TransactionLog[];
}