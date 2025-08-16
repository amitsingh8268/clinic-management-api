using ClinicManagement.Application.Interfaces;
using ClinicManagement.Domain.Entities;
using MediatR;

namespace ClinicManagement.Application.Features
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, int>
    {
        private readonly IUserRepository _user;
        private readonly IPasswordHasher _hasher;
        public AddUserCommandHandler(IUserRepository user, IPasswordHasher hasher)
        {
            _hasher = hasher;
            _user = user;
        }

        public async Task<int> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
     
            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                RoleId = request.RoleId, // admin
                HospitalId = 1,
                HashedPassword = _hasher.Hash(request.Password)
            };

            await _user.AddAsync(user, cancellationToken);
            await _user.SaveChangesAsync(cancellationToken);
            return user.UserId;
        }
    }
}
