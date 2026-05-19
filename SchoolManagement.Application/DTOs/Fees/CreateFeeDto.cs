using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Application.DTOs.Fees;

public class CreateFeeDto
{
    [Required]
    public Guid StudentId { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    [Range(1, double.MaxValue)]
    public decimal Amount { get; set; }

    [Required]
    public DateTime DueDate { get; set; }
}