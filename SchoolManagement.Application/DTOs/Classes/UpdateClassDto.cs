namespace SchoolManagement.Application.DTOs.Classes;

public class UpdateClassDto
{
    public string Name { get; set; } = string.Empty;

    public string Section { get; set; } = string.Empty;

    public string? RoomNumber { get; set; }

    public Guid? ClassTeacherId { get; set; }
}