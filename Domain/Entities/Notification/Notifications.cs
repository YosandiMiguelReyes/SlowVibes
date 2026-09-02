using Domain.Base;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Entities.Notification.Enums;

namespace Domain.Entities.Notification
{
    public class Notifications : BaseEntity<int>, ICreatedAt
    {
        public string Type { get; private set; } = string.Empty; 
        public string Recipient { get; private set; } = string.Empty;
        public string Message { get; private set; } = string.Empty;

        public NotificationStatus Status { get; private set; } 

        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset? SentAt { get; private set; }

        private Notifications(){}

        private Notifications(string type, string recipient, string message)
        {
            Type = type;
            Recipient = recipient;
            Message = message;
            Status = NotificationStatus.Pending;
            CreatedAt = DateTimeOffset.UtcNow;
        }

        public static Notifications Create(string type, string recipient, string message)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new DomainException("El tipo de notificación es requerido.");

            if (string.IsNullOrWhiteSpace(recipient))
                throw new DomainException("El destinatario de la notificación es requerido.");

            if (string.IsNullOrWhiteSpace(message))
                throw new DomainException("El mensaje de la notificación es requerido.");

            return new Notifications(
                type.Trim(),
                recipient.Trim(),
                message.Trim());
        }

        public void MarkAsSent(DateTimeOffset sentAt)
        {
            EnsureIsPending();
            Status = NotificationStatus.Sent;
            SentAt = sentAt;
        }

        public void MarkAsFailed()
        {
            EnsureIsPending();
            Status = NotificationStatus.Failed;
        }

        public void EnsureIsPending()
        {
            if (Status != NotificationStatus.Pending)
                throw new DomainException(
                    "Solo se pueden modificar notificaciones pendientes.");
        }
    }
}
