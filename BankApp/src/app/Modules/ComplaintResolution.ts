import { Complaint } from "./Complaint";
import { Employee } from "./Employee";

export interface ComplaintResolution {
    resolutionId?: number | null;
    complaintId?: number | null;
    resolutionDate?: Date | null;
    resolutionMethod?: string | null;
    resolutionDescription?: string | null;
    employeeId?: string | null;
    complaint?: Complaint | null;
    employee?: Employee | null;
}