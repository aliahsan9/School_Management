using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.DTOs.Subjects;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure.Persistence;

namespace SchoolManagement.Infrastructure.Services;

public class SubjectService : ISubjectService
{
    private readonly AppDbContext _context;
    private readonly ITenantProvider _tenantProvider;

    public SubjectService(AppDbContext context, ITenantProvider tenantProvider)
    {
        _context = context;
        _tenantProvider = tenantProvider;
    }

    public async Task<Guid> CreateAsync(CreateSubjectDto dto)
    {
        var subject = new Subject
        {
            TenantId = _tenantProvider.GetTenantId(),
            Name = dto.Name,
            Code = dto.Code,
            Description = dto.Description
        };

        await _context.Subjects.AddAsync(subject);
        await _context.SaveChangesAsync();

        return subject.Id;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var subject = await _context.Subjects
            .FirstOrDefaultAsync(x => x.Id == id);

        if (subject == null)
            return false;

        _context.Subjects.Remove(subject);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<SubjectResponseDto>> GetAllAsync()
    {
        var subjects = await _context.Subjects
            .AsNoTracking()
            .ToListAsync();

        return subjects.Select(s => new SubjectResponseDto
        {
            Id = s.Id,
            Name = s.Name,
            Code = s.Code,
            Description = s.Description,
            TenantId = s.TenantId
        }).ToList();
    }

    public async Task<SubjectResponseDto?> GetByIdAsync(Guid id)
    {
        var s = await _context.Subjects
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (s == null)
            return null;

        return new SubjectResponseDto
        {
            Id = s.Id,
            Name = s.Name,
            Code = s.Code,
            Description = s.Description,
            TenantId = s.TenantId
        };
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateSubjectDto dto)
    {
        var subject = await _context.Subjects
            .FirstOrDefaultAsync(x => x.Id == id);

        if (subject == null)
            return false;

        subject.Name = dto.Name;
        subject.Code = dto.Code;
        subject.Description = dto.Description;

        await _context.SaveChangesAsync();

        return true;
    }
}