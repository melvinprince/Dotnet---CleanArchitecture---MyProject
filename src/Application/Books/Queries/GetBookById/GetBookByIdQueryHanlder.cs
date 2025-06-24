// File: src/Application/Books/Queries/GetBookById/GetBookByIdQueryHandler.cs
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;
using Application.Common.Models;

namespace Application.Books.Queries.GetBookById
{
    public class GetBookByIdQueryHandler
        : IRequestHandler<GetBookByIdQuery, BookDto>
    {
        private readonly IApplicationDbContext _context;
        public GetBookByIdQueryHandler(IApplicationDbContext context)
            => _context = context;

        public async Task<BookDto> Handle(
            GetBookByIdQuery request,
            CancellationToken cancellationToken)
        {
            var b = await _context.Books
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (b is null)
                throw new KeyNotFoundException("Book not found.");

            return new BookDto(
                b.Id, b.Title, b.ISBN, b.PublishedDate, b.AuthorId, b.Status
            );
        }
    }
}
