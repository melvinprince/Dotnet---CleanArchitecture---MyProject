import { Component, OnInit } from '@angular/core';
import { AuthorDto, AuthorsClient } from 'src/app/api-client';

@Component({
  selector: 'app-authors-list',
  templateUrl: './authors-list.component.html',
  styleUrls: ['./authors-list.component.scss'],
})
export class AuthorsListComponent implements OnInit {
  authors: AuthorDto[] = [];
  loading = false;
  error: string | null = null;

  constructor(private authorService: AuthorsClient) {}

  ngOnInit(): void {
    this.fetchAuthors();
  }

  fetchAuthors() {
    this.loading = true;
    this.error = null;

    this.authorService.getAuthors().subscribe({
      next: (data) => {
        this.authors = data;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load authors.';
        console.error(err);
        this.loading = false;
      },
    });
  }
}
