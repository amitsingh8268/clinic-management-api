namespace ClinicManagement.Application.DTOs
{
    public class LoginResultDto
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!; // raw token (API controller will set cookie)
        public UserDto User { get; set; } = null!;
    }
}
