using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LibrarySystem.Entities
{
    public partial class LibrarySystemContext : DbContext
    {
        public LibrarySystemContext() {}

        public LibrarySystemContext(DbContextOptions<LibrarySystemContext> options)
            : base(options)
        {
            
        }

        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Rental> Rental { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Rental>(entity =>
            {
                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Rental)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Rental__BookId__5CD6CB2B");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Rental)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Rental__Customer__5BE2A6F2");
            });

            OnModelCreatingPartial(modelBuilder);

            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Author = "John Travolta", Isbn = "12312123132", Title = "Sun is shining" },
                new Book { Id = 2, Author = "Angelina Jolie", Isbn = "342545667765", Title = "Master that game" },
                new Book { Id = 3, Author = "Larry Clint", Isbn = "756324vddsffd", Title = "Stop sending SMS" },
                new Book { Id = 4, Author = "Haskel Peanut", Isbn = "12312123132", Title = "I like peanutbutter" }
                );

            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = Guid.Parse("123e4567-e89b-12d3-a456-426655440000"), FullName = "Andrzej Duda" },
                new Customer { Id = Guid.Parse("123e5432-e89b-12d3-a456-426655440000"), FullName = "Barack Obama" },
                new Customer { Id = Guid.Parse("213e4567-e89b-12d3-a456-426655440000"), FullName = "Tristan Burba" }
                );

            modelBuilder.Entity<Rental>().HasData(
                new Rental { Id = 1, CustomerId = Guid.Parse("123e4567-e89b-12d3-a456-426655440000"), BookId = 2, StartDate = new DateTime(2012, 12, 12)},
                new Rental { Id = 2, CustomerId = Guid.Parse("213e4567-e89b-12d3-a456-426655440000"), BookId = 4, StartDate = new DateTime(2018, 1, 1), EndDate = new DateTime(2019, 1, 1)}
                );
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
