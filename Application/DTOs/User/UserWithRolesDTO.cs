namespace Application.DTOs.User;

public class UserWithRolesDTO
{
    public string FullName { get; set; } = string.Empty;
    public string? UserName { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public bool IsActive { get; set; }
    public List<string> Roles { get; set; } = [];
}
