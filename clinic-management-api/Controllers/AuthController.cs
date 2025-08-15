using ClinicManagement.Application.DTOs;
using ClinicManagement.Application.Features;
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
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _mediator.Send(new LoginCommand(dto.Username, dto.Password));
            // set HttpOnly secure cookie for refresh token
            Response.Cookies.Append("refreshToken", result.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(30)
            });
            return Ok(new { accessToken = result.AccessToken, user = result.User });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            var cookie = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(cookie)) return Unauthorized();
            var res = await _mediator.Send(new RefreshTokenCommand(cookie));
            // rotate cookie
            Response.Cookies.Append("refreshToken", res.RefreshToken, new CookieOptions { HttpOnly = true, Secure = true, SameSite = SameSiteMode.None, Expires = DateTime.UtcNow.AddDays(30) });
            return Ok(new { accessToken = res.AccessToken });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var cookie = Request.Cookies["refreshToken"];
            if (!string.IsNullOrEmpty(cookie)) await _mediator.Send(new RevokeRefreshTokenCommand(cookie));
            Response.Cookies.Delete("refreshToken");
            return NoContent();
        }
    }
}
