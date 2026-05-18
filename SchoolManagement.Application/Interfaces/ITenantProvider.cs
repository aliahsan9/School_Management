namespace SchoolManagement.Application.Interfaces;

public interface ITenantProvider
{
    Guid GetTenantId();
}