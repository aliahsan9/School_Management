namespace SchoolManagement.Application.DTOs.Payments;

public class PaymentResponseDto
{
    public Guid Id { get; set; }

    public Guid FeeId { get; set; }

    public decimal AmountPaid { get; set; }

    public string PaymentMethod { get; set; } = string.Empty;

    public DateTime PaymentDate { get; set; }
}