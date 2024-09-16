import { Complaint } from "./Complaint";
import { Customer } from "./Customer";

export interface ComplaintFeedback {
    feedbackId?: number | null;
    complaintId?: number | null;
    customerId?: string | null;
    feedbackDate?: Date | null;
    feedbackRating?: number | null;
    feedbackComments?: string | null;
    complaint?: Complaint | null;
    customer?: Customer | null;
}