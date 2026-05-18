using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.DTOs.Classes;
using SchoolManagement.Application.Interfaces;

namespace SchoolManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ClassesController : ControllerBase
{
    private readonly IClassService _classService;

    public ClassesController(IClassService classService)
    {
        _classService = classService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _classService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _classService.GetByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateClassDto dto)
    {
        var id = await _classService.CreateAsync(dto);
        return Ok(new { ClassId = id });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateClassDto dto)
    {
        var result = await _classService.UpdateAsync(id, dto);

        if (!result)
            return NotFound();

        return Ok("Class updated successfully");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _classService.DeleteAsync(id);

        if (!result)
            return NotFound();

        return Ok("Class deleted successfully");
    }
}