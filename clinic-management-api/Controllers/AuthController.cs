using ClinicManagement.Application.DTOs;
using ClinicManagement.Application.Features;
using ClinicManagement.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace clinic_management_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand dto)
        {
            var result = await _mediator.Send(new LoginCommand(dto.email, dto.Password));
            return Ok(new { accessToken = result.AccessToken, refreshToken = result.RefreshToken, user = result.User });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken)) return Unauthorized();
            var res = await _mediator.Send(new RefreshTokenCommand(refreshToken));
            return Ok(new { accessToken = res.AccessToken, refreshToken = res.RefreshToken });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(LogoutCommand dto)
        {
            await _mediator.Send(new LogoutCommand(dto.tokenHash));
            return NoContent();
        }
    }
}
