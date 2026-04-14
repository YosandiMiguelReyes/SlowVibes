using Domain.Base;

namespace Domain.Entities.Roles
{
    public class Roles : BaseEntity<int>
    {
        public string Name { get; set; } //max length 50
    }
}
