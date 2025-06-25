// src/app/author-add/author-add.component.ts
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AuthorService } from 'src/app/web-api-client';
import { Router } from '@angular/router';

@Component({
  selector: 'app-author-add',
  templateUrl: './add-author.component.html',
  styleUrls: ['./add-author.component.scss'],
})
export class AuthorAddComponent implements OnInit {
  loading = false;
  error: string | null = null;
  success = false;

  form = this.fb.group({
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    dateOfBirth: [''],
    biography: [''],
  });

  constructor(
    private fb: FormBuilder,
    private authorService: AuthorService,
    private router: Router
  ) {}

  ngOnInit(): void {
    // nothing to preload
  }

  onSubmit(): void {
    if (this.form.invalid) return;

    this.error = null;
    const payload = {
      firstName: this.form.value.firstName!,
      lastName: this.form.value.lastName!,
      dateOfBirth: this.form.value.dateOfBirth || undefined,
      biography: this.form.value.biography || undefined,
    };

    this.authorService.create(payload).subscribe({
      next: (newId) => {
        this.success = true;
        setTimeout(() => this.router.navigate(['/authors']), 1000);
      },
      error: (err) => {
        console.error(err);
        this.error = 'Failed to add author.';
      },
    });
  }
}
