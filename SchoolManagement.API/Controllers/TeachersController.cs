using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.DTOs.Teachers;
using SchoolManagement.Application.Interfaces;

namespace SchoolManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TeachersController : ControllerBase
{
    private readonly ITeacherService _teacherService;

    public TeachersController(
        ITeacherService teacherService)
    {
        _teacherService = teacherService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var teachers = await _teacherService.GetAllAsync();

        return Ok(teachers);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var teacher = await _teacherService.GetByIdAsync(id);

        if (teacher == null)
        {
            return NotFound(new
            {
                message = "Teacher not found"
            });
        }

        return Ok(teacher);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateTeacherDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var teacherId =
            await _teacherService.CreateAsync(dto);

        return Ok(new
        {
            message = "Teacher created successfully",
            teacherId = teacherId
        });
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateTeacherDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updated =
            await _teacherService.UpdateAsync(id, dto);

        if (!updated)
        {
            return NotFound(new
            {
                message = "Teacher not found"
            });
        }

        return Ok(new
        {
            message = "Teacher updated successfully"
        });
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted =
            await _teacherService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound(new
            {
                message = "Teacher not found"
            });
        }

        return Ok(new
        {
            message = "Teacher deleted successfully"
        });
    }
}