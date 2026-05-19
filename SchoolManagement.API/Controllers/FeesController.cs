using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SchoolManagement.Application.DTOs.Fees;
using SchoolManagement.Application.Interfaces;

namespace SchoolManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FeesController : ControllerBase
{
    private readonly IFeeService _feeService;

    public FeesController(IFeeService feeService)
    {
        _feeService = feeService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateFeeDto dto
    )
    {
        var id = await _feeService.CreateFeeAsync(dto);

        return Ok(new
        {
            feeId = id,
            message = "Fee created successfully"
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _feeService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _feeService.GetByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }
}