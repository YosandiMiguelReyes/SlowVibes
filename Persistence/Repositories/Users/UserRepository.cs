using Domain.Entities.User;
using Domain.Repositories;
using Persistence.BaseRepository;
using Persistence.Interfaces.Users;
using System.Linq.Expressions;


namespace Persistence.Repositories.Users
{
    public class UserRepository : BaseRepository<User, int>, IUserRepository
    {
        public Task AddAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetByCredentialsWithRolesAsync(string identifier)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetUsersByRoleAsync(string roleName)
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

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateStatusAsync(int userId, bool isActive)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<User>> IBaseRepository<User, int>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<User> IBaseRepository<User, int>.GetAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
