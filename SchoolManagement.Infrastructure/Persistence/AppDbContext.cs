
using Microsoft.EntityFrameworkCore;

namespace SchoolManagement.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options) 
            : base (options) { }


    }
}
