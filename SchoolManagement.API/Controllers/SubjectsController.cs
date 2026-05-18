using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.DTOs.Subjects;
using SchoolManagement.Application.Interfaces;

namespace SchoolManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SubjectsController : ControllerBase
{
    private readonly ISubjectService _subjectService;

    public SubjectsController(ISubjectService subjectService)
    {
        _subjectService = subjectService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _subjectService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _subjectService.GetByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateSubjectDto dto)
    {
        var id = await _subjectService.CreateAsync(dto);
        return Ok(new { SubjectId = id });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateSubjectDto dto)
    {
        var result = await _subjectService.UpdateAsync(id, dto);

        if (!result)
            return NotFound();

        return Ok("Subject updated successfully");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _subjectService.DeleteAsync(id);

        if (!result)
            return NotFound();

        return Ok("Subject deleted successfully");
    }
}