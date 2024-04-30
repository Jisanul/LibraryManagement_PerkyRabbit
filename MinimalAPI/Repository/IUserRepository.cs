using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();

        Task<User> GetUserByID(int Id);

        Task InsertUser(User user);

        Task UpdateUser(User user);

        Task SaveAsync();

        Task DeleteUser(int userID);
    }
}
