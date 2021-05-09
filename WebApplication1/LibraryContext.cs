using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
namespace WebApplication1
{
    public class LibraryContext: DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options):base(options)
        {
            Database.EnsureCreated();
        }
        public virtual DbSet<GenreBookModel> GenreBooks { get; set; }
        public virtual DbSet<AuthorModel> Authors { get; set; }
        public virtual DbSet<AdminModel> Admins { get; set; }
        public virtual DbSet<UserModel> Users { get; set; }
        public virtual DbSet<BookModel> Books { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminModel>().ToTable("Admins", t => t.ExcludeFromMigrations());
            modelBuilder.Entity<GenreBookModel>().ToTable("GenreBooks", t => t.ExcludeFromMigrations());
            modelBuilder.Entity<AuthorModel>().ToTable("Authors", t => t.ExcludeFromMigrations());
            modelBuilder.Entity<UserModel>().ToTable("Users", t => t.ExcludeFromMigrations());
        }
        
    }
}
