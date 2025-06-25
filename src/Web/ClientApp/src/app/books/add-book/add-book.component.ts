import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import {
  AddBookCommand,
  AuthorDto,
  AuthorsClient,
  BooksClient,
} from 'src/app/api-client';

@Component({
  selector: 'app-book-add',
  templateUrl: './add-book.component.html',
  styleUrls: ['./add-book.component.scss'],
})
export class BookAddComponent implements OnInit {
  authors: AuthorDto[] = [];
  loading = false;
  error: string | null = null;
  success = false;

  form = this.fb.group({
    title: ['', Validators.required],
    isbn: ['', [Validators.required, Validators.minLength(10)]],
    authorId: ['', Validators.required],
    publishedDate: ['', Validators.required],
  });

  constructor(
    private fb: FormBuilder,
    private authorService: AuthorsClient,
    private bookService: BooksClient,
    private router: Router
  ) {}

  ngOnInit() {
    this.loading = true;
    this.authorService.getAuthors().subscribe({
      next: (authors) => {
        this.authors = authors;
        this.loading = false;
      },
      error: (err) => {
        console.error(err);
        this.error = 'Could not load authors.';
        this.loading = false;
      },
    });
  }

  onSubmit() {
    if (this.form.invalid) return;
    this.error = null;
    const payload = {
      title: this.form.value.title!,
      isbn: this.form.value.isbn!,
      authorId: this.form.value.authorId!,
      publishedDate: this.form.value.publishedDate!, // format: YYYY-MM-DD
    };
    const cmd = new AddBookCommand();
    cmd.title = payload.title;
    cmd.isbn = payload.isbn;
    cmd.authorId = payload.authorId;
    cmd.publishedDate = new Date(payload.publishedDate);

    this.bookService.addBook(cmd).subscribe({
      next: (newId) => {
        this.success = true;
        // Optionally redirect back to list after 1s
        setTimeout(() => this.router.navigate(['/books']), 1000);
      },
      error: (err) => {
        console.error(err);
        this.error = 'Failed to add book.';
      },
    });
  }
}
