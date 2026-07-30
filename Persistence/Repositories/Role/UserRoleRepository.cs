using Domain.Entities.Roles;
using Microsoft.EntityFrameworkCore;
using Persistence.BaseRepository;
using Persistence.Context;
using Persistence.Interfaces.Role;
using Domain.Result;

namespace Persistence.Repositories.Role
{
    public class UserRoleRepository : BaseRepository<UserRoles, int>, IUserRolesRepository
    {
        public UserRoleRepository(SlowVibesDbContext context) : base(context)
        {
            
        }

        public async Task<OperationResult<bool>> RemoveRoleFromUserAsync(int userId, int roleId)
        {
            var UserRole = await _dbSet
                            .FirstOrDefaultAsync(ur => ur.RoleId == roleId && ur.UserId == userId);
            
            if(UserRole == null)
            {
                return new OperationResult<bool>
                {
                    IsSuccess = false,
                    Message = "El Usuario no tiene asignado ese rol.",
                    Data = false
                };
            }

            _dbSet.Remove(UserRole);

            return new OperationResult<bool>
            {
                IsSuccess = true,
                Message = "El Rol ha sido removido correctamente",
                Data = true
            };
        }
    }
}
