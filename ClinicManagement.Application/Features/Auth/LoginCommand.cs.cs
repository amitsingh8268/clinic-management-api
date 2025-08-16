namespace ClinicManagement.Application.Features
{
    using ClinicManagement.Application.DTOs;
    using MediatR;

    public record LoginCommand(string email, string Password) : IRequest<LoginResultDto>;
}
