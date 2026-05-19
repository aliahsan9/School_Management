namespace SchoolManagement.Domain.Entities;

public class StudentClass
{
    public Guid? Id { get; set; } = Guid.NewGuid();

    public Guid? StudentId { get; set; }

    public Guid? ClassId { get; set; }

    public string AcademicYear { get; set; } = string.Empty;

    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public Student Student { get; set; } = null!;

    public Class Class { get; set; } = null!;
}