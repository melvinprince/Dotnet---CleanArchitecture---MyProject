using MediatR;
using System;

namespace Application.Books.Commands.BorrowBook
{
    public class BorrowBookCommand : IRequest<Unit>
    {
        public Guid BookId { get; set; }
        public Guid BorrowerId { get; set; }

        public BorrowBookCommand(Guid bookId, Guid borrowerId)
        {
            BookId = bookId;
            BorrowerId = borrowerId;
        }
        public BorrowBookCommand() { }
    }
}
