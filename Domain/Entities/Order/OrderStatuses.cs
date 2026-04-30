using Domain.Base;

namespace Domain.Entities.Order
{
    public class OrderStatuses : BaseEntity<int>
    {
        public string Name { get; set; } //max length 30
    }
}
