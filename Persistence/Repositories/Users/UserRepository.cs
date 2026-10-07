using Domain.Entities.User;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.BaseRepository;
using Persistence.Context;
using Application.Contracts.Repositories.Users;
using Application.DTOs.User;
using System.Linq.Expressions;
//using Persistence.Mappers.UserMappers;


namespace Persistence.Repositories.Users
{
    public class UserRepository : BaseRepository<Domain.Entities.User.Users, int>, IUserRepository
    {
        public UserRepository(SlowVibesDbContext context) : base(context)
        {

        }

        public Task<AdminUserWithRole?> AdminGetByCredentialsWithRolesAsync(string identifier)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AdminUserWithRole>> AdminGetUsersByRoleAsync(string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AdminUserWithRole>> AdminGetUsersByStatusAsync(bool status)
        {
            throw new NotImplementedException();
        }

        public Task<UserWithRolesDTO?> GetByCredentialsWithRolesAsync(string identifier)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserWithRolesDTO>> GetUsersByRoleAsync(string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserWithRolesDTO>> GetUsersByStatusAsync(bool status)
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
