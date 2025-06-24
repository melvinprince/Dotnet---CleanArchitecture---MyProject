// File: src/Application/Authors/Queries/GetAuthorById/GetAuthorByIdQueryHandler.cs
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;
using Application.Common.Models;

namespace Application.Authors.Queries.GetAuthorById
{
    public class GetAuthorByIdQueryHandler
        : IRequestHandler<GetAuthorByIdQuery, AuthorDto>
    {
        private readonly IApplicationDbContext _context;

        public GetAuthorByIdQueryHandler(IApplicationDbContext context)
            => _context = context;

        public async Task<AuthorDto> Handle(
            GetAuthorByIdQuery request,
            CancellationToken cancellationToken)
        {
            var a = await _context.Authors
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (a == null)
                throw new KeyNotFoundException("Author not found.");

            return new AuthorDto(
                a.Id, a.FirstName, a.LastName, a.DateOfBirth, a.Biography
            );
        }
    }
}
