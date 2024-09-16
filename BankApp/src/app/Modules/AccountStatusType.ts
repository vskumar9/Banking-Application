import { Account } from "./Account";

export interface AccountStatusType {
    accountStatusTypeId?: number | null;
    accountStatusDescription?: string | null;
    accounts?: Account[];
}