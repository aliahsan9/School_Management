using SchoolManagement.Domain.Common;
using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Domain.Entities;

public class Teacher : TenantEntity
{
    public Guid UserId { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public Gender Gender { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string Qualification { get; set; } = string.Empty;

    public int ExperienceYears { get; set; }

    public string PhoneNumber { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public DateTime JoiningDate { get; set; }

    public decimal Salary { get; set; }

    public bool IsActive { get; set; } = true;

    // Navigation
    public Tenant Tenant { get; set; } = null!;

    public User User { get; set; } = null!;

    public ICollection<Class> Classes { get; set; } = new List<Class>();

    public ICollection<ClassSubject> ClassSubjects { get; set; } = new List<ClassSubject>();
}