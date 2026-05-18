using SchoolManagement.Application.DTOs.Payments;

namespace SchoolManagement.Application.Interfaces;

public interface IPaymentService
{
    Task<Guid> CreatePaymentAsync(CreatePaymentDto dto);

    Task<List<PaymentResponseDto>> GetPaymentsByFeeAsync(Guid feeId);
}