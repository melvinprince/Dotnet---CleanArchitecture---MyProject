// File: src/Application/Borrowers/Commands/DeleteBorrower/DeleteBorrowerCommandHandler.cs
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Borrowers.Commands.DeleteBorrower
{
    public class DeleteBorrowerCommandHandler
        : IRequestHandler<DeleteBorrowerCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        public DeleteBorrowerCommandHandler(IApplicationDbContext context)
            => _context = context;

        public async Task<Unit> Handle(
            DeleteBorrowerCommand request,
            CancellationToken cancellationToken)
        {
            var borrower = await _context.Borrowers
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (borrower == null)
                throw new KeyNotFoundException("Borrower not found.");

            _context.Borrowers.Remove(borrower);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
