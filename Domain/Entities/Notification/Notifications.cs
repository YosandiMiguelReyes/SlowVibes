using Domain.Base;
using Domain.Interfaces;

namespace Domain.Entities.Notification
{
    public class Notifications : BaseEntity<int>, ICreatedAt
    {
        public string? Type { get; set; } //max 20 characters
        public string? Recipient { get; set; } //max 150 characters
        public string? Message { get; set; }
        public string? Status { get; set; } //max 20 characters
        public DateTime CreatedAt { get; set; }
        public DateTime SentAt { get; set; }
    }
}
