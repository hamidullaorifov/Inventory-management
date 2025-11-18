import { Component, EventEmitter, Output, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CreateFieldRequest, FieldType } from '../../../models/inventory.model';

@Component({
  selector: 'app-add-field-modal',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './add-field-modal.component.html',
  styleUrls: ['./add-field-modal.component.css']
})
export class AddFieldModalComponent {
  @Output() fieldAdded = new EventEmitter<CreateFieldRequest>();
  @Output() cancelled = new EventEmitter<void>();
  @Input() isVisible = false;
  fieldData: CreateFieldRequest = {
    type: FieldType.SingleLineText,
    title: '',
    description: '',
    showInTable: true
  };

  fieldTypes = [
    { value: FieldType.SingleLineText, label: 'Single Line Text' },
    { value: FieldType.MultiLineText, label: 'Multi Line Text' },
    { value: FieldType.Number, label: 'Number' },
    { value: FieldType.DocumentOrImage, label: 'Document or Image' },
    { value: FieldType.Boolean, label: 'Boolean' }
  ];

  open(): void {
    this.isVisible = true;
    this.resetForm();
  }

  close(): void {
    this.isVisible = false;
  }

  onSubmit(): void {
    console.log('Submitting field data:', this.fieldData);
    
    if (this.fieldData.title.trim()) {
      this.fieldAdded.emit({ ...this.fieldData });
      this.close();
    }
  }

  onCancel(): void {
    this.cancelled.emit();
    this.close();
  }

  private resetForm(): void {
    this.fieldData = {
      type: FieldType.SingleLineText,
      title: '',
      description: '',
      showInTable: true
    };
  }
}