

using Domain.Base;
using Domain.Interfaces;

namespace Domain.Entities.User
{
    public class User :BaseEntity<int>, IIsActive, ICreatedAt
    {
        public string? FullName { get; set; } //max length 150
        public string UserName { get; set; } //max length 50
        public string? Email { get; set; } //max length 150 
        public string Phone { get; set; } //max length 20
        public string? PasswordHash { get; set; } //max length 255
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }

    }
}
