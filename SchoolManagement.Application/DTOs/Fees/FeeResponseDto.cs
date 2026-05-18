namespace SchoolManagement.Application.DTOs.Fees;

public class FeeResponseDto
{
    public Guid Id { get; set; }

    public Guid StudentId { get; set; }

    public string StudentName { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public decimal PaidAmount { get; set; }

    public decimal RemainingAmount { get; set; }

    public string Status { get; set; } = string.Empty;

    public DateTime DueDate { get; set; }
}