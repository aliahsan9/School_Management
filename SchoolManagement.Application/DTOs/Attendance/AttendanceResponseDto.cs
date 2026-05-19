namespace SchoolManagement.Application.DTOs.Attendance;

public class AttendanceResponseDto
{
    public Guid Id { get; set; }

    public Guid StudentId { get; set; }

    public Guid ClassId { get; set; }

    public string StudentName { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public DateTime Date { get; set; }
}