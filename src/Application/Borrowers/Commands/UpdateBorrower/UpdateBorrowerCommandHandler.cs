// File: src/Application/Borrowers/Commands/UpdateBorrower/UpdateBorrowerCommandHandler.cs
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Borrowers.Commands.UpdateBorrower
{
    public class UpdateBorrowerCommandHandler
        : IRequestHandler<UpdateBorrowerCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        public UpdateBorrowerCommandHandler(IApplicationDbContext context)
            => _context = context;

        public async Task<Unit> Handle(
            UpdateBorrowerCommand request,
            CancellationToken cancellationToken)
        {
            var borrower = await _context.Borrowers
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (borrower == null)
                throw new KeyNotFoundException("Borrower not found.");

            borrower.FullName    = request.FullName;
            borrower.Email       = request.Email;
            borrower.PhoneNumber = request.PhoneNumber;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
