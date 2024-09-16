import { Permission } from "./Permission";
import { Role } from "./Role";

export interface RolePermission {
    rolePermissionId?: number | null;
    roleId?: number | null;
    permissionId?: number | null;
    permission?: Permission | null;
    role?: Role | null;
}