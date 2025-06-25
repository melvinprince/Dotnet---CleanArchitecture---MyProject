using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class LibraryDbContext : DbContext, IApplicationDbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books => Set<Book>();
        public DbSet<Author> Authors => Set<Author>();
        public DbSet<Borrower> Borrowers => Set<Borrower>();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            // Later: Add domain event publishing here
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Add Fluent configurations here later if needed
        }
    }
}
