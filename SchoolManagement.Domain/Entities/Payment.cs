using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain.Entities;

public class Payment : TenantEntity
{
    public Guid FeeId { get; set; }

    public decimal AmountPaid { get; set; }

    public DateTime PaymentDate { get; set; }

    public string PaymentMethod { get; set; } = string.Empty;

    public string? TransactionReference { get; set; }

    public string? Notes { get; set; }

    // Navigation
    public Tenant Tenant { get; set; } = null!;

    public Fee Fee { get; set; } = null!;
}