using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Application.DTOs.Attendance;

public class StudentAttendanceDto
{
    [Required]
    public Guid StudentId { get; set; }

    [Required]
    public int Status { get; set; }
}