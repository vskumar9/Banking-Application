import { Employee } from "./Employee";
import { Role } from "./Role";

export interface UserRole {
    userRoleId?: number | null;
    employeeId: string;
    roleId?: number | null;
    employee?: Employee | null;
    role?: Role | null;
}