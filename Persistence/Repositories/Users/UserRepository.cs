using Domain.Entities.User;
using Domain.Repositories;
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

        public Task<Domain.Entities.User.Users?> GetByCredentialsWithRolesAsync(string identifier)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Domain.Entities.User.Users>> GetUsersByRoleAsync(string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Domain.Entities.User.Users>> GetUsersByStatusAsync(bool status)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsEmailUniqueAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsUsernameUniqueAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task UpdateStatusAsync(int userId, bool isActive)
        {
            throw new NotImplementedException();
        }
    }
}
