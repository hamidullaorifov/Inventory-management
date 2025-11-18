// Add to existing interfaces
export interface CreateItemRequest {
  customId: string;
  fieldValues: CreateFieldValueRequest[];
}

export interface CreateFieldValueRequest {
  fieldDefinitionId: string;
  stringValue?: string;
  numberValue?: number;
  boolValue?: boolean;
}

// Update existing FieldValueDto to match the request structure if needed
export interface FieldValueDto {
  fieldDefinitionId: string;
  stringValue?: string;
  numberValue?: number;
  boolValue?: boolean;
}