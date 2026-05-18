using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    private readonly ITenantProvider _tenantProvider;

    public AppDbContext(
        DbContextOptions<AppDbContext> options,
        ITenantProvider tenantProvider)
        : base(options)
    {
        _tenantProvider = tenantProvider;
    }

    // DbSets
    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Teacher> Teachers => Set<Teacher>();
    public DbSet<Class> Classes => Set<Class>();
    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<ClassSubject> ClassSubjects => Set<ClassSubject>();
    public DbSet<StudentClass> StudentClasses => Set<StudentClass>();
    public DbSet<Attendance> Attendances => Set<Attendance>();
    public DbSet<Fee> Fees => Set<Fee>();
    public DbSet<Payment> Payments => Set<Payment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ---------------------------
        // 1. COMPOSITE KEYS
        // ---------------------------
        modelBuilder.Entity<UserRole>()
            .HasKey(x => new { x.UserId, x.RoleId });

        modelBuilder.Entity<ClassSubject>()
            .HasKey(x => new { x.ClassId, x.SubjectId });

        modelBuilder.Entity<StudentClass>()
            .HasKey(x => new { x.StudentId, x.ClassId });

        // ---------------------------
        // 2. GLOBAL DELETE BEHAVIOR FIX (IMPORTANT)
        // Prevent multiple cascade path error
        // ---------------------------
        foreach (var foreignKey in modelBuilder.Model.GetEntityTypes()
                     .SelectMany(e => e.GetForeignKeys()))
        {
            foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
        }

        // ---------------------------
        // 3. GLOBAL QUERY FILTERS (TENANT SCOPING)
        // ---------------------------
        modelBuilder.Entity<Student>()
            .HasQueryFilter(x => x.TenantId == _tenantProvider.GetTenantId());

        modelBuilder.Entity<Teacher>()
            .HasQueryFilter(x => x.TenantId == _tenantProvider.GetTenantId());

        modelBuilder.Entity<Class>()
            .HasQueryFilter(x => x.TenantId == _tenantProvider.GetTenantId());

        modelBuilder.Entity<Subject>()
            .HasQueryFilter(x => x.TenantId == _tenantProvider.GetTenantId());

        modelBuilder.Entity<Attendance>()
            .HasQueryFilter(x => x.TenantId == _tenantProvider.GetTenantId());

        modelBuilder.Entity<Fee>()
            .HasQueryFilter(x => x.TenantId == _tenantProvider.GetTenantId());

        modelBuilder.Entity<Payment>()
            .HasQueryFilter(x => x.TenantId == _tenantProvider.GetTenantId());

        // IMPORTANT: avoid filter breaking required relationships
        modelBuilder.Entity<ClassSubject>()
            .HasQueryFilter(x => x.Class.TenantId == _tenantProvider.GetTenantId());

        modelBuilder.Entity<StudentClass>()
            .HasQueryFilter(x => x.Class.TenantId == _tenantProvider.GetTenantId());

        // ---------------------------
        // 4. DECIMAL PRECISION FIX (NO WARNINGS)
        // ---------------------------
        modelBuilder.Entity<Fee>()
            .Property(x => x.Amount)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Payment>()
            .Property(x => x.AmountPaid)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Teacher>()
            .Property(x => x.Salary)
            .HasPrecision(18, 2);
    }
}