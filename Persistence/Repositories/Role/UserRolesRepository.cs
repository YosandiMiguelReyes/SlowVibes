

using Domain.Entities.Roles;
using Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Persistence.BaseRepository;
using Persistence.Context;
using Persistence.Interfaces.Role;

namespace Persistence.Repositories.Role
{
    public class UserRolesRepository : BaseRepository<UserRoles, int>, IUserRolesRepository
    {
        public UserRolesRepository( SlowVibesDbContext context) : base(context)
        {
            
        }

        
    }
}
