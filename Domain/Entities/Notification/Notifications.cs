using Domain.Base;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Entities.Notification.Enums;

namespace Domain.Entities.Notification
{
    public class Notifications : BaseEntity<int>, ICreatedAt
    {
        public string Type { get; private set; } = string.Empty; //max 20 characters
        public string Recipient { get; private set; } = string.Empty;//max 150 characters
        public string Message { get; private set; } = string.Empty;

        public NotificationStatus Status { get; private set; } //max 20 characters

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
                throw new DomainException("El tipo de notificacion es requerido.");

            if (string.IsNullOrWhiteSpace(recipient))
                throw new DomainException("El recibidor de la notificacion es requerido.");

            if (string.IsNullOrWhiteSpace(message))
                throw new DomainException("El mensaje de la notificacion es requerido.");

            return new Notifications(
                type.Trim(),
                recipient.Trim(),
                message.Trim());
        }

        public void MarkAsSent(DateTimeOffset sentAt)
        {
            if (Status == NotificationStatus.Sent)
                throw new DomainException(
                    "Notification has already been sent.");

            Status = NotificationStatus.Sent;
            SentAt = sentAt;
        }

        public void MarkAsFailed()
        {
            Status = NotificationStatus.Failed;
        }
    }
}
