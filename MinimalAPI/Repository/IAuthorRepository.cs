using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repository
{
    public interface IAuthorRepository
    {

        Task<IEnumerable<Author>> GetAuthors();

        Task<Author> GetAuthorByID(int authorID);

        Task InsertAuthor(Author author);

        Task UpdateAuthor(Author author);

        void Save();
        Task SaveAsync();
        Task DeleteAuthor(int author);
    }
}
