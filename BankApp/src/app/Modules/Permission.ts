import { RolePermission } from "./RolePermission";

export interface Permission {
    permissionId?: number | null;
    permissionName: string;
    description?: string | null;
    rolePermissions?: RolePermission[];
}