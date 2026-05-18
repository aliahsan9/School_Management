using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain.Entities;

public class Class : TenantEntity
{
    public string Name { get; set; } = string.Empty;

    public string Section { get; set; } = string.Empty;

    public string? RoomNumber { get; set; }

    public Guid? ClassTeacherId { get; set; }

    // Navigation
    public Tenant Tenant { get; set; } = null!;

    public Teacher? ClassTeacher { get; set; }

    public ICollection<StudentClass> StudentClasses { get; set; } = new List<StudentClass>();

    public ICollection<ClassSubject> ClassSubjects { get; set; } = new List<ClassSubject>();

    public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
}