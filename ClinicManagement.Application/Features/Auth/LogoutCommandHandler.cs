using ClinicManagement.Application.Interfaces;
using MediatR;

namespace ClinicManagement.Application.Features.Auth
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Unit>
    {
        private readonly IUserRepository _user;
        public LogoutCommandHandler(IUserRepository user)
        {
            _user = user;
        }

        public async Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var user = _user.GetAllAsync().FirstOrDefault(u =>
               u.RefreshToken.Any(rt => rt.TokenHash == request.tokenHash && rt.Revoked == null));

            if (user == null)
                throw new UnauthorizedAccessException("Invalid refresh token");

            user.RevokeRefreshToken(request.tokenHash);
            await _user.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

    }
}
