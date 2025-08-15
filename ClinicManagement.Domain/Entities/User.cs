namespace ClinicManagement.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public string? PasswordHash { get; set; } = null!;
        public string? Role { get; set; } = "User";
        public List<RefreshToken> RefreshTokens { get; set; } = new();
        public void AddRefreshToken(RefreshToken token) => RefreshTokens.Add(token);

        public void RevokeRefreshToken(string token)
        {
            var rt = RefreshTokens.FirstOrDefault(t => t.TokenHash == token);
            if (rt != null)
                rt.Revoked = DateTime.UtcNow;
        }
    }
}
