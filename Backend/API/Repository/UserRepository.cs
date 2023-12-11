using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    // Interface defining the contract for user-related data operations
    public interface IUserRepository
    {
        User GetUserByUsername(string username);
        void AddUser(User user);
    }

    public class UserRepository : IUserRepository
    {
        private readonly YourDbContext _dbContext;

        public UserRepository(YourDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User GetUserByUsername(string username)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Username == username);
        }

        public void AddUser(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }
    }
}
