using SchoolManagement.Application.DTOs.Auth;

namespace SchoolManagement.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterSchoolAsync(RegisterSchoolRequestDto request);

    Task<AuthResponseDto> LoginAsync(LoginRequestDto request);
}