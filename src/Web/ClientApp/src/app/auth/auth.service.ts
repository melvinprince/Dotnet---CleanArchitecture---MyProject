import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { BehaviorSubject, catchError, of, tap } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private authState = new BehaviorSubject<boolean>(false);
  isAuthenticated$ = this.authState.asObservable();

  constructor(private http: HttpClient, private router: Router) {}

  check() {
    return this.http.get('/api/Session', { observe: 'response' }).pipe(
      tap(() => this.authState.next(true)),
      catchError(() => {
        this.authState.next(false);
        return of(null);
      })
    );
  }

  login(body: { email: string; password: string }) {
    return this.http
      .post('/api/Login', body)
      .pipe(tap(() => this.authState.next(true)));
  }

  logout() {
    return this.http.post('/api/Session/logout', {}).pipe(
      tap(() => {
        this.authState.next(false);
        this.router.navigate(['/login']);
      })
    );
  }
}
