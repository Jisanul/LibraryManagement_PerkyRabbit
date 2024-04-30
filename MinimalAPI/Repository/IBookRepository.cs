using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repository
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> Getbooks();

        Task<Book> GetBookByID(int bookID);

        Task InsertBook(Book book);

        Task UpdateBook(Book book);

        void Save();
        Task SaveAsync();
        Task DeleteBook(int book);
    }
}
