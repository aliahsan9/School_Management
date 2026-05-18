using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.DTOs.Students;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Enums;
using SchoolManagement.Infrastructure.Persistence;

namespace SchoolManagement.Infrastructure.Services;

public class StudentService : IStudentService
{
    private readonly AppDbContext _context;
    private readonly ITenantProvider _tenantProvider;

    public StudentService(
        AppDbContext context,
        ITenantProvider tenantProvider)
    {
        _context = context;
        _tenantProvider = tenantProvider;
    }

    public async Task<Guid> CreateAsync(CreateStudentDto dto)
    {
        var tenantId = _tenantProvider.GetTenantId();

        var student = new Student
        {
            TenantId = tenantId,
            AdmissionNumber = dto.AdmissionNumber,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Gender = (Gender)dto.Gender,
            DateOfBirth = dto.DateOfBirth,
            FatherName = dto.FatherName,
            MotherName = dto.MotherName,
            PhoneNumber = dto.PhoneNumber,
            Address = dto.Address,
            AdmissionDate = dto.AdmissionDate,
            IsActive = true
        };

        await _context.Students.AddAsync(student);

        // Optional: Assign to class if provided
        if (dto.ClassId.HasValue)
        {
            var studentClass = new StudentClass
            {
                StudentId = student.Id,
                ClassId = dto.ClassId.Value,
                AcademicYear = DateTime.UtcNow.Year.ToString(),
                AssignedAt = DateTime.UtcNow
            };

            await _context.StudentClasses.AddAsync(studentClass);
        }

        await _context.SaveChangesAsync();

        return student.Id;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var student = await _context.Students
            .FirstOrDefaultAsync(x => x.Id == id);

        if (student == null)
            return false;

        _context.Students.Remove(student);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<StudentResponseDto>> GetAllAsync()
    {
        var students = await _context.Students
            .AsNoTracking()
            .ToListAsync();

        return students.Select(s => new StudentResponseDto
        {
            Id = s.Id,
            AdmissionNumber = s.AdmissionNumber,
            FullName = s.FirstName + " " + s.LastName,
            Gender = s.Gender.ToString(),
            DateOfBirth = s.DateOfBirth,
            PhoneNumber = s.PhoneNumber,
            Address = s.Address,
            IsActive = s.IsActive,
            TenantId = s.TenantId
        }).ToList();
    }

    public async Task<StudentResponseDto?> GetByIdAsync(Guid id)
    {
        var s = await _context.Students
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (s == null)
            return null;

        return new StudentResponseDto
        {
            Id = s.Id,
            AdmissionNumber = s.AdmissionNumber,
            FullName = s.FirstName + " " + s.LastName,
            Gender = s.Gender.ToString(),
            DateOfBirth = s.DateOfBirth,
            PhoneNumber = s.PhoneNumber,
            Address = s.Address,
            IsActive = s.IsActive,
            TenantId = s.TenantId
        };
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateStudentDto dto)
    {
        var student = await _context.Students
            .FirstOrDefaultAsync(x => x.Id == id);

        if (student == null)
            return false;

        student.FirstName = dto.FirstName;
        student.LastName = dto.LastName;
        student.PhoneNumber = dto.PhoneNumber;
        student.Address = dto.Address;
        student.IsActive = dto.IsActive;

        await _context.SaveChangesAsync();

        return true;
    }
}