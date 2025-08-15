using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Application.Interfaces
{
    public interface ITokenService
    {
        string CreateAccessToken(User user);
        string GenerateRefreshToken();           // raw token
        string HashToken(string token);
        DateTime GetRefreshTokenExpiry();
    }
}
