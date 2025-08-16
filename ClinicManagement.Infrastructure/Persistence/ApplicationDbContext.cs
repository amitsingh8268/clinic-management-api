using ClinicManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option) {}

        public DbSet<User> User { get; set; } = null!;

        public DbSet<RefreshToken> RefreshToken { get; set; } = null!;
    }
}
