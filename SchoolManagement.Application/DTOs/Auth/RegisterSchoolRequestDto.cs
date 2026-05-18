namespace SchoolManagement.Application.DTOs.Auth;

public class RegisterSchoolRequestDto
{
    public string SchoolName { get; set; } = string.Empty;

    public string SchoolEmail { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string AdminName { get; set; } = string.Empty;

    public string AdminEmail { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}