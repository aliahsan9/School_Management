using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Interfaces;

namespace SchoolManagement.Infrastructure.Services;

public class TenantProvider : ITenantProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TenantProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetTenantId()
    {
        var tenantClaim = _httpContextAccessor
            .HttpContext?
            .User?
            .FindFirst("TenantId");

        if (tenantClaim == null)
        {
            return Guid.Empty;
        }

        return Guid.Parse(tenantClaim.Value);
    }
}