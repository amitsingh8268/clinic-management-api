using ClinicManagement.Application.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace clinic_management_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("user")]
        public async Task<IActionResult> AddUser(AddUserCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(AddUser), new { id }, id);
        }
    }
}
