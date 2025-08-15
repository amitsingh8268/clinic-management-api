namespace ClinicManagement.Application.Features
{
    using ClinicManagement.Application.DTOs;
    using MediatR;

    public record LoginCommand(string Username, string Password) : IRequest<LoginResultDto>;
}
