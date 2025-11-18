import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { FieldType, InventoryDetailsDto } from '../../../models/inventory.model';
import { CreateFieldValueRequest, CreateItemRequest } from '../../../models/item.model';
import { InventoryService } from '../../../core/services/inventory.service';

@Component({
  selector: 'app-add-item',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './add-item.component.html',
  styleUrls: ['./add-item.component.css']
})
export class AddItemComponent implements OnInit, OnDestroy {
  // Expose FieldType to the template
  FieldType = FieldType;
  
  private destroy$ = new Subject<void>();
  
  inventoryId: string = '';
  inventory: InventoryDetailsDto | null = null;
  loading = true;
  error: string | null = null;
  submitting = false;
  
  itemData: CreateItemRequest = {
    customId: '',
    fieldValues: []
  };

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private inventoryService: InventoryService
  ) {}

  ngOnInit(): void {
    this.inventoryId = this.route.snapshot.paramMap.get('id') || '';
    
    if (!this.inventoryId) {
      this.error = 'Inventory ID not provided';
      this.loading = false;
      return;
    }

    this.loadInventoryDetails();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  private loadInventoryDetails(): void {
    this.inventoryService.getInventoryDetails(this.inventoryId)
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (data) => {
          this.inventory = data;
          this.initializeFieldValues();
          this.loading = false;
        },
        error: (error) => {
          this.error = 'Failed to load inventory details';
          this.loading = false;
          console.error('Error loading inventory:', error);
        }
      });
  }

  private initializeFieldValues(): void {
    if (!this.inventory) return;

    this.itemData.fieldValues = this.inventory.fields.map(field => ({
      fieldDefinitionId: field.id,
      stringValue: undefined,
      numberValue: undefined,
      boolValue: undefined
    }));
  }

  getFieldValue(fieldId: string): CreateFieldValueRequest {
    return this.itemData.fieldValues.find(fv => fv.fieldDefinitionId === fieldId) || {
      fieldDefinitionId: fieldId,
      stringValue: undefined,
      numberValue: undefined,
      boolValue: undefined
    };
  }

  updateFieldValue(fieldId: string, value: any, valueType: 'string' | 'number' | 'boolean'): void {
    let fieldValue = this.itemData.fieldValues.find(fv => fv.fieldDefinitionId === fieldId);
    
    if (!fieldValue) {
      fieldValue = {
        fieldDefinitionId: fieldId,
        stringValue: undefined,
        numberValue: undefined,
        boolValue: undefined
      };
      this.itemData.fieldValues.push(fieldValue);
    }

    // Reset all values first
    fieldValue.stringValue = undefined;
    fieldValue.numberValue = undefined;
    fieldValue.boolValue = undefined;

    // Set the appropriate value based on type
    switch (valueType) {
      case 'string':
        fieldValue.stringValue = value || undefined;
        break;
      case 'number':
        fieldValue.numberValue = value !== '' ? Number(value) : undefined;
        break;
      case 'boolean':
        fieldValue.boolValue = value !== null ? Boolean(value) : undefined;
        break;
    }
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

  onFileSelected(fieldId: string, event: any): void {
    const file = event.target.files[0];
    if (file) {
      // For DocumentOrImage fields, we'll store the file name as stringValue
      // In a real application, you would upload the file and store the URL/path
      this.updateFieldValue(fieldId, file.name, 'string');
    }
  }

  onSubmit(): void {
    if (!this.validateForm()) {
      return;
    }

    this.submitting = true;
    this.error = null;

    this.inventoryService.addItemToInventory(this.inventoryId, this.itemData)
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (response) => {
          this.submitting = false;
          this.router.navigate(['/inventory', this.inventoryId]);
        },
        error: (error) => {
          this.error = 'Failed to add item. Please try again.';
          this.submitting = false;
          console.error('Error adding item:', error);
        }
      });
  }

  onCancel(): void {
    this.router.navigate(['/inventory', this.inventoryId]);
  }

  private validateForm(): boolean {
    if (!this.itemData.customId.trim()) {
      this.error = 'Custom ID is required';
      return false;
    }

    // Validate field values based on their types
    if (this.inventory) {
      for (const field of this.inventory.fields) {
        const fieldValue = this.getFieldValue(field.id);
        
        switch (field.type) {
          case FieldType.Number:
            if (fieldValue.numberValue === undefined || fieldValue.numberValue === null || isNaN(fieldValue.numberValue)) {
              this.error = `Field "${field.title}" is required and must be a valid number`;
              return false;
            }
            break;
          case FieldType.Boolean:
            if (fieldValue.boolValue === undefined || fieldValue.boolValue === null) {
              this.error = `Field "${field.title}" is required`;
              return false;
            }
            break;
          case FieldType.SingleLineText:
          case FieldType.MultiLineText:
          case FieldType.DocumentOrImage:
            if (!fieldValue.stringValue?.trim()) {
              this.error = `Field "${field.title}" is required`;
              return false;
            }
            break;
        }
      }
    }

    return true;
  }
}