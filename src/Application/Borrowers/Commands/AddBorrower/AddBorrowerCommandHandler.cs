// File: src/Application/Borrowers/Commands/AddBorrower/AddBorrowerCommandHandler.cs
using MediatR;
using Application.Common.Interfaces;
using Domain.Entities;
using System;

namespace Application.Borrowers.Commands.AddBorrower
{
    public class AddBorrowerCommandHandler
        : IRequestHandler<AddBorrowerCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        public AddBorrowerCommandHandler(IApplicationDbContext context)
            => _context = context;

        public async Task<Guid> Handle(
            AddBorrowerCommand request,
            CancellationToken cancellationToken)
        {
               
            var borrower = new Borrower
            {
                Id             = Guid.NewGuid(),
                FullName       = request.FullName,
                Email          = request.Email,
                PhoneNumber    = request.PhoneNumber,
                DateRegistered = DateTime.UtcNow
            };


            _context.Borrowers.Add(borrower);
            await _context.SaveChangesAsync(cancellationToken);

            return borrower.Id;
        }
    }
}
