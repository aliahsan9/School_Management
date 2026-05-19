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

    public AttendanceService(
        AppDbContext context,
        ITenantProvider tenantProvider
    )
    {
        _context = context;
        _tenantProvider = tenantProvider;
    }

    // ============================
    // MARK ATTENDANCE (FIXED)
    // ============================
    public async Task<bool> MarkAttendanceAsync(MarkAttendanceDto dto)
    {
        if (dto == null)
            throw new Exception("Request body is null.");

        if (dto.Students == null || dto.Students.Count == 0)
            throw new Exception("Students list cannot be empty.");

        var tenantId = _tenantProvider.GetTenantId();

        if (tenantId == Guid.Empty)
            throw new Exception("Invalid tenant.");

        var startDate = dto.Date.Date;
        var endDate = startDate.AddDays(1);

        // CHECK DUPLICATE (SAFE DATE RANGE QUERY)
        var alreadyExists = await _context.Attendances
            .AnyAsync(x =>
                x.TenantId == tenantId &&
                x.ClassId == dto.ClassId &&
                x.Date >= startDate &&
                x.Date < endDate
            );

        if (alreadyExists)
            throw new Exception("Attendance already marked for this class and date.");

        // CREATE RECORDS
        var records = dto.Students.Select(x => new Attendance
        {
            TenantId = tenantId,
            StudentId = x.StudentId,
            ClassId = dto.ClassId,
            Date = startDate,
            Status = (AttendanceStatus)x.Status
        }).ToList();

        await _context.Attendances.AddRangeAsync(records);
        await _context.SaveChangesAsync();

        return true;
    }

    // ============================
    // CLASS ATTENDANCE
    // ============================
    public async Task<List<AttendanceResponseDto>> GetClassAttendanceAsync(
        Guid classId,
        DateTime date
    )
    {
        var tenantId = _tenantProvider.GetTenantId();

        var startDate = date.Date;
        var endDate = startDate.AddDays(1);

        var data = await _context.Attendances
            .Include(x => x.Student)
            .Where(x =>
                x.TenantId == tenantId &&
                x.ClassId == classId &&
                x.Date >= startDate &&
                x.Date < endDate
            )
            .OrderBy(x => x.Student.FirstName)
            .ToListAsync();

        return data.Select(x => new AttendanceResponseDto
        {
            Id = x.Id,
            StudentId = x.StudentId,
            ClassId = x.ClassId,
            StudentName = $"{x.Student.FirstName} {x.Student.LastName}",
            Status = x.Status.ToString(),
            Date = x.Date
        }).ToList();
    }

    // ============================
    // STUDENT ATTENDANCE
    // ============================
    public async Task<List<AttendanceResponseDto>> GetStudentAttendanceAsync(
        Guid studentId
    )
    {
        var tenantId = _tenantProvider.GetTenantId();

        var data = await _context.Attendances
            .Include(x => x.Student)
            .Where(x =>
                x.TenantId == tenantId &&
                x.StudentId == studentId
            )
            .OrderByDescending(x => x.Date)
            .ToListAsync();

        return data.Select(x => new AttendanceResponseDto
        {
            Id = x.Id,
            StudentId = x.StudentId,
            ClassId = x.ClassId,
            StudentName = $"{x.Student.FirstName} {x.Student.LastName}",
            Status = x.Status.ToString(),
            Date = x.Date
        }).ToList();
    }
}