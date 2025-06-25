using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Book> Books { get; }
    DbSet<Author> Authors { get; }
    DbSet<Borrower> Borrowers { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
