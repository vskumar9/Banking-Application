import { Account } from "./Account";

export interface SavingsInterestRate {
    interestSavingsRateId?: number | null;
    interestRateValue?: number | null;
    interestRateDescription?: string | null;
    accounts?: Account[];
}