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