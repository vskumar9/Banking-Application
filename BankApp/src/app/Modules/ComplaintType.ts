import { Complaint } from "./Complaint";

export interface ComplaintType {
    complaintTypeId?: number | null;
    complaintTypeName?: string | null;
    complaintTypeDescription?: string | null;
    complaints?: Complaint[];
}