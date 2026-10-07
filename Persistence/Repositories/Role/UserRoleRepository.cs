using Domain.Entities.Roles;
using Microsoft.EntityFrameworkCore;
using Persistence.BaseRepository;
using Persistence.Context;
using Application.Contracts.Repositories.Role;
using Application.Result;

namespace Persistence.Repositories.Role
{
    public class UserRoleRepository : BaseRepository<UserRoles, int>, IUserRolesRepository
    {
        public UserRoleRepository(SlowVibesDbContext context) : base(context)
        {
            
        }

        public Task<OperationResult<bool>> RemoveRoleFromUserAsync(int userId, int roleId)
        {
            throw new NotImplementedException();
        }
    }
}
