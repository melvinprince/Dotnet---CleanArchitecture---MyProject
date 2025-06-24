// src/Domain/Entities/Book.cs
using System;
using Domain.Enums;

namespace Domain.Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string ISBN { get; set; }
        public DateTime PublishedDate { get; set; }
        public Guid AuthorId { get; set; }

        // ← New foreign key + nav
        public Guid? BorrowerId { get; private set; }
        public Borrower? Borrower { get; private set; }

        public BookStatus Status { get; private set; }

        public Book()
        {
        }

        public Book(Guid id, string title, string isbn, DateTime publishedDate, Guid authorId)
        {
            Id             = id;
            Title          = title;
            ISBN           = isbn;
            PublishedDate  = publishedDate;
            AuthorId       = authorId;
            Status         = BookStatus.Available;
            BorrowerId     = null;
        }

        // Assigns a borrower and marks as borrowed
        public void Borrow(Guid borrowerId)
        {
            if (Status == BookStatus.Borrowed)
                throw new InvalidOperationException("Book is already borrowed.");

            BorrowerId = borrowerId;
            Status     = BookStatus.Borrowed;
        }

        // Clears borrower and marks as available
        public void Return()
        {
            if (Status == BookStatus.Available)
                throw new InvalidOperationException("Book is already available.");

            BorrowerId = null;
            Status     = BookStatus.Available;
        }
    }
}
