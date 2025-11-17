import { Component, OnInit } from '@angular/core';
import {
  InventoryService,
  InventoryListResponse,
} from '../../../core/services/inventory.service';
import { InventorySummary } from '../../../models/inventory.model';
import { debounceTime, distinctUntilChanged, Subject } from 'rxjs';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterLink } from "@angular/router";

@Component({
  selector: 'app-inventories-list',
  templateUrl: './inventories-list.component.html',
  imports: [CommonModule, ReactiveFormsModule, FormsModule, RouterLink],
  styleUrls: ['./inventories-list.component.css'],
})
export class InventoriesListComponent implements OnInit {
  inventories: InventorySummary[] = [];
  total = 0;
  page = 1;
  pageSize = 20;
  loading = false;
  sortBy = 'createdAt';
  sortDir: 'asc' | 'desc' = 'desc';
  search$ = new Subject<string>();
  searchTerm = '';

  constructor(private svc: InventoryService) {}

  ngOnInit(): void {
    // debounce search input
    this.search$
      .pipe(debounceTime(300), distinctUntilChanged())
      .subscribe((term) => {
        this.page = 1;
        this.searchTerm = term;
        this.load();
      });
    this.load();
  }

  load(): void {
    this.loading = true;
    this.svc
      .list(
        this.page,
        this.pageSize,
        this.searchTerm,
        this.sortBy,
        this.sortDir
      )
      .subscribe({
        next: (res: InventoryListResponse) => {
          this.inventories = res.items;
          this.total = res.total;
          this.loading = false;
        },
        error: () => (this.loading = false),
      });
  }

  onPageChange(newPage: number) {
    this.page = newPage;
    this.load();
  }

  setSort(column: string) {
    if (this.sortBy === column)
      this.sortDir = this.sortDir === 'asc' ? 'desc' : 'asc';
    else {
      this.sortBy = column;
      this.sortDir = 'desc';
    }
    this.load();
  }

  // helper to show truncated description
  short(desc?: string) {
    return desc ? (desc.length > 120 ? desc.slice(0, 117) + '...' : desc) : '';
  }

  getCeil(value: number): number {
    return Math.ceil(value);
  }
}
