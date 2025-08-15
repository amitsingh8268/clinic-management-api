using ClinicManagement.Application.DTOs;
using MediatR;

namespace ClinicManagement.Application.Features
{
    public record RefreshTokenCommand(string Token) : IRequest<AuthResponseDto>;
}
