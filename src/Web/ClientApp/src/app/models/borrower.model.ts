export interface Borrower {
  id: string;
  fullName: string;
  email: string;
  phoneNumber?: string;
  dateRegistered: string; // ISO 8601
}
