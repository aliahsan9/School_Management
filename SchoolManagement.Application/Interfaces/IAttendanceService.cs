using SchoolManagement.Application.DTOs.Attendance;

namespace SchoolManagement.Application.Interfaces;

public interface IAttendanceService
{
    Task<bool> MarkAttendanceAsync(
        MarkAttendanceDto dto
    );

    Task<List<AttendanceResponseDto>>
        GetClassAttendanceAsync(
            Guid classId,
            DateTime date
        );

    Task<List<AttendanceResponseDto>>
        GetStudentAttendanceAsync(
            Guid studentId
        );
}