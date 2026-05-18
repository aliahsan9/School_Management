using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain.Entities;

public class User : TenantEntity
{
    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    // Navigation
    public Tenant Tenant { get; set; } = null!;

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    public Student? Student { get; set; }

    public Teacher? Teacher { get; set; }
}