using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.DTOs.Teachers;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Enums;
using SchoolManagement.Infrastructure.Persistence;

namespace SchoolManagement.Infrastructure.Services;

public class TeacherService : ITeacherService
{
    private readonly AppDbContext _context;
    private readonly ITenantProvider _tenantProvider;

    public TeacherService(
        AppDbContext context,
        ITenantProvider tenantProvider)
    {
        _context = context;
        _tenantProvider = tenantProvider;
    }

    public async Task<Guid> CreateAsync(CreateTeacherDto dto)
    {
        // Validate UserId if provided
        if (dto.UserId.HasValue)
        {
            var userExists = await _context.Users
                .AnyAsync(x => x.Id == dto.UserId.Value);

            if (!userExists)
                throw new Exception("Selected user does not exist.");
        }

        var teacher = new Teacher
        {
            TenantId = _tenantProvider.GetTenantId(),

            FirstName = dto.FirstName,
            LastName = dto.LastName,

            Gender = (Gender)dto.Gender,
            DateOfBirth = dto.DateOfBirth,

            Qualification = dto.Qualification,
            ExperienceYears = dto.ExperienceYears,

            PhoneNumber = dto.PhoneNumber,
            Address = dto.Address,

            JoiningDate = dto.JoiningDate,
            Salary = dto.Salary,

            // IMPORTANT FIX
            UserId = dto.UserId ?? null,

            IsActive = true
        };

        await _context.Teachers.AddAsync(teacher);

        await _context.SaveChangesAsync();

        return teacher.Id;
    }

    public async Task<List<TeacherResponseDto>> GetAllAsync()
    {
        var teachers = await _context.Teachers
            .AsNoTracking()
            .ToListAsync();

        return teachers.Select(t => new TeacherResponseDto
        {
            Id = t.Id,
            FullName = $"{t.FirstName} {t.LastName}",
            Gender = t.Gender.ToString(),

            Qualification = t.Qualification,
            ExperienceYears = t.ExperienceYears,

            PhoneNumber = t.PhoneNumber,

            Salary = t.Salary,

            IsActive = t.IsActive,

            TenantId = t.TenantId
        }).ToList();
    }

    public async Task<TeacherResponseDto?> GetByIdAsync(Guid id)
    {
        var teacher = await _context.Teachers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (teacher == null)
            return null;

        return new TeacherResponseDto
        {
            Id = teacher.Id,
            FullName = $"{teacher.FirstName} {teacher.LastName}",
            Gender = teacher.Gender.ToString(),

            Qualification = teacher.Qualification,
            ExperienceYears = teacher.ExperienceYears,

            PhoneNumber = teacher.PhoneNumber,

            Salary = teacher.Salary,

            IsActive = teacher.IsActive,

            TenantId = teacher.TenantId
        };
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateTeacherDto dto)
    {
        var teacher = await _context.Teachers
            .FirstOrDefaultAsync(x => x.Id == id);

        if (teacher == null)
            return false;

        teacher.FirstName = dto.FirstName;
        teacher.LastName = dto.LastName;

        teacher.PhoneNumber = dto.PhoneNumber;
        teacher.Address = dto.Address;

        teacher.ExperienceYears = dto.ExperienceYears;

        teacher.Salary = dto.Salary;

        teacher.IsActive = dto.IsActive;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var teacher = await _context.Teachers
            .FirstOrDefaultAsync(x => x.Id == id);

        if (teacher == null)
            return false;

        _context.Teachers.Remove(teacher);

        await _context.SaveChangesAsync();

        return true;
    }
}