using LibraryManagementSystem.DBContexts;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Repository
{
    public class BorrowedBookRepository : IBorrowedBookRepository
    {


        private readonly LibraryDBContext _dbContext;

        public BorrowedBookRepository(LibraryDBContext dbContext)
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

        public async Task<IEnumerable<BorrowedBook>> GetBorrowedBooks()
        {
            return await _dbContext.BorrowedBooks.Include(b => b.Member).Include(b => b.Book).ToListAsync();
        }

        public async Task<BorrowedBook> GetBorrowedBookByID(int borrowedBookId)
        {
            return await _dbContext.BorrowedBooks.FirstOrDefaultAsync(a => a.BorrowedBookID == borrowedBookId);
        }

        public async Task InsertBorrowedBook(BorrowedBook borrowedBook)
        {
            await _dbContext.AddAsync(borrowedBook);
            await _dbContext.SaveChangesAsync();
        }


        public async Task UpdateBorrowedBook(BorrowedBook borrowedBook)
        {
            _dbContext.Entry(borrowedBook).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteBorrowedBook(int borrowedBookId)
        {
            var borrowedBook = await _dbContext.BorrowedBooks.FindAsync(borrowedBookId);
            if (borrowedBook != null)
            {
                _dbContext.BorrowedBooks.Remove(borrowedBook);
                await _dbContext.SaveChangesAsync();
            }
        }

    }

}
