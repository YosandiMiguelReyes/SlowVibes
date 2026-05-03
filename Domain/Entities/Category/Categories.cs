using Domain.Base;
using Domain.Interfaces;
namespace Domain.Entities.Category
{
    public class Categories : BaseEntity<int>, IIsActive
    {
        public string? Name { get; set; } //max length 100
        public bool IsActive { get; set; }
    }
}
