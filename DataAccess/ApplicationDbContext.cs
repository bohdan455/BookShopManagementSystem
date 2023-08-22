using DataAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace DataAccess
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<Discount> Discounts { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<OrderDetails> OrderDetails { get; set; }

        public DbSet<OrderPart> OrderParts { get; set; }

        public DbSet<Publisher> Publishers { get; set; }

        public DbSet<ReservedBook> ReservedBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Author>().HasIndex(a => new { a.UserId, a.Name }).IsUnique();
            builder.Entity<Genre>().HasIndex(g => new { g.UserId, g.Name }).IsUnique();
            builder.Entity<Publisher>().HasIndex(p => new { p.UserId,p.Name }).IsUnique();

            builder.Entity<Book>().Property(b => b.DiscountId).IsRequired(false);
            builder.Entity<Book>().Property(b => b.PreviousBookId).IsRequired(false);

            builder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Book>()
                .HasOne(b => b.Genre)
                .WithMany(g => g.Books)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Book>()
                .HasOne(b => b.Publisher)
                .WithMany(a => a.Books)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Book>()
                .HasOne(b => b.Discount)
                .WithMany(d => d.Books)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<OrderPart>()
                .HasOne(op => op.Order)
                .WithMany(od => od.OrderParts)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}