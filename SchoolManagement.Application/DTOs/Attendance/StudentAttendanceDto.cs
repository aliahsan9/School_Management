namespace SchoolManagement.Application.DTOs.Attendance;

public class StudentAttendanceDto
{
    public Guid StudentId { get; set; }

    public int Status { get; set; } // Present, Absent, Late, Leave
}