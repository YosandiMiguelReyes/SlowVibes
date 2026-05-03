

using Domain.Base;
using Domain.Interfaces;

namespace Domain.Entities.Audit
{
    public class AuditLogs : BaseEntity<int>, ICreatedAt
    {
        public int? UserId { get; set; }
        public string? Action { get; set; } //max length 100
        public string? Entity { get; set; } //max length 100
        public int? EntityId { get; set; }
        public string Details { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
