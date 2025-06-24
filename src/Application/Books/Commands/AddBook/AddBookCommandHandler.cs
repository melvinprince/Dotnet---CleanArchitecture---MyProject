// src/Application/Books/Commands/AddBook/AddBookCommandHandler.cs
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Books.Commands.AddBook
{
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public AddBookCommandHandler(IApplicationDbContext context)
            => _context = context;

        public async Task<Guid> Handle(
            AddBookCommand request,
            CancellationToken cancellationToken)
        {
            // 1. Verify the author exists
            var authorExists = await _context.Authors
                .AnyAsync(a => a.Id == request.AuthorId, cancellationToken);
            if (!authorExists)
                throw new Exception("Author not found.");

            // 2. Use object initializer so 'Title' and 'ISBN' are seen as set
            var book = new Book
            {
                Id            = Guid.NewGuid(),
                Title         = request.Title,
                ISBN          = request.ISBN,
                PublishedDate = request.PublishedDate,
                AuthorId      = request.AuthorId,
                // Status will default to `0` => Available
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync(cancellationToken);

            return book.Id;
        }
    }
}
