export interface Author {
  id: string;
  firstName: string;
  lastName: string;
  dateOfBirth?: string; // ISO 8601
  biography?: string;
}
