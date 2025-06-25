// src/app/borrowers/borrowers-list.component.ts
import { Component, OnInit } from '@angular/core';
import { BorrowerDto, BorrowersClient } from 'src/app/api-client';

@Component({
  selector: 'app-borrowers-list',
  templateUrl: './borrowers-list.component.html',
  styleUrls: ['./borrowers-list.component.scss'],
})
export class BorrowersListComponent implements OnInit {
  borrowers: BorrowerDto[] = [];
  loading = false;
  error: string | null = null;

  constructor(private borrowerService: BorrowersClient) {}

  ngOnInit(): void {
    this.fetchBorrowers();
  }

  fetchBorrowers() {
    this.loading = true;
    this.error = null;

    this.borrowerService.getBorrowers().subscribe({
      next: (data) => {
        this.borrowers = data;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load borrowers.';
        console.error(err);
        this.loading = false;
      },
    });
  }
}
