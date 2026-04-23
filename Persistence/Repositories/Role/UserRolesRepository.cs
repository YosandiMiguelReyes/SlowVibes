

using Domain.Entities.Roles;
using Persistence.BaseRepository;
using Persistence.Interfaces.Role;

namespace Persistence.Repositories.Role
{
    public class UserRolesRepository : BaseRepository<UserRoles, int>, IUserRolesRepository
    {
    }
}
