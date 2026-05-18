using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.DTOs.Attendance;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Enums;
using SchoolManagement.Infrastructure.Persistence;

namespace SchoolManagement.Infrastructure.Services;

public class AttendanceService : IAttendanceService
{
    private readonly AppDbContext _context;
    private readonly ITenantProvider _tenantProvider;

    public AttendanceService(AppDbContext context, ITenantProvider tenantProvider)
    {
        _context = context;
        _tenantProvider = tenantProvider;
    }

    public async Task<bool> MarkAttendanceAsync(MarkAttendanceDto dto)
    {
        var tenantId = _tenantProvider.GetTenantId();

        // Prevent duplicate attendance for same class + date
        var exists = await _context.Attendances
            .AnyAsync(x =>
                x.ClassId == dto.ClassId &&
                x.Date.Date == dto.Date.Date);

        if (exists)
            throw new Exception("Attendance already marked for this class on this date.");

        var records = dto.Students.Select(s => new Attendance
        {
            TenantId = tenantId,
            ClassId = dto.ClassId,
            StudentId = s.StudentId,
            Date = dto.Date.Date,
            Status = (AttendanceStatus)s.Status
        }).ToList();

        await _context.Attendances.AddRangeAsync(records);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<AttendanceResponseDto>> GetClassAttendanceAsync(Guid classId, DateTime date)
    {
        var data = await _context.Attendances
            .Include(x => x.Student)
            .Where(x =>
                x.ClassId == classId &&
                x.Date.Date == date.Date)
            .ToListAsync();

        return data.Select(x => new AttendanceResponseDto
        {
            StudentId = x.StudentId,
            StudentName = x.Student.FirstName + " " + x.Student.LastName,
            Status = x.Status.ToString(),
            Date = x.Date
        }).ToList();
    }

    public async Task<List<AttendanceResponseDto>> GetStudentAttendanceAsync(Guid studentId)
    {
        var data = await _context.Attendances
            .Include(x => x.Student)
            .Where(x => x.StudentId == studentId)
            .OrderByDescending(x => x.Date)
            .ToListAsync();

        return data.Select(x => new AttendanceResponseDto
        {
            StudentId = x.StudentId,
            StudentName = x.Student.FirstName + " " + x.Student.LastName,
            Status = x.Status.ToString(),
            Date = x.Date
        }).ToList();
    }
}