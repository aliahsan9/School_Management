using SchoolManagement.Application.DTOs.Teachers;

namespace SchoolManagement.Application.Interfaces;

public interface ITeacherService
{
    Task<List<TeacherResponseDto>> GetAllAsync();

    Task<TeacherResponseDto?> GetByIdAsync(Guid id);

    Task<Guid> CreateAsync(CreateTeacherDto dto);

    Task<bool> UpdateAsync(Guid id, UpdateTeacherDto dto);

    Task<bool> DeleteAsync(Guid id);
}