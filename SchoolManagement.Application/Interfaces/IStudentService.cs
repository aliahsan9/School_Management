using SchoolManagement.Application.DTOs.Students;

namespace SchoolManagement.Application.Interfaces;

public interface IStudentService
{
    Task<List<StudentResponseDto>> GetAllAsync();

    Task<StudentResponseDto?> GetByIdAsync(Guid id);

    Task<Guid> CreateAsync(CreateStudentDto dto);

    Task<bool> UpdateAsync(Guid id, UpdateStudentDto dto);

    Task<bool> DeleteAsync(Guid id);
}