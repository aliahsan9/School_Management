namespace SchoolManagement.Domain.Entities;

public class Role
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = string.Empty;

    // Navigation
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}