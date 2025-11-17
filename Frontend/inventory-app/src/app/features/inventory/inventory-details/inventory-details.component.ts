import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { InventoryService } from '../../../core/services/inventory.service';
import { InventoryDetailsDto, InventoryFieldDto, ItemDetailsDto } from '../../../models/inventory.model';

@Component({
  selector: 'app-inventory-details',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './inventory-details.component.html',
  styleUrls: ['./inventory-details.component.css']
})
export class InventoryDetailsComponent implements OnInit {
  inventory: InventoryDetailsDto | null = null;
  loading = true;
  error: string | null = null;

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
}