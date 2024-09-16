import { Employee } from "./Employee";

export interface Configuration {
    configurationId?: number | null;
    configKey: string;
    configValue: string;
    description?: string | null;
    lastUpdated?: Date | null;
    updatedBy: string;
    updatedByNavigation?: Employee | null;
}