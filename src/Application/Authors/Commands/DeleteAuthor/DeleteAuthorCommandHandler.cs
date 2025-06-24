// File: src/Application/Authors/Commands/DeleteAuthor/DeleteAuthorCommandHandler.cs
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;

namespace Application.Authors.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandHandler
        : IRequestHandler<DeleteAuthorCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public DeleteAuthorCommandHandler(IApplicationDbContext context)
            => _context = context;

        public async Task<Unit> Handle(
            DeleteAuthorCommand request,
            CancellationToken cancellationToken)
        {
            var author = await _context.Authors
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (author == null)
                throw new KeyNotFoundException("Author not found.");

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
