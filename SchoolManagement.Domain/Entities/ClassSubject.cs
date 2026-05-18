namespace SchoolManagement.Domain.Entities;

public class ClassSubject
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid ClassId { get; set; }

    public Guid SubjectId { get; set; }

    public Guid? TeacherId { get; set; }

    // Navigation
    public Class Class { get; set; } = null!;

    public Subject Subject { get; set; } = null!;

    public Teacher? Teacher { get; set; }
}