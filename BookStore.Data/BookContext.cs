using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookStore.Model;
using Microsoft.EntityFrameworkCore.Metadata;
using BookStore.Model.Entities;
using System.Linq.Expressions;
using System;

namespace BookStore.Data
{
    public class BookContext : DbContext
    {
        public DbSet<Author> Author { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<Order> Orders { get; set; }

        public BookContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }


            modelBuilder.Entity<Author>()
                .ToTable("Author");
            
            modelBuilder.Entity<Author>()
               .Property(a => a.Name)
               .HasMaxLength(100)
               .IsRequired();

            //modelBuilder.Entity<Author>()
            //   .Property(a => a.ContactNumber)
            //   .IsRequired();

            modelBuilder.Entity<Author>()
                .Property(a => a.CreateDate)
                .HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<Books>()
                .ToTable("Books");

            modelBuilder.Entity<Books>()
                .Property(b => b.Isbn)
                .IsRequired();

            modelBuilder.Entity<Books>()
                .Property(b => b.Title)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Books>()
                 .Property(b => b.Price)
                 .IsRequired();

            modelBuilder.Entity<Books>()
                .Property(b => b.AvailableQuantity)
                .IsRequired();

            modelBuilder.Entity<Books>()
                .Property(b => b.CreateDate)
                .HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<Books>()
              .HasOne(b => b.Author)
             .WithMany(a => a.BooksCreated);

            modelBuilder.Entity<Order>()
               .ToTable("Order");

            modelBuilder.Entity<Order>()
                .Property(o => o.BookId)
                .IsRequired();

            modelBuilder.Entity<Order>()
                .Property(o => o.OrderQuantity)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Order>()
                 .Property(o => o.Cost)
                 .IsRequired();

            modelBuilder.Entity<Order>()
                .Property(o => o.OrderDate)
                .IsRequired();

            modelBuilder.Entity<Order>()
            .Property(o => o.CreateDate)
            .HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<Order>()
               .HasOne(o => o.Books)
               .WithMany(b=> b.Orders)
               .HasForeignKey(o => o.BookId);



        }
    }
}
