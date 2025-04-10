export interface ComplianceStandardDTO {
    ComplianceStandardId: string;
    StandardName: string;
    Description: string;
    LastChecked: string; // DateTime as string (ISO format)
    IsCompliant: boolean;
}

export interface ProductionProcessComplianceDTO {
    ProductionProcessId: string;
    ProcessName: string;
    LinkedComplianceStandards: ComplianceStandardDTO[];
}
