using System;
using Domain.Enums;

namespace Domain.Entities
{
    public class Book
    {
        public Guid Id { get;  set; }
        public required string Title { get;  set; }
        public required string ISBN { get;  set; }
        public DateTime PublishedDate { get;  set; }
        public Guid AuthorId { get;  set; }
        public BookStatus Status { get;  set; }

        // Default constructor for EF Core and serialization
        public Book()
        {
        }

        // Parameterized constructor
        public Book(Guid id, string title, string isbn, DateTime publishedDate, Guid authorId)
        {
            Id = id;
            Title = title;
            ISBN = isbn;
            PublishedDate = publishedDate;
            AuthorId = authorId;
            Status = BookStatus.Available; // default status
        }

        // Business logic method example
        public void Borrow()
        {
            if (Status == BookStatus.Borrowed)
                throw new InvalidOperationException("Book is already borrowed.");

            Status = BookStatus.Borrowed;
        }

        public void Return()
        {
            if (Status == BookStatus.Available)
                throw new InvalidOperationException("Book is already available.");

            Status = BookStatus.Available;
        }
    }
}
