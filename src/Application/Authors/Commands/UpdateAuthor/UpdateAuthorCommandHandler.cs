// File: src/Application/Authors/Commands/UpdateAuthor/UpdateAuthorCommandHandler.cs
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandHandler
        : IRequestHandler<UpdateAuthorCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public UpdateAuthorCommandHandler(IApplicationDbContext context)
            => _context = context;

        public async Task<Unit> Handle(
            UpdateAuthorCommand request,
            CancellationToken cancellationToken)
        {
            var author = await _context.Authors
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (author == null)
                throw new KeyNotFoundException("Author not found.");

            author.FirstName   = request.FirstName;
            author.LastName    = request.LastName;
            author.DateOfBirth = request.DateOfBirth;
            author.Biography   = request.Biography;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
