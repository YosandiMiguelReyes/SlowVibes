using Domain.Entities.Roles;
using Domain.Repositories;
using Application.Result;

namespace Persistence.Interfaces.Role
{
    public interface IUserRolesRepository : IBaseRepository<UserRoles, int>
    {
        Task<OperationResult<bool>> RemoveRoleFromUserAsync(int userId, int roleId);
    }
}
