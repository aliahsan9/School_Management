using SchoolManagement.Domain.Common;
using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Domain.Entities;

public class Student : TenantEntity
{
    public Guid? UserId { get; set; }

    public string AdmissionNumber { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public Gender Gender { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string FatherName { get; set; } = string.Empty;

    public string MotherName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public DateTime AdmissionDate { get; set; }

    public bool IsActive { get; set; } = true;

    // Navigation
    public Tenant Tenant { get; set; } = null!;

    public User? User { get; set; }

    public ICollection<StudentClass> StudentClasses { get; set; } = new List<StudentClass>();

    public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public ICollection<Fee> Fees { get; set; } = new List<Fee>();
}