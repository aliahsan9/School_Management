using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Interfaces;

public interface IJwtService
{
    string GenerateToken(User user, string role);
}