using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.DTOs.Payments;
using SchoolManagement.Application.Interfaces;

namespace SchoolManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentsController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePaymentDto dto)
    {
        var id = await _paymentService.CreatePaymentAsync(dto);
        return Ok(new { PaymentId = id });
    }

    [HttpGet("fee/{feeId}")]
    public async Task<IActionResult> GetByFee(Guid feeId)
    {
        var result = await _paymentService.GetPaymentsByFeeAsync(feeId);
        return Ok(result);
    }
}