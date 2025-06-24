// File: src/Application/Authors/Queries/GetAuthorsList/GetAuthorsListQueryHandler.cs
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;
using Application.Common.Models;

namespace Application.Authors.Queries.GetAuthorsList
{
    public class GetAuthorsListQueryHandler
        : IRequestHandler<GetAuthorsListQuery, List<AuthorDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetAuthorsListQueryHandler(IApplicationDbContext context)
            => _context = context;

        public async Task<List<AuthorDto>> Handle(
            GetAuthorsListQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Authors
                .Select(a => new AuthorDto(
                    a.Id,
                    a.FirstName,
                    a.LastName,
                    a.DateOfBirth,
                    a.Biography
                ))
                .ToListAsync(cancellationToken);
        }
    }
}
