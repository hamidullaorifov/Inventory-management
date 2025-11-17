import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { map, Observable, of, Subject } from 'rxjs';
import { InventoryCreateRequest, InventoryDetailsDto, InventorySummary, ItemDetailsDto, UserAccessDto } from '../../models/inventory.model';
import { environment } from '../../../environments/environments';

export interface InventoryListResponse {
  total: number;
  items: InventorySummary[];
}

@Injectable({ providedIn: 'root' })
export class InventoryService {
  private apiUrl = `${environment.apiUrl}/inventories`;
  constructor(private http: HttpClient) {}
  private discussionUpdates = new Subject<any>();




  getInventoryDetails(inventoryId: string): Observable<InventoryDetailsDto> {
    return this.http.get<InventoryDetailsDto>(`${this.apiUrl}/${inventoryId}`);
  } 


// Save settings with optimistic locking token
saveInventorySettings(id: string, payload: Partial<InventoryDetailsDto>, version?: string): Observable<{result: InventoryDetailsDto, version?: string}> {
const headers = version ? new HttpHeaders({ 'If-Match': version }) : undefined;
return this.http.put<InventoryDetailsDto>(`${this.apiUrl}/${id}/settings`, payload, { headers }).pipe(
map(res => ({ result: res, version: (res as any).version }))
);
}


getItems(id: string): Observable<ItemDetailsDto[]> {
return this.http.get<ItemDetailsDto[]>(`${this.apiUrl}/${id}/items`);
}


addItem(inventoryId: string, item: Partial<ItemDetailsDto>): Observable<ItemDetailsDto> {
return this.http.post<ItemDetailsDto>(`${this.apiUrl}/${inventoryId}/items`, item);
}


updateItem(inventoryId: string, itemId: string, item: Partial<ItemDetailsDto>): Observable<ItemDetailsDto> {
return this.http.put<ItemDetailsDto>(`${this.apiUrl}/${inventoryId}/items/${itemId}`, item);
}


// Tags autocomplete
searchTags(prefix: string): Observable<string[]> {
if (!prefix) return of([]);
return this.http.get<string[]>(`${this.apiUrl}/tags/autocomplete?q=${encodeURIComponent(prefix)}`);
}


// User autocomplete for access management
searchUsers(q: string): Observable<UserAccessDto[]> {
if (!q) return of([]);
return this.http.get<UserAccessDto[]>(`/api/users?search=${encodeURIComponent(q)}`);
}


// Discussion
postDiscussionMessage(inventoryId: string, text: string): Observable<any> {
return this.http.post(`/api/inventories/${inventoryId}/discussion`, { text });
}


// Subscribe to discussion updates (stub: in practice open WS and forward messages)
subscribeDiscussion(inventoryId: string): Observable<any> {
// In a real app: return Observable from WebSocket or EventSource
return this.discussionUpdates.asObservable();
}


// For demo/testing: push discussion updates
_pushDiscussionUpdate(msg: any) {
this.discussionUpdates.next(msg);
}
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
