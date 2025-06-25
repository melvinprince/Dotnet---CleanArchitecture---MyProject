// src/app/app.module.ts
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_ID } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import {
  HTTP_INTERCEPTORS,
  provideHttpClient,
  withInterceptorsFromDi,
} from '@angular/common/http';
import { ModalModule } from 'ngx-bootstrap/modal';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

// ← import your token and env
import { API_BASE_URL } from './api-client';
import { environment } from '../environments/environment';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { HeroComponent } from './hero/hero.component';
import { FeaturesComponent } from './features/features.component';
import { FooterComponent } from './footer/footer.component';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { BooksListComponent } from './books/books-list/books-list.component';
import { BorrowersListComponent } from './borrowers/borrowers-list/borrowers-list.component';
import { AuthorsListComponent } from './authors/authors-list/authors-list.component';
import { BorrowBookComponent } from './borrow-book-form/borrow-book-form.component';
import { BookAddComponent } from './books/add-book/add-book.component';
import { AuthorAddComponent } from './authors/add-author/add-author.component';
import { BorrowerAddComponent } from './borrowers/add-borrower/add-borrower.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    BooksListComponent,
    HeroComponent,
    FeaturesComponent,
    FooterComponent,
    BorrowersListComponent,
    AuthorsListComponent,
    BookAddComponent,
    AuthorAddComponent,
    BorrowerAddComponent,
    BorrowBookComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'books', component: BooksListComponent },
      { path: 'books/new', component: BookAddComponent },
      { path: 'borrowers', component: BorrowersListComponent },
      { path: 'authors', component: AuthorsListComponent },
      { path: 'authors/new', component: AuthorAddComponent },
      { path: 'borrow', component: BorrowBookComponent },
      { path: 'borrowers/new', component: BorrowerAddComponent },
    ]),
    BrowserAnimationsModule,
    ModalModule.forRoot(),
  ],
  providers: [
    { provide: APP_ID, useValue: 'ng-cli-universal' },
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
    provideHttpClient(withInterceptorsFromDi()),
    { provide: API_BASE_URL, useValue: environment.apiBaseUrl },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
