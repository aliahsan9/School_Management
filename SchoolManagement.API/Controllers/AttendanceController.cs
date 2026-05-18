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

    [HttpPost("mark")]
    public async Task<IActionResult> MarkAttendance(MarkAttendanceDto dto)
    {
        var result = await _attendanceService.MarkAttendanceAsync(dto);
        return Ok("Attendance marked successfully");
    }

    [HttpGet("class")]
    public async Task<IActionResult> GetClassAttendance(Guid classId, DateTime date)
    {
        var result = await _attendanceService.GetClassAttendanceAsync(classId, date);
        return Ok(result);
    }

    [HttpGet("student/{studentId}")]
    public async Task<IActionResult> GetStudentAttendance(Guid studentId)
    {
        var result = await _attendanceService.GetStudentAttendanceAsync(studentId);
        return Ok(result);
    }
}