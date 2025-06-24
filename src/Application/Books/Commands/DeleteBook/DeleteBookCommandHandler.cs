// File: src/Application/Books/Commands/DeleteBook/DeleteBookCommandHandler.cs
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;

namespace Application.Books.Commands.DeleteBook
{
    public class DeleteBookCommandHandler
        : IRequestHandler<DeleteBookCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        public DeleteBookCommandHandler(IApplicationDbContext context)
            => _context = context;

        public async Task<Unit> Handle(
            DeleteBookCommand request,
            CancellationToken cancellationToken)
        {
            var book = await _context.Books
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (book is null)
                throw new KeyNotFoundException("Book not found.");

            _context.Books.Remove(book);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
