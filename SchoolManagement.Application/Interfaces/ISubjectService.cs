using SchoolManagement.Application.DTOs.Subjects;

namespace SchoolManagement.Application.Interfaces;

public interface ISubjectService
{
    Task<List<SubjectResponseDto>> GetAllAsync();

    Task<SubjectResponseDto?> GetByIdAsync(Guid id);

    Task<Guid> CreateAsync(CreateSubjectDto dto);

    Task<bool> UpdateAsync(Guid id, UpdateSubjectDto dto);

    Task<bool> DeleteAsync(Guid id);
}