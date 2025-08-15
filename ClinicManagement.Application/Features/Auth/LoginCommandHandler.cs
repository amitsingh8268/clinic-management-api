using ClinicManagement.Application.DTOs;
using ClinicManagement.Application.Interfaces;
using ClinicManagement.Domain.Entities;
using MediatR;

namespace ClinicManagement.Application.Features
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResultDto>
    {
        private readonly IUserRepository _user;
        private readonly ITokenService _token;
        private readonly IPasswordHasher _hasher;
        public LoginCommandHandler(IUserRepository users, ITokenService tokens, IPasswordHasher hasher)
        {
            _user = users; _token = tokens; _hasher = hasher;
        }


        public async Task<LoginResultDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _user.GetByUsernameAsync(request.Username, cancellationToken) 
                ??  throw new UnauthorizedAccessException("Invalid credentials");

            if (!_hasher.Verify(user.PasswordHash, request.Password))
              throw new UnauthorizedAccessException("Invalid credentials");

            var accessToken = _token.CreateAccessToken(user);
            var refreshToken = _token.GenerateRefreshToken();
            var hashed = _token.HashToken(refreshToken);

            user.AddRefreshToken(new  RefreshToken
            {
                TokenHash = hashed,
                Created = DateTime.UtcNow,
                Expires = _token.GetRefreshTokenExpiry()
            });
            await _user.SaveChangesAsync(cancellationToken);
            return new LoginResultDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                User = new DTOs.UserDto { Username = user.Username, Email = user.Email },
            };
        }
    }
}
