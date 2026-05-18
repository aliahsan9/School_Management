using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain.Entities;

public class Subject : TenantEntity
{
    public string Name { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;

    public string? Description { get; set; }

    // Navigation
    public Tenant Tenant { get; set; } = null!;

    public ICollection<ClassSubject> ClassSubjects { get; set; } = new List<ClassSubject>();
}