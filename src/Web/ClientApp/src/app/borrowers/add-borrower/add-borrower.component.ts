// src/app/borrower-add/borrower-add.component.ts
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { BorrowerService } from 'src/app/api-client';
import { Router } from '@angular/router';

@Component({
  selector: 'app-borrower-add',
  templateUrl: './add-borrower.component.html',
  styleUrls: ['./add-borrower.component.scss'],
})
export class BorrowerAddComponent implements OnInit {
  loading = false;
  error: string | null = null;
  success = false;

  form = this.fb.group({
    fullName: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    phoneNumber: [''],
  });

  constructor(
    private fb: FormBuilder,
    private borrowerService: BorrowerService,
    private router: Router
  ) {}

  ngOnInit(): void {
    // no preload needed
  }

  onSubmit(): void {
    if (this.form.invalid) return;

    this.error = null;
    const payload = {
      fullName: this.form.value.fullName!,
      email: this.form.value.email!,
      phoneNumber: this.form.value.phoneNumber || undefined,
    };

    this.borrowerService.create(payload).subscribe({
      next: (newId) => {
        this.success = true;
        setTimeout(() => this.router.navigate(['/borrowers']), 1000);
      },
      error: (err) => {
        console.error(err);
        this.error = 'Failed to add borrower.';
      },
    });
  }
}
