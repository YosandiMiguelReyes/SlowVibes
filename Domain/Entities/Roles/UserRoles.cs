using Domain.Base;

namespace Domain.Entities.Roles
{
    public class UserRoles : BaseEntity<int>
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
