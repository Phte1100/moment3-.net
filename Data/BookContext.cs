using BookDirectory.Models;
using Microsoft.EntityFrameworkCore;

namespace BookDirectory.Data
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId);
        }
        public DbSet<BookDirectory.Models.User> User { get; set; } = default!;
        public DbSet<BookDirectory.Models.Loan> Loan { get; set; } = default!;
    }
}
