using MediatR;

namespace ClinicManagement.Application.Features
{
    public record AddUserCommand(string FirstName, string LastName, string Email, string Password, int RoleId) : IRequest<int>;
    
}
