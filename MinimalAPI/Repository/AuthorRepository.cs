using LibraryManagementSystem.DBContexts;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Repository
{
    public class AuthorRepository : IAuthorRepository
    {


        private readonly LibraryDBContext _dbContext;

        public AuthorRepository(LibraryDBContext dbContext)
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

        public async Task<IEnumerable<Author>> GetAuthors()
        {
            return await _dbContext.Authors.ToListAsync();
        }

        public async Task<Author> GetAuthorByID(int authorId)
        {
            return await _dbContext.Authors.FirstOrDefaultAsync(a => a.AuthorID == authorId);
        }

        public async Task InsertAuthor(Author author)
        {
            await _dbContext.AddAsync(author);
            await _dbContext.SaveChangesAsync();
        }


        public async Task UpdateAuthor(Author author)
        {
            _dbContext.Entry(author).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAuthor(int authorId)
        {
            var author = await _dbContext.Authors.FindAsync(authorId);
            if (author != null)
            {
                _dbContext.Authors.Remove(author);
                await _dbContext.SaveChangesAsync();
            }
        }

    }
}
