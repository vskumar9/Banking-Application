import { TransactionLog } from "./TransactionLog";

export interface TransactionType {
    transactionTypeId?: number | null;
    transactionTypeName?: string | null;
    transactionTypeDescription?: string | null;
    transactionFeeAmount?: number | null;
    transactionLogs?: TransactionLog[];
}