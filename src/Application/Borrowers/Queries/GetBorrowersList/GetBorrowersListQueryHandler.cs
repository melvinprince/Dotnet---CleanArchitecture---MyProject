// File: src/Application/Borrowers/Queries/GetBorrowersList/GetBorrowersListQueryHandler.cs
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;
using Application.Common.Models;

namespace Application.Borrowers.Queries.GetBorrowersList
{
    public class GetBorrowersListQueryHandler
        : IRequestHandler<GetBorrowersListQuery, List<BorrowerDto>>
    {
        private readonly IApplicationDbContext _context;
        public GetBorrowersListQueryHandler(IApplicationDbContext context)
            => _context = context;

        public async Task<List<BorrowerDto>> Handle(
            GetBorrowersListQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Borrowers
                .Select(b => new BorrowerDto(
                    b.Id,
                    b.FullName,
                    b.Email,
                    b.PhoneNumber,
                    b.DateRegistered
                ))
                .ToListAsync(cancellationToken);
        }
    }
}
