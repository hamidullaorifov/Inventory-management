// src/app/components/inventory-create/inventory-create.component.ts
import { Component, OnInit, inject, signal } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';
import { Subject } from 'rxjs';

import { CategoryService } from '../../../core/services/category.service';
import { TagService } from '../../../core/services/tag.service';
import { InventoryService } from '../../../core/services/inventory.service';
import { Category, InventoryCreateRequest } from '../../../models/inventory.model';

@Component({
  selector: 'app-inventory-create',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
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
  showSuggestions = signal(false);

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
      next: (tags) => {
        this.suggestedTags.set(tags);
        this.showSuggestions.set(tags.length > 0);
      },
      error: (error) => console.error('Error loading tags:', error)
    });
  }

  onTagInput(event: Event) {
    const input = (event.target as HTMLInputElement).value;
    if (input.length > 1) {
      this.tagSearchInput.next(input);
    } else {
      this.suggestedTags.set([]);
      this.showSuggestions.set(false);
    }
  }

  onTagKeyDown(event: KeyboardEvent) {
    if (event.key === 'Enter') {
      event.preventDefault();
      this.addCustomTag();
    } else if (event.key === 'Escape') {
      this.hideSuggestions();
    }
  }

  addCustomTag() {
    const tagInput = this.inventoryForm.get('tagInput')?.value?.trim();
    if (tagInput && tagInput.length > 0) {
      this.addTag(tagInput);
    }
  }

  addTag(tag: string) {
    const trimmedTag = tag.trim();
    if (!trimmedTag) return;

    const currentTags = this.selectedTags();
    if (!currentTags.includes(trimmedTag)) {
      this.selectedTags.set([...currentTags, trimmedTag]);
    }
    this.clearTagInput();
    this.hideSuggestions();
  }

  removeTag(tag: string) {
    this.selectedTags.set(this.selectedTags().filter(t => t !== tag));
  }

  selectSuggestion(tag: string) {
    this.addTag(tag);
  }

  clearTagInput() {
    this.inventoryForm.get('tagInput')?.setValue('');
    this.suggestedTags.set([]);
  }

  hideSuggestions() {
    this.showSuggestions.set(false);
  }

  onTagInputBlur() {
    // Small delay to allow for suggestion click
    setTimeout(() => {
      this.hideSuggestions();
    }, 200);
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
      
      // Show validation message if no tags
      if (this.selectedTags().length === 0) {
        alert('Please add at least one tag. You can type a tag and press Enter to add it.');
      }
    }
  }

  private markFormGroupTouched() {
    Object.keys(this.inventoryForm.controls).forEach(key => {
      const control = this.inventoryForm.get(key);
      control?.markAsTouched();
    });
  }
}