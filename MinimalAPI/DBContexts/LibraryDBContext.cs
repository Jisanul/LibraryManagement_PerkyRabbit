using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.DBContexts
{
    public class LibraryDBContext : DbContext
    {        
        public LibraryDBContext(DbContextOptions<LibraryDBContext> options) : base(options)
        {
        }
      
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BorrowedBook> BorrowedBooks { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
