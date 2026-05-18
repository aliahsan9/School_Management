namespace SchoolManagement.Application.DTOs.Teachers;

public class UpdateTeacherDto
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public int ExperienceYears { get; set; }

    public decimal Salary { get; set; }

    public bool IsActive { get; set; }
}