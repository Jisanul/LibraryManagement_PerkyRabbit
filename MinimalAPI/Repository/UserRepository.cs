using LibraryManagementSystem.DBContexts;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly LibraryDBContext _dbContext;

        public UserRepository(LibraryDBContext dbContext)
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

        public async Task<IEnumerable<User>> GetUsers( )
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User> GetUserByID(int userId)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(a => a.Id == userId);
        }

        public async Task InsertUser(User user)
        {
            await _dbContext.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }


        public async Task UpdateUser(User user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteUser(int userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
            }
        }
    }

}
