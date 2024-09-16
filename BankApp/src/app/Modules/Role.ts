import { RolePermission } from "./RolePermission";
import { UserRole } from "./UserRole";

export interface Role {
    roleId?: number | null;
    roleName: string;
    description?: string | null;
    rolePermissions?: RolePermission[];
    userRoles?: UserRole[];
}