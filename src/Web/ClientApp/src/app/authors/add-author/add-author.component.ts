// src/app/author-add/author-add.component.ts
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AddAuthorCommand, AuthorsClient } from 'src/app/api-client';

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
    private authorService: AuthorsClient,
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
    const cmd = new AddAuthorCommand();
    cmd.firstName = payload.firstName;
    cmd.lastName = payload.lastName;
    cmd.dateOfBirth = new Date(payload.dateOfBirth!);
    cmd.biography = payload.biography;

    this.authorService.addAuthor(cmd).subscribe({
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
