namespace UserService.Models
{
    public class User
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = "User";
        public byte[]? Salt { get; set; }
        public int CartId { get; set; }
    }
}
