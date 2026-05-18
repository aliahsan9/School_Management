namespace SchoolManagement.Application.DTOs.Teachers;

public class TeacherResponseDto
{
    public Guid Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Gender { get; set; } = string.Empty;

    public string Qualification { get; set; } = string.Empty;

    public int ExperienceYears { get; set; }

    public string PhoneNumber { get; set; } = string.Empty;

    public decimal Salary { get; set; }

    public bool IsActive { get; set; }

    public Guid TenantId { get; set; }
}