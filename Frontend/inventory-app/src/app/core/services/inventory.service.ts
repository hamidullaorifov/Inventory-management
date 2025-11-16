import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { InventoryCreateRequest, InventorySummary } from '../../models/inventory.model';
import { environment } from '../../../environments/environments';

export interface InventoryListResponse {
  total: number;
  items: InventorySummary[];
}

@Injectable({ providedIn: 'root' })
export class InventoryService {
  private apiUrl = `${environment.apiUrl}/inventories`;
  constructor(private http: HttpClient) {}

  // Query inventories with server-side paging, sorting and search
  list(
    page = 1,
    pageSize = 20,
    q = '',
    sortBy = 'createdAt',
    sortDir: 'asc' | 'desc' = 'desc'
  ): Observable<InventoryListResponse> {
    let params = new HttpParams()
      .set('limit', String(pageSize))
      .set('offset', String((page - 1) * pageSize))
      .set('sortBy', sortBy)
      .set('sortDir', sortDir);
    if (q) params = params.set('search', q);
    return this.http.get<InventoryListResponse>(this.apiUrl, { params });
  }

  createInventory(inventory: InventoryCreateRequest): Observable<string> {
    return this.http.post<string>(`${this.apiUrl}`, inventory);
  }
}
