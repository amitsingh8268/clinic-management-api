namespace ClinicManagement.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public string? Email { get; set; } = null!;
        public string? HashedPassword { get; set; } = null!;
        public int RoleId { get; set; }
        public int HospitalId { get; set; }
        public List<RefreshToken> RefreshToken { get; set; } = new();
        public void AddRefreshToken(RefreshToken token) => RefreshToken.Add(token);

        public void RevokeRefreshToken(string tokenHash)
        {
            var rt = RefreshToken.FirstOrDefault(t => t.TokenHash == tokenHash);
            if (rt != null)
            {
                rt.Revoked = DateTime.UtcNow;
                rt.IsActive = false;
            }
        }
    }
}
