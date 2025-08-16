using ClinicManagement.Application.Interfaces;
using ClinicManagement.Domain.Entities;
using ClinicManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(User user, CancellationToken ct = default)
        {
            await _context.User.AddAsync(user, ct);
        }

        public Task<User?> GetByIdAsync(int userId, CancellationToken ct = default)
        {
            return _context.User.Include(u => u.RefreshToken).FirstOrDefaultAsync(u => u.UserId == userId, ct);
        }

        public Task<User?> GetByUsernameAsync(string email, CancellationToken ct = default)
        {
           return _context.User.Include(u => u.RefreshToken).FirstOrDefaultAsync(u=> u.Email == email, ct);
        }

        public Task SaveChangesAsync(CancellationToken ct = default)
        {
           return _context.SaveChangesAsync(ct);
        }

        public IEnumerable<User> GetAllAsync()
        {
            return _context.User.Include(u=> u.RefreshToken);
        }

    }
}
