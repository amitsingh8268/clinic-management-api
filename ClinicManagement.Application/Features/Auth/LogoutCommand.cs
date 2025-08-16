using MediatR;

namespace ClinicManagement.Application.Features
{
    public sealed record LogoutCommand(string tokenHash) : IRequest<Unit>;
}
