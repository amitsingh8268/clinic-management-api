using ClinicManagement.Application.DTOs;
using ClinicManagement.Application.Interfaces;
using ClinicManagement.Domain.Entities;
using MediatR;
using System.Linq;

namespace ClinicManagement.Application.Features.Auth
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResponseDto>
    {
        private readonly IUserRepository _user;
        private readonly ITokenService _token;

        public RefreshTokenCommandHandler(IUserRepository users, ITokenService tokens)
        {
            _user = users;
            _token = tokens;
        }

        public async Task<AuthResponseDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var user = _user.GetAllAsync().FirstOrDefault(u =>
                u.RefreshTokens.Any(rt => rt.TokenHash == request.Token && rt.Revoked != null && rt.Expires > DateTime.UtcNow));

            if (user is null)
                throw new UnauthorizedAccessException("Invalid refresh token");

            var newAccessToken = _token.CreateAccessToken(user);
            var newRefreshToken = _token.GenerateRefreshToken();
            var newHashed = _token.HashToken(newRefreshToken);

            user.AddRefreshToken(new RefreshToken
            {
                TokenHash = newHashed,
                Created = DateTime.UtcNow,
                Expires = _token.GetRefreshTokenExpiry()
            });
            await _user.SaveChangesAsync(cancellationToken);
            return new AuthResponseDto(newAccessToken, newHashed);

        }
    }
}
