import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { InventoryService } from '../../../core/services/inventory.service';
import { AddFieldModalComponent } from '../add-field-modal/add-field-modal.component';
import { InventoryDetailsDto, CreateFieldRequest, FieldType, ItemDetailsDto, InventoryFieldDto } from '../../../models/inventory.model';

@Component({
  selector: 'app-inventory-details',
  standalone: true,
  imports: [CommonModule, AddFieldModalComponent, RouterLink],
  templateUrl: './inventory-details.component.html',
  styleUrls: ['./inventory-details.component.css']
})
export class InventoryDetailsComponent implements OnInit {
  inventory: InventoryDetailsDto | null = null;
  loading = true;
  error: string | null = null;
  addingField = false;

  constructor(
    private route: ActivatedRoute,
    private inventoryService: InventoryService
  ) {}

  ngOnInit(): void {
    const inventoryId = this.route.snapshot.paramMap.get('id');
    if (inventoryId) {
      this.loadInventoryDetails(inventoryId);
    } else {
      this.error = 'Inventory ID not provided';
      this.loading = false;
    }
  }

  private loadInventoryDetails(inventoryId: string): void {
    this.inventoryService.getInventoryDetails(inventoryId).subscribe({
      next: (data) => {
        this.inventory = data;
        this.loading = false;
      },
      error: (error) => {
        this.error = 'Failed to load inventory details';
        this.loading = false;
        console.error('Error loading inventory:', error);
      }
    });
  }

  getFieldValue(item: ItemDetailsDto, fieldId: string): any {
    const fieldValue = item.fieldValues.find(fv => fv.fieldDefinitionId === fieldId);
    if (!fieldValue) return '-';

    if (fieldValue.stringValue !== undefined) return fieldValue.stringValue;
    if (fieldValue.numberValue !== undefined) return fieldValue.numberValue;
    if (fieldValue.boolValue !== undefined) return fieldValue.boolValue ? 'Yes' : 'No';
    
    return '-';
  }

  getTableFields(): InventoryFieldDto[] {
    return this.inventory?.fields.filter(field => field.showInTable) || [];
  }

  // Field management methods
  openAddFieldModal(): void {
    this.addingField = true;
  }

  onFieldAdded(fieldData: CreateFieldRequest): void {
    if (!this.inventory) return;

    const inventoryId = this.inventory.id;
    this.inventoryService.addFieldToInventory(inventoryId, fieldData).subscribe({
      next: (newField) => {
        // Add the new field to the local inventory data
        this.inventory!.fields.push(newField);
        this.addingField = false;
        
        // Show success message (you could use a toast service here)
        console.log('Field added successfully');
      },
      error: (error) => {
        this.error = 'Failed to add field';
        console.error('Error adding field:', error);
        this.addingField = false;
      }
    });
  }
  getFieldTypeLabel(type: FieldType): string {
    switch (type) {
      case FieldType.SingleLineText: return 'Single Line Text';
      case FieldType.MultiLineText: return 'Multi Line Text';
      case FieldType.Number: return 'Number';
      case FieldType.DocumentOrImage: return 'Document/Image';
      case FieldType.Boolean: return 'Boolean';
      default: return 'Unknown';
    }
  }
  onAddFieldCancelled(): void {
    this.addingField = false;
  }
}