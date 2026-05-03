

using Domain.Base;
using Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.User
{
    [Table("Users")]
    public class Users :BaseEntity<int>, IIsActive, ICreatedAt
    {
        public string FullName { get; set; } //max length 150
        public string? UserName { get; set; } //max length 50
        public string Email { get; set; } //max length 150 
        public string? Phone { get; set; } //max length 20
        public string PasswordHash { get; set; } //max length 255
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
