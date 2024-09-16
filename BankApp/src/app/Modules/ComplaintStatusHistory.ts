import { Complaint } from "./Complaint";

export interface ComplaintStatusHistory {
    statusHistoryId?: number | null;
    complaintId?: number | null;
    statusDate?: Date | null;
    complaintStatus?: string | null;
    statusComments?: string | null;
    complaint?: Complaint | null;
}