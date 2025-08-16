namespace ClinicManagement.Domain.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string TokenHash { get; set; } = null!;
        public DateTime Expires { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Revoked { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public bool IsActive { get; set; }
    }
}
