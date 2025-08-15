using ClinicManagement.Application.Interfaces;
using BC = BCrypt.Net.BCrypt;

namespace ClinicManagement.Infrastructure.Security
{
    public class BcryptPasswordHasher : IPasswordHasher
    {
        public string Hash(string password)
        {
            return BC.HashPassword(password);
        }

        public bool Verify(string passwordHash, string password)
        {
            return BC.Verify(password, passwordHash);
        }
    }
}
