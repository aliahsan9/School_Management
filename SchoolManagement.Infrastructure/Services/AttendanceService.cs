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

    public async Task<bool> MarkAttendanceAsync(
        MarkAttendanceDto dto
    )
    {
        var tenantId =
            _tenantProvider.GetTenantId();

        // CHECK DUPLICATE

        var alreadyExists =
            await _context.Attendances
            .AnyAsync(x =>

                x.TenantId == tenantId &&

                x.ClassId == dto.ClassId &&

                x.Date.Date == dto.Date.Date
            );

        if (alreadyExists)
        {
            throw new Exception(
                "Attendance already marked for this class and date."
            );
        }

        var records =
            dto.Students.Select(x =>
                new Attendance
                {
                    TenantId = tenantId,

                    StudentId = x.StudentId,

                    ClassId = dto.ClassId,

                    Date = dto.Date.Date,

                    Status =
                        (AttendanceStatus)x.Status
                }
            ).ToList();

        await _context.Attendances
            .AddRangeAsync(records);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<AttendanceResponseDto>>
        GetClassAttendanceAsync(
            Guid classId,
            DateTime date
        )
    {
        var tenantId =
            _tenantProvider.GetTenantId();

        var data =
            await _context.Attendances

            .Include(x => x.Student)

            .Where(x =>

                x.TenantId == tenantId &&

                x.ClassId == classId &&

                x.Date.Date == date.Date
            )

            .OrderBy(x => x.Student.FirstName)

            .ToListAsync();

        return data.Select(x =>
            new AttendanceResponseDto
            {
                Id = x.Id,

                StudentId = x.StudentId,

                ClassId = x.ClassId,

                StudentName =
                    $"{x.Student.FirstName} {x.Student.LastName}",

                Status = x.Status.ToString(),

                Date = x.Date
            }
        ).ToList();
    }

    public async Task<List<AttendanceResponseDto>>
        GetStudentAttendanceAsync(
            Guid studentId
        )
    {
        var tenantId =
            _tenantProvider.GetTenantId();

        var data =
            await _context.Attendances

            .Include(x => x.Student)

            .Where(x =>

                x.TenantId == tenantId &&

                x.StudentId == studentId
            )

            .OrderByDescending(x => x.Date)

            .ToListAsync();

        return data.Select(x =>
            new AttendanceResponseDto
            {
                Id = x.Id,

                StudentId = x.StudentId,

                ClassId = x.ClassId,

                StudentName =
                    $"{x.Student.FirstName} {x.Student.LastName}",

                Status = x.Status.ToString(),

                Date = x.Date
            }
        ).ToList();
    }
}