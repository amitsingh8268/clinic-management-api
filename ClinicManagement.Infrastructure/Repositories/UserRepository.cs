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
            await _context.Users.AddAsync(user, ct);
        }

        public Task<User?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return _context.Users.Include(u => u.RefreshTokens).FirstOrDefaultAsync(u => u.Id == id, ct);
        }

        public Task<User?> GetByUsernameAsync(string username, CancellationToken ct = default)
        {
           return _context.Users.Include(u => u.RefreshTokens).FirstOrDefaultAsync(u=> u.Username == username, ct);
        }

        public Task SaveChangesAsync(CancellationToken ct = default)
        {
           return _context.SaveChangesAsync(ct);
        }

        public IEnumerable<User> GetAllAsync()
        {
            return _context.Users.Include(u=> u.RefreshTokens);
        }

    }
}
