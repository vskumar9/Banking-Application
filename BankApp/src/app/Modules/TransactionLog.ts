import { Account } from "./Account";
import { Customer } from "./Customer";
import { Employee } from "./Employee";
import { TransactionType } from "./TransactionType";

export interface TransactionLog {
    transactionId?: number | null;
    transactionDate?: Date | null;
    transactionTypeId?: number | null;
    transactionAmount?: number | null;
    newBalance?: number | null;
    accountId?: number | null;
    employeeId?: string | null;
    customerId?: string | null;
    account?: Account | null;
    customer?: Customer | null;
    employee?: Employee | null;
    transactionType?: TransactionType | null;
}