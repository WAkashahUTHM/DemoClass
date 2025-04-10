export interface CreateContractDTO {
    title: string;
    description: string;
    startDate: string;  // ISO String
    endDate: string;    // ISO String
    supplierId: string;
}

export interface SupplierDTO {
    supplierId: string;
    name: string;
    category: string;
    contactInfo: string;
}

export interface ContractDTO {
    contractId: string;
    title: string;
    description: string;
    startDate: string;  // ISO String
    endDate: string;    // ISO String
    supplier: SupplierDTO;
}