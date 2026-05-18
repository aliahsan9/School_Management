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

    public TeachersController(ITeacherService teacherService)
    {
        _teacherService = teacherService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _teacherService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _teacherService.GetByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTeacherDto dto)
    {
        var id = await _teacherService.CreateAsync(dto);
        return Ok(new { TeacherId = id });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateTeacherDto dto)
    {
        var result = await _teacherService.UpdateAsync(id, dto);

        if (!result)
            return NotFound();

        return Ok("Teacher updated successfully");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _teacherService.DeleteAsync(id);

        if (!result)
            return NotFound();

        return Ok("Teacher deleted successfully");
    }
}