using Domain.Entities.User;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.BaseRepository;
using Persistence.Context;
using Persistence.Interfaces.Users;
using System.Linq.Expressions;


namespace Persistence.Repositories.Users
{
    public class UserRepository : BaseRepository<Domain.Entities.User.Users, int>, IUserRepository
    {
        public UserRepository(SlowVibesDbContext context) : base (context)
        {
         
        }

        public async Task<Domain.Entities.User.Users?> GetByCredentialsWithRolesAsync(string identifier)
        {
            throw new NotImplementedException();

        }

        public Task<IEnumerable<Domain.Entities.User.Users>> GetUsersByRoleAsync(string roleName)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Domain.Entities.User.Users>> GetUsersByStatusAsync(bool status)
        {
            return await _dbSet.Where(u => u.IsActive == status).ToListAsync();
        }

        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            return await _dbSet.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> IsUsernameUniqueAsync(string username)
        {
            return await _dbSet.AnyAsync(u => u.UserName == username);
        }

        public async Task UpdateStatusAsync(int userId, bool isActive)
        {
            var user = await GetAsync(userId);

            if (user != null)
            {
                user.IsActive = isActive;
                
                _dbSet.Update(user);

            }
            else
            {
                throw new Exception($"User with ID {userId} not found.");
            }
            

        }
    }
}
