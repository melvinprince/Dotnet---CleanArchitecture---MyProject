// File: src/Application/Books/Queries/GetBooksList/GetBooksListQueryHandler.cs
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;
using Application.Common.Models;

namespace Application.Books.Queries.GetBooksList
{
    public class GetBooksListQueryHandler
        : IRequestHandler<GetBooksListQuery, List<BookDto>>
    {
        private readonly IApplicationDbContext _context;
        public GetBooksListQueryHandler(IApplicationDbContext context)
            => _context = context;

        public async Task<List<BookDto>> Handle(
            GetBooksListQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Books
                .Select(b => new BookDto(
                    b.Id,
                    b.Title,
                    b.ISBN,
                    b.PublishedDate,
                    b.AuthorId,
                    b.Status
                ))
                .ToListAsync(cancellationToken);
        }
    }
}
