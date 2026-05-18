using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.DTOs.Auth;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure.Persistence;

namespace SchoolManagement.Application.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;

    private readonly IJwtService _jwtService;

    public AuthService(
        AppDbContext context,
        IJwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    public async Task<AuthResponseDto> RegisterSchoolAsync(
        RegisterSchoolRequestDto request)
    {
        var emailExists = await _context.Users
            .AnyAsync(x => x.Email == request.AdminEmail);

        if (emailExists)
        {
            throw new Exception("Email already exists.");
        }

        var tenant = new Tenant
        {
            SchoolName = request.SchoolName,
            Email = request.SchoolEmail,
            PhoneNumber = request.PhoneNumber,
            Address = request.Address
        };

        await _context.Tenants.AddAsync(tenant);

        var user = new User
        {
            TenantId = tenant.Id,
            FullName = request.AdminName,
            Email = request.AdminEmail,
            PhoneNumber = request.PhoneNumber,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };

        await _context.Users.AddAsync(user);

        var role = await _context.Roles
            .FirstOrDefaultAsync(x => x.Name == "SchoolAdmin");

        if (role == null)
        {
            role = new Role
            {
                Name = "SchoolAdmin"
            };

            await _context.Roles.AddAsync(role);
        }

        var userRole = new UserRole
        {
            UserId = user.Id,
            RoleId = role.Id
        };

        await _context.UserRoles.AddAsync(userRole);

        await _context.SaveChangesAsync();

        var token = _jwtService.GenerateToken(user, role.Name);

        return new AuthResponseDto
        {
            Token = token,
            Email = user.Email,
            FullName = user.FullName,
            TenantId = tenant.Id
        };
    }

    public async Task<AuthResponseDto> LoginAsync(
        LoginRequestDto request)
    {
        var user = await _context.Users
            .Include(x => x.UserRoles)
            .ThenInclude(x => x.Role)
            .FirstOrDefaultAsync(x =>
                x.Email == request.Email);

        if (user == null)
        {
            throw new Exception("Invalid credentials.");
        }

        var passwordValid = BCrypt.Net.BCrypt.Verify(
            request.Password,
            user.PasswordHash);

        if (!passwordValid)
        {
            throw new Exception("Invalid credentials.");
        }

        var role = user.UserRoles
            .Select(x => x.Role.Name)
            .FirstOrDefault() ?? "SchoolAdmin";

        var token = _jwtService.GenerateToken(user, role);

        return new AuthResponseDto
        {
            Token = token,
            Email = user.Email,
            FullName = user.FullName,
            TenantId = user.TenantId
        };
    }
}