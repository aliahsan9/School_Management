using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.DTOs.Payments;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure.Persistence;

namespace SchoolManagement.Infrastructure.Services;

public class PaymentService : IPaymentService
{
    private readonly AppDbContext _context;
    private readonly ITenantProvider _tenantProvider;

    public PaymentService(AppDbContext context, ITenantProvider tenantProvider)
    {
        _context = context;
        _tenantProvider = tenantProvider;
    }

    public async Task<Guid> CreatePaymentAsync(CreatePaymentDto dto)
    {
        var payment = new Payment
        {
            TenantId = _tenantProvider.GetTenantId(),
            FeeId = dto.FeeId,
            AmountPaid = dto.AmountPaid,
            PaymentMethod = dto.PaymentMethod,
            TransactionReference = dto.TransactionReference,
            Notes = dto.Notes,
            PaymentDate = DateTime.UtcNow
        };

        await _context.Payments.AddAsync(payment);
        await _context.SaveChangesAsync();

        return payment.Id;
    }

    public async Task<List<PaymentResponseDto>> GetPaymentsByFeeAsync(Guid feeId)
    {
        var payments = await _context.Payments
            .Where(x => x.FeeId == feeId)
            .AsNoTracking()
            .ToListAsync();

        return payments.Select(p => new PaymentResponseDto
        {
            Id = p.Id,
            FeeId = p.FeeId,
            AmountPaid = p.AmountPaid,
            PaymentMethod = p.PaymentMethod,
            PaymentDate = p.PaymentDate
        }).ToList();
    }
}