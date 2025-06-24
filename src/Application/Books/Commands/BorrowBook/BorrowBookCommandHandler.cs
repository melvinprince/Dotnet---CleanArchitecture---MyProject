using Application.Common.Interfaces;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Books.Commands.BorrowBook
{
    public class BorrowBookCommandHandler : IRequestHandler<BorrowBookCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public BorrowBookCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(BorrowBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _context.Books
                .FirstOrDefaultAsync(b => b.Id == request.BookId, cancellationToken);

            if (book == null)
                throw new Exception("Book not found.");

            if (book.Status == BookStatus.Borrowed)
                throw new Exception("Book is already borrowed.");

            var borrower = await _context.Borrowers
                .FirstOrDefaultAsync(b => b.Id == request.BorrowerId, cancellationToken);

            if (borrower == null)
                throw new Exception("Borrower not found.");

            book.Borrow();

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
