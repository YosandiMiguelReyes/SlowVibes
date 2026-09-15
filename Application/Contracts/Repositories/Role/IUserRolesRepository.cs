using Domain.Entities.Roles;
using Domain.Repositories;
using Domain.Result;

namespace Application.Contracts.Repositories.Role
{
    public interface IUserRolesRepository : IBaseRepository<UserRoles, int>
    {
        Task<OperationResult<bool>> RemoveRoleFromUserAsync(int userId, int roleId);
    }
}
