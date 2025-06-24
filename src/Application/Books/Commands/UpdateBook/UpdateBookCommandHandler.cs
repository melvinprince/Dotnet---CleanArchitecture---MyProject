// File: src/Application/Books/Commands/UpdateBook/UpdateBookCommandHandler.cs
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;

namespace Application.Books.Commands.UpdateBook
{
    public class UpdateBookCommandHandler
        : IRequestHandler<UpdateBookCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        public UpdateBookCommandHandler(IApplicationDbContext context)
            => _context = context;

        public async Task<Unit> Handle(
            UpdateBookCommand request,
            CancellationToken cancellationToken)
        {
            var book = await _context.Books
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (book is null)
                throw new KeyNotFoundException("Book not found.");

            book.Title         = request.Title;
            book.ISBN          = request.ISBN;
            book.PublishedDate = request.PublishedDate;
            book.AuthorId      = request.AuthorId;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
