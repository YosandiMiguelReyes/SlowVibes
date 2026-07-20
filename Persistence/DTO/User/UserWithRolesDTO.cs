using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.DTO.User
{
    public class UserWithRolesDTO
    {
        public string FullName { get; set; } //max length 150
        public string? UserName { get; set; } //max length 50
        public string Email { get; set; } //max length 150 
        public string? Phone { get; set; } //max length 20
        public bool IsActive { get; set; }
        public List<string> Roles { get; set; }
    }
}
