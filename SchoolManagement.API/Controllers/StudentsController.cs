using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.DTOs.Students;
using SchoolManagement.Application.Interfaces;

namespace SchoolManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _studentService.GetAllAsync();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _studentService.GetByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateStudentDto dto)
    {
        var id = await _studentService.CreateAsync(dto);

        return Ok(new
        {
            Message = "Student created successfully",
            StudentId = id
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateStudentDto dto)
    {
        var result = await _studentService.UpdateAsync(id, dto);

        if (!result)
            return NotFound(new
            {
                Message = "Student not found"
            });

        return Ok(new
        {
            Message = "Student updated successfully"
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _studentService.DeleteAsync(id);

        if (!result)
            return NotFound(new
            {
                Message = "Student not found"
            });

        return Ok(new
        {
            Message = "Student deleted successfully"
        });
    }
}