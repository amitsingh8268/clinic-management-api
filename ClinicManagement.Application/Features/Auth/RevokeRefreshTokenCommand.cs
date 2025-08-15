using MediatR;

namespace ClinicManagement.Application.Features
{
    public record RevokeRefreshTokenCommand(string Token) : IRequest;
}
