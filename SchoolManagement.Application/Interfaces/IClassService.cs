using SchoolManagement.Application.DTOs.Classes;

namespace SchoolManagement.Application.Interfaces;

public interface IClassService
{
    Task<List<ClassResponseDto>> GetAllAsync();

    Task<ClassResponseDto?> GetByIdAsync(Guid id);

    Task<Guid> CreateAsync(CreateClassDto dto);

    Task<bool> UpdateAsync(Guid id, UpdateClassDto dto);

    Task<bool> DeleteAsync(Guid id);
}