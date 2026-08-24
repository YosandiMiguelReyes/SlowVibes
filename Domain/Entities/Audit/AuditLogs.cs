using Domain.Base;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Domain.Entities.Audit
{
    public class AuditLogs : BaseEntity<int>, ICreatedAt
    {
        public int? UserId { get; private set; }
        public string Action { get; private set; } = string.Empty;
        public string Entity { get; private set; } = string.Empty;
        public int EntityId { get; private set; }
        public string Details { get; private set; } = string.Empty;
        public DateTimeOffset CreatedAt { get; private set; }

        private AuditLogs (){}

        private AuditLogs (int? userId, string action, string entity, int entityId, string? details)
        {
            UserId = userId;
            Action = action;
            Entity = entity;
            EntityId = entityId;
            Details = details?.Trim() ?? string.Empty;
            CreatedAt = DateTimeOffset.UtcNow;
        }

        public static AuditLogs Create(int? userId, string action, string entity, int entityId, string? details)
        {
            if(string.IsNullOrWhiteSpace(action))
                throw new DomainException("La acción del log de auditoría es obligatoria.");
            if(string.IsNullOrWhiteSpace(entity))
                throw new DomainException("La entidad afectada es obligatoria.");
            if(entityId <= 0)
                throw new DomainException("El log debe estar asociado a un registro válido.");



            return new AuditLogs(userId, action.Trim(), entity.Trim(), entityId, details ?? string.Empty);
        }

    }
}
