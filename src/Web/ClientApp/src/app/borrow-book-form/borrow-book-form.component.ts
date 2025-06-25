import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import {
  BookService,
  BorrowerService,
  Book,
  Borrower,
} from 'src/app/web-api-client';

@Component({
  selector: 'app-borrow-book-form',
  templateUrl: './borrow-book-form.component.html',
  styleUrls: ['./borrow-book-form.component.scss'],
})
export class BorrowBookFormComponent implements OnInit {
  books: Book[] = [];
  borrowers: Borrower[] = [];
  loading = false;
  error: string | null = null;
  success = false;

  form = this.fb.group({
    bookId: ['', Validators.required],
    borrowerId: ['', Validators.required],
  });

  constructor(
    private fb: FormBuilder,
    private bookService: BookService,
    private borrowerService: BorrowerService
  ) {}

  ngOnInit() {
    this.loading = true;
    Promise.all([
      this.bookService.getAll().toPromise(),
      this.borrowerService.getAll().toPromise(),
    ])
      .then(([books, borrowers]) => {
        // only show available books
        this.books = books.filter((b) => b.status === 'Available');
        this.borrowers = borrowers;
        this.loading = false;
      })
      .catch((err) => {
        console.error(err);
        this.error = 'Failed to load data.';
        this.loading = false;
      });
  }

  onSubmit() {
    if (this.form.invalid) return;
    this.error = null;
    const { bookId, borrowerId } = this.form.value;
    this.bookService.borrow(bookId, borrowerId).subscribe({
      next: () => (this.success = true),
      error: (err) => {
        console.error(err);
        this.error = 'Could not borrow book.';
      },
    });
  }
}
