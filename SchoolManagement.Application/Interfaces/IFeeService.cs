using SchoolManagement.Application.DTOs.Fees;

namespace SchoolManagement.Application.Interfaces;

public interface IFeeService
{
    Task<Guid> CreateFeeAsync(CreateFeeDto dto);

    Task<List<FeeResponseDto>> GetAllAsync();

    Task<FeeResponseDto?> GetByIdAsync(Guid id);
}