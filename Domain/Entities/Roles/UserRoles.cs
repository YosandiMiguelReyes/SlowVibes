using Domain.Base;
using Domain.Entities.User;

namespace Domain.Entities.Roles
{
    public class UserRoles : BaseEntity<int>
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        //navegation properties
        public virtual Users User { get; set; }
        public virtual Roles Role { get; set; }
    }
}
