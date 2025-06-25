// src/app/books/books-list.component.ts
import { Component, OnInit } from '@angular/core';
import { BookDto, BooksClient } from 'src/app/api-client';

@Component({
  selector: 'app-books-list',
  templateUrl: './books-list.component.html',
  styleUrls: ['./books-list.component.scss'],
})
export class BooksListComponent implements OnInit {
  books: BookDto[] = [];
  loading = false;
  error: string | null = null;

  constructor(private bookService: BooksClient) {}

  ngOnInit(): void {
    this.fetchBooks();
  }

  fetchBooks() {
    this.loading = true;
    this.error = null;

    this.bookService.getBooks().subscribe({
      next: (data) => {
        this.books = data;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load books.';
        console.error(err);
        this.loading = false;
      },
    });
  }
}
