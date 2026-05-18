using SchoolManagement.Domain.Common;
using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Domain.Entities;

public class Attendance : TenantEntity
{
    public Guid StudentId { get; set; }

    public Guid ClassId { get; set; }

    public DateTime Date { get; set; }

    public AttendanceStatus Status { get; set; }

    // Navigation
    public Tenant Tenant { get; set; } = null!;

    public Student Student { get; set; } = null!;

    public Class Class { get; set; } = null!;
}