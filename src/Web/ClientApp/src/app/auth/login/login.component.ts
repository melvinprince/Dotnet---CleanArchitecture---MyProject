import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
})
export class LoginComponent {
  form = this.fb.group({
    email: ['', Validators.required],
    password: ['', Validators.required],
  });

  constructor(private fb: FormBuilder, private auth: AuthService) {}

  login() {
    const body = this.form.value as { email: string; password: string };
    this.auth.login(body).subscribe({
      next: () =>
        location.assign(
          new URLSearchParams(location.search).get('ReturnUrl') ?? '/'
        ),
      error: () => alert('Invalid credentials'),
    });
  }
}
