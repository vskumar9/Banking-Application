import { Employee } from "./Employee";

export interface AuditLog {
    auditLogId?: number | null;
    action?: string | null;
    employeeId?: string | null;
    actionDate?: Date | null;
    ipAddress?: string | null;
    details?: string | null;
    employee?: Employee | null;
}