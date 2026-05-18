namespace SchoolManagement.Application.DTOs.Classes;

public class ClassResponseDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Section { get; set; } = string.Empty;

    public string? RoomNumber { get; set; }

    public Guid? ClassTeacherId { get; set; }

    public Guid TenantId { get; set; }
}