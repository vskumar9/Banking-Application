import { ComplaintFeedback } from "./ComplaintFeedback";
import { ComplaintResolution } from "./ComplaintResolution";
import { ComplaintStatusHistory } from "./ComplaintStatusHistory";
import { ComplaintType } from "./ComplaintType";
import { Customer } from "./Customer";
import { Employee } from "./Employee";

export interface Complaint {
    complaintId?: number | null;
    customerId?: string | null;
    complaintTypeId?: number | null;
    complaintDate?: Date | null;
    complaintDescription?: string | null;
    files?: string | null;
    complaintStatus?: string | null;
    employeeId?: string | null;
    resolutionDate?: Date | null;
    resolutionComments?: string | null;
    complaintFeedbacks?: ComplaintFeedback[];
    complaintResolutions?: ComplaintResolution[];
    complaintStatusHistories?: ComplaintStatusHistory[];
    complaintType?: ComplaintType | null;
    customer?: Customer | null;
    employee?: Employee | null;
}