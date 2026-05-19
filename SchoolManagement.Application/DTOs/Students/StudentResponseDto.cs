namespace SchoolManagement.Application.DTOs.Students;

public class StudentResponseDto
{
    public Guid Id { get; set; }

    public string AdmissionNumber { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public int Gender { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string FatherName { get; set; } = string.Empty;

    public string MotherName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public DateTime AdmissionDate { get; set; }

    public bool IsActive { get; set; }

    public Guid TenantId { get; set; }
}