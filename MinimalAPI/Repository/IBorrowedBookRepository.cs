using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repository
{
    public interface IBorrowedBookRepository
    {
        Task<IEnumerable<BorrowedBook>> GetBorrowedBooks();

        Task<BorrowedBook> GetBorrowedBookByID(int borrowedBookID);

        Task InsertBorrowedBook(BorrowedBook borrowedBook);

        Task UpdateBorrowedBook(BorrowedBook borrowedBook);

        void Save();
        Task SaveAsync();
        Task DeleteBorrowedBook(int borrowedBook);
    }
}
