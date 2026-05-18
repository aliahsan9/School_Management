namespace SchoolManagement.Application.DTOs.Classes;

public class CreateClassDto
{
    public string Name { get; set; } = string.Empty;   // e.g. "Grade 10"

    public string Section { get; set; } = string.Empty; // e.g. "A"

    public string? RoomNumber { get; set; }

    public Guid? ClassTeacherId { get; set; }
}