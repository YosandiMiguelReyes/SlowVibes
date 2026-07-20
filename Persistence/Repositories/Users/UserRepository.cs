using Domain.Entities.User;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.BaseRepository;
using Persistence.Context;
using Persistence.Interfaces.Users;
using Persistence.DTO.User;
using System.Linq.Expressions;
using Persistence.Mappers.UserMappers;


namespace Persistence.Repositories.Users
{
    public class UserRepository : BaseRepository<Domain.Entities.User.Users, int>, IUserRepository
    {
        public UserRepository(SlowVibesDbContext context) : base(context)
        {

        }

        //---------------------------ADMIN ONLY-------------------------------------------
        public async Task<AdminUserWithRole?> AdminGetByCredentialsWithRolesAsync(string identifier)
        {

            return await _dbSet
                .Where(u => (u.Email == identifier || u.UserName == identifier))
                .Select(UserMapper.AsAdminUserWithRole).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AdminUserWithRole>> AdminGetUsersByRoleAsync(string roleName)
        {
            return await _dbSet
                .Where(u => u.UsersRoles.Any(ur => ur.Role.Name == roleName))
                .Select(UserMapper.AsAdminUserWithRole).ToListAsync();
        }

        public async Task<IEnumerable<AdminUserWithRole>> AdminGetUsersByStatusAsync(bool status)
        {
            return await _dbSet
                .Where(u => u.IsActive == status)
                .Select(UserMapper.AsAdminUserWithRole).ToListAsync();
        }

        //----------------------------------------------------------------------


        public async Task<UserWithRolesDTO?> GetByCredentialsWithRolesAsync(string identifier)
        {
            return await _dbSet
                .Where(u => (u.Email == identifier || u.UserName == identifier) && u.IsActive == true)
                .Select(UserMapper.AsUserWithRole).FirstOrDefaultAsync();

        }

        public async Task<IEnumerable<UserWithRolesDTO>> GetUsersByRoleAsync(string roleName)
        {
            return await _dbSet
                .Where(u => u.UsersRoles.Any(ur => ur.Role.Name == roleName) && u.IsActive == true)
                .Select(UserMapper.AsUserWithRole).ToListAsync();
        }


        //Staff only
        public async Task<IEnumerable<UserWithRolesDTO>> GetUsersByStatusAsync(bool status)
        {
            return await _dbSet
                .Where(u => u.IsActive == status)
                .Select(UserMapper.AsUserWithRole).ToListAsync();
        }

        //----------------------------------------------------------------------

        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            return !await _dbSet.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> IsUsernameUniqueAsync(string username)
        {
            return !await _dbSet.AnyAsync(u => u.UserName == username);
        }

        public async Task UpdateStatusAsync(int userId, bool isActive)
        {
            var user = await GetAsync(userId);

            if (user == null)
            {
                throw new Exception($"Usuario con ID {userId} no encontrado.");
            }

            user.IsActive = isActive;

        }
    }
}
