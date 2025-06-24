// File: src/Application/Borrowers/Queries/GetBorrowerById/GetBorrowerByIdQueryHandler.cs
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;
using Application.Common.Models;

namespace Application.Borrowers.Queries.GetBorrowerById
{
    public class GetBorrowerByIdQueryHandler
        : IRequestHandler<GetBorrowerByIdQuery, BorrowerDto>
    {
        private readonly IApplicationDbContext _context;
        public GetBorrowerByIdQueryHandler(IApplicationDbContext context)
            => _context = context;

        public async Task<BorrowerDto> Handle(
            GetBorrowerByIdQuery request,
            CancellationToken cancellationToken)
        {
            var b = await _context.Borrowers
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (b == null)
                throw new KeyNotFoundException("Borrower not found.");

            return new BorrowerDto(
                b.Id, b.FullName, b.Email, b.PhoneNumber, b.DateRegistered
            );
        }
    }
}
