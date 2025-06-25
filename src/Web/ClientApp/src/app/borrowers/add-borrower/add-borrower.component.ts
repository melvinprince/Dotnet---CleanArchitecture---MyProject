// src/app/borrower-add/borrower-add.component.ts
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AddBorrowerCommand, BorrowersClient } from 'src/app/api-client';

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
    private borrowerService: BorrowersClient,
    private router: Router
  ) {}

  ngOnInit(): void {
    // no preload needed
  }

  onSubmit(): void {
    if (this.form.invalid) return;

    this.error = null;
    const cmd = new AddBorrowerCommand();
    cmd.fullName = this.form.value.fullName!;
    cmd.email = this.form.value.email!;
    cmd.phoneNumber = this.form.value.phoneNumber;

    this.borrowerService.addBorrower(cmd).subscribe({
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
