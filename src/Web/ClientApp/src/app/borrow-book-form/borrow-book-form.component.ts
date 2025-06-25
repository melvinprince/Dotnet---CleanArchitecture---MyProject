import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import {
  BookService,
  BorrowerService,
  Book,
  Borrower,
} from 'src/app/web-api-client';
import { Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-borrow-book',
  templateUrl: './borrow-book-form.component.html',
  styleUrls: ['./borrow-book-form.component.scss'],
})
export class BorrowBookComponent implements OnInit {
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
    private borrowerService: BorrowerService,
    private router: Router
  ) {}

  async ngOnInit() {
    this.loading = true;

    try {
      const [books, borrowers] = await Promise.all([
        firstValueFrom(this.bookService.getAll()),
        firstValueFrom(this.borrowerService.getAll()),
      ]);

      this.books = books.filter((b) => b.status === 0);

      this.borrowers = borrowers;
    } catch (err) {
      console.error('❌ Error loading books or borrowers:', err);
      this.error = 'Failed to load books or borrowers.';
    } finally {
      this.loading = false;
    }
  }

  onSubmit() {
    if (this.form.invalid) return;
    this.error = null;

    const { bookId, borrowerId } = this.form.value;

    this.bookService.borrow(bookId!, borrowerId!).subscribe({
      next: () => {
        this.success = true;
        setTimeout(() => this.router.navigate(['/books']), 1000);
      },
      error: (err) => {
        console.error('❌ Failed to borrow book:', err);
        this.error = 'Failed to borrow book.';
      },
    });
  }
}
