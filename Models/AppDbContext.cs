using Microsoft.EntityFrameworkCore;

namespace Contract_Monthly_Claim_System_POE.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Claim> Claims { get; set; }
    }
}
