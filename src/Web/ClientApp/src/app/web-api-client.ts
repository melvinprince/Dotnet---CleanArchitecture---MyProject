import { Injectable, Inject, InjectionToken } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

/**
 * Base URL injection token. In AppModule providers, bind to environment.apiBaseUrl
 */
export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');

/**
 * Shared base service wrapping HttpClient and API base URL
 */
@Injectable({ providedIn: 'root' })
export class ApiService {
  constructor(
    protected http: HttpClient,
    @Inject(API_BASE_URL) protected baseUrl: string
  ) {}
}

// --- Borrower Models & Service ---
export interface Borrower {
  id: string;
  fullName: string;
  email: string;
  phoneNumber?: string;
  dateRegistered: string; // ISO 8601
}

@Injectable({ providedIn: 'root' })
export class BorrowerService extends ApiService {
  getAll(): Observable<Borrower[]> {
    return this.http.get<Borrower[]>(`${this.baseUrl}/Borrowers`);
  }

  getById(id: string): Observable<Borrower> {
    return this.http.get<Borrower>(`${this.baseUrl}/Borrowers/${id}`);
  }

  create(payload: {
    fullName: string;
    email: string;
    phoneNumber?: string;
  }): Observable<string> {
    return this.http.post<string>(`${this.baseUrl}/Borrowers`, payload);
  }

  update(id: string, payload: Borrower): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/Borrowers/${id}`, payload);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/Borrowers/${id}`);
  }
}

// --- Author Models & Service ---
export interface Author {
  id: string;
  firstName: string;
  lastName: string;
  dateOfBirth?: string; // ISO 8601
  biography?: string;
}

@Injectable({ providedIn: 'root' })
export class AuthorService extends ApiService {
  getAll(): Observable<Author[]> {
    return this.http.get<Author[]>(`${this.baseUrl}/Authors`);
  }

  getById(id: string): Observable<Author> {
    return this.http.get<Author>(`${this.baseUrl}/Authors/${id}`);
  }

  create(payload: {
    firstName: string;
    lastName: string;
    dateOfBirth?: string;
    biography?: string;
  }): Observable<string> {
    return this.http.post<string>(`${this.baseUrl}/Authors`, payload);
  }

  update(id: string, payload: Author): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/Authors/${id}`, payload);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/Authors/${id}`);
  }
}

// --- Book Models & Service ---
export type BookStatus = 'Available' | 'Borrowed';

export interface Book {
  id: string;
  title: string;
  isbn: string;
  publishedDate: string; // ISO 8601
  authorId: string;
  status: BookStatus;
  borrowerId?: string;
}

@Injectable({ providedIn: 'root' })
export class BookService extends ApiService {
  getAll(): Observable<Book[]> {
    return this.http.get<Book[]>(`${this.baseUrl}/Books`);
  }

  getById(id: string): Observable<Book> {
    return this.http.get<Book>(`${this.baseUrl}/Books/${id}`);
  }

  create(payload: {
    authorId: string;
    title: string;
    isbn: string;
    publishedDate: string;
  }): Observable<string> {
    return this.http.post<string>(`${this.baseUrl}/Books`, payload);
  }

  update(
    id: string,
    payload: Partial<Omit<Book, 'status' | 'borrowerId'>>
  ): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/Books/${id}`, payload);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/Books/${id}`);
  }

  borrow(bookId: string, borrowerId: string): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/Books/borrow`, {
      bookId,
      borrowerId,
    });
  }
}
