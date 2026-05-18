using SchoolManagement.Domain.Common;
using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Domain.Entities;

public class Fee : TenantEntity
{
    public Guid StudentId { get; set; }

    public string Title { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public DateTime DueDate { get; set; }

    public FeeStatus Status { get; set; }

    // Navigation
    public Tenant Tenant { get; set; } = null!;

    public Student Student { get; set; } = null!;

    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}