namespace SchoolManagement.Application.DTOs.Attendance;

public class MarkAttendanceDto
{
    public Guid ClassId { get; set; }

    public DateTime Date { get; set; }

    public List<StudentAttendanceDto> Students { get; set; } = new();
}