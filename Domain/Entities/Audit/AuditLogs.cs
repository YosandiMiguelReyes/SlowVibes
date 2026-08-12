

using System.Data;
using Domain.Base;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Domain.Entities.Audit
{
    public class AuditLogs : BaseEntity<int>, ICreatedAt
    {
        public int? UserId { get; private set; }
        public string? Action { get; private set; } //max length 100
        public string? Entity { get; private set; } //max length 100
        public int? EntityId { get; private set; }
        public string Details { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private AuditLogs (){}

        private AuditLogs (int? userId, string? action, string? entity, int entityId, string details)
        {
            UserId = userId;
            Action = action;
            Entity = entity;
            EntityId = entityId;
            Details = details;
            CreatedAt = DateTime.UtcNow;
        }

        public static AuditLogs CreateAuditLogs(int? userId, string? action, string? entity, int entityId, string details)
        {
            if(string.IsNullOrWhiteSpace(action))
                throw new DomainException("La accion del log de auditoria es obligatoria.");
            if(string.IsNullOrWhiteSpace(entity))
                throw new DomainException("La entidad afectada es obligatoria.");
            if(entityId <= 0)
                throw new DomainException("El log debe estar asociado a un registro valido.");



            return new AuditLogs(userId, action, entity, entityId, details ?? string.Empty);
        }

    }
}
