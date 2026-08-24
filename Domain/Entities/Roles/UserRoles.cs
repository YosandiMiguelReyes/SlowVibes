using Domain.Base;
using Domain.Entities.Roles.Enums;
using Domain.Exceptions;

namespace Domain.Entities.Roles
{
    public class UserRoles : BaseEntity<int>
    {
        public int UserId { get; private set; }

        public Role Role { get; private set; }

        // EF Core
        private UserRoles() { }

        private UserRoles(int userId, Role role)
        {
            UserId = userId;
            Role = role;
        }

        public static UserRoles Create(int userId, Role role)
        {
            if (userId <= 0)
                throw new DomainException(
                    "Favor agregar un usuario válido.");

            return new UserRoles(userId, role);
        }

        public void ChangeRole(Role newRole)
        {
            Role = newRole;
        }
    }
}