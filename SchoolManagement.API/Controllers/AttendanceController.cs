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
    private readonly IAttendanceService _attendanceService;

    public AttendanceController(IAttendanceService attendanceService)
    {
        _attendanceService = attendanceService;
    }

    // ============================
    // MARK ATTENDANCE
    // ============================
    [HttpPost("mark")]
    public async Task<IActionResult> MarkAttendance([FromBody] MarkAttendanceDto dto)
    {
        if (dto == null)
        {
            return BadRequest(new
            {
                message = "Request body is null"
            });
        }

        try
        {
            var result = await _attendanceService.MarkAttendanceAsync(dto);

            return Ok(new
            {
                message = "Attendance marked successfully",
                success = result
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "Error while marking attendance",
                error = ex.Message
            });
        }
    }

    // ============================
    // GET CLASS ATTENDANCE
    // ============================
    [HttpGet("class")]
    public async Task<IActionResult> GetClassAttendance(
        [FromQuery] Guid classId,
        [FromQuery] DateTime date
    )
    {
        try
        {
            var result = await _attendanceService.GetClassAttendanceAsync(classId, date);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "Error fetching class attendance",
                error = ex.Message
            });
        }
    }

    // ============================
    // GET STUDENT ATTENDANCE
    // ============================
    [HttpGet("student/{studentId}")]
    public async Task<IActionResult> GetStudentAttendance(Guid studentId)
    {
        try
        {
            var result = await _attendanceService.GetStudentAttendanceAsync(studentId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "Error fetching student attendance",
                error = ex.Message
            });
        }
    }
}