import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';

export interface LoginResponse {
  token: string;
  id: number;
  email: string;
  fullName: string;
  language: string;
  profilePictureUrl: string;
}

export interface User {
  id: number;
  email: string;
  fullName: string;
  language: string;
  profilePictureUrl: string;
}
export interface RegisterRequest {
  email: string;
  password: string;
  name: string;
  profilePictureUrl: string;
}
export interface RegisterResponse {
  id: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'http://localhost:5186/api';
  private currentUserSubject = new BehaviorSubject<User | null>(null);
  public currentUser$ = this.currentUserSubject.asObservable();

  constructor(private http: HttpClient) {
    // Check if user data exists in localStorage
    const savedUser = localStorage.getItem('currentUser');
    if (savedUser) {
      this.currentUserSubject.next(JSON.parse(savedUser));
    }
  }

  login(email: string, password: string): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.apiUrl}/accounts/login`, { email, password })
      .pipe(
        tap(response => {
          // Store token and user data
          localStorage.setItem('token', response.token);
          const user: User = {
          id: response.id,
          email: response.email,
          fullName: response.fullName,
          language: response.language,
          profilePictureUrl: response.profilePictureUrl
        };

        localStorage.setItem('currentUser', JSON.stringify(user));
        this.currentUserSubject.next(user);
        })
      );
  }
  register(request: RegisterRequest): Observable<any> {
    return this.http.post<RegisterResponse>(`${this.apiUrl}/accounts/register`, request);
  }
  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  getCurrentUser(): User | null {
    return this.currentUserSubject.value;
  }
}