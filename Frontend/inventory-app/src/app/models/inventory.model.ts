export interface InventorySummary {
  id: string;
  name: string;
  description?: string;
  ownerId?: string;
  ownerName?: string;
  tags?: string[];
  imageUrl?: string | null;
  createdAt?: string;
}

export interface InventoryCreateRequest {
  categoryId: string;
  name: string;
  description: string;
  imageUrl?: string;
  tags: string[];
}

export interface Category {
  id: string;
  name: string;
}

export interface InventoryDetailsDto {
  id: string;
  name: string;
  description?: string;
  category: string;
  imageUrl?: string;
  isPublic: boolean;
  ownerId: string;
  ownerName: string;
  createdAt: Date;
  tags: string[];
  fields: InventoryFieldDto[];
  items: ItemDetailsDto[];
  writeAccessUsers: UserAccessDto[];
}

export interface InventoryFieldDto {
  id: string;
  title: string;
  description?: string;
  type: string;
  showInTable: boolean;
}

export interface ItemDetailsDto {
  id: string;
  customId: string;
  fieldValues: FieldValueDto[];
}

export interface UserAccessDto {
  userId: string;
  fullName: string;
  email: string;
}

export interface FieldValueDto {
  fieldDefinitionId: string;
  stringValue?: string;
  numberValue?: number;
  boolValue?: boolean;
}