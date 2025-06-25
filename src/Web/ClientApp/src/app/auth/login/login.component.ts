import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
})
export class LoginComponent {
  form = this.fb.group({
    email: ['', Validators.required],
    password: ['', Validators.required],
  });

  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    private router: Router
  ) {}

  login() {
    const body = this.form.value;
    this.http.post('/api/Login', body).subscribe({
      next: () =>
        this.router.navigateByUrl(
          new URLSearchParams(location.search).get('ReturnUrl') ?? '/'
        ),
      error: () => alert('Invalid credentials'),
    });
  }
}
