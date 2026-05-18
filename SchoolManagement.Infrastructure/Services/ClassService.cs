using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.DTOs.Classes;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure.Persistence;

namespace SchoolManagement.Infrastructure.Services;

public class ClassService : IClassService
{
    private readonly AppDbContext _context;
    private readonly ITenantProvider _tenantProvider;

    public ClassService(AppDbContext context, ITenantProvider tenantProvider)
    {
        _context = context;
        _tenantProvider = tenantProvider;
    }

    public async Task<Guid> CreateAsync(CreateClassDto dto)
    {
        var classEntity = new Class
        {
            TenantId = _tenantProvider.GetTenantId(),

            Name = dto.Name,
            Section = dto.Section,
            RoomNumber = dto.RoomNumber,
            ClassTeacherId = dto.ClassTeacherId
        };

        await _context.Classes.AddAsync(classEntity);
        await _context.SaveChangesAsync();

        return classEntity.Id;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var classEntity = await _context.Classes
            .FirstOrDefaultAsync(x => x.Id == id);

        if (classEntity == null)
            return false;

        _context.Classes.Remove(classEntity);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<ClassResponseDto>> GetAllAsync()
    {
        var classes = await _context.Classes
            .AsNoTracking()
            .ToListAsync();

        return classes.Select(c => new ClassResponseDto
        {
            Id = c.Id,
            Name = c.Name,
            Section = c.Section,
            RoomNumber = c.RoomNumber,
            ClassTeacherId = c.ClassTeacherId,
            TenantId = c.TenantId
        }).ToList();
    }

    public async Task<ClassResponseDto?> GetByIdAsync(Guid id)
    {
        var c = await _context.Classes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (c == null)
            return null;

        return new ClassResponseDto
        {
            Id = c.Id,
            Name = c.Name,
            Section = c.Section,
            RoomNumber = c.RoomNumber,
            ClassTeacherId = c.ClassTeacherId,
            TenantId = c.TenantId
        };
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateClassDto dto)
    {
        var classEntity = await _context.Classes
            .FirstOrDefaultAsync(x => x.Id == id);

        if (classEntity == null)
            return false;

        classEntity.Name = dto.Name;
        classEntity.Section = dto.Section;
        classEntity.RoomNumber = dto.RoomNumber;
        classEntity.ClassTeacherId = dto.ClassTeacherId;

        await _context.SaveChangesAsync();
        return true;
    }
}