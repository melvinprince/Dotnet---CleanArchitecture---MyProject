// File: src/Application/Authors/Commands/AddAuthor/AddAuthorCommandHandler.cs
using MediatR;
using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Authors.Commands.AddAuthor
{
    public class AddAuthorCommandHandler
        : IRequestHandler<AddAuthorCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public AddAuthorCommandHandler(IApplicationDbContext context)
            => _context = context;

        public async Task<Guid> Handle(
            AddAuthorCommand request,
            CancellationToken cancellationToken)
        {
            var author = new Author
            {
                Id           = Guid.NewGuid(),
                FirstName    = request.FirstName,
                LastName     = request.LastName,
                DateOfBirth  = request.DateOfBirth,
                Biography    = request.Biography
            };

            _context.Authors.Add(author);
            await _context.SaveChangesAsync(cancellationToken);

            return author.Id;
        }
    }
}
