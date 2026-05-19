using Microsoft.EntityFrameworkCore;

using SchoolManagement.Application.DTOs.Fees;
using SchoolManagement.Application.Interfaces;

using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Enums;

using SchoolManagement.Infrastructure.Persistence;

namespace SchoolManagement.Infrastructure.Services;

public class FeeService : IFeeService
{
    private readonly AppDbContext _context;
    private readonly ITenantProvider _tenantProvider;

    public FeeService(
        AppDbContext context,
        ITenantProvider tenantProvider
    )
    {
        _context = context;
        _tenantProvider = tenantProvider;
    }

    public async Task<Guid> CreateFeeAsync(CreateFeeDto dto)
    {
        var tenantId = _tenantProvider.GetTenantId();

        var fee = new Fee
        {
            TenantId = tenantId,
            StudentId = dto.StudentId,
            Title = dto.Title,
            Amount = dto.Amount,
            DueDate = dto.DueDate,
            Status = FeeStatus.Pending
        };

        await _context.Fees.AddAsync(fee);
        await _context.SaveChangesAsync();

        return fee.Id;
    }

    public async Task<List<FeeResponseDto>> GetAllAsync()
    {
        var tenantId = _tenantProvider.GetTenantId();

        var fees = await _context.Fees
            .Include(x => x.Student)
            .Include(x => x.Payments)
            .Where(x => x.TenantId == tenantId)
            .AsNoTracking()
            .ToListAsync();

        return fees.Select(f =>
        {
            var paid = f.Payments?.Sum(p => p.AmountPaid) ?? 0;

            return new FeeResponseDto
            {
                Id = f.Id,
                StudentId = f.StudentId,
                StudentName =
                    f.Student != null
                        ? $"{f.Student.FirstName} {f.Student.LastName}"
                        : "Unknown",

                Title = f.Title,
                Amount = f.Amount,
                PaidAmount = paid,
                RemainingAmount = f.Amount - paid,
                Status = CalculateStatus(f.Amount, paid).ToString(),
                DueDate = f.DueDate
            };
        }).ToList();
    }

    public async Task<FeeResponseDto?> GetByIdAsync(Guid id)
    {
        var tenantId = _tenantProvider.GetTenantId();

        var f = await _context.Fees
            .Include(x => x.Student)
            .Include(x => x.Payments)
            .Where(x => x.TenantId == tenantId)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (f == null)
            return null;

        var paid = f.Payments?.Sum(p => p.AmountPaid) ?? 0;

        return new FeeResponseDto
        {
            Id = f.Id,
            StudentId = f.StudentId,
            StudentName =
                f.Student != null
                    ? $"{f.Student.FirstName} {f.Student.LastName}"
                    : "Unknown",

            Title = f.Title,
            Amount = f.Amount,
            PaidAmount = paid,
            RemainingAmount = f.Amount - paid,
            Status = CalculateStatus(f.Amount, paid).ToString(),
            DueDate = f.DueDate
        };
    }

    private FeeStatus CalculateStatus(decimal total, decimal paid)
    {
        if (paid <= 0) return FeeStatus.Pending;
        if (paid < total) return FeeStatus.Partial;
        return FeeStatus.Paid;
    }
}