using Domain.Entities.User;
using Persistence.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Persistence.Mappers.UserMappers
{
    public static class UserMapper
    {
        public static readonly Expression<Func<Users, UserWithRolesDTO>> AsUserWithRole = u => new UserWithRolesDTO
        {
            FullName = u.FullName,
            UserName = u.UserName ?? "Sin nombre de usuario",
            Email = u.Email,
            Phone = u.Phone ?? "Sin telefono",
            IsActive = u.IsActive,
            Roles = u.UsersRoles.Select(ur => ur.Role.Name).ToList()
        };

        public static readonly Expression<Func<Users, AdminUserWithRole>> AsAdminUserWithRole = u => new AdminUserWithRole
        {
            FullName = u.FullName,
            UserName = u.UserName ?? "Sin nombre de usuario",
            Email = u.Email,
            Phone = u.Phone ?? "Sin telefono",
            IsActive = u.IsActive,
            CreatedAt = u.CreatedAt,
            Roles = u.UsersRoles.Select(ur => ur.Role.Name).ToList()
        };
    }
}
