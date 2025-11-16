import { Component, OnInit, inject, signal } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';
import { Subject } from 'rxjs';

import { CategoryService } from '../../../core/services/category.service';
import { TagService } from '../../../core/services/tag.service';
import { InventoryService } from '../../../core/services/inventory.service';
import { Category, InventoryCreateRequest } from '../../../models/inventory.model';

@Component({
  selector: 'app-inventory-create',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './inventory-create.component.html',
  styleUrls: ['./inventory-create.component.css']
})
export class InventoryCreateComponent implements OnInit {
  private fb = inject(FormBuilder);
  private categoryService = inject(CategoryService);
  private tagService = inject(TagService);
  private inventoryService = inject(InventoryService);
  private router = inject(Router);

  inventoryForm!: FormGroup;
  categories = signal<Category[]>([]);
  suggestedTags = signal<string[]>([]);
  selectedTags = signal<string[]>([]);
  tagSearchInput = new Subject<string>();
  isLoading = signal(false);
  isSubmitting = signal(false);

  ngOnInit() {
    this.initializeForm();
    this.loadCategories();
    this.setupTagAutocomplete();
  }

  private initializeForm() {
    this.inventoryForm = this.fb.group({
      categoryId: ['', Validators.required],
      name: ['', [Validators.required, Validators.minLength(2)]],
      description: ['', [Validators.required, Validators.minLength(10)]],
      imageUrl: [''],
      tagInput: ['']
    });
  }

  private loadCategories() {
    this.isLoading.set(true);
    this.categoryService.getAllCategories().subscribe({
      next: (categories) => {
        this.categories.set(categories);
        this.isLoading.set(false);
      },
      error: (error) => {
        console.error('Error loading categories:', error);
        this.isLoading.set(false);
      }
    });
  }

  private setupTagAutocomplete() {
    this.tagSearchInput.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      switchMap(query => this.tagService.getAutocompleteTags(query))
    ).subscribe({
      next: (tags) => this.suggestedTags.set(tags),
      error: (error) => console.error('Error loading tags:', error)
    });
  }

  onTagInput(event: Event) {
    const input = (event.target as HTMLInputElement).value;
    if (input.length > 1) {
      this.tagSearchInput.next(input);
    } else {
      this.suggestedTags.set([]);
    }
  }

  addTag(tag: string) {
    const currentTags = this.selectedTags();
    if (!currentTags.includes(tag)) {
      this.selectedTags.set([...currentTags, tag]);
    }
    this.inventoryForm.get('tagInput')?.setValue('');
    this.suggestedTags.set([]);
  }

  removeTag(tag: string) {
    this.selectedTags.set(this.selectedTags().filter(t => t !== tag));
  }

  onSubmit() {
    if (this.inventoryForm.valid && this.selectedTags().length > 0) {
      this.isSubmitting.set(true);

      const formData: InventoryCreateRequest = {
        categoryId: this.inventoryForm.value.categoryId,
        name: this.inventoryForm.value.name,
        description: this.inventoryForm.value.description,
        imageUrl: this.inventoryForm.value.imageUrl || undefined,
        tags: this.selectedTags()
      };

      this.inventoryService.createInventory(formData).subscribe({
        next: (response) => {
          this.isSubmitting.set(false);
          this.router.navigate(['/inventory']);
        },
        error: (error) => {
          console.error('Error creating inventory:', error);
          this.isSubmitting.set(false);
        }
      });
    } else {
      this.markFormGroupTouched();
    }
  }

  private markFormGroupTouched() {
    Object.keys(this.inventoryForm.controls).forEach(key => {
      const control = this.inventoryForm.get(key);
      control?.markAsTouched();
    });
  }
}