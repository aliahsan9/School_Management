namespace SchoolManagement.Application.DTOs.Payments;

public class CreatePaymentDto
{
    public Guid FeeId { get; set; }

    public decimal AmountPaid { get; set; }

    public string PaymentMethod { get; set; } = string.Empty;

    public string? TransactionReference { get; set; }

    public string? Notes { get; set; }
}