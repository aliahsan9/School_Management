using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SchoolManagement.Application.DTOs.Attendance;
using SchoolManagement.Application.Interfaces;

namespace SchoolManagement.API.Controllers;

[ApiController]

[Route("api/[controller]")]

[Authorize]
public class AttendanceController : ControllerBase
{
    private readonly IAttendanceService
        _attendanceService;

    public AttendanceController(
        IAttendanceService attendanceService
    )
    {
        _attendanceService = attendanceService;
    }

    // ============================
    // MARK ATTENDANCE
    // ============================

    [HttpPost("mark")]
    public async Task<IActionResult>
        MarkAttendance(
            [FromBody] MarkAttendanceDto dto
        )
    {
        var result =
            await _attendanceService
            .MarkAttendanceAsync(dto);

        return Ok(new
        {
            message =
                "Attendance marked successfully",

            success = result
        });
    }

    // ============================
    // GET CLASS ATTENDANCE
    // ============================

    [HttpGet("class")]
    public async Task<IActionResult>
        GetClassAttendance(
            [FromQuery] Guid classId,

            [FromQuery] DateTime date
        )
    {
        var result =
            await _attendanceService
            .GetClassAttendanceAsync(
                classId,
                date
            );

        return Ok(result);
    }

    // ============================
    // GET STUDENT ATTENDANCE
    // ============================

    [HttpGet("student/{studentId}")]
    public async Task<IActionResult>
        GetStudentAttendance(
            Guid studentId
        )
    {
        var result =
            await _attendanceService
            .GetStudentAttendanceAsync(
                studentId
            );

        return Ok(result);
    }
}