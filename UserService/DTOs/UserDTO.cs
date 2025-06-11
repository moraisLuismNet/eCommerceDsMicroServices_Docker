namespace UserService.DTOs
{
    public class UserDTO
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
        public byte[]? Salt { get; set; }
    }
}
