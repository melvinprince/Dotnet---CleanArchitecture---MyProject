export type BookStatus = 0 | 1;

export interface Book {
  id: string;
  title: string;
  isbn: string;
  publishedDate: string; // ISO 8601
  authorId: string;
  status: BookStatus;
  borrowerId?: string;
}
