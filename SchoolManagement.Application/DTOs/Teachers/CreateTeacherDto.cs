namespace SchoolManagement.Application.DTOs.Teachers;

public class CreateTeacherDto
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public int Gender { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string Qualification { get; set; } = string.Empty;

    public int ExperienceYears { get; set; }

    public string PhoneNumber { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public DateTime JoiningDate { get; set; }

    public decimal Salary { get; set; }

    // OPTIONAL: link teacher to login account
    public Guid? UserId { get; set; }
}