using LibraryManagementSystem.DBContexts;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDBContext _dbContext;

        public BookRepository(LibraryDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Book>> Getbooks()
        {
            return await _dbContext.Books.ToListAsync();
        }

        public async Task<Book> GetBookByID(int bookId)
        {
            return await _dbContext.Books.FirstOrDefaultAsync(a => a.BookID == bookId);
        }

        public async Task InsertBook(Book book)
        {
            await _dbContext.AddAsync(book);
            await _dbContext.SaveChangesAsync();
        }


        public async Task UpdateBook(Book book)
        {
            _dbContext.Entry(book).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteBook(int bookId)
        {
            var book = await _dbContext.Books.FindAsync(bookId);
            if (book != null)
            {
                _dbContext.Books.Remove(book);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
