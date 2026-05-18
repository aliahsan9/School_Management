namespace SchoolManagement.Application.DTOs.Fees;

public class CreateFeeDto
{
    public Guid StudentId { get; set; }

    public string Title { get; set; } = string.Empty; // e.g. "Monthly Fee"

    public decimal Amount { get; set; }

    public DateTime DueDate { get; set; }
}